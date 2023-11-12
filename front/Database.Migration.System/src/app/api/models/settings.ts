/* tslint:disable */
/* eslint-disable */
import { DatabaseType } from './database-type';
import { User } from './user';
export interface Settings {
  destinationConnectionString?: null | string;
  destinationDatabaseType?: DatabaseType;
  id?: number;
  sourceConnectionString?: null | string;
  sourceDatabaseType?: DatabaseType;
  user?: User;
  userId?: number;
}
