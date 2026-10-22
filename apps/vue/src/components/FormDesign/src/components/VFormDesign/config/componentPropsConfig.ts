import { IBaseFormAttrs } from './formItemPropsConfig';

interface IBaseComponentProps {
  [key: string]: IBaseFormAttrs[];
}

type BaseFormAttrs = Omit<IBaseFormAttrs, 'tag'>;

export const baseComponentControlAttrs: Omit<IBaseFormAttrs, 'tag'>[] = [
  {
    // Controls without disabled attributes cannot be used as form controls.
    name: 'disabled',
    label: 'disabled',
  },
  {
    // Controls without disabled attributes cannot be used as form controls.
    name: 'autofocus',
    label: 'Automatically get focus',
    includes: [
      'Input',
      'Select',
      'ApiSelect',
      'InputTextArea',
      'InputNumber',
      'DatePicker',
      'RangePicker',
      'MonthPicker',
      'TimePicker',
      'Cascader',
      'TreeSelect',
      'ApiTreeSelect',
      'Switch',
      'AutoComplete',
      'Slider',
    ],
  },
  {
    name: 'allowClear',
    label: 'clearable',
    includes: [
      'Input',
      'Select',
      'ApiSelect',
      'InputTextArea',
      'InputNumber',
      'DatePicker',
      'RangePicker',
      'MonthPicker',
      'TimePicker',
      'Cascader',
      'TreeSelect',
      'ApiTreeSelect',
      'AutoComplete',
    ],
  },
  { name: 'fullscreen', label: 'fullscreen', includes: ['Calendar'] },
  {
    name: 'showSearch',
    label: 'Searchable',
    includes: ['Select', 'ApiSelect', 'TreeSelect', 'ApiTreeSelect', 'Cascader', 'Transfer'],
  },
  {
    name: 'showTime',
    label: 'display time',
    includes: ['DatePicker', 'RangePicker', 'MonthPicker'],
  },
  {
    name: 'range',
    label: 'two-way sliding',
    includes: [],
  },
  {
    name: 'allowHalf',
    label: 'Allow half selection',
    includes: ['Rate'],
  },
  {
    name: 'multiple',
    label: 'Multiple selection',
    includes: ['Select', 'ApiSelect', 'TreeSelect', 'ApiTreeSelect', 'Upload'],
  },
  {
    name: 'directory',
    label: 'folder',
    includes: ['Upload'],
  },
  {
    name: 'withCredentials',
    label: 'carry cookies',
    includes: ['Upload'],
  },
  {
    name: 'bordered',
    label: 'Show border',
    includes: ['Select', 'ApiSelect', 'Input'],
  },
  {
    name: 'defaultActiveFirstOption',
    label: 'Highlight the first option',
    component: 'Checkbox',
    includes: ['Select', 'ApiSelect', 'AutoComplete'],
  },
  {
    name: 'dropdownMatchSelectWidth',
    label: 'The drop-down menu is as wide as the selector',
    component: 'Checkbox',
    includes: ['Select', 'ApiSelect', 'TreeSelect', 'ApiTreeSelect', 'AutoComplete'],
  },
];

// Shared attributes
export const baseComponentCommonAttrs: Omit<IBaseFormAttrs, 'tag'>[] = [
  {
    name: 'size',
    label: 'size',
    component: 'RadioGroup',
    componentProps: {
      options: [
        {
          label: 'default',
          value: 'default',
        },
        {
          label: 'big',
          value: 'large',
        },
        {
          label: 'small',
          value: 'small',
        },
      ],
    },
    includes: ['InputNumber', 'Input', 'Cascader', 'Button'],
  },
  {
    name: 'placeholder',
    label: 'placeholder',
    component: 'Input',
    componentProps: {
      placeholder: 'Please enter placeholder',
    },
    includes: [
      'AutoComplete',
      'InputTextArea',
      'InputNumber',
      'Input',
      'InputTextArea',
      'Select',
      'ApiSelect', 
      'DatePicker',
      'MonthPicker',
      'TimePicker',
      'TreeSelect',
      'ApiTreeSelect', 
      'Cascader',
    ],
  },
  {
    name: 'style',
    label: 'style',
    component: 'Input',
    componentProps: {
      placeholder: 'Please enter the style',
    },
  },
  {
    name: 'open',
    label: 'Always expand the drop-down menu',
    component: 'RadioGroup',
    componentProps: {
      options: [
        {
          label: 'default',
          value: undefined,
        },
        {
          label: 'Yes',
          value: true,
        },
        {
          label: 'No',
          value: false,
        },
      ],
    },
    includes: ['Select', 'ApiSelect', 'AutoComplete'],
  },
];

