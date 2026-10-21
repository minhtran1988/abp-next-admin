---
outline: deep
---

# Vben Form form

The form components provided by the framework can be adapted to frameworks such as `Element Plus`, `Ant Design Vue`, and `Naive UI`.

> If there is no parameter description in the document, you can try to find it in the online examples

::: info is written in front

If you feel that the encapsulation of existing components is not ideal enough, or does not fully meet your needs, you can use native components directly, or encapsulate a suitable component yourself. The components provided by the framework are not binding. Whether to use them or not depends entirely on your needs and freedom.

:::

## Adapter

The underlying form uses [vee-validate](https://vee-validate.logaretm.com/v4/) for form validation, so you can use all the features of `vee-validate`. For different UI frameworks, we provide adapters to better adapt to different UI frameworks.

### Adapter Description

Each application has a different UI framework, so within the application's `src/adapter/form` and `src/adapter/component`, you can adapt components according to your own needs. The following is the adapter sample code of `Ant Design Vue`. You can view the instructions according to the comments:

::: details ant design vue form adapter

```ts
import type {
  VbenFormSchema as FormSchema,
  VbenFormProps,
} from '@vben/common-ui';

import type { ComponentType } from './component';

import { setupVbenForm, useVbenForm as useForm, z } from '@vben/common-ui';
import { $t } from '@vben/locales';

setupVbenForm<ComponentType>({
  config: {
    //The ant design vue component library defaults to v-model:value
    baseModelPropName: 'value',
    // Some components are v-model:checked or v-model:fileList
    modelPropNameMap: {
      Checkbox: 'checked',
      Radio: 'checked',
      Switch: 'checked',
      Upload: 'fileList',
    },
  },
  defineRules: {
    //International adaptation is required for input items
    required: (value, _params, ctx) => {
      if (value === undefined || value === null || value.length === 0) {
        return $t('ui.formRules.required', [ctx.label]);
      }
      return true;
    },
    // International adaptation is required for selected projects
    selectRequired: (value, _params, ctx) => {
      if (value === undefined || value === null) {
        return $t('ui.formRules.selectRequired', [ctx.label]);
      }
      return true;
    },
  },
});

const useVbenForm = useForm<ComponentType>;

export { useVbenForm, z };
export type VbenFormSchema = FormSchema<ComponentType>;
export type { VbenFormProps };
```

:::

::: details ant design vue component adapter

```ts
/**
 * Basic components commonly used by common components were originally placed inside adapter/form, which limited the scope of use. They are extracted here to facilitate use in other places.
 * Can be used for vben-form, vben-modal, vben-drawer and other components,
 */

import type { BaseFormComponentType } from '@vben/common-ui';

import type { Component, SetupContext } from 'vue';
import { h } from 'vue';

import { globalShareState, IconPicker } from '@vben/common-ui';
import { $t } from '@vben/locales';

const AutoComplete = defineAsyncComponent(
  () => import('ant-design-vue/es/auto-complete'),
);
const Button = defineAsyncComponent(() => import('ant-design-vue/es/button'));
const Checkbox = defineAsyncComponent(
  () => import('ant-design-vue/es/checkbox'),
);
const CheckboxGroup = defineAsyncComponent(() =>
  import('ant-design-vue/es/checkbox').then((res) => res.CheckboxGroup),
);
const DatePicker = defineAsyncComponent(
  () => import('ant-design-vue/es/date-picker'),
);
const Divider = defineAsyncComponent(() => import('ant-design-vue/es/divider'));
const Input = defineAsyncComponent(() => import('ant-design-vue/es/input'));
const InputNumber = defineAsyncComponent(
  () => import('ant-design-vue/es/input-number'),
);
const InputPassword = defineAsyncComponent(() =>
  import('ant-design-vue/es/input').then((res) => res.InputPassword),
);
const Mentions = defineAsyncComponent(
  () => import('ant-design-vue/es/mentions'),
);
const Radio = defineAsyncComponent(() => import('ant-design-vue/es/radio'));
const RadioGroup = defineAsyncComponent(() =>
  import('ant-design-vue/es/radio').then((res) => res.RadioGroup),
);
const RangePicker = defineAsyncComponent(() =>
  import('ant-design-vue/es/date-picker').then((res) => res.RangePicker),
);
const Rate = defineAsyncComponent(() => import('ant-design-vue/es/rate'));
const Select = defineAsyncComponent(() => import('ant-design-vue/es/select'));
const Space = defineAsyncComponent(() => import('ant-design-vue/es/space'));
const Switch = defineAsyncComponent(() => import('ant-design-vue/es/switch'));
const Textarea = defineAsyncComponent(() =>
  import('ant-design-vue/es/input').then((res) => res.Textarea),
);
const TimePicker = defineAsyncComponent(
  () => import('ant-design-vue/es/time-picker'),
);
const TreeSelect = defineAsyncComponent(
  () => import('ant-design-vue/es/tree-select'),
);
const Upload = defineAsyncComponent(() => import('ant-design-vue/es/upload'));


const withDefaultPlaceholder = <T extends Component>(
  component: T,
  type: 'input' | 'select',
) => {
  return (props: any, { attrs, slots }: Omit<SetupContext, 'expose'>) => {
    const placeholder = props?.placeholder || $t(`ui.placeholder.${type}`);
    return h(component, { ...props, ...attrs, placeholder }, slots);
  };
};

// Here you need to adapt according to the business component library. The components that need to be used need to be typed here.
export type ComponentType =
  | 'AutoComplete'
  | 'Checkbox'
  | 'CheckboxGroup'
  | 'DatePicker'
  | 'DefaultButton'
  | 'Divider'
  | 'Input'
  | 'InputNumber'
  | 'InputPassword'
  | 'Mentions'
  | 'PrimaryButton'
  | 'Radio'
  | 'RadioGroup'
  | 'RangePicker'
  | 'Rate'
  | 'Select'
  | 'Space'
  | 'Switch'
  | 'Textarea'
  | 'TimePicker'
  | 'TreeSelect'
  | 'Upload'
  | 'IconPicker';
  | BaseFormComponentType;

async function initComponentAdapter() {
  const components: Partial<Record<ComponentType, Component>> = {
    // If your component is relatively large, you can use asynchronous loading
    // Button: () =>
    // import('xxx').then((res) => res.Button),

    AutoComplete,
    Checkbox,
    CheckboxGroup,
    DatePicker,
    //Customize the default button
    DefaultButton: (props, { attrs, slots }) => {
      return h(Button, { ...props, attrs, type: 'default' }, slots);
    },
    Divider,
    IconPicker,
    Input: withDefaultPlaceholder(Input, 'input'),
    InputNumber: withDefaultPlaceholder(InputNumber, 'input'),
    InputPassword: withDefaultPlaceholder(InputPassword, 'input'),
    Mentions: withDefaultPlaceholder(Mentions, 'input'),
    //Customize the main button
    PrimaryButton: (props, { attrs, slots }) => {
      return h(Button, { ...props, attrs, type: 'primary' }, slots);
    },
    Radio,
    RadioGroup,
    RangePicker,
    Rate,
    Select: withDefaultPlaceholder(Select, 'select'),
    Space,
    Switch,
    Textarea: withDefaultPlaceholder(Textarea, 'input'),
    TimePicker,
    TreeSelect: withDefaultPlaceholder(TreeSelect, 'select'),
    Upload,
  };

  //Register the component into the global shared state
  globalShareState.setComponents(components);

  //Define message prompts in global shared state
  globalShareState.defineMessage({
    // Copy success message prompt
    copyPreferencesSuccess: (title, content) => {
      notification.success({
        description: content,
        message: title,
        placement: 'bottomRight',
      });
    },
  });
}

export { initComponentAdapter };
```

:::

## Basic usage

::: tip README

In the sample code below, there are some internationalization and theme color mismatch problems. These problems only appear in the document. These problems will not occur in actual use. They can be ignored and don't worry about it.

:::

Use `useVbenForm` to create the most basic form.

<DemoPreview dir="demos/vben-form/basic" />

## Query form

A query form is a special form used to query data. Query forms will not trigger form validation, only query events.

<DemoPreview dir="demos/vben-form/query" />

## Form validation

Form validation is a very important function and can be verified through the `rules` attribute.

<DemoPreview dir="demos/vben-form/rules" />

## Form linkage

Form linkage is a very common feature and can be linked through the `dependencies` attribute.

_Note_ You need to specify the `triggerFields` attribute of `dependencies` to set whose changes will trigger it, so that the form components can be linked correctly.

<DemoPreview dir="demos/vben-form/dynamic" />

## Custom components

If your business component library does not provide a component, you can encapsulate a component yourself and add it to the form.

<DemoPreview dir="demos/vben-form/custom" />

## Operation

Some common form operations.

<DemoPreview dir="demos/vben-form/api" />

## API

`useVbenForm` returns an array, the first element is the form component and the second element is the form method.

```vue
<script setup lang="ts">
import { useVbenForm } from '#/adapter/form';

// Form is a pop-up window component
// formApi is the pop-up window method
const [Form, formApi] = useVbenForm({
  //properties
  // event
});
</script>

<template>
  <Form />
</template>
```

### FormApi

The second parameter returned by useVbenForm is an object that contains some form methods.

| Method name | Description | Type | Version number |
| --- | --- | --- | --- |
| submitForm | Submit form | `(e:Event)=>Promise<Record<string,any>>` | - |
| validateAndSubmitForm | Submit and verify the form | `(e:Event)=>Promise<Record<string,any>>` | - |
| resetForm | Reset form | `()=>Promise<void>` | - |
| setValues | Set form values. By default, fields not defined in the schema will be filtered. Filtering can be turned off through the filterFields parameter | `(fields: Record<string, any>, filterFields?: boolean, shouldValidate?: boolean) => Promise<void>` | - |
| getValues | Get form values | `(fields:Record<string, any>,shouldValidate: boolean = false)=>Promise<void>` | - |
| validate | form validation | `()=>Promise<void>` | - |
| validateField | Validate the specified field | `(fieldName: string)=>Promise<ValidationResult<unknown>>` | - |
| isFieldValid | Check whether a field has passed validation | `(fieldName: string)=>Promise<boolean>` | - |
| resetValidate | Reset form validation | `()=>Promise<void>` | - |
| updateSchema | update formSchema | `(schema:FormSchema[])=>void` | - |
| setFieldValue | Set field value | `(field: string, value: any, shouldValidate?: boolean)=>Promise<void>` | - |
| setState | Set component state (props) | `(stateOrFn:\| ((prev: VbenFormProps) => Partial<VbenFormProps>)\| Partial<VbenFormProps>)=>Promise<void>` | - |
| getState | Get component state (props) | `()=>Promise<VbenFormProps>` | - |
| form | Form object instance, which can operate the form, see [useForm](https://vee-validate.logaretm.com/v4/api/use-form/) | - | - |
| getFieldComponentRef | Get the component instance of the specified field | `<T=unknown>(fieldName: string)=>T` | >5.5.3 |
| getFocusedField | Get the currently focused field | `()=>string\|undefined` | >5.5.3 |

## Props

All properties can be passed into the first parameter of `useVbenForm`.

| Property name | Description | Type | Default value |
| --- | --- | --- | --- |
| layout | form item layout | `'horizontal' \| 'vertical'\| 'inline'` | `horizontal` |
| showCollapseButton | Whether to display the collapse button | `boolean` | `false` |
| wrapperClass | Form layout, based on tailwindcss | `any` | - |
| actionWrapperClass | form operation area class | `any` | - |
| actionLayout | Form action button position | `'newLine' \| 'rowEnd' \| 'inline'` | `rowEnd` |
| actionPosition | Form action button alignment | `'left' \| 'center' \| 'right'` | `right` |
| handleReset | Form reset callback | `(values: Record<string, any>,) => Promise<void> \| void` | - |
| handleSubmit | form submission callback | `(values: Record<string, any>,) => Promise<void> \| void` | - |
| handleValuesChange | form value change callback | `(values: Record<string, any>, fieldsChanged: string[]) => void` | - |
| handleCollapsedChange | Form collapse and expansion state change callback | `(collapsed: boolean) => void` | - |
| actionButtonsReverse | Replace the action button position | `boolean` | `false` |
| resetButtonOptions | Reset button component parameters | `ActionButtonOptions` | - |
| submitButtonOptions | Submit button component parameters | `ActionButtonOptions` | - |
| showDefaultActions | Whether to display the default action button | `boolean` | `true` |
| collapsed | Whether to collapse, effective when `showCollapseButton` is `true` | `boolean` | `false` |
| collapseTriggerResize | When collapsing, trigger the `resize` event | `boolean` | `false` |
| collapsedRows | Number of rows to keep when folding | `number` | `1` |
| fieldMappingTime | Used to map array values in the form into 2 fields | `[string, [string, string],Nullable<string>\|[string,string]\|((any,string)=>any)?][]` | - |
| commonConfig | Common configuration of form items, each configuration will be passed to each form item, and form items can override | `FormCommonConfig` | - |
| schema | Each configuration of the form item | `FormSchema[]` | - |
| submitOnEnter | Submit the form when Enter is pressed | `boolean` | false |
| submitOnChange | Submit the form when the field value changes (internal anti-shake, this property is generally used for table search forms) | `boolean` | false |
| compact | Whether to compact mode (ignoring the space reserved for verification information) | `boolean` | false |
| scrollToFirstError | Whether to automatically scroll to the first error field when form validation fails | `boolean` | false |

::: tip handleValuesChange

The first parameter `values` of the `handleValuesChange` callback function loads the current value object after the form is changed, and the second parameter `fieldsChanged` is an array containing all changed field names. Note: The second parameter is only available in v5.5.4 (exclusive) and above, and passes the field name defined in the schema. If you are using field mapping and need to check which fields have changed, please note that this parameter does not contain the mapped field name.

:::

::: tip fieldMappingTime

This attribute is used to map the array value in the form into 2 fields. It should be passed in an array. Each item of the array is a mapping rule. The first member of the rule is a string, indicating the field name that needs to be mapped. The second member is an array, indicating the mapped field name. The third member is an optional format mask for formatting the date and time field. You can also provide a formatting function (the parameters are the current value and the current field name, and the formatted value is returned). If the format mask is explicitly set to null, the original value is mapped without formatting (applies to non-datetime fields). For example: `[['timeRange', ['startTime', 'endTime'], 'YYYY-MM-DD']]`, `timeRange` should be an array type value with at least 2 members. Form will format the first two values ​​of `timeRange` according to the format mask `YYYY-MM-DD` and map them to the `startTime` and `endTime` fields. The third parameter of each item is an optional format mask,

:::

### TS type description

::: details ActionButtonOptions

```ts
export interface ActionButtonOptions {
  /** style */
  class?: ClassType;
  /** Whether to disable */
  disabled?: boolean;
  /** Is loading */
  loading?: boolean;
  /** button size */
  size?: ButtonVariantSize;
  /** Button type */
  variant?: ButtonVariants;
  /** Whether to display */
  show?: boolean;
  /** Button text */
  content?: string;
  /** Any attribute */
  [key: string]: any;
}
```

:::

::: details FormCommonConfig

```ts
export interface FormCommonConfig {
  /**
   * Props for all form items
   */
  componentProps?: ComponentProps;
  /**
   * Control styles for all form items
   */
  controlClass?: string;
  /**
   * Display a colon after the label of the form item
   */
  colon?: boolean;
  /**
   * Disabled state of all form items
   * @default false
   */
  disabled?: boolean;
  /**
   * Control styles for all form items
   * @default {}
   */
  formFieldProps?: Partial<typeof Field>;
  /**
   * Grid layout for all form items
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
   * The model attribute names of all form items. When using a custom component, you can specify the component's model attribute name through this configuration. Components already registered in modelPropNameMap are not affected by this configuration
   * @default "modelValue"
   */
  modelPropName?: string;
  /**
   * Wrapper styles for all form items
   */
  wrapperClass?: string;
}
```

:::

::: details FormSchema

```ts
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
  description?: string;
  /** Field name, also used as the name of the custom slot */
  fieldName: string;
  /** Help information */
  help?: CustomRenderType;
  /** Whether to hide the form item */
  hide?: boolean;
  /** The label of the form (if it is a string, it will be used for the message prompt of the default required rule) */
  label?: CustomRenderType;
  /** Custom component internal rendering */
  renderComponentContent?: RenderComponentContentType;
  /** Field rules */
  rules?: FormSchemaRuleType;
  /** Suffix */
  suffix?: CustomRenderType;
}
```

:::

### Form linkage

Form linkage requires linkage through the `dependencies` attribute within the schema, allowing you to add dependencies between fields to control fields based on the values of other fields.

```ts
dependencies: {
  // Trigger field. The linkage will only be triggered when the values of these fields change.
  triggerFields: ['name'],
  // Dynamically determine whether the current field needs to be displayed. If it is not displayed, it will be destroyed directly.
  if(values,formApi){},
  // Dynamically determine whether the current field needs to be displayed. If not displayed, use css to hide it.
  show(values,formApi){},
  // Dynamically determine whether the current field needs to be disabled
  disabled(values,formApi){},
  // This function will be triggered when the field changes
  trigger(values,formApi){},
  // dynamic rules
  rules(values,formApi){},
  //Dynamic required
  required(values,formApi){},
  //Dynamic component parameters
  componentProps(values,formApi){},
}
```

### Form verification

Form validation needs to be configured through the `rules` attribute in the schema.

The value of rules can be a string (predefined verification rule name) or a zod schema.

#### Predefined verification rules

```ts
// Indicates that the field is required. By default, it will be internationalized according to the required adapter.
{
  rules: 'required';
}

// Indicates that the field is required. By default, it will be internationalized according to the adapter's required, used for drop-down selection and so on.
{
  rules: 'selectRequired';
}
```

#### zod

Rules also supports the schema of zod, which can perform more complex verification. For the use of zod, please see [zod documentation](https://zod.dev/).

```ts
import { z } from '#/adapter/form';

//Basic type
{
  rules: z.string().min(1, { message: 'Please enter a string' });
}

// Optional (can be undefined) and carries a default value. Note that zod's optional does not include the empty string''
{
  rules: z.string().default('default value').optional();
}

// Can be an empty string, undefined or an email address (two different uses)
{
  rules: z.union([z.string().email().optional(), z.literal('')]);
}

{
  rules: z.string().email().or(z.literal('')).optional();
}

//Complex verification
{
  z.string()
    .min(1, { message: 'Please enter' })
    .refine((value) => value === '123', {
      message: 'Value must be 123',
    });
}
```

## Slots

Custom content can be inserted into the form using the following slots

| slot name | description |
| ------------- | ------------------ |
| reset-before | Reset the previous position of the button |
| submit-before | The position before the submit button |
| expand-before | The position before the expand button |
| expand-after | The position after the expand button |

::: tip field slot

In addition to the above built-in slots, the `fieldName` of each field in the `schema` attribute can be used as a slot name. These field slots have a higher priority than the components defined by `component`. That is, when slots with the same name as `fieldName` are provided, the contents of these slots will be used as components of these fields, and the value of `component` will be ignored.

:::
