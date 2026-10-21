import { RuleType } from 'ant-design-vue/lib/form/interface';

interface IRule {
  type?: RuleType | 'array';
  trigger?: 'blur' | 'change' | ['change', 'blur'];
}

export interface IField extends IRule {
  /** Field name */
  name?: string;
  /** Field prefix
   * @description for localized field names
   * @example DisplayName => L('ResourceName.DisplayName:Field')
   */
  prefix?: string;
  /** Connector
   * @description Connection character used when localizing field names
   * @example . => L('ResourceName.DisplayName.Field')
   * @example : => L('ResourceName.DisplayName:Field')
   */
  connector?: string;
  /** Localized resources */
  resourceName?: string;
}

export interface IFieldRequired extends IField {
  /** Is it necessary */
  required?: boolean;
}

export interface IFieldBeetWeen extends IField {
  /** Starting value */
  start: number;
  /** end value */
  end: number;
}

export interface IFieldLength extends IField {
  /** length */
  length: number;
}

export interface IFieldRange extends IField {
  /** Minimum value */
  minimum: number;
  /** Maximum value */
  maximum: number;
}

export interface IFieldRegular extends IField {
  /** Regular expression */
  expression: string;
}

export interface IFieldMatch extends IFieldRequired {
  /** Field name */
  name: string;
  /** Compare fields */
  matchField: string;
  /** Compare field values */
  matchValue: string;
}

export interface IFieldContains extends IField {
  /** Verify whether the value is included in the defined value */
  value: string;
}

export interface IFieldValidator extends IFieldRequired {
  /**Validity validator */
  validator: (value: any) => boolean;
}

export interface IDefineFieldValidator extends IFieldRequired {
  message?: string;
  validator: (rule: any, value: any, callback: any, source?: any, options?: any) => Promise<void> | void;
}
