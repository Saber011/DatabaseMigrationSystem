import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatInputModule} from "@angular/material/input";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatCardModule} from '@angular/material/card';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatButtonModule} from "@angular/material/button";
import { MatExpansionModule} from "@angular/material/expansion";
import {FateMaterialModule, FateModule} from "fate-editor";
import {MatPaginatorModule} from "@angular/material/paginator";
import {MatSelectModule} from "@angular/material/select";
import {MatListModule} from "@angular/material/list";
import {MigrationLogComponent} from "./components/migration-log/migration-log.component";
import {MigrationLogRoutingModule} from "./migration-log-routing.module";
import {MatTableModule} from "@angular/material/table";


@NgModule({
  declarations: [MigrationLogComponent],
  imports: [
    CommonModule,
    MigrationLogRoutingModule,
    MatInputModule,
    MatFormFieldModule,
    MatCardModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatExpansionModule,
    FateMaterialModule,
    FateModule,
    MatPaginatorModule,
    MatSelectModule,
    MatListModule,
    MatTableModule,
  ],
})
export class MigrationLogModule {}