const componentAttrs: IBaseComponentProps = {
  AutoComplete: [
    {
      name: 'backfill',
      label: 'Automatic backfill',
      component: 'Switch',
      componentProps: {
        span: 8,
      },
    },
    {
      name: 'defaultOpen',
      label: 'Expand the drop-down menu by default',
      component: 'Checkbox',
    },
  ],
  IconPicker: [
    {
      name: 'mode',
      label: 'mode',
      component: 'RadioGroup',
      componentProps: {
        options: [
          { label: 'ICONIFY', value: null },
          { label: 'SVG', value: 'svg' },
          // { label: 'combination', value: 'combobox' },
        ],
      },
    },
  ],

  // https://developer.mozilla.org/zh-CN/docs/Web/HTML/Element/input#%3Cinput%3E_types
  Input: [
    {
      name: 'type',
      label: 'type',
      component: 'Select',
      componentProps: {
        options: [
          { value: 'text', label: 'text' },
          { value: 'search', label: 'search' },
          { value: 'number', label: 'number' },
          { value: 'range', label: 'numeric range' },
          { value: 'password', label: 'password' },
          { value: 'date', label: 'date' },
          { value: 'datetime-local', label: 'Date-no time zone' },
          { value: 'time', label: 'time' },
          { value: 'month', label: 'year and month' },
          { value: 'week', label: 'week' },
          { value: 'email', label: 'mailbox' },
          { value: 'url', label: 'URL' },
          { value: 'tel', label: 'phone number' },
          { value: 'file', label: 'file' },
          { value: 'button', label: 'button' },
          { value: 'submit', label: 'submit button' },
          { value: 'reset', label: 'reset button' },
          { value: 'radio', label: 'radio button' },
          { value: 'checkbox', label: 'checkbox' },
          { value: 'color', label: 'color' },
          { value: 'image', label: 'image' },
          { value: 'hidden', label: 'hidden' },
        ],
      },
    },
    {
      name: 'defaultValue',
      label: 'default value',
      component: 'Input',
      componentProps: {
        type: 'text',
        placeholder: 'Please enter the default value',
      },
    },
    {
      name: 'prefix',
      label: 'prefix',
      component: 'Input',
      componentProps: {
        type: 'text',
        placeholder: 'Please enter a prefix',
      },
    },
    {
      name: 'suffix',
      label: 'suffix',
      component: 'Input',
      componentProps: {
        type: 'text',
        placeholder: 'Please enter a suffix',
      },
    },
    {
      name: 'addonBefore',
      label: 'prefix label',
      component: 'Input',
      componentProps: {
        type: 'text',
        placeholder: 'Please enter the prefix tag',
      },
    },
    {
      name: 'addonAfter',
      label: 'Post label',
      component: 'Input',
      componentProps: {
        type: 'text',
        placeholder: 'Please enter the post label',
      },
    },
    {
      name: 'maxLength',
      label: 'maximum length',
      component: 'InputNumber',
      componentProps: {
        type: 'text',
        placeholder: 'Please enter the maximum length',
      },
    },
  ],

  InputNumber: [
    {
      name: 'defaultValue',
      label: 'default value',
      component: 'InputNumber',
      componentProps: {
        placeholder: 'Please enter the default value',
      },
    },
    {
      name: 'min',
      label: 'minimum value',
      component: 'InputNumber',
      componentProps: {
        type: 'text',
        placeholder: 'Please enter the minimum value',
      },
    },
    {
      name: 'max',
      label: 'maximum value',
      component: 'InputNumber',
      componentProps: {
        type: 'text',
        placeholder: 'Please enter the maximum value',
      },
    },
    {
      name: 'precision',
      label: 'Numerical precision',
      component: 'InputNumber',
      componentProps: {
        type: 'text',
        placeholder: 'Please enter the maximum value',
      },
    },
    {
      name: 'step',
      label: 'step size',
      component: 'InputNumber',
      componentProps: {
        type: 'text',
        placeholder: 'Please enter the step size',
      },
    },
    {
      name: 'decimalSeparator',
      label: 'decimal point',
      component: 'Input',
      componentProps: { type: 'text', placeholder: 'Please enter a decimal point' },
    },
    {
      name: 'addonBefore',
      label: 'prefix label',
      component: 'Input',
      componentProps: {
        type: 'text',
        placeholder: 'Please enter the prefix tag',
      },
    },
    {
      name: 'addonAfter',
      label: 'Post label',
      component: 'Input',
      componentProps: {
        type: 'text',
        placeholder: 'Please enter the post label',
      },
    },
    {
      name: 'controls',
      label: 'Show increase/decrease buttons',
      component: 'Checkbox',
    },
    {
      name: 'keyboard',
      label: 'Enable keyboard shortcuts',
      component: 'Checkbox',
    },
    {
      name: 'stringMode',
      label: 'Character value mode',
      component: 'Checkbox',
    },
    {
      name: 'bordered',
      label: 'Show border',
      component: 'Checkbox',
    },
  ],
  InputTextArea: [
    {
      name: 'defaultValue',
      label: 'default value',
      component: 'Input',
      componentProps: {
        type: 'text',
        placeholder: 'Please enter the default value',
      },
    },
    {
      name: 'maxlength',
      label: 'maximum length',
      component: 'InputNumber',
      componentProps: {
        placeholder: 'Please enter the maximum length',
      },
    },
    {
      name: 'minlength',
      label: 'minimum length',
      component: 'InputNumber',
      componentProps: {
        placeholder: 'Please enter the minimum length',
      },
    },
    {
      name: 'cols',
      label: 'Number of visible columns',
      component: 'InputNumber',
      componentProps: {
        placeholder: 'Please enter the number of visible columns',
        min: 0,
      },
    },
    {
      name: 'rows',
      label: 'Number of visible lines',
      component: 'InputNumber',
      componentProps: {
        placeholder: 'Please enter the number of visible rows',
        min: 0,
      },
    },
    {
      name: 'minlength',
      label: 'minimum length',
      component: 'InputNumber',
      componentProps: {
        placeholder: 'Please enter the minimum length',
      },
    },
    {
      name: 'autosize',
      label: 'Adaptive content height',
      component: 'Checkbox',
    },
    {
      name: 'showCount',
      label: 'Show word count',
      component: 'Checkbox',
    },
    {
      name: 'readonly',
      label: 'Read-only',
      component: 'Checkbox',
    },
    {
      name: 'spellcheck',
      label: 'read-write check',
      component: 'Checkbox',
    },
    {
      name: 'autocomplete',
      label: 'Auto-complete',
      component: 'RadioGroup',
      componentProps: {
        options: [
          { label: 'normal', value: null },
          { label: 'on', value: 'on' },
          { label: 'off', value: 'off' },
        ],
      },
    },
    {
      name: 'autocorrect',
      label: 'Auto-correct errors',
      component: 'RadioGroup',
      componentProps: {
        options: [
          { label: 'normal', value: null },
          { label: 'on', value: 'on' },
          { label: 'off', value: 'off' },
        ],
      },
    },
  ],
  Select: [
    {
      name: 'mode',
      label: 'Selection mode (default radio selection)',
      component: 'RadioGroup',
      componentProps: {
        options: [
          { label: 'radio choice', value: null },
          { label: 'multiple', value: 'multiple' },
          { label: 'label', value: 'tags' },
          // { label: 'combination', value: 'combobox' },
        ],
      },
    },
    {
      name: 'autoClearSearchValue',
      label: 'Clear the search box after selection',
      component: 'Checkbox',
    },
    {
      name: 'labelInValue',
      label: 'The label of the option is packed into the value',
      component: 'Checkbox',
    },
    {
      name: 'showArrow',
      label: 'Show drop-down arrow',
      component: 'Checkbox',
    },
    {
      name: 'defaultOpen',
      label: 'Default expansion drop-down menu',
      component: 'Checkbox',
    },
  ],
  Checkbox: [
    {
      name: 'indeterminate',
      label: 'Set indeterminate state',
      component: 'Checkbox',
    },
  ],
  CheckboxGroup: [],
  RadioGroup: [
    {
      name: 'defaultValue',
      label: 'default value',
      component: 'Input',
      componentProps: {
        placeholder: 'Please enter the default value',
      },
    },
    {
      name: 'buttonStyle',
      label: 'RadioButton style',
      component: 'RadioGroup',
      componentProps: {
        options: [
          {
            label: 'outline',
            value: 'outline',
          },
          {
            label: 'solid',
            value: 'solid',
          },
        ],
      },
    },
    {
      name: 'optionType',
      label: 'options type',
      component: 'RadioGroup',
      componentProps: {
        options: [
          {
            label: 'default',
            value: 'default',
          },
          {
            label: 'button',
            value: 'button',
          },
        ],
        // Update its own control configuration value based on the values ​​of other options
        // compProp properties of the current component,
        // configProps, when and all configuration options of the component
        // self, currently configured componentProps attribute
        // Return true value for update
        // _propsFunc: (compProp, configProps, self) => {
        //   console.log("i'm called");
        //   console.log(compProp, configProps, self);
        //   if (compProp['buttonStyle'] && compProp['buttonStyle'] == 'outline') {
        //     if (!self['disabled']) {
        //       self['disabled'] = true;
        //       return 1;
        //     }
        //   } else {
        //     if (self['disabled']) {
        //       self['disabled'] = false;
        //       return 1;
        //     }
        //   }

        //   // return prop.optionType == 'button';
        // },
      },
    },
    {
      name: 'size',
      label: 'size',
      component: 'RadioGroup',
      componentProps: {
        options: [
          {
            label: 'default',
            value: 'default',
          },
          {
            label: 'big',
            value: 'large',
          },
          {
            label: 'small',
            value: 'small',
          },
        ],
      },
    },
  ],
  DatePicker: [
    {
      name: 'format',
      label: 'Display format (format)',
      component: 'Input',
      componentProps: {
        placeholder: 'YYYY-MM-DD',
      },
    },
    {
      name: 'valueFormat',
      label: 'Bind value format (valueFormat)',
      component: 'Input',
      componentProps: {
        placeholder: 'YYYY-MM-DD',
      },
    },
  ],
  RangePicker: [
    {
      name: 'placeholder',
      label: 'placeholder',
      children: [
        {
          name: '',
          label: '',
          component: 'Input',
        },
        {
          name: '',
          label: '',
          component: 'Input',
        },
      ],
    },
    {
      name: 'format',
      label: 'Display format (format)',
      component: 'Input',
      componentProps: {
        placeholder: 'YYYY-MM-DD HH:mm:ss',
      },
    },
    {
      name: 'valueFormat',
      label: 'Bind value format (valueFormat)',
      component: 'Input',
      componentProps: {
        placeholder: 'YYYY-MM-DD',
      },
    },
  ],
  MonthPicker: [
    {
      name: 'format',
      label: 'Display format (format)',
      component: 'Input',
      componentProps: {
        placeholder: 'YYYY-MM',
      },
    },
    {
      name: 'valueFormat',
      label: 'Bind value format (valueFormat)',
      component: 'Input',
      componentProps: {
        placeholder: 'YYYY-MM',
      },
    },
  ],
  TimePicker: [
    {
      name: 'format',
      label: 'Display format (format)',
      component: 'Input',
      componentProps: {
        placeholder: 'YYYY-MM',
      },
    },
    {
      name: 'valueFormat',
      label: 'Bind value format (valueFormat)',
      component: 'Input',
      componentProps: {
        placeholder: 'YYYY-MM',
      },
    },
  ],
  Slider: [
    {
      name: 'defaultValue',
      label: 'default value',
      component: 'InputNumber',
      componentProps: {
        placeholder: 'Please enter the default value',
      },
    },
    {
      name: 'min',
      label: 'minimum value',
      component: 'InputNumber',
      componentProps: {
        placeholder: 'Please enter the minimum value',
      },
    },
    {
      name: 'max',
      label: 'maximum value',
      component: 'InputNumber',
      componentProps: {
        placeholder: 'Please enter the maximum value',
      },
    },
    {
      name: 'step',
      label: 'step size',
      component: 'InputNumber',
      componentProps: {
        placeholder: 'Please enter the step size',
      },
    },
    {
      name: 'tooltipPlacement',
      label: 'Tooltip placement',
      component: 'Select',
      componentProps: {
        options: [
          { value: 'top', label: 'Top' },
          { value: 'left', label: 'left' },
          { value: 'right', label: 'right' },
          { value: 'bottom', label: 'Bottom' },
          { value: 'topLeft', label: 'top right' },
          { value: 'topRight', label: 'top left' },
          { value: 'bottomLeft', label: 'bottom right' },
          { value: 'bottomRight', label: 'bottom left' },
          { value: 'leftTop', label: 'bottom left' },
          { value: 'leftBottom', label: 'upper left' },
          { value: 'rightTop', label: 'lower right' },
          { value: 'rightBottom', label: 'upper right' },
        ],
      },
    },
    {
      name: 'tooltipVisible',
      label: 'Always show Tooltip',
      component: 'Checkbox',
    },
    {
      name: 'dots',
      label: 'Can only be dragged to the scale',
      component: 'Checkbox',
    },
    {
      name: 'range',
      label: 'Double slider mode',
      component: 'Checkbox',
    },
    {
      name: 'reverse',
      label: 'reverse axis',
      component: 'Checkbox',
    },
    {
      name: 'vertical',
      label: 'vertical direction',
      component: 'Checkbox',
    },
    {
      name: 'included',
      label: 'The value is an inclusion relationship',
      component: 'Checkbox',
    },
  ],
  Rate: [
    {
      name: 'defaultValue',
      label: 'default value',
      component: 'InputNumber',
      componentProps: {
        placeholder: 'Please enter the default value',
      },
    },
    {
      name: 'character',
      label: 'custom character',
      component: 'Input',
      componentProps: {
        placeholder: 'Please enter custom characters',
      },
    },
    {
      name: 'count',
      label: 'start total',
      component: 'InputNumber',
      componentProps: {
        placeholder: 'Please enter custom characters',
      },
    },
  ],
  Switch: [
    {
      name: 'checkedChildren',
      label: 'Content when selected',
      component: 'Input',
      componentProps: {
        placeholder: 'Please enter the selected content',
      },
    },
    {
      name: 'checkedValue',
      label: 'value when selected',
      component: 'Input',
      componentProps: {
        placeholder: 'Please enter the selected value',
      },
    },
    {
      name: 'unCheckedChildren',
      label: 'Content when not selected',
      component: 'Input',
      componentProps: {
        placeholder: 'Please enter the unselected content',
      },
    },
    {
      name: 'unCheckedValue',
      label: 'Value when not selected',
      component: 'Input',
      componentProps: {
        placeholder: 'Please enter the unselected value',
      },
    },
    {
      name: 'loading',
      label: 'Loading switch',
      component: 'Checkbox',
    },
    {
      name: 'size',
      label: 'size',
      component: 'RadioGroup',
      componentProps: {
        options: [
          {
            label: 'default',
            value: 'default',
          },
          {
            label: 'small',
            value: 'small',
          },
        ],
      },
    },
  ],
  TreeSelect: [
    {
      name: 'defaultValue',
      label: 'default value',
      component: 'Input',
      componentProps: {
        placeholder: 'Please enter the default value',
      },
    },
    {
      name: 'searchPlaceholder',
      label: 'Search box default text',
      component: 'Input',
      componentProps: {
        placeholder: 'Please enter the default text in the search box',
      },
    },
    {
      name: 'treeNodeFilterProp',
      label: 'The treeNode attribute corresponding to input filtering',
      component: 'Input',
      componentProps: {
        defaultValue: 'value',
      },
    },
    {
      name: 'treeNodeLabelProp',
      label: 'As a displayed prop setting',
      component: 'Input',
      componentProps: {
        defaultValue: 'title',
      },
    },
    {
      name: 'dropdownClassName',
      label: 'className attribute of drop-down menu',
      component: 'Input',
      componentProps: {
        placeholder: 'Please enter the className attribute of the drop-down menu',
      },
    },

    {
      name: 'labelInValue',
      label: 'The label of the option is packed into the value',
      component: 'Checkbox',
    },
    {
      name: 'treeIcon',
      label: 'Show the icon in front of the TreeNode title',
      component: 'Checkbox',
    },
    {
      name: 'treeCheckable',
      label: 'Options can be checked',
      component: 'Checkbox',
    },
    {
      name: 'treeCheckStrictly',
      label: 'Node selection is fully controlled',
      component: 'Checkbox',
    },
    {
      name: 'treeDefaultExpandAll',
      label: 'Expand all by default',
      component: 'Checkbox',
    },
    {
      name: 'treeLine',
      label: 'Show line style',
      component: 'Checkbox',
    },
    {
      name: 'maxTagCount',
      label: 'How many tags can be displayed at most',
      component: 'InputNumber',
      componentProps: {
        placeholder: 'How many tags can be displayed at most',
      },
    },
    {
      name: 'size',
      label: 'size',
      component: 'RadioGroup',
      componentProps: {
        options: [
          {
            label: 'default',
            value: 'default',
          },
          {
            label: 'small',
            value: 'small',
          },
        ],
      },
    },
  ],
  Cascader: [
    {
      name: 'expandTrigger',
      label: 'Secondary expansion mode (default click)',
      component: 'RadioGroup',
      componentProps: {
        options: [
          {
            label: 'click',
            value: 'click',
          },
          {
            label: 'hover',
            value: 'hover',
          },
        ],
      },
    },
  ],
  Button: [
    {
      name: 'type',
      label: 'type',
      component: 'RadioGroup',
      componentProps: {
        options: [
          {
            label: 'default',
            value: 'default',
          },
          {
            label: 'primary',
            value: 'primary',
          },
          {
            label: 'danger',
            value: 'danger',
          },
          {
            label: 'dashed',
            value: 'dashed',
          },
        ],
      },
    },
    {
      name: 'handle',
      label: 'operation',
      component: 'RadioGroup',
      componentProps: {
        options: [
          {
            label: 'submit',
            value: 'submit',
          },
          {
            label: 'reset',
            value: 'reset',
          },
        ],
      },
    },
  ],
  Upload: [
    {
      name: 'action',
      label: 'upload address',
      component: 'Input',
    },
    {
      name: 'name',
      label: 'Attachment parameter name (name)',
      component: 'Input',
    },
  ],
  // ColorPicker: [
  //   {
  //     name: 'defaultValue',
  // label: 'default value',
  //     component: 'AColorPicker',
  //   },
  // ],
  Avatar: [
    {
      name: 'alt',
      label: 'Alternative text when the image cannot be displayed',
      component: 'Input',
    },
    {
      name: 'crossOrigin',
      label: 'cors attribute setting',
      component: 'Select',
      componentProps: {
        options: [
          { value: 'anonymous', label: 'anonymous' },
          { value: 'use-credentials', label: 'Use credentials' },
          { value: '', label: 'None' },
        ],
      },
    },
    {
      name: 'draggable',
      label: 'Allow dragging the picture',
      component: 'Switch',
    },
    {
      name: 'gap',
      label: 'The distance between the character type and the left and right boundaries is unit pixels',
      component: 'InputNumber',
    },
    {
      name: 'icon',
      label: 'Set the icon type of the avatar',
      component: 'Input',
    },
    {
      name: 'shape',
      label: 'Specify the shape of the avatar',
      component: 'Select',
      componentProps: {
        options: [
          { value: 'circle', label: 'circle' },
          { value: 'square', label: 'square' },
        ],
        value: 'circle'
      },
    },
  ],
  Descriptions: [
    {
      name: 'title',
      label: 'The title of the description list, displayed at the top',
      component: 'Input',
    },
    {
      name: 'bordered',
      label: 'Show border',
      component: 'Switch',
    },
    {
      name: 'colon',
      label: 'Configure the default value of colon of Descriptions.Item',
      component: 'Switch',
      componentProps: {
        defaultChecked: true,
      }
    },
    {
      name: 'column',
      label: 'Number of DescriptionItems in a row',
      component: 'InputNumber',
    },
    {
      name: 'layout',
      label: 'Describe layout',
      component: 'Select',
      componentProps: {
        options: [
          { value: 'horizontal', label: 'horizontal' },
          { value: 'vertical', label: 'vertical' },
        ],
        value: 'horizontal'
      },
    },
  ],
  Table: [
    {
      name: 'Configuration description of table columns',
      label: 'data array',
    },
    {
      name: 'dataSource',
      label: 'data array',
    },
  ],
  slot: [
    {
      name: 'slotName',
      label: 'slot name',
      component: 'Input',
    },
  ],
  Transfer: [
    // {
    //   name: 'operations',
    // label: 'A collection of operation copywriting, in order from top to bottom',
    //   component: 'Input',
    //   componentProps: {
    //     type: 'text',
    //     // defaultValue: ['>', '<'],
    //   },
    // },
    // {
    //   name: 'titles',
    // label: 'Title collection, order from left to right',
    //   component: 'Input',
    //   componentProps: {
    //     type: 'text',
    //     // defaultValue: ['', ''],
    //   },
    // },
    {
      name: 'oneWay',
      label: 'Displayed in one-way style',
      component: 'Checkbox',
    },
    {
      name: 'pagination',
      label: 'Use paging style',
      component: 'Checkbox',
    },
    {
      name: 'showSelectAll',
      label: 'Show select all checkbox',
      component: 'Checkbox',
    },
  ],
};

