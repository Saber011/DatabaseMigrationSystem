/* tslint:disable */
/* eslint-disable */
import { UserRoles } from './user-roles';
export interface Role {
  name?: null | string;
  roleId?: number;
  roles?: null | Array<UserRoles>;
}
