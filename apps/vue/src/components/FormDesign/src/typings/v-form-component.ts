import { IAnyObject } from './base-type';
// import { ComponentOptions } from 'vue/types/options';
import { ComponentOptions } from 'vue';
import { IVFormMethods } from '../hooks/useVFormMethods';
import { ColEx } from '/@/components/Form/src/types';

import { SelectValue } from 'ant-design-vue/lib/select';
import { validateOptions } from 'ant-design-vue/lib/form/useForm';
import { RuleError } from 'ant-design-vue/lib/form/interface';
import { FormItem } from '/@/components/Form';
type LayoutType = 'horizontal' | 'vertical' | 'inline';
type labelLayout = 'flex' | 'Grid';
export type PropsTabKey = 1 | 2 | 3;
type ColSpanType = number | string;

declare type Value = [number, number] | number;
/**
 * Component properties
 */
export interface IVFormComponent {
  // extends Omit<FormSchema, 'component' | 'label' | 'field' | 'rules'> {
  // Corresponding fields
  field?: string;
  // component type
  component: string;
  // component label
  label?: string;
  // Custom component control instance
  componentInstance?: ComponentOptions<any>;
  // component icon
  icon?: string;
  // Component verification rules
  rules?: Partial<IValidationRule>[];
  // Whether to hide
  hidden?: boolean;
  // hide label
  hiddenLabel?: boolean;
  // component width
  width?: string;
  // Is it required?
  required?: boolean;
  // Required prompt
  message?: string;
  // Prompt message
  helpMessage?: string;
  // Attributes passed to the component. By default, all props will be passed to the control.
  componentProps?: IAnyObject;
  // Listen to the component event object and pass it to the control in v-on mode
  on?: IAnyObject<(...any: []) => void>;
  // component options
  options?: IAnyObject;
  // unique identifier
  key?: string;
  // Reference formModelItem
  itemProps?: Partial<FormItem>;

  colProps?: Partial<ColEx>;
  // Linkage field
  link?: string[];
  // Callback for linkage attribute changes
  update?: (value: any, formItem: IVFormComponent, fApi: IVFormMethods) => void;
  // Number of control grids
  // span?: number;
  // Label layout
  labelCol?: IAnyObject;
  // component layout
  wrapperCol?: IAnyObject;
  // child control
  columns?: Array<{ span: number; children: any[] }>;
}

declare type namesType = string | string[];

/**
 * Form configuration
 */
export interface IFormConfig {
  // Form item configuration list
  // schemas: IVFormComponent[];
  // form configuration
  // config: {
  layout?: LayoutType;
  labelLayout?: labelLayout;
  labelWidth?: number;
  labelCol?: Partial<IACol>;
  wrapperCol?: Partial<IACol>;
  hideRequiredMark?: boolean;
  // Whether to disable
  schemas: IVFormComponent[];
  disabled?: boolean;
  labelAlign?: 'left' | 'right';
  // Internal component size of the form
  size?: 'default' | 'small' | 'large';
  // };
  // currently selected item
  currentItem?: IVFormComponent;
  activeKey?: PropsTabKey;
  colon?: boolean;
}

export interface AForm {
  /**
   * Hide required mark of all form items
   * @default false
   * @type boolean
   */
  hideRequiredMark: boolean;

  /**
   * The layout of label. You can set span offset to something like {span: 3, offset: 12} or sm: {span: 3, offset: 12} same as with <Col>
   * @type IACol
   */
  labelCol: IACol;

  /**
   * Define form layout
   * @default 'horizontal'
   * @type string
   */
  layout: 'horizontal' | 'inline' | 'vertical';

  /**
   * The layout for input controls, same as labelCol
   * @type IACol
   */
  wrapperCol: IACol;

  /**
   * change default props colon value of Form.Item (only effective when prop layout is horizontal)
   * @type boolean
   * @default true
   */
  colon: boolean;

  /**
   * text align of label of all items
   * @type 'left' | 'right'
   * @default 'left'
   */
  labelAlign: 'left' | 'right';

  /**
   * data of form component
   * @type object
   */
  model: IAnyObject;

  /**
   * validation rules of form
   * @type object
   */
  rules: IAnyObject;

  /**
   * Default validate message. And its format is similar with newMessages's returned value
   * @type any
   */
  validateMessages?: any;

  /**
   * whether to trigger validation when the rules prop is changed
   * @type Boolean
   * @default true
   */
  validateOnRuleChange: boolean;

