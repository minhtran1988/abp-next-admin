/**
 * @description: form configuration
 */
import { IVFormComponent } from '../typings/v-form-component';
import { isArray } from 'lodash-es';
import { customComponentMap as VbenCmp, add } from '/@/components/Form/src/componentMap';
import { ComponentType } from '/@/components/Form/src/types';

import { componentMap as Cmp } from '../components';
import { Component } from 'vue';

const componentMap = new Map<string, Component>();

// If there are other controls, they can be initialized here

// Register Ant control library
Cmp.forEach((value, key) => {
  componentMap.set(key, value);
  if (VbenCmp[key] == null) {
    add(key as ComponentType, value);
  }
});
// Register vben control library
VbenCmp.forEach((value, key) => {
  componentMap.set(key, value);
});

export { componentMap };

/**
 * Set up custom form controls
 * @param {IVFormComponent | IVFormComponent[]} config
 */
export function setFormDesignComponents(config: IVFormComponent | IVFormComponent[]) {
  if (isArray(config)) {
    config.forEach((item) => {
      const { componentInstance: component, ...rest } = item;
      componentMap[item.component] = component;
      customComponents.push(Object.assign({ props: {} }, rest));
    });
  } else {
    const { componentInstance: component, ...rest } = config;
    componentMap[config.component] = component;
    customComponents.push(Object.assign({ props: {} }, rest));
  }
}

// Custom controls set externally
export const customComponents: IVFormComponent[] = [];

// Left control list and initialized control properties
// props.slotName will generate a slot at the formitem level and bind the current record value
// Attribute props, type is object, cannot be undefined or null.
export const baseComponents: IVFormComponent[] = [
  {
    component: 'InputCountDown',
    label: 'Countdown input',
    icon: 'line-md:iconify2',
    colProps: { span: 24 },
    field: '',
    componentProps: {},
  },
  {
    component: 'IconPicker',
    label: 'icon selector',
    icon: 'line-md:iconify2',
    colProps: { span: 24 },
    field: '',
    componentProps: {},
  },
  {
    component: 'StrengthMeter',
    label: 'Password strength',
    icon: 'wpf:password1',
    colProps: { span: 24 },
    field: '',
    componentProps: {},
  },
  {
    component: 'AutoComplete',
    label: 'autocomplete',
    icon: 'wpf:password1',
    colProps: { span: 24 },
    field: '',
    componentProps: {
      placeholder: 'Please enter a regular expression',
      options: [
        {
          value: '/^(?:(?:\\+|00)86)?1[3-9]\\d{9}$/',
          label: 'Mobile phone number',
        },
        {
          value: '/^((ht|f)tps?:\\/\\/)?[\\w-]+(\\.[\\w-]+)+:\\d{1,5}\\/?$/',
          label: 'URL with port number',
        },
      ],
    },
  },
  {
    component: 'Divider',
    label: 'Separating line',
    icon: 'radix-icons:divider-horizontal',
    colProps: { span: 24 },
    field: '',
    componentProps: {
      orientation: 'center',
      dashed: true,
    },
  },
  {
    component: 'Checkbox',
    label: 'checkbox',
    icon: 'ant-design:check-circle-outlined',
    colProps: { span: 24 },
    field: '',
  },
  {
    component: 'CheckboxGroup',
    label: 'checkbox-group',
    icon: 'ant-design:check-circle-filled',
    field: '',
    colProps: { span: 24 },
    componentProps: {
      options: [
        {
          label: 'Option 1',
          value: '1',
        },
        {
          label: 'Option 2',
          value: '2',
        },
      ],
    },
  },
  {
    component: 'Input',
    label: 'input box',
    icon: 'bi:input-cursor-text',
    field: '',
    colProps: { span: 24 },
    componentProps: {
      type: 'text',
    },
  },
  {
    component: 'InputNumber',
    label: 'Number input box',
    icon: 'ant-design:field-number-outlined',
    field: '',
    colProps: { span: 24 },
    componentProps: { style: 'width:200px' },
  },
  {
    component: 'InputTextArea',
    label: 'text field',
    icon: 'ant-design:file-text-filled',
    field: '',
    colProps: { span: 24 },
    componentProps: {},
  },
  {
    component: 'Select',
    label: 'drop-down selection',
    icon: 'gg:select',
    field: '',
    colProps: { span: 24 },
    componentProps: {
      options: [
        {
          label: 'Option 1',
          value: '1',
        },
        {
          label: 'Option 2',
          value: '2',
        },
      ],
    },
  },

  {
    component: 'Radio',
    label: 'radio button',
    icon: 'ant-design:check-circle-outlined',
    field: '',
    colProps: { span: 24 },
    componentProps: {},
  },
  {
    component: 'RadioGroup',
    label: 'Radio button-group',
    icon: 'carbon:radio-button-checked',
    field: '',
    colProps: { span: 24 },
    componentProps: {
      options: [
        {
          label: 'Option 1',
          value: '1',
        },
        {
          label: 'Option 2',
          value: '2',
        },
      ],
    },
  },
  {
    component: 'DatePicker',
    label: 'Date selection',
    icon: 'healthicons:i-schedule-school-date-time-outline',
    field: '',
    colProps: { span: 24 },
    componentProps: {},
  },
  {
    component: 'RangePicker',
    label: 'date range',
    icon: 'healthicons:i-schedule-school-date-time-outline',
    field: '',
    colProps: { span: 24 },
    componentProps: {
      placeholder: ['start date', 'end date'],
    },
  },
  {
    component: 'MonthPicker',
    label: 'month selection',
    icon: 'healthicons:i-schedule-school-date-time-outline',
    field: '',
    colProps: { span: 24 },
    componentProps: {
      placeholder: 'Please select a month',
    },
  },
  {
    component: 'TimePicker',
    label: 'Time selection',
    icon: 'healthicons:i-schedule-school-date-time',
    field: '',
    colProps: { span: 24 },
    componentProps: {},
  },
  {
    component: 'Slider',
    label: 'Sliding input bar',
    icon: 'vaadin:slider',
    field: '',
    colProps: { span: 24 },
    componentProps: {},
  },
  {
    component: 'Rate',
    label: 'rating',
    icon: 'ic:outline-star-rate',
    field: '',
    colProps: { span: 24 },
    componentProps: {},
  },
  {
    component: 'Switch',
    label: 'switch',
    icon: 'entypo:switch',
    field: '',
    colProps: { span: 24 },
    componentProps: {},
  },
  {
    component: 'TreeSelect',
    label: 'Tree selection',
    icon: 'clarity:tree-view-line',
    field: '',
    colProps: { span: 24 },
    componentProps: {
      treeData: [
        {
          label: 'Option 1',
          value: '1',
          children: [
            {
              label: 'Option 3',
              value: '1-1',
            },
          ],
        },
        {
          label: 'Option 2',
          value: '2',
        },
      ],
    },
  },
  {
    component: 'Upload',
    label: 'upload',
    icon: 'ant-design:upload-outlined',
    field: '',
    colProps: { span: 24 },
    componentProps: {
      api: () => 1,
    },
  },
  {
    component: 'Cascader',
    label: 'cascading selection',
    icon: 'ant-design:check-outlined',
    field: '',
    colProps: { span: 24 },
    componentProps: {
      options: [
        {
          label: 'Option 1',
          value: '1',
          children: [
            {
              label: 'Option 3',
              value: '1-1',
            },
          ],
        },
        {
          label: 'Option 2',
          value: '2',
        },
      ],
    },
  },
  // {
  //   component: 'Button',
  // label: 'button',
  //   icon: 'dashicons:button',
  //   field: '',
  //   colProps: { span: 24 },
  //   hiddenLabel: true,
  //   componentProps: {},
  // },
  // {
  //   component: 'ColorPicker',
  // label: 'color picker',
  //   icon: 'carbon:color-palette',
  //   field: '',
  //   colProps: { span: 24 },
  //   componentProps: {
  //     defaultValue: '',
  //     value: '',
  //   },
  // },
  {
    component: 'Avatar',
    label: 'avatar',
    icon: 'radix-icons:avatar',
    field: '',
    colProps: { span: 24 },
    componentProps: {},
  },
  {
    component: 'Descriptions',
    label: 'Description list',
    icon: 'cil:description',
    field: '',
    colProps: { span: 24 },
    componentProps: {
      options: [
        {
          label: 'Option 1',
          value: '1',
        },
        {
          label: 'Option 2',
          value: '2',
        },
      ],
    },
  },
  {
    component: 'slot',
    label: 'slot',
    icon: 'vs:timeslot-question',
    field: '',
    colProps: { span: 24 },
    componentProps: {
      slotName: 'slotName',
    },
  },
];

