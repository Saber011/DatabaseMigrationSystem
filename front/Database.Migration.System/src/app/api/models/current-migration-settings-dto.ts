/* tslint:disable */
/* eslint-disable */
import { DatabaseType } from './database-type';
export interface CurrentMigrationSettingsDto {
  destinationDatabaseDataInfo?: null | string;
  destinationDatabaseType?: DatabaseType;
  sourceDatabaseDataInfo?: null | string;
  sourceDatabaseType?: DatabaseType;
}
