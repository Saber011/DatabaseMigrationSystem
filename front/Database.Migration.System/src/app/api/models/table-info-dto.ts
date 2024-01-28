/* tslint:disable */
/* eslint-disable */
import { FieldDto } from './field-dto';
export interface TableInfoDto {
  dataCount?: number;
  fields?: null | Array<FieldDto>;
  schema?: null | string;
  tableName?: null | string;
}
