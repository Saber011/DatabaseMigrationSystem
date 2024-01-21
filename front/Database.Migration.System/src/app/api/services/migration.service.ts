/* tslint:disable */
/* eslint-disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse, HttpContext } from '@angular/common/http';
import { BaseService } from '../base-service';
import { ApiConfiguration } from '../api-configuration';
import { StrictHttpResponse } from '../strict-http-response';
import { RequestBuilder } from '../request-builder';
import { Observable } from 'rxjs';
import { map, filter } from 'rxjs/operators';

import { CurrentMigrationSettingsDto } from '../models/current-migration-settings-dto';
import { MigrateTableCommand } from '../models/migrate-table-command';
import { MigrationStatusDto } from '../models/migration-status-dto';
import { TableInfosDto } from '../models/table-infos-dto';
import { UserMigrationData } from '../models/user-migration-data';

@Injectable({
  providedIn: 'root',
})
export class MigrationService extends BaseService {
  constructor(
    config: ApiConfiguration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * Path part for operation apiMigrationGetTablesGet
   */
  static readonly ApiMigrationGetTablesGetPath = '/api/Migration/GetTables';

  /**
   * Получить список таблиц с исходной базы данных.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiMigrationGetTablesGet()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiMigrationGetTablesGet$Response(params?: {
  },
  context?: HttpContext

): Observable<StrictHttpResponse<TableInfosDto>> {

    const rb = new RequestBuilder(this.rootUrl, MigrationService.ApiMigrationGetTablesGetPath, 'get');
    if (params) {
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json',
      context: context
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<TableInfosDto>;
      })
    );
  }

  /**
   * Получить список таблиц с исходной базы данных.
   *
   *
   *
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiMigrationGetTablesGet$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiMigrationGetTablesGet(params?: {
  },
  context?: HttpContext

): Observable<TableInfosDto> {

    return this.apiMigrationGetTablesGet$Response(params,context).pipe(
      map((r: StrictHttpResponse<TableInfosDto>) => r.body as TableInfosDto)
    );
  }

  /**
   * Path part for operation apiMigrationMigrateTablesPost
   */
  static readonly ApiMigrationMigrateTablesPostPath = '/api/Migration/MigrateTables';

  /**
   * Мигрировать все таблицы.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiMigrationMigrateTablesPost()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiMigrationMigrateTablesPost$Response(params?: {
  },
  context?: HttpContext

): Observable<StrictHttpResponse<void>> {

    const rb = new RequestBuilder(this.rootUrl, MigrationService.ApiMigrationMigrateTablesPostPath, 'post');
    if (params) {
    }

    return this.http.request(rb.build({
      responseType: 'text',
      accept: '*/*',
      context: context
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return (r as HttpResponse<any>).clone({ body: undefined }) as StrictHttpResponse<void>;
      })
    );
  }

  /**
   * Мигрировать все таблицы.
   *
   *
   *
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiMigrationMigrateTablesPost$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiMigrationMigrateTablesPost(params?: {
  },
  context?: HttpContext

): Observable<void> {

    return this.apiMigrationMigrateTablesPost$Response(params,context).pipe(
      map((r: StrictHttpResponse<void>) => r.body as void)
    );
  }

  /**
   * Path part for operation apiMigrationMigrateTablePost
   */
  static readonly ApiMigrationMigrateTablePostPath = '/api/Migration/MigrateTable';

  /**
   * Мигрировать переданные таблицы.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiMigrationMigrateTablePost()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiMigrationMigrateTablePost$Response(params?: {
    body?: MigrateTableCommand
  },
  context?: HttpContext

): Observable<StrictHttpResponse<void>> {

    const rb = new RequestBuilder(this.rootUrl, MigrationService.ApiMigrationMigrateTablePostPath, 'post');
    if (params) {
      rb.body(params.body, 'application/*+json');
    }

    return this.http.request(rb.build({
      responseType: 'text',
      accept: '*/*',
      context: context
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return (r as HttpResponse<any>).clone({ body: undefined }) as StrictHttpResponse<void>;
      })
    );
  }

  /**
   * Мигрировать переданные таблицы.
   *
   *
   *
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiMigrationMigrateTablePost$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiMigrationMigrateTablePost(params?: {
    body?: MigrateTableCommand
  },
  context?: HttpContext

): Observable<void> {

    return this.apiMigrationMigrateTablePost$Response(params,context).pipe(
      map((r: StrictHttpResponse<void>) => r.body as void)
    );
  }

  /**
   * Path part for operation apiMigrationGetStatusGet
   */
  static readonly ApiMigrationGetStatusGetPath = '/api/Migration/GetStatus';

  /**
   * Получить статус миграции.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiMigrationGetStatusGet()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiMigrationGetStatusGet$Response(params?: {
  },
  context?: HttpContext

): Observable<StrictHttpResponse<MigrationStatusDto>> {

    const rb = new RequestBuilder(this.rootUrl, MigrationService.ApiMigrationGetStatusGetPath, 'get');
    if (params) {
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json',
      context: context
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<MigrationStatusDto>;
      })
    );
  }

  /**
   * Получить статус миграции.
   *
   *
   *
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiMigrationGetStatusGet$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiMigrationGetStatusGet(params?: {
  },
  context?: HttpContext

): Observable<MigrationStatusDto> {

    return this.apiMigrationGetStatusGet$Response(params,context).pipe(
      map((r: StrictHttpResponse<MigrationStatusDto>) => r.body as MigrationStatusDto)
    );
  }

  /**
   * Path part for operation apiMigrationGetMigrationJournalDataGet
   */
  static readonly ApiMigrationGetMigrationJournalDataGetPath = '/api/Migration/GetMigrationJournalData';

  /**
   * Получить данные об миграциях.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiMigrationGetMigrationJournalDataGet()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiMigrationGetMigrationJournalDataGet$Response(params?: {
  },
  context?: HttpContext

): Observable<StrictHttpResponse<Array<UserMigrationData>>> {

    const rb = new RequestBuilder(this.rootUrl, MigrationService.ApiMigrationGetMigrationJournalDataGetPath, 'get');
    if (params) {
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json',
      context: context
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<Array<UserMigrationData>>;
      })
    );
  }

  /**
   * Получить данные об миграциях.
   *
   *
   *
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiMigrationGetMigrationJournalDataGet$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiMigrationGetMigrationJournalDataGet(params?: {
  },
  context?: HttpContext

): Observable<Array<UserMigrationData>> {

    return this.apiMigrationGetMigrationJournalDataGet$Response(params,context).pipe(
      map((r: StrictHttpResponse<Array<UserMigrationData>>) => r.body as Array<UserMigrationData>)
    );
  }

  /**
   * Path part for operation apiMigrationCancelMigrationPost
   */
  static readonly ApiMigrationCancelMigrationPostPath = '/api/Migration/CancelMigration';

  /**
   * Получить статус миграции.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiMigrationCancelMigrationPost()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiMigrationCancelMigrationPost$Response(params?: {
  },
  context?: HttpContext

): Observable<StrictHttpResponse<void>> {

    const rb = new RequestBuilder(this.rootUrl, MigrationService.ApiMigrationCancelMigrationPostPath, 'post');
    if (params) {
    }

    return this.http.request(rb.build({
      responseType: 'text',
      accept: '*/*',
      context: context
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return (r as HttpResponse<any>).clone({ body: undefined }) as StrictHttpResponse<void>;
      })
    );
  }

  /**
   * Получить статус миграции.
   *
   *
   *
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiMigrationCancelMigrationPost$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiMigrationCancelMigrationPost(params?: {
  },
  context?: HttpContext

): Observable<void> {

    return this.apiMigrationCancelMigrationPost$Response(params,context).pipe(
      map((r: StrictHttpResponse<void>) => r.body as void)
    );
  }

  /**
   * Path part for operation apiMigrationGetCurrentMigrationSettingsGet
   */
  static readonly ApiMigrationGetCurrentMigrationSettingsGetPath = '/api/Migration/GetCurrentMigrationSettings';

  /**
   * Получить информацию по заданнаном подключении.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiMigrationGetCurrentMigrationSettingsGet()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiMigrationGetCurrentMigrationSettingsGet$Response(params?: {
  },
  context?: HttpContext

): Observable<StrictHttpResponse<CurrentMigrationSettingsDto>> {

    const rb = new RequestBuilder(this.rootUrl, MigrationService.ApiMigrationGetCurrentMigrationSettingsGetPath, 'get');
    if (params) {
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json',
      context: context
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<CurrentMigrationSettingsDto>;
      })
    );
  }

  /**
   * Получить информацию по заданнаном подключении.
   *
   *
   *
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiMigrationGetCurrentMigrationSettingsGet$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiMigrationGetCurrentMigrationSettingsGet(params?: {
  },
  context?: HttpContext

): Observable<CurrentMigrationSettingsDto> {

    return this.apiMigrationGetCurrentMigrationSettingsGet$Response(params,context).pipe(
      map((r: StrictHttpResponse<CurrentMigrationSettingsDto>) => r.body as CurrentMigrationSettingsDto)
    );
  }

}
