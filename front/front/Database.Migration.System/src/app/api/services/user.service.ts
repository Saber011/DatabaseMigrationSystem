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

import { DeleteUserCommand } from '../models/delete-user-command';
import { GetUsersQuery } from '../models/get-users-query';
import { UpdateUserCommand } from '../models/update-user-command';
import { UserDto } from '../models/user-dto';

@Injectable({
  providedIn: 'root',
})
export class UserService extends BaseService {
  constructor(
    config: ApiConfiguration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * Path part for operation apiUserGetAllUsersGet
   */
  static readonly ApiUserGetAllUsersGetPath = '/api/User/GetAllUsers';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiUserGetAllUsersGet()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiUserGetAllUsersGet$Response(params?: {
    request?: GetUsersQuery;
  },
  context?: HttpContext

): Observable<StrictHttpResponse<Array<UserDto>>> {

    const rb = new RequestBuilder(this.rootUrl, UserService.ApiUserGetAllUsersGetPath, 'get');
    if (params) {
      rb.query('request', params.request, {});
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json',
      context: context
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<Array<UserDto>>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiUserGetAllUsersGet$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiUserGetAllUsersGet(params?: {
    request?: GetUsersQuery;
  },
  context?: HttpContext

): Observable<Array<UserDto>> {

    return this.apiUserGetAllUsersGet$Response(params,context).pipe(
      map((r: StrictHttpResponse<Array<UserDto>>) => r.body as Array<UserDto>)
    );
  }

  /**
   * Path part for operation apiUserGetUserByIdGet
   */
  static readonly ApiUserGetUserByIdGetPath = '/api/User/GetUserById';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiUserGetUserByIdGet()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiUserGetUserByIdGet$Response(params?: {
    UserId?: number;
  },
  context?: HttpContext

): Observable<StrictHttpResponse<UserDto>> {

    const rb = new RequestBuilder(this.rootUrl, UserService.ApiUserGetUserByIdGetPath, 'get');
    if (params) {
      rb.query('UserId', params.UserId, {});
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json',
      context: context
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<UserDto>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiUserGetUserByIdGet$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiUserGetUserByIdGet(params?: {
    UserId?: number;
  },
  context?: HttpContext

): Observable<UserDto> {

    return this.apiUserGetUserByIdGet$Response(params,context).pipe(
      map((r: StrictHttpResponse<UserDto>) => r.body as UserDto)
    );
  }

  /**
   * Path part for operation apiUserDeleteUserPost
   */
  static readonly ApiUserDeleteUserPostPath = '/api/User/DeleteUser';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiUserDeleteUserPost()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiUserDeleteUserPost$Response(params?: {
    body?: DeleteUserCommand
  },
  context?: HttpContext

): Observable<StrictHttpResponse<void>> {

    const rb = new RequestBuilder(this.rootUrl, UserService.ApiUserDeleteUserPostPath, 'post');
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
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiUserDeleteUserPost$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiUserDeleteUserPost(params?: {
    body?: DeleteUserCommand
  },
  context?: HttpContext

): Observable<void> {

    return this.apiUserDeleteUserPost$Response(params,context).pipe(
      map((r: StrictHttpResponse<void>) => r.body as void)
    );
  }

  /**
   * Path part for operation apiUserUpdateUserInfoPost
   */
  static readonly ApiUserUpdateUserInfoPostPath = '/api/User/UpdateUserInfo';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiUserUpdateUserInfoPost()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiUserUpdateUserInfoPost$Response(params?: {
    body?: UpdateUserCommand
  },
  context?: HttpContext

): Observable<StrictHttpResponse<void>> {

    const rb = new RequestBuilder(this.rootUrl, UserService.ApiUserUpdateUserInfoPostPath, 'post');
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
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiUserUpdateUserInfoPost$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiUserUpdateUserInfoPost(params?: {
    body?: UpdateUserCommand
  },
  context?: HttpContext

): Observable<void> {

    return this.apiUserUpdateUserInfoPost$Response(params,context).pipe(
      map((r: StrictHttpResponse<void>) => r.body as void)
    );
  }

}
