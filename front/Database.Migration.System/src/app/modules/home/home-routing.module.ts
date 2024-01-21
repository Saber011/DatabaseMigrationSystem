import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {HomeComponent} from "./components/home/home.component";
import {ApiComponent} from "./components/api/api.component";
import {MigrationsComponent} from "./components/migrations/migrations.component";
import {PrivacyPolicyComponent} from "./components/privacy-policy/privacy-policy.component";
import {TermsOfServiceComponent} from "./components/terms-of-service/terms-of-service.component";

const routes: Routes = [
  {
    path: 'api',
    component: ApiComponent,
  },
  {
    path: 'migrations',
    component: MigrationsComponent,
  },
  {
    path: 'privacy-policy',
    component: PrivacyPolicyComponent,
  },
  {
    path: 'terms-of-service',
    component: TermsOfServiceComponent,
  },
  {
    path: '',
    component: HomeComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class HomeRoutingModule {}
