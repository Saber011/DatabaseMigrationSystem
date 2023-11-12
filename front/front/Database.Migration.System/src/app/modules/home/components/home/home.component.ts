import {Component, OnInit, ViewChild} from '@angular/core';
import {MigrationService} from "../../../../api/services/migration.service";
import {MatTableDataSource} from "@angular/material/table";
import {MatSort} from "@angular/material/sort";
import {TableInfoDto} from "../../../../api/models/table-info-dto";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {MatCheckboxChange} from "@angular/material/checkbox";
import {map} from "rxjs/operators";
import {MigrateTableRequest} from "../../../../api/models/migrate-table-request";

export interface TableElement {
  schemaName: string;
  tableName: string;
  dataCount: number;
  selected?: null | boolean;
  bindingNumber: null | number;
}


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  @ViewChild(MatSort) sort!: MatSort;
  displayedColumns: string[] = ['schemaName', 'tableName', 'dataCount', 'select', 'bindingNumber'];
  dataSource!: MatTableDataSource<TableInfoDto>;
  dataSource2!: MatTableDataSource<TableInfoDto>;
  bindingCounter: number = 0;
  form: FormGroup;
  isDataLoaded = false;
  isDataAvailable = false;
  migrationStarted: boolean = false;
  migrationMessage: string = '';
  constructor(private migrationService :MigrationService,
              private fb: FormBuilder,
)  {
    this.form = this.fb.group({
      data: ['', Validators.required]
    });
  }

  ngOnInit(){
    this.refreshData();
    this.checkStatus();
  }

  private refreshData() {
    this.migrationService.apiMigrationGetTablesGet()
      .subscribe(value => {
        this.isDataLoaded = true;
        if(value && value.sourceTables && value.destinationTables && value.sourceTables.length > 0 && value.destinationTables.length > 0) {
          this.isDataAvailable = true;
          value.sourceTables.forEach(table => {
            table.selected = false;
            table.bindingNumber = null;
          });
          value.destinationTables.forEach(table => {
            table.selected = false;
            table.bindingNumber = null;
          });
          this.dataSource = new MatTableDataSource(value.sourceTables);
          this.dataSource2 = new MatTableDataSource(value.destinationTables);
          this.dataSource.sort = this.sort;
          this.dataSource2.sort = this.sort;
        } else {
          this.isDataAvailable = false;
        }
      });
  }


  startMigrate() {
    const selectedSourceTables = this.dataSource.data.filter(x => x.selected);
    const selectedDestinationTables = this.dataSource2.data.filter(x => x.selected);

    const migrateTableRequests: MigrateTableRequest[] = selectedSourceTables.map(sourceTable => {
      const correspondingDestinationTable = selectedDestinationTables.find(destTable => destTable.bindingNumber === sourceTable.bindingNumber);
      if (correspondingDestinationTable) {
        return {
          sourceSchema: sourceTable.schema,
          sourceTable: sourceTable.tableName,
          destinationSchema: correspondingDestinationTable.schema,
          destinationTable: correspondingDestinationTable.tableName,
        };
      }
      return null;
    }).filter(x => x !== null) as MigrateTableRequest[];

    if(migrateTableRequests.length > 0){
      this.migrationService.apiMigrationMigrateTablePost({ body: {tables: migrateTableRequests}})
        .subscribe(x => {
          this.migrationStarted = true;
          this.migrationMessage = 'Миграция запущена.';
        });
    } else {
      this.migrationService.apiMigrationMigrateTablesPost()
        .subscribe(x => {
          this.migrationStarted = true;
          this.migrationMessage = 'Миграция запущена.';
        })
    }
  }


  cancelMigrate() {
    this.migrationService.apiMigrationCancelMigrationPost()
      .subscribe(x => {
        this.migrationStarted = false;
        this.migrationMessage = 'Миграция остановленна.';
        this.refreshData();
      })
  }

  checkStatus() {
    this.migrationService.apiMigrationGetStatusGet()
      .subscribe(x => {
        const matchingTable = this.dataSource2.data[this.dataSource2.data.length - 1]
        console.log(matchingTable)
        this.migrationStarted = (x.status != 2 && x.status != 3) ||
          (x.currentTable?.toLowerCase() !== matchingTable.tableName?.toLowerCase() );
        this.migrationMessage = 'Текущая таблица в миграции ' + x.currentTable;
        this.refreshData();

      })
  }


  onSourceTableSelected(event: MatCheckboxChange, element: TableElement) {
    const selected = event.checked;
    const matchingTable = this.dataSource2.data.find(
      item => item.tableName?.toLowerCase() == element.tableName.toLowerCase()
    );

    if (matchingTable) {
      matchingTable.selected = selected;
      element.selected = selected;
      if (selected) {
        this.bindingCounter++;
        element.bindingNumber = this.bindingCounter;
        matchingTable.bindingNumber = this.bindingCounter;

      } else {
        if(this.bindingCounter > 1){
          this.bindingCounter--;
          element.bindingNumber = null;
          matchingTable.bindingNumber = null;
        } else {
          element.bindingNumber = null;
          matchingTable.bindingNumber = null;
          this.bindingCounter = 0;
        }
      }
    }
  }

}
