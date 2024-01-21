import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {HomeRoutingModule} from './home-routing.module';
import {MatInputModule} from "@angular/material/input";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatCardModule} from '@angular/material/card';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatButtonModule} from "@angular/material/button";
import { HomeComponent } from './components/home/home.component';
import {MatGridListModule} from "@angular/material/grid-list";
import {MatCheckboxModule} from "@angular/material/checkbox";
import {MatTableModule} from "@angular/material/table";
import {ApiComponent} from "./components/api/api.component";
import {MatAccordion, MatExpansionModule, MatExpansionPanel} from "@angular/material/expansion";;
import { MigrationsComponent } from './components/migrations/migrations.component'
import {MatSelectModule} from "@angular/material/select";;
import { PrivacyPolicyComponent } from './components/privacy-policy/privacy-policy.component'
;
import { TermsOfServiceComponent } from './components/terms-of-service/terms-of-service.component'

@NgModule({
  declarations: [HomeComponent, ApiComponent, MigrationsComponent, PrivacyPolicyComponent, TermsOfServiceComponent],
  imports: [
    CommonModule,
    HomeRoutingModule,
    MatInputModule,
    MatFormFieldModule,
    MatCardModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatGridListModule,
    MatCheckboxModule,
    MatTableModule,
    MatExpansionModule,
    MatSelectModule
  ],
})
export class HomeModule {}
