/* tslint:disable */
/* eslint-disable */
import { ColumnMapping } from './column-mapping';
export interface MigrateTableRequest {
  columnsMapping?: null | Array<ColumnMapping>;
  destinationSchema?: null | string;
  destinationTable?: null | string;
  sourceSchema?: null | string;
  sourceTable?: null | string;
}
