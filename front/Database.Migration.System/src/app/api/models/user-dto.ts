/* tslint:disable */
/* eslint-disable */
import { RefreshToken } from './refresh-token';
export interface UserDto {
  id?: number;
  login?: null | string;
  refreshTokens?: null | Array<RefreshToken>;
  roles?: null | Array<number>;
}
