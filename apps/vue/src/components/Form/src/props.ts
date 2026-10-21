import type { FieldMapToTime, FormSchema, TabFormSchema } from './types/form';
import type { CSSProperties, PropType } from 'vue';
import type { ColEx } from './types';
import type { TableActionType } from '/@/components/Table';
import type { ButtonProps } from 'ant-design-vue/es/button/buttonTypes';
import type { RowProps } from 'ant-design-vue/lib/grid/Row';
import { propTypes } from '/@/utils/propTypes';

export const basicProps = {
  model: {
    type: Object as PropType<Recordable>,
    default: () => ({}),
  },
  // Label width fixed width
  labelWidth: {
    type: [Number, String] as PropType<number | string>,
    default: 0,
  },
  fieldMapToTime: {
    type: Array as PropType<FieldMapToTime>,
    default: () => [],
  },
  compact: propTypes.bool,
  // form configuration rules
  schemas: {
    type: Array as PropType<FormSchema[]>,
    default: () => [],
  },
  mergeDynamicData: {
    type: Object as PropType<Recordable>,
    default: null,
  },
  baseRowStyle: {
    type: Object as PropType<CSSProperties>,
  },
  baseColProps: {
    type: Object as PropType<Partial<ColEx>>,
  },
  autoSetPlaceHolder: propTypes.bool.def(true),
  // Whether to automatically submit when clicking Enter on the INPUT component
  autoSubmitOnEnter: propTypes.bool.def(false),
  submitOnReset: propTypes.bool,
  submitOnChange: propTypes.bool,
  size: propTypes.oneOf(['default', 'small', 'large']).def('default'),
  // disable form
  disabled: propTypes.bool,
  emptySpan: {
    type: [Number, Object] as PropType<number | Recordable>,
    default: 0,
  },
  // Whether to display the collapse expand button
  showAdvancedButton: propTypes.bool,
  // Conversion time
  transformDateFunc: {
    type: Function as PropType<Fn>,
    default: (date: any) => {
      return date?.format?.('YYYY-MM-DD HH:mm:ss') ?? date;
    },
  },
  rulesMessageJoinLabel: propTypes.bool.def(true),
  // Automatically collapse if more than 3 lines
  autoAdvancedLine: propTypes.number.def(3),
  // Number of rows not affected by folding
  alwaysShowLines: propTypes.number.def(1),

  // Whether to display the operation button
  showActionButtonGroup: propTypes.bool.def(true),
  // Operation column Col configuration
  actionColOptions: Object as PropType<Partial<ColEx>>,
  // Show reset button
  showResetButton: propTypes.bool.def(true),
  // Whether to focus on the first input box only works when the first form item is input.
  autoFocusFirstItem: propTypes.bool,
  // Reset button configuration
  resetButtonOptions: Object as PropType<Partial<ButtonProps>>,

  // Show confirmation button
  showSubmitButton: propTypes.bool.def(true),
  // Confirm button configuration
  submitButtonOptions: Object as PropType<Partial<ButtonProps>>,

  // Custom reset function
  resetFunc: Function as PropType<() => Promise<void>>,
  submitFunc: Function as PropType<() => Promise<void>>,

  // The following are the default props
  hideRequiredMark: propTypes.bool,

  labelCol: Object as PropType<Partial<ColEx>>,

  layout: propTypes.oneOf(['horizontal', 'vertical', 'inline']).def('horizontal'),
  tableAction: {
    type: Object as PropType<TableActionType>,
  },

  wrapperCol: Object as PropType<Partial<ColEx>>,

  colon: propTypes.bool,

  labelAlign: propTypes.string,

  rowProps: Object as PropType<RowProps>,
};

export const tabProps = {
  tab: propTypes.string,
  model: {
    type: Object as PropType<Recordable>,
    default: {},
  },
  // Label width fixed width
  labelWidth: {
    type: [Number, String] as PropType<number | string>,
    default: 0,
  },
  fieldMapToTime: {
    type: Array as PropType<FieldMapToTime>,
    default: () => [],
  },
  compact: propTypes.bool,
  // form configuration rules
  schemas: {
    type: Array as PropType<TabFormSchema[]>,
    default: () => [],
  },
  mergeDynamicData: {
    type: Object as PropType<Recordable>,
    default: null,
  },
  baseRowStyle: {
    type: Object as PropType<CSSProperties>,
  },
  baseColProps: {
    type: Object as PropType<Partial<ColEx>>,
  },
  autoSetPlaceHolder: propTypes.bool.def(true),
  // Whether to automatically submit when clicking Enter on the INPUT component
  autoSubmitOnEnter: propTypes.bool.def(false),
  submitOnReset: propTypes.bool,
  size: propTypes.oneOf(['default', 'small', 'large']).def('default'),
  // disable form
  disabled: propTypes.bool,
  emptySpan: {
    type: [Number, Object] as PropType<number>,
    default: 0,
  },
  // Whether to display the collapse expand button
  showAdvancedButton: propTypes.bool,
  // Conversion time
  transformDateFunc: {
    type: Function as PropType<Fn>,
    default: (date: any) => {
      return date._isAMomentObject ? date?.format('YYYY-MM-DD HH:mm:ss') : date;
    },
  },
  rulesMessageJoinLabel: propTypes.bool.def(true),
  // Automatically collapse if more than 3 lines
  autoAdvancedLine: propTypes.number.def(3),

  // Whether to display the operation button
  showActionButtonGroup: propTypes.bool.def(true),
  // Operation column Col configuration
  actionColOptions: Object as PropType<Partial<ColEx>>,
  // Show reset button
  showResetButton: propTypes.bool.def(true),
  // Whether to focus on the first input box only works when the first form item is input.
  autoFocusFirstItem: propTypes.bool,
  // Reset button configuration
  resetButtonOptions: Object as PropType<Partial<ButtonProps>>,

  // Show confirmation button
  showSubmitButton: propTypes.bool.def(true),
  // Confirm button configuration
  submitButtonOptions: Object as PropType<Partial<ButtonProps>>,

  // Custom reset function
  resetFunc: Function as PropType<() => Promise<void>>,
  submitFunc: Function as PropType<() => Promise<void>>,

  // The following are the default props
  hideRequiredMark: propTypes.bool,

  labelCol: Object as PropType<Partial<ColEx>>,

  layout: propTypes.oneOf(['horizontal', 'vertical', 'inline']).def('horizontal'),
  tableAction: {
    type: Object as PropType<TableActionType>,
  },

  wrapperCol: Object as PropType<Partial<ColEx>>,

  colon: propTypes.bool,

  labelAlign: propTypes.string,

  rowProps: Object as PropType<RowProps>,
};
