type RuleType =
  | 'boolean'
  | 'date'
  | 'email'
  | 'enum'
  | 'float'
  | 'hex'
  | 'integer'
  | 'method'
  | 'number'
  | 'object'
  | 'regexp'
  | 'string'
  | 'url';

interface Rule {
  [key: string]: any;
  trigger?: 'blur' | 'change' | ['change', 'blur'];
  type?: 'array' | RuleType;
}

interface Field extends Rule {
  /** Connector
   * @description Connection character used when localizing field names
   * @example . => L('ResourceName.DisplayName.Field')
   * @example : => L('ResourceName.DisplayName:Field')
   */
  connector?: string;
  /** Field name */
  name?: string;
  /** Field prefix
   * @description for localized field names
   * @example DisplayName => L('ResourceName.DisplayName:Field')
   */
  prefix?: string;
  /** Localized resources */
  resourceName?: string;
}

interface FieldRequired extends Field {
  /** Is it necessary */
  required?: boolean;
}

interface FieldBeetWeen extends Field {
  /** end value */
  end: number;
  /** Starting value */
  start: number;
}

interface FieldLength extends Field {
  /** length */
  length: number;
}

interface FieldRange extends Field {
  /** Maximum value */
  maximum: number;
  /** Minimum value */
  minimum: number;
}

interface FieldRegular extends Field {
  /** Regular expression */
  expression: string;
}

interface FieldMatch extends FieldRequired {
  /** Compare fields */
  matchField: string;
  /** Compare field values */
  matchValue: string;
  /** Field name */
  name: string;
}

interface FieldContains extends Field {
  /** Verify whether the value is included in the defined value */
  value: string;
}

interface FieldValidator extends FieldRequired {
  /**Validity validator */
  validator: (value: any) => boolean;
}

interface FieldDefineValidator extends FieldRequired {
  message?: string;
  validator: (
    rule: any,
    value: any,
    callback: any,
    source?: any,
    options?: any,
  ) => Promise<void> | void;
}

export type {
  Field,
  FieldBeetWeen,
  FieldContains,
  FieldDefineValidator,
  FieldLength,
  FieldMatch,
  FieldRange,
  FieldRegular,
  FieldRequired,
  FieldValidator,
  Rule,
  RuleType,
};
