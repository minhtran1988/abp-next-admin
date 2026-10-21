/** Advanced query condition attributes */
export interface AdvanceSearchProps {
  /** Use advanced query */
  useAdvancedSearch?: boolean;
  /** Allow fields to be searched repeatedly */
  allowDuplicateFieldSearch?: boolean;
  /** Field list api */
  defineFieldApi?: () => Promise<any>;
  /**
   * Custom field list
   * @remarks Customize the filter function after returning the field list from the server, and return the field list
   */
  defineFieldReplace?: (response: any) => DefineParamter[];
  /**
   * Field list api returns result fields
   * @remarks Returns the field names of the field list in the data structure from the server, default: items
  */
  listField?: string;
  /** Advanced query API */
  fetchApi?: (...arg: any) => Promise<any>,
}

/** Options */
export interface ParamterOption {
  /** Key name */
  key: string;
  /** key value */
  value: any;
}

/** Custom fields */
export interface DefineParamter {
  /** Field name */
  name: string;
  /** Field description */
  description?: string;
  /** Data type (backend) */
  type: string;
  /** Data type (js) */
  javaScriptType: string;
  /** List of available operation conditions, used to further constrain the available comparison operators for fields */
  availableComparator?: DynamicComparison[];
  /** Options */
  options: ParamterOption[];
}

/** Connection conditions */
export enum DynamicLogic {
  /** and */
  And = 0,
  /** or */
  Or = 1
}

/** Operation conditions */
export enum DynamicComparison {
  /** equals */
  Equal = 0,
  /** Not equal to */
  NotEqual = 1,
  /** Less than */
  LessThan = 2,
  /** Less than or equal to */
  LessThanOrEqual = 3,
  /** Greater than */
  GreaterThan = 4,
  /** Greater than or equal to */
  GreaterThanOrEqual = 5,
  /** Left contains */
  StartsWith = 6,
  /** Left does not contain */
  NotStartsWith = 7,
  /** Right contains */
  EndsWith = 8,
  /** Right does not contain */
  NotEndsWith = 9,
  /** contains */
  Contains = 10,
  /** Does not contain */
  NotContains = 11,
  /** empty */
  Null = 12,
  /** Not empty */
  NotNull = 13,
}

/** Dynamic query field */
export interface DynamicParamter {
  /** Field name */
  field: string;
  /** Connection conditions */
  logic: DynamicLogic;
  /** Operation conditions */
  comparison: DynamicComparison;
  /** Comparison value */
  value: any;
  /** Data type (js), only used as front-end input control switch */
  javaScriptType?: string;
}

/** Dynamic query conditions */
export interface DynamicQueryable {
  /** Parameter list */
  paramters: DynamicParamter[];
}