// https://next.antdv.com/components/transfer-cn
const transferControl = {
  component: 'Transfer',
  label: 'shuttle box',
  icon: 'bx:bx-transfer-alt',
  field: '',
  colProps: { span: 24 },
  componentProps: {
    render: (item) => item.title,
    dataSource: [
      {
        key: 'key-1',
        title: 'Title 1',
        description: 'Description',
        disabled: false,
        chosen: true,
      },
      {
        key: 'key-2',
        title: 'title2',
        description: 'description2',
        disabled: true,
      },
      {
        key: 'key-3',
        title: 'Title 3',
        description: 'Description 3',
        disabled: false,
        chosen: true,
      },
    ],
  },
};

baseComponents.push(transferControl);

const tableControl = {
  component: 'Table',
  label: 'table',
  icon: 'bx:bx-table',
  field: '',
  colProps: { span: 24 },
  componentProps: {
    render: (item) => item.title,
    columns: [
      {
        align: 'left',
        title: 'Column 1',
        dataIndex: 'col1'
      },
      {
        align: 'left',
        title: 'Column 2',
        dataIndex: 'col2'
      },
      {
        align: 'left',
        title: 'Column 3',
        dataIndex: 'col3'
      },
    ],
    dataSource: [
      {
        col1: 'data 1-1',
        col2: 'data 1-2',
        col3: 'data 1-3',
      },
      {
        col1: 'data 2-1',
        col2: 'data 2-2',
        col3: 'data 2-3',
      },
      {
        col1: 'data 3-1',
        col2: 'data 3-2',
        col3: 'data 3-3',
      },
    ],
  },
}

baseComponents.push(tableControl);

export const layoutComponents: IVFormComponent[] = [
  {
    field: '',
    component: 'Grid',
    label: 'Grid layout',
    icon: 'icon-grid',
    componentProps: {},
    columns: [
      {
        span: 12,
        children: [],
      },
      {
        span: 12,
        children: [],
      },
    ],
    colProps: { span: 24 },
    options: {
      gutter: 0,
    },
  },
];