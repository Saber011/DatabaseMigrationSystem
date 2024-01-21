import {Component, OnInit, ViewChild} from '@angular/core';
import {MatTableDataSource} from "@angular/material/table";
import {MigrateTableRequest} from "../../../../api/models/migrate-table-request";
import {MatCheckboxChange} from "@angular/material/checkbox";
import {TableElement} from "../home/home.component";
import {MatSort} from "@angular/material/sort";
import {TableInfoDto} from "../../../../api/models/table-info-dto";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {MigrationService} from "../../../../api/services/migration.service";

@Component({
  selector: 'app-migrations',
  templateUrl: './migrations.component.html',
  styleUrls: ['./migrations.component.css']
})
export class MigrationsComponent implements OnInit {
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
              private fb: FormBuilder,) {
    this.form = this.fb.group({
      data: ['', Validators.required]
    });
  }

  sourceTables :Table[] = [
    {
      name: 'Table1',
      count: 1000,
      selectedSourceTable: null, // Добавлено новое свойство
      fields: [
        { name: 'Field1', type: 'int' },
        { name: 'Field2', type: 'varchar' },
        { name: 'Field3', type: 'datetime' },
      ],
    },
    {
      name: 'Table2',
      count: 800,
      selectedSourceTable: null, // Добавлено новое свойство
      fields: [
        { name: 'Field1', type: 'float' },
        { name: 'Field2', type: 'text' },
      ],
    },
    // Добавьте еще таблицы по аналогии
  ];

  destinationTables: Table[] = [
    {
      name: 'DestTable1',
      count: 500,
      fields: [
        { name: 'DestField1', type: 'int', defaultValue: '0' },
        { name: 'DestField2', type: 'varchar', defaultValue: 'N/A' },
      ],
      selectedSourceTable: null, // Добавлено новое свойство
    },
    {
      name: 'DestTable2',
      count: 300,
      fields: [
        { name: 'DestField1', type: 'boolean', defaultValue: 'false' },
        { name: 'DestField2', type: 'date', defaultValue: '2024-01-01' },
      ],
      selectedSourceTable: null, // Добавлено новое свойство
    },
    // Добавьте еще таблицы по аналогии
  ];

  ngOnInit(): void {
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

  updateSelectedSourceTable(selectedTableName: string, destinationTable: any) {
    // Обновляем выбранную таблицу в удаленном источнике данных
    destinationTable.selectedSourceTable = this.sourceTables.find(t => t.name === selectedTableName);

    // Обновляем статус выделения для всех таблиц
    this.sourceTables.forEach(table => {
      if (table.name === selectedTableName) {
        table.selectedInDestination = !table.selectedInDestination;
      } else if (!this.destinationTables.some(dt => dt.selectedSourceTable?.name === table.name)) {
        table.selectedInDestination = false;
      }
    });
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
interface Table {
  name: string;
  count: number;
  fields: Array<{ name: string; type: string; defaultValue?: string }>;
  selectedSourceTable: any | undefined | null;
  selectedInDestination?: boolean;
}
