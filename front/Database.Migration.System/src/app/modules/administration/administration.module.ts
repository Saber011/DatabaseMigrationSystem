import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {AdministrationRoutingModule} from './administration-routing.module';
import {MatInputModule} from "@angular/material/input";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatCardModule} from '@angular/material/card';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatButtonModule} from "@angular/material/button";
import {MatGridListModule} from "@angular/material/grid-list";
import {MatTableModule} from "@angular/material/table";
import {MatSortModule} from "@angular/material/sort";
import {MatDialogModule} from "@angular/material/dialog";
import {FateMaterialModule, FateModule} from "fate-editor";
import { AdministrationComponent } from './administration/administration.component';
import { UserDialog } from './user-dialog/user-dialog.component';

@NgModule({
  declarations: [AdministrationComponent, UserDialog],
  imports: [
    CommonModule,
    AdministrationRoutingModule,
    MatInputModule,
    MatFormFieldModule,
    MatCardModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatGridListModule,
    MatTableModule,
    MatFormFieldModule,
    MatSortModule,
    MatDialogModule,
    FateMaterialModule,
    FateModule,
  ],
})
export class AdministrationModule {}
