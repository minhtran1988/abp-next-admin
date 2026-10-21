import type { FieldOptions, FormContext, GenericObject } from 'vee-validate';
import type { ZodTypeAny } from 'zod';

import type { Component, HtmlHTMLAttributes, Ref } from 'vue';

import type { VbenButtonProps } from '@vben-core/shadcn-ui';
import type { ClassType, MaybeComputedRef } from '@vben-core/typings';

import type { FormApi } from './form-api';

export type FormLayout = 'horizontal' | 'inline' | 'vertical';

export type BaseFormComponentType =
  | 'DefaultButton'
  | 'PrimaryButton'
  | 'VbenCheckbox'
  | 'VbenInput'
  | 'VbenInputPassword'
  | 'VbenPinInput'
  | 'VbenSelect'
  | (Record<never, never> & string);

type Breakpoints = '2xl:' | '3xl:' | '' | 'lg:' | 'md:' | 'sm:' | 'xl:';

type GridCols = 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10 | 11 | 12 | 13;

export type WrapperClassType =
  | `${Breakpoints}grid-cols-${GridCols}`
  | (Record<never, never> & string);

export type FormItemClassType =
  | `${Breakpoints}cols-end-${'auto' | GridCols}`
  | `${Breakpoints}cols-span-${'auto' | 'full' | GridCols}`
  | `${Breakpoints}cols-start-${'auto' | GridCols}`
  | (Record<never, never> & string)
  | WrapperClassType;

export type FormFieldOptions = Partial<
  FieldOptions & {
    validateOnBlur?: boolean;
    validateOnChange?: boolean;
    validateOnInput?: boolean;
    validateOnModelUpdate?: boolean;
  }
>;

export interface FormShape {
  /** Default value */
  default?: any;
  /** Field name */
  fieldName: string;
  /** Is it required */
  required?: boolean;
  rules?: ZodTypeAny;
}

export type MaybeComponentPropKey =
  | 'options'
  | 'placeholder'
  | 'title'
  | keyof HtmlHTMLAttributes
  | (Record<never, never> & string);

export type MaybeComponentProps = { [K in MaybeComponentPropKey]?: any };

export type FormActions = FormContext<GenericObject>;

export type CustomRenderType = (() => Component | string) | string;

export type FormSchemaRuleType =
  | 'required'
  | 'selectRequired'
  | null
  | (Record<never, never> & string)
  | ZodTypeAny;

type FormItemDependenciesCondition<T = boolean | PromiseLike<boolean>> = (
  value: Partial<Record<string, any>>,
  actions: FormActions,
) => T;

type FormItemDependenciesConditionWithRules = (
  value: Partial<Record<string, any>>,
  actions: FormActions,
) => FormSchemaRuleType | PromiseLike<FormSchemaRuleType>;

type FormItemDependenciesConditionWithProps = (
  value: Partial<Record<string, any>>,
  actions: FormActions,
) => MaybeComponentProps | PromiseLike<MaybeComponentProps>;

export interface FormItemDependencies {
  /**
   * Component parameters
   * @returns component parameters
   */
  componentProps?: FormItemDependenciesConditionWithProps;
  /**
   * Whether to disable
   * @returns whether to disable
   */
  disabled?: boolean | FormItemDependenciesCondition;
  /**
   * Whether to render (delete dom)
   * @returns whether to render
   */
  if?: boolean | FormItemDependenciesCondition;
  /**
   * Is it required?
   * @returns Is it required?
   */
  required?: FormItemDependenciesCondition;
  /**
   *Field rules
   */
  rules?: FormItemDependenciesConditionWithRules;
  /**
   * Whether to hide (Css)
   * @returns whether to hide
   */
  show?: boolean | FormItemDependenciesCondition;
  /**
   * Any trigger will be executed
   */
  trigger?: FormItemDependenciesCondition<void>;
  /**
   * Trigger field
   */
  triggerFields: string[];
}

