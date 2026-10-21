import type { AuditedEntityDto } from '@abp/core';

enum DataAccessOperation {
  /** Delete */
  Delete = 2,
  /** Query */
  Read = 0,
  /** Update */
  Write = 1,
}

enum DataAccessFilterOperate {
  /** contains */
  Contains = 9,
  /** Right contains */
  EndsWith = 8,
  /** equals */
  Equal = 1,
  /** Greater than */
  Greater = 5,
  /** Greater than or equal to */
  GreaterOrEqual = 6,
  /** Less than */
  Less = 3,
  /** Less than or equal to */
  LessOrEqual = 4,
  /** Does not contain */
  NotContains = 10,
  /** Not equal to */
  NotEqual = 2,
  /** Left contains */
  StartsWith = 7,
}

enum DataAccessFilterLogic {
  /** and */
  And = 0,
  /** or */
  Or = 1,
}

interface DataAccessFilterRule {
  [key: string]: any;
  field: string;
  isLeft: boolean;
  javaScriptType: string;
  operate: DataAccessFilterOperate;
  typeFullName: string;
  value: string;
}

interface DataAccessFilterGroup {
  groups: DataAccessFilterGroup[];
  logic: DataAccessFilterLogic;
  rules: DataAccessFilterRule[];
}

interface EntityRuleDtoBase extends AuditedEntityDto<string> {
  accessedProperties: string[];
  entityTypeFullName: string;
  entityTypeId: string;
  filterGroup: DataAccessFilterGroup;
  isEnabled: boolean;
  operation: DataAccessOperation;
  tenantId?: string;
}

interface EntityEnumInfoModel {
  key: string;
  value: any;
}

interface EntityPropertyInfoModel {
  displayName: string;
  enums: EntityEnumInfoModel[];
  javaScriptName: string;
  javaScriptType: string;
  name: string;
  operates: DataAccessFilterOperate[];
  typeFullName: string;
}

interface EntityTypeInfoModel {
  displayName: string;
  name: string;
  properties: EntityPropertyInfoModel[];
}

interface EntityTypeInfoGetModel {
  operation: DataAccessOperation;
}

export { DataAccessFilterLogic, DataAccessFilterOperate, DataAccessOperation };

export type {
  DataAccessFilterGroup,
  DataAccessFilterRule,
  EntityRuleDtoBase,
  EntityTypeInfoGetModel,
  EntityTypeInfoModel,
};