function deleteProps(list: Omit<IBaseFormAttrs, 'tag'>[], key: string) {
  list.forEach((element, index) => {
    if (element.name == key) {
      list.splice(index, 1);
    }
  });
}

componentAttrs['StrengthMeter'] = componentAttrs['Input'];
componentAttrs['StrengthMeter'].push({
  name: 'visibilityToggle',
  label: 'Show switch button',
  component: 'Checkbox',
});

deleteProps(componentAttrs['StrengthMeter'], 'type');
deleteProps(componentAttrs['StrengthMeter'], 'prefix');
deleteProps(componentAttrs['StrengthMeter'], 'defaultValue');
deleteProps(componentAttrs['StrengthMeter'], 'suffix');
// Component properties
// name properties of the control
export const baseComponentAttrs: IBaseComponentProps = componentAttrs;

// Find required configuration items among all options
const findCompoentProps = (props, name) => {
  const idx = props.findIndex((value: BaseFormAttrs, _index) => {
    return value.name == name;
  });
  if (idx) {
    if (props[idx].componentProps) {
      return props[idx].componentProps;
    }
  }
};

// Update its own control configuration value based on the values ​​of other options
export const componentPropsFuncs = {
  RadioGroup: (compProp, options: BaseFormAttrs[]) => {
    const props = findCompoentProps(options, 'size');
    if (props) {
      if (compProp['optionType'] && compProp['optionType'] != 'button') {
        props['disabled'] = true;
        compProp['size'] = null;
      } else {
        props['disabled'] = false;
      }
    }
  },
};