type ComponentProps =
  | ((
      value: Partial<Record<string, any>>,
      actions: FormActions,
    ) => MaybeComponentProps)
  | MaybeComponentProps;

export interface FormCommonConfig {
  /**
   * Display a colon after the Label
   */
  colon?: boolean;
  /**
   * Props for all form items
   */
  componentProps?: ComponentProps;
  /**
   * Control styles for all form items
   */
  controlClass?: string;
  /**
   * Disabled state of all form items
   * @default false
   */
  disabled?: boolean;
  /**
   * Whether to disable change event listening for all form items
   * @default true
   */
  disabledOnChangeListener?: boolean;
  /**
   * Whether to disable input event listening for all form items
   * @default true
   */
  disabledOnInputListener?: boolean;
  /**
   * The empty state value of all form items is undefined by default, and the empty state value of naive-ui is null.
   */
  emptyStateValue?: null | undefined;
  /**
   * Control styles for all form items
   * @default {}
   */
  formFieldProps?: FormFieldOptions;
  /**
   * Grid layout of all form items, supporting functional form
   * @default ""
   */
  formItemClass?: (() => string) | string;
  /**
   * Hide all form item labels
   * @default false
   */
  hideLabel?: boolean;
  /**
   * Whether to hide required marks
   * @default false
   */
  hideRequiredMark?: boolean;
  /**
   * Label styles for all form items
   * @default ""
   */
  labelClass?: string;
  /**
   * Label width of all form items
   */
  labelWidth?: number;
  /**
   * The model attribute names of all form items
   * @default "modelValue"
   */
  modelPropName?: string;
  /**
   * Wrapper styles for all form items
   */
  wrapperClass?: string;
}

type RenderComponentContentType = (
  value: Partial<Record<string, any>>,
  api: FormActions,
) => Record<string, any>;

export type HandleSubmitFn = (
  values: Record<string, any>,
) => Promise<void> | void;

export type HandleResetFn = (
  values: Record<string, any>,
) => Promise<void> | void;

export type FieldMappingTime = [
  string,
  [string, string],
  (
    | ((value: any, fieldName: string) => any)
    | [string, string]
    | null
    | string
  )?,
][];

export type ArrayToStringFields = Array<
  | [string[], string?] // Nested array format, optional delimiter
  | string //Single field, using default delimiter
  | string[] // Simple array format, the last element can be a separator
>;

export interface FormSchema<
  T extends BaseFormComponentType = BaseFormComponentType,
> extends FormCommonConfig {
  /** Components */
  component: Component | T;
  /** Component parameters */
  componentProps?: ComponentProps;
  /** Default value */
  defaultValue?: any;
  /** Dependencies */
  dependencies?: FormItemDependencies;
  /** Description */
  description?: CustomRenderType;
  /** Field name */
  fieldName: string;
  /** Help information */
  help?: CustomRenderType;
  /** Whether to hide the form item */
  hide?: boolean;
  /** Form items */
  label?: CustomRenderType;
  // Custom component internal rendering
  renderComponentContent?: RenderComponentContentType;
  /** Field rules */
  rules?: FormSchemaRuleType;
  /** Suffix */
  suffix?: CustomRenderType;
}

export interface FormFieldProps extends FormSchema {
  required?: boolean;
}

export interface FormRenderProps<
  T extends BaseFormComponentType = BaseFormComponentType,
