/* tslint:disable */
/* eslint-disable */
import { MigrationStatus } from './migration-status';
import { TimeSpan } from './time-span';
export interface UserMigrationData {
  destinationDatabase?: null | string;
  endDate?: string;
  executionTime?: TimeSpan;
  id?: number;
  migrationStatus?: MigrationStatus;
  sourceDatabase?: null | string;
  startDate?: string;
  tableList?: null | string;
}
