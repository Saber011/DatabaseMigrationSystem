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

import { SetSettingsCommand } from '../models/set-settings-command';
import { Settings } from '../models/settings';

@Injectable({
  providedIn: 'root',
})
export class SettingsService extends BaseService {
  constructor(
    config: ApiConfiguration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * Path part for operation apiSettingsSetSettingsPost
   */
  static readonly ApiSettingsSetSettingsPostPath = '/api/Settings/SetSettings';

  /**
   * Задать настройки.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiSettingsSetSettingsPost()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiSettingsSetSettingsPost$Response(params?: {
    body?: SetSettingsCommand
  },
  context?: HttpContext

): Observable<StrictHttpResponse<void>> {

    const rb = new RequestBuilder(this.rootUrl, SettingsService.ApiSettingsSetSettingsPostPath, 'post');
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
   * Задать настройки.
   *
   *
   *
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiSettingsSetSettingsPost$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiSettingsSetSettingsPost(params?: {
    body?: SetSettingsCommand
  },
  context?: HttpContext

): Observable<void> {

    return this.apiSettingsSetSettingsPost$Response(params,context).pipe(
      map((r: StrictHttpResponse<void>) => r.body as void)
    );
  }

  /**
   * Path part for operation apiSettingsGetSettingsGet
   */
  static readonly ApiSettingsGetSettingsGetPath = '/api/Settings/GetSettings';

  /**
   * Задать настройки.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiSettingsGetSettingsGet()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiSettingsGetSettingsGet$Response(params?: {
  },
  context?: HttpContext

): Observable<StrictHttpResponse<Settings>> {

    const rb = new RequestBuilder(this.rootUrl, SettingsService.ApiSettingsGetSettingsGetPath, 'get');
    if (params) {
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json',
      context: context
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<Settings>;
      })
    );
  }

  /**
   * Задать настройки.
   *
   *
   *
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiSettingsGetSettingsGet$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiSettingsGetSettingsGet(params?: {
  },
  context?: HttpContext

): Observable<Settings> {

    return this.apiSettingsGetSettingsGet$Response(params,context).pipe(
      map((r: StrictHttpResponse<Settings>) => r.body as Settings)
    );
  }

}
