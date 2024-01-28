import {Component, OnInit, ViewChild} from '@angular/core';
import {MatTableDataSource} from "@angular/material/table";
import {MigrateTableRequest} from "../../../../api/models/migrate-table-request";
import {MatCheckboxChange} from "@angular/material/checkbox";
import {TableElement} from "../home/home.component";
import {MatSort} from "@angular/material/sort";
import {TableInfoDto} from "../../../../api/models/table-info-dto";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {MigrationService} from "../../../../api/services/migration.service";
import {FieldDto} from "../../../../api/models/field-dto";

@Component({
  selector: 'app-migrations',
  templateUrl: './migrations.component.html',
  styleUrls: ['./migrations.component.css']
})
export class MigrationsComponent implements OnInit {
  @ViewChild(MatSort) sort!: MatSort;
  form: FormGroup;
  isDataLoaded = false;
  isDataAvailable = false;
  migrationStarted: boolean = false;
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
      schema: 'schema',
      selectedSourceTable: null,
      fields: [
        { name: 'Field1', type: 'int' },
        { name: 'Field2', type: 'varchar' },
        { name: 'Field3', type: 'datetime' },
      ],
    },
    {
      name: 'Table2',
      schema: 'schema',
      count: 800,
      selectedSourceTable: null,
      fields: [
        { name: 'Field1', type: 'float' },
        { name: 'Field2', type: 'text' },
      ],
    },
  ];

  destinationTables: Table[] = [
    {
      name: 'DestTable1',
      count: 500,
      schema: 'schema',
      fields: [
        { name: 'DestField1', type: 'int', },
        { name: 'DestField2', type: 'varchar', },
      ],
      selectedSourceTable: null,
    },
    {
      name: 'DestTable2',
      count: 300,
      schema: 'schema',
      fields: [
        { name: 'DestField1', type: 'boolean', },
        { name: 'DestField2', type: 'date',  },
      ],
      selectedSourceTable: null,
    },
  ];

  ngOnInit(): void {
    this.refreshData();
  }


  private refreshData() {
    this.migrationService.apiMigrationGetTablesGet()
      .subscribe(value => {
        this.isDataLoaded = true;
        if(value && value.sourceTables && value.destinationTables && value.sourceTables.length > 0 && value.destinationTables.length > 0) {
          this.isDataAvailable = true;

          this.sourceTables = value.sourceTables.map(table => ({
            name: table.tableName ?? '', // Используйте пустую строку, если tableName null или undefined
            count: table.dataCount ?? 0, // Используйте 0, если dataCount null или undefined
            selectedSourceTable: null,
            schema: table.schema ?? '',
            fields: table.fields
          }));

          this.destinationTables = value.destinationTables.map(table => ({
            name: table.tableName ?? '', // Используйте пустую строку, если tableName null или undefined
            count: table.dataCount ?? 0, // Используйте 0, если dataCount null или undefined
            selectedSourceTable: null,
            schema: table.schema ?? '',
            fields: table.fields
          }));


        } else {
          this.isDataAvailable = false;
        }
      });
  }



  startMigrate() {
    const selectedDestinationTables = this.destinationTables.filter(x => x.selectedSourceTable);
    console.log(this.destinationTables);
    console.log(selectedDestinationTables);
    const migrateTableRequests: MigrateTableRequest[] = selectedDestinationTables.map(table =>
    {
        return {
          sourceSchema: table.selectedSourceTable.schema ,
          sourceTable: table.selectedSourceTable.name,
          destinationSchema: table.schema,
          destinationTable: table.name,
        };
    });

    console.log(migrateTableRequests);
    if(migrateTableRequests.length > 0){
      this.migrationService.apiMigrationMigrateTablePost({ body: {tables: migrateTableRequests}})
        .subscribe(x => {
          this.migrationStarted = true;
        });
    }
  }


  cancelMigrate() {
    this.migrationService.apiMigrationCancelMigrationPost()
      .subscribe(x => {
        this.migrationStarted = false;
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
  checked: any;
}
interface Table {
  name: string;
  schema: string;
  count: number;
  fields:  FieldDto[] | null | undefined;
  selectedSourceTable: any | undefined | null;
  selectedInDestination?: boolean;
}
