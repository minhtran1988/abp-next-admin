import { IAnyObject } from '../../../typings/base-type';
import { baseComponents, customComponents } from '../../../core/formItemConfig';

export const globalConfigState: { span: number } = {
  span: 24,
};
export interface IBaseFormAttrs {
  name: string; // field name
  label: string; //Field label
  component?: string; // Property control
  componentProps?: IAnyObject; // Properties passed to the control
  exclude?: string[]; // Controls that need to be excluded
  includes?: string[]; // Qualified components
  on?: IAnyObject;
  children?: IBaseFormAttrs[];
  category?: 'control' | 'input';
}

export interface IBaseFormItemControlAttrs extends IBaseFormAttrs {
  target?: 'props' | 'options'; // Bind to a target key under the object
}

export const baseItemColumnProps: IBaseFormAttrs[] = [
  {
    name: 'span',
    label: 'Raster number',
    component: 'Slider',
    on: {
      change(value: number) {
        globalConfigState.span = value;
      },
    },
    componentProps: {
      max: 24,
      min: 0,
      marks: { 12: '' },
    },
  },

  {
    name: 'offset',
    label: 'The number of intervals on the left side of the grid',
    component: 'Slider',
    componentProps: {
      max: 24,
      min: 0,
      marks: { 12: '' },
    },
  },
  {
    name: 'order',
    label: 'Grid order, valid in flex layout mode',
    component: 'Slider',
    componentProps: {
      max: 24,
      min: 0,
      marks: { 12: '' },
    },
  },
  {
    name: 'pull',
    label: 'Move the grid to the left by the number of cells',
    component: 'Slider',
    componentProps: {
      max: 24,
      min: 0,
      marks: { 12: '' },
    },
  },
  {
    name: 'push',
    label: 'Move the grid to the right by the number of cells',
    component: 'Slider',
    componentProps: {
      max: 24,
      min: 0,
      marks: { 12: '' },
    },
  },
  {
    name: 'xs',
    label: '<576px responsive grid',
    component: 'Slider',
    componentProps: {
      max: 24,
      min: 0,
      marks: { 12: '' },
    },
  },
  {
    name: 'sm',
    label: '≥576px responsive grid',
    component: 'Slider',
    componentProps: {
      max: 24,
      min: 0,
      marks: { 12: '' },
    },
  },
  {
    name: 'md',
    label: '≥768p responsive raster',
    component: 'Slider',

    componentProps: {
      max: 24,
      min: 0,
      marks: { 12: '' },
    },
  },
  {
    name: 'lg',
    label: '≥992px responsive grid',
    component: 'Slider',
    componentProps: {
      max: 24,
      min: 0,
      marks: { 12: '' },
    },
  },
  {
    name: 'xl',
    label: '≥1200px responsive grid',
    component: 'Slider',
    componentProps: {
      max: 24,
      min: 0,
      marks: { 12: '' },
    },
  },
  {
    name: 'xxl',
    label: '≥1600px responsive grid',
    component: 'Slider',
    componentProps: {
      max: 24,
      min: 0,
      marks: { 12: '' },
    },
  },
  {
    name: '≥2000px',
    label: '≥1600px responsive grid',
    component: 'Slider',
    componentProps: {
      max: 24,
      min: 0,
      marks: { 12: '' },
    },
  },
];

// Configuration items of the control property panel
export const advanceFormItemColProps: IBaseFormAttrs[] = [
  {
    name: 'labelCol',
    label: 'label col',
    component: 'Slider',
    componentProps: {
      max: 24,
      min: 0,
      marks: { 12: '' },
    },
    exclude: ['Grid'],
  },
  {
    name: 'wrapperCol',
    label: 'control-span',
    component: 'Slider',
    componentProps: {
      max: 24,
      min: 0,
      marks: { 12: '' },
    },
    exclude: ['Grid'],
  },
];
// Configuration items of the control property panel
export const baseFormItemProps: IBaseFormAttrs[] = [
  {
    // Dynamically switch the type of control
    name: 'component',
    label: 'Control-FormItem',
    component: 'Select',
    componentProps: {
      options: baseComponents
        .concat(customComponents)
        .map((item) => ({ value: item.component, label: item.label })),
    },
  },
  {
    name: 'label',
    label: 'label',
    component: 'Input',
    componentProps: {
      type: 'Input',
      placeholder: 'Please enter a label',
    },
    exclude: ['Grid'],
  },
  {
    name: 'field',
    label: 'Field ID',
    component: 'Input',
    componentProps: {
      type: 'InputTextArea',
      placeholder: 'Please enter the field ID',
    },
    exclude: ['Grid'],
  },
  {
    name: 'helpMessage',
    label: 'helpMessage',
    component: 'Input',
    componentProps: {
      placeholder: 'Please enter prompt information',
    },
    exclude: ['Grid'],
  },
];

// Configuration items of the control property panel
export const advanceFormItemProps: IBaseFormAttrs[] = [
  {
    name: 'labelAlign',
    label: 'label alignment',
    component: 'RadioGroup',
    componentProps: {
      options: [
        {
          label: 'Left',
          value: 'left',
        },
        {
          label: 'right',
          value: 'right',
        },
      ],
    },
    exclude: ['Grid'],
  },

  {
    name: 'help',
    label: 'help',
    component: 'Input',
    componentProps: {
      placeholder: 'Please enter prompt information',
    },
    exclude: ['Grid'],
  },
  {
    name: 'extra',
    label: 'Extra message',
    component: 'Input',
    componentProps: {
      type: 'InputTextArea',
      placeholder: 'Please enter additional information',
    },
    exclude: ['Grid'],
  },
  {
    name: 'validateTrigger',
    label: 'validateTrigger',
    component: 'Input',
    componentProps: {
      type: 'InputTextArea',
      placeholder: 'Please enter validateTrigger',
    },
    exclude: ['Grid'],
  },
  {
    name: 'validateStatus',
    label: 'Verification status',
    component: 'RadioGroup',
    componentProps: {
      options: [
        {
          label: 'default',
          value: '',
        },
        {
          label: 'success',
          value: 'success',
        },
        {
          label: 'warning',
          value: 'warning',
        },
        {
          label: 'error',
          value: 'error',
        },
        {
          label: 'Verifying',
          value: 'validating',
        },
      ],
    },
    exclude: ['Grid'],
  },
];

export const baseFormItemControlAttrs: IBaseFormItemControlAttrs[] = [
  {
    name: 'required',
    label: 'required',
    component: 'Checkbox',
    exclude: ['alert'],
  },
  {
    name: 'hidden',
    label: 'hide',
    component: 'Checkbox',
    exclude: ['alert'],
  },
  {
    name: 'hiddenLabel',
    component: 'Checkbox',
    exclude: ['Grid'],
    label: 'hide label',
  },
  {
    name: 'colon',
    label: 'Display colon after label',
    component: 'Checkbox',
    componentProps: {},
    exclude: ['Grid'],
  },
  {
    name: 'hasFeedback',
    label: 'Input feedback',
    component: 'Checkbox',
    componentProps: {},
    includes: ['Input'],
  },
  {
    name: 'autoLink',
    label: 'Auto-association',
    component: 'Checkbox',
    componentProps: {},
    includes: ['Input'],
  },
  {
    name: 'validateFirst',
    label: 'Verification error stopped',
    component: 'Checkbox',
    componentProps: {},
    includes: ['Input'],
  },
];
