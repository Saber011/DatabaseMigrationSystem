/* tslint:disable */
/* eslint-disable */
import { DatabaseType } from './database-type';
export interface SetSettingsCommand {
  destinationConnectionString?: null | string;
  destinationDatabaseType?: DatabaseType;
  sourceConnectionString?: null | string;
  sourceDatabaseType?: DatabaseType;
}