  /**
   * validate the whole form. Takes a callback as a param. After validation,
   * the callback will be executed with two params: a boolean indicating if the validation has passed,
   * and an object containing all fields that fail the validation. Returns a promise if callback is omitted
   * @type Function
   */
  validate: <T = any>(names?: namesType, option?: validateOptions) => Promise<T>;

  /**
   * validate one or several form items
   * @type Function
   */
  validateField: (
    name: string,
    value: any,
    rules: Record<string, unknown>[],
    option?: validateOptions,
  ) => Promise<RuleError[]>;
  /**
   * reset all the fields and remove validation result
   */
  resetFields: () => void;

  /**
   * clear validation message for certain fields.
   * The parameter is prop name or an array of prop names of the form items whose validation messages will be removed.
   * When omitted, all fields' validation messages will be cleared
   * @type string[] | string
   */
  clearValidate: (props: string[] | string) => void;
}

interface IACol {
  /**
   * raster number of cells to occupy, 0 corresponds to display: none
   * @default none (0)
   * @type ColSpanType
   */
  span: Value;

  /**
   * raster order, used in flex layout mode
   * @default 0
   * @type ColSpanType
   */
  order: ColSpanType;

  /**
   * the layout fill of flex
   * @default none
   * @type ColSpanType
   */
  flex: ColSpanType;

  /**
   * the number of cells to offset Col from the left
   * @default 0
   * @type ColSpanType
   */
  offset: ColSpanType;

  /**
   * the number of cells that raster is moved to the right
   * @default 0
   * @type ColSpanType
   */
  push: ColSpanType;

  /**
   * the number of cells that raster is moved to the left
   * @default 0
   * @type ColSpanType
   */
  pull: ColSpanType;

  /**
   * <576px and also default setting, could be a span value or an object containing above props
   * @type { span: ColSpanType, offset: ColSpanType } | ColSpanType
   */
  xs: { span: ColSpanType; offset: ColSpanType } | ColSpanType;

  /**
   * ≥576px, could be a span value or an object containing above props
   * @type { span: ColSpanType, offset: ColSpanType } | ColSpanType
   */
  sm: { span: ColSpanType; offset: ColSpanType } | ColSpanType;

  /**
   * ≥768px, could be a span value or an object containing above props
   * @type { span: ColSpanType, offset: ColSpanType } | ColSpanType
   */
  md: { span: ColSpanType; offset: ColSpanType } | ColSpanType;

  /**
   * ≥992px, could be a span value or an object containing above props
   * @type { span: ColSpanType, offset: ColSpanType } | ColSpanType
   */
  lg: { span: ColSpanType; offset: ColSpanType } | ColSpanType;

  /**
   * ≥1200px, could be a span value or an object containing above props
   * @type { span: ColSpanType, offset: ColSpanType } | ColSpanType
   */
  xl: { span: ColSpanType; offset: ColSpanType } | ColSpanType;

  /**
   * ≥1600px, could be a span value or an object containing above props
   * @type { span: ColSpanType, offset: ColSpanType } | ColSpanType
   */
  xxl: { span: ColSpanType; offset: ColSpanType } | ColSpanType;
}

export interface IValidationRule {
  trigger?: 'change' | 'blur' | ['change', 'blur'];
  /**
   * validation error message
   * @type string | Function
   */
  message?: string | number;

  /**
   * built-in validation type, available options: https://github.com/yiminghe/async-validator#type
   * @default 'string'
   * @type string
   */
  type?: string;

  /**
   * indicates whether field is required
   * @default false
   * @type boolean
   */
  required?: boolean;

  /**
   * treat required fields that only contain whitespace as errors
   * @default false
   * @type boolean
   */
  whitespace?: boolean;

  /**
   * validate the exact length of a field
   * @type number
   */
  len?: number;

  /**
   * validate the min length of a field
   * @type number
   */
  min?: number;

  /**
   * validate the max length of a field
   * @type number
   */
  max?: number;

  /**
   * validate the value from a list of possible values
   * @type string | string[]
   */
  enum?: string | string[];

  /**
   * validate from a regular expression
   * @type boolean
   */
  pattern?: SelectValue;

  /**
   * transform a value before validation
   * @type Function
   */
  transform?: (value: any) => any;

  /**
   * custom validate function (Note: callback must be called)
   * @type Function
   */
  validator?: (rule: any, value: any, callback: () => void) => any;
}