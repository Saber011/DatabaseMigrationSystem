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

import { AuthenticateCommand } from '../models/authenticate-command';
import { AuthenticateInfo } from '../models/authenticate-info';
import { RegisterUserCommand } from '../models/register-user-command';
import { RevokeTokenCommand } from '../models/revoke-token-command';

@Injectable({
  providedIn: 'root',
})
export class AccountService extends BaseService {
  constructor(
    config: ApiConfiguration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * Path part for operation apiAccountAuthenticatePost
   */
  static readonly ApiAccountAuthenticatePostPath = '/api/Account/Authenticate';

  /**
   * Авторизация.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiAccountAuthenticatePost()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiAccountAuthenticatePost$Response(params?: {
    body?: AuthenticateCommand
  },
  context?: HttpContext

): Observable<StrictHttpResponse<AuthenticateInfo>> {

    const rb = new RequestBuilder(this.rootUrl, AccountService.ApiAccountAuthenticatePostPath, 'post');
    if (params) {
      rb.body(params.body, 'application/*+json');
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json',
      context: context
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<AuthenticateInfo>;
      })
    );
  }

  /**
   * Авторизация.
   *
   *
   *
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiAccountAuthenticatePost$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiAccountAuthenticatePost(params?: {
    body?: AuthenticateCommand
  },
  context?: HttpContext

): Observable<AuthenticateInfo> {

    return this.apiAccountAuthenticatePost$Response(params,context).pipe(
      map((r: StrictHttpResponse<AuthenticateInfo>) => r.body as AuthenticateInfo)
    );
  }

  /**
   * Path part for operation apiAccountRefreshTokenPost
   */
  static readonly ApiAccountRefreshTokenPostPath = '/api/Account/RefreshToken';

  /**
   * Получить рефреш токен.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiAccountRefreshTokenPost()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiAccountRefreshTokenPost$Response(params?: {
  },
  context?: HttpContext

): Observable<StrictHttpResponse<AuthenticateInfo>> {

    const rb = new RequestBuilder(this.rootUrl, AccountService.ApiAccountRefreshTokenPostPath, 'post');
    if (params) {
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json',
      context: context
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<AuthenticateInfo>;
      })
    );
  }

  /**
   * Получить рефреш токен.
   *
   *
   *
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiAccountRefreshTokenPost$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiAccountRefreshTokenPost(params?: {
  },
  context?: HttpContext

): Observable<AuthenticateInfo> {

    return this.apiAccountRefreshTokenPost$Response(params,context).pipe(
      map((r: StrictHttpResponse<AuthenticateInfo>) => r.body as AuthenticateInfo)
    );
  }

  /**
   * Path part for operation apiAccountRevokeTokenPost
   */
  static readonly ApiAccountRevokeTokenPostPath = '/api/Account/RevokeToken';

  /**
   * Удалить токен.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiAccountRevokeTokenPost()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiAccountRevokeTokenPost$Response(params?: {
    body?: RevokeTokenCommand
  },
  context?: HttpContext

): Observable<StrictHttpResponse<void>> {

    const rb = new RequestBuilder(this.rootUrl, AccountService.ApiAccountRevokeTokenPostPath, 'post');
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
   * Удалить токен.
   *
   *
   *
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiAccountRevokeTokenPost$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiAccountRevokeTokenPost(params?: {
    body?: RevokeTokenCommand
  },
  context?: HttpContext

): Observable<void> {

    return this.apiAccountRevokeTokenPost$Response(params,context).pipe(
      map((r: StrictHttpResponse<void>) => r.body as void)
    );
  }

  /**
   * Path part for operation apiAccountRegisterUserPost
   */
  static readonly ApiAccountRegisterUserPostPath = '/api/Account/RegisterUser';

  /**
   * Регистрация нового пользователя.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiAccountRegisterUserPost()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiAccountRegisterUserPost$Response(params?: {
    body?: RegisterUserCommand
  },
  context?: HttpContext

): Observable<StrictHttpResponse<void>> {

    const rb = new RequestBuilder(this.rootUrl, AccountService.ApiAccountRegisterUserPostPath, 'post');
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
   * Регистрация нового пользователя.
   *
   *
   *
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiAccountRegisterUserPost$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiAccountRegisterUserPost(params?: {
    body?: RegisterUserCommand
  },
  context?: HttpContext

): Observable<void> {

    return this.apiAccountRegisterUserPost$Response(params,context).pipe(
      map((r: StrictHttpResponse<void>) => r.body as void)
    );
  }

}
