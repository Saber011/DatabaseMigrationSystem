/* tslint:disable */
/* eslint-disable */
import { Settings } from './settings';
import { UserRoles } from './user-roles';
import { UserToken } from './user-token';
export interface User {
  id?: number;
  isDeleted?: number;
  login?: null | string;
  password?: null | string;
  roles?: null | Array<UserRoles>;
  settings?: Settings;
  userTokens?: null | Array<UserToken>;
}
