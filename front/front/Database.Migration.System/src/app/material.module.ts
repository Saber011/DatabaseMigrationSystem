import { NgModule } from '@angular/core';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatNativeDateModule} from '@angular/material/core';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatSliderModule} from '@angular/material/slider';
import {MatTableModule} from '@angular/material/table';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatCardModule} from "@angular/material/card";
import {MatMenuModule} from "@angular/material/menu";

@NgModule({
  imports: [
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatInputModule,
    MatFormFieldModule,
    MatSliderModule,
    MatTableModule,
    MatIconModule,
    MatCardModule,
    MatMenuModule
  ],
  exports: [
    MatButtonModule,
  ]
})
export class MaterialModule {}