> {
  /**
   * Form field array mapping string configuration uses "," by default
   */
  arrayToStringFields?: ArrayToStringFields;
  /**
   * Whether to fold or not, takes effect when showCollapseButton=true
   * true: fold false: expand
   */
  collapsed?: boolean;
  /**
   * Keep the number of rows when folding
   * @default 1
   */
  collapsedRows?: number;
  /**
   * Whether to trigger the resize event
   * @default false
   */
  collapseTriggerResize?: boolean;
  /**
   * General backup configuration for form items. When the sub-project is not configured, the configuration here is used. The sub-project configuration has a higher priority than this configuration.
   */
  commonConfig?: FormCommonConfig;
  /**
   * Compact mode (remove the space reserved for verification information at the bottom of each form item)
   */
  compact?: boolean;
  /**
   * Component v-model event binding
   */
  componentBindEventMap?: Partial<Record<BaseFormComponentType, string>>;
  /**
   * Component collection
   */
  componentMap: Record<BaseFormComponentType, Component>;
  /**
   *Map form fields to time format
   */
  fieldMappingTime?: FieldMappingTime;
  /**
   * Form example
   */
  form?: FormContext<GenericObject>;
  /**
   * Form item layout
   */
  layout?: FormLayout;
  /**
   * form definition
   */
  schema?: FormSchema<T>[];

  /**
   * Whether to display expand/collapse
   */
  showCollapseButton?: boolean;
  /**
   * Format date
   */

  /**
   * Form grid layout
   * @default "grid-cols-1"
   */
  wrapperClass?: WrapperClassType;
}

export interface ActionButtonOptions extends VbenButtonProps {
  [key: string]: any;
  content?: MaybeComputedRef<string>;
  show?: boolean;
}

export interface VbenFormProps<
  T extends BaseFormComponentType = BaseFormComponentType,
> extends Omit<
  FormRenderProps<T>,
  'componentBindEventMap' | 'componentMap' | 'form'
> {
  /**
   * Whether the operation button is reversed (submit button in front)
   */
  actionButtonsReverse?: boolean;
  /**
   * Style of operation button group
   * newLine: Display in a new line. rowEnd: Display within the row, aligned to the right (default). inline: use grid default style
   */
  actionLayout?: 'inline' | 'newLine' | 'rowEnd';
  /**
   * The display position of the operation button group is displayed on the right by default
   */
  actionPosition?: 'center' | 'left' | 'right';
  /**
   * Form operation area class
   */
  actionWrapperClass?: ClassType;
  /**
   * Form field array mapping string configuration uses "," by default
   */
  arrayToStringFields?: ArrayToStringFields;

  /**
   * Form field mapping
   */
  fieldMappingTime?: FieldMappingTime;
  /**
   * Form collapse and expansion state change callback
   */
  handleCollapsedChange?: (collapsed: boolean) => void;
  /**
   * Form reset callback
   */
  handleReset?: HandleResetFn;
  /**
   * Form submission callback
   */
  handleSubmit?: HandleSubmitFn;
  /**
   * Form value change callback
   */
  handleValuesChange?: (
    values: Record<string, any>,
    fieldsChanged: string[],
  ) => void;
  /**
   *Reset button parameters
   */
  resetButtonOptions?: ActionButtonOptions;

  /**
   * Whether to automatically scroll to the first error field when verification fails
   * @default false
   */
  scrollToFirstError?: boolean;

  /**
   * Whether to display the default operation button
   * @default true
   */
  showDefaultActions?: boolean;

  /**
   *Submit button parameters
   */
  submitButtonOptions?: ActionButtonOptions;

  /**
   * Whether to submit the form when the field value changes
   * @default false
   */
  submitOnChange?: boolean;

  /**
   * Whether to submit the form when pressing Enter
   * @default false
   */
  submitOnEnter?: boolean;
}

export type ExtendedFormApi = FormApi & {
  useStore: <T = NoInfer<VbenFormProps>>(
    selector?: (state: NoInfer<VbenFormProps>) => T,
  ) => Readonly<Ref<T>>;
};

export interface VbenFormAdapterOptions<
  T extends BaseFormComponentType = BaseFormComponentType,
> {
  config?: {
    baseModelPropName?: string;
    disabledOnChangeListener?: boolean;
    disabledOnInputListener?: boolean;
    emptyStateValue?: null | undefined;
    modelPropNameMap?: Partial<Record<T, string>>;
  };
  defineRules?: {
    required?: (
      value: any,
      params: any,
      ctx: Record<string, any>,
    ) => boolean | string;
    selectRequired?: (
      value: any,
      params: any,
      ctx: Record<string, any>,
    ) => boolean | string;
  };
}
