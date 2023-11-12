/* tslint:disable */
/* eslint-disable */
import { MigrationStatus } from './migration-status';
export interface MigrationStatusDto {
  currentTable?: null | string;
  status?: MigrationStatus;
}
