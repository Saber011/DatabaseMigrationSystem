import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {MigrationLogComponent} from "./components/migration-log/migration-log.component";

const routes: Routes = [
  {
    path: '',
    component: MigrationLogComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MigrationLogRoutingModule {}
