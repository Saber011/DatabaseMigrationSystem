import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {AppRoutingModule} from "./app-routing.module";
import {HttpClientModule} from "@angular/common/http";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {ApiModule} from "./api/api.module";
import {AppShellComponent} from "./app-shell/app-shell.component";
import {CoreModule} from "./core/core.module";
import {RouterModule} from "@angular/router";
import { AppPreloaderContainerComponent } from './component/app-preloader-container/app-preloader-container.component';
import {SelectivePreloadingStrategy} from "./core/services/selective-preload-strategy";
import { NotFoundComponent } from './component/not-found/not-found.component';

@NgModule({
  declarations: [
    AppComponent,
    AppShellComponent,
    AppPreloaderContainerComponent,
    NotFoundComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    ApiModule.forRoot({rootUrl: ''}),
    FormsModule,
    ReactiveFormsModule,
    CoreModule,
    RouterModule
  ],
  providers: [SelectivePreloadingStrategy],
  bootstrap: [AppComponent]
})
export class AppModule { }
