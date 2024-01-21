﻿import {Component, OnInit, ViewChild} from '@angular/core';
import {MigrationService} from "../../../../api/services/migration.service";
import {MatTableDataSource} from "@angular/material/table";
import {MatSort} from "@angular/material/sort";
import {TableInfoDto} from "../../../../api/models/table-info-dto";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {MatCheckboxChange} from "@angular/material/checkbox";
import {map} from "rxjs/operators";
import {MigrateTableRequest} from "../../../../api/models/migrate-table-request";
import {Router} from "@angular/router";

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


  constructor(private router: Router,) {
  }
  ngOnInit(){

  }


  onClick() {
    this.router.navigate(['/home/migrations']);
  }
}
