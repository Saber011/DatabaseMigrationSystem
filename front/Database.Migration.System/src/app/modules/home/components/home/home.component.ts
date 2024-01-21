import {Component, OnInit, ViewChild} from '@angular/core';
import {MigrationService} from "../../../../api/services/migration.service";
import {MatTableDataSource} from "@angular/material/table";
import {MatSort} from "@angular/material/sort";
import {TableInfoDto} from "../../../../api/models/table-info-dto";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {MatCheckboxChange} from "@angular/material/checkbox";
import {map} from "rxjs/operators";
import {MigrateTableRequest} from "../../../../api/models/migrate-table-request";
import {Router} from "@angular/router";
import {DatabaseType} from "../../../../api/models/database-type";

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
  migrationMessage: string | null | undefined = '';
  migrationMessage2: string | null | undefined = '';

  sourceType: DatabaseType | null | undefined;
  destinationType: DatabaseType | null | undefined;

  constructor(private router: Router, private migrationService :MigrationService,) {
  }
  ngOnInit(){

    this.migrationService.apiMigrationGetCurrentMigrationSettingsGet$Response()
         .subscribe(value => {
           this.migrationMessage = value.body.destinationDatabaseDataInfo;
           this.migrationMessage2 = value.body.sourceDatabaseDataInfo;
           this.sourceType = value.body.sourceDatabaseType;
           this.destinationType = value.body.destinationDatabaseType;
         })
    ;
  }


  onClick() {
    this.router.navigate(['/home/migrations']);
  }
}
