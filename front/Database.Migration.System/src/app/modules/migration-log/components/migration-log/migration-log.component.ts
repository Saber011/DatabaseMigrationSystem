import {Component, ViewChild} from '@angular/core';
import {MatSort} from "@angular/material/sort";
import {MatTableDataSource} from "@angular/material/table";
import {TableInfoDto} from "../../../../api/models/table-info-dto";
import {MigrationService} from "../../../../api/services/migration.service";
import {UserMigrationData} from "../../../../api/models/user-migration-data";
import {MatPaginator} from "@angular/material/paginator";

export interface Migration {
  id: number;
  startDate: string;
  endDate: string;
  sourceDb: string;
  remoteDb: string;
  tablesList: string;
  totalExecutionTime: string;
  migrationStatus: string;
}

const MIGRATION_DATA: Migration[] = [
  {id: 1, startDate: '2023-06-01 12:00:00', endDate: '2023-06-01 12:30:00', sourceDb: 'Исходная БД', remoteDb: 'Удаленная БД', tablesList: 'Table1, Table2', totalExecutionTime: '30 минут', migrationStatus: 'Успешно'},
  {id: 2, startDate: '2023-06-02 10:00:00', endDate: '2023-06-02 11:30:00', sourceDb: 'Исходная БД', remoteDb: 'Удаленная БД', tablesList: 'Table3, Table4', totalExecutionTime: '90 минут', migrationStatus: 'Ошибка'}
];

@Component({
  selector: 'app-migration-log',
  templateUrl: './migration-log.component.html',
  styleUrls: ['./migration-log.component.css']
})
export class MigrationLogComponent {
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  dataSource!: MatTableDataSource<UserMigrationData>;

  constructor(private migrationService :MigrationService,) {
  }
  displayedColumns: string[] = ['id', 'startDate', 'endDate', 'sourceDatabase', 'destinationDatabase', 'tableList', 'executionTime', 'migrationStatus'];
  isMigrationRunning: any = true;
  currentProgress: number = 76;
  ngOnInit(){
    this.refreshData();
  }

  private refreshData() {
    this.migrationService.apiMigrationGetMigrationJournalDataGet()
      .subscribe(value => {
          this.dataSource = new MatTableDataSource(value);
          this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
      });
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

}
