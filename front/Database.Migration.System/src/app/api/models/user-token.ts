/* tslint:disable */
/* eslint-disable */
import { User } from './user';
export interface UserToken {
  created?: string;
  createdByIp?: null | string;
  expires?: string;
  replacedByToken?: null | string;
  revoked?: string;
  revokedByIp?: null | string;
  token?: null | string;
  user?: User;
  userId?: number;
}
