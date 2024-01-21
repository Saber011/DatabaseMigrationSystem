import {Component, OnInit, ViewChild} from '@angular/core';
import {MigrationService} from "../../../../api/services/migration.service";
import {MatTableDataSource} from "@angular/material/table";
import {MatSort} from "@angular/material/sort";
import {TableInfoDto} from "../../../../api/models/table-info-dto";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {MatCheckboxChange} from "@angular/material/checkbox";
import {map} from "rxjs/operators";
import {MigrateTableRequest} from "../../../../api/models/migrate-table-request";



@Component({
  selector: 'app-home',
  templateUrl: './api.component.html',
  styleUrls: ['./api.component.css']
})
export class ApiComponent implements OnInit {

  constructor()  {
  }

  ngOnInit(){
  }
}
