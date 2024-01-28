/* tslint:disable */
/* eslint-disable */
import { MigrationStatus } from './migration-status';
import { TimeSpan } from './time-span';
export interface UserMigrationData {
  currentRecordsCount?: number;
  migrationDuration?: TimeSpan;
  migrationId?: string;
  migrationProgressPercentage?: number;
  migrationStatus?: MigrationStatus;
  totalRecordsForMigration?: number;
}
