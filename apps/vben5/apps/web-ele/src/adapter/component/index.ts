/**
 * Basic components commonly used by common components were originally placed inside adapter/form, which limited the scope of use. They are extracted here to facilitate use in other places.
 * Can be used for vben-form, vben-modal, vben-drawer and other components,
 */

import type { Component } from 'vue';

import type { BaseFormComponentType } from '@vben/common-ui';
import type { Recordable } from '@vben/types';

import {
  defineAsyncComponent,
  defineComponent,
  getCurrentInstance,
  h,
  ref,
} from 'vue';

import { ApiComponent, globalShareState, IconPicker } from '@vben/common-ui';
import { $t } from '@vben/locales';

import { ElNotification } from 'element-plus';

const ElButton = defineAsyncComponent(() =>
  Promise.all([
    import('element-plus/es/components/button/index'),
    import('element-plus/es/components/button/style/css'),
  ]).then(([res]) => res.ElButton),
);
const ElCheckbox = defineAsyncComponent(() =>
  Promise.all([
    import('element-plus/es/components/checkbox/index'),
    import('element-plus/es/components/checkbox/style/css'),
  ]).then(([res]) => res.ElCheckbox),
);
const ElCheckboxButton = defineAsyncComponent(() =>
  Promise.all([
    import('element-plus/es/components/checkbox/index'),
    import('element-plus/es/components/checkbox-button/style/css'),
  ]).then(([res]) => res.ElCheckboxButton),
);
const ElCheckboxGroup = defineAsyncComponent(() =>
  Promise.all([
    import('element-plus/es/components/checkbox/index'),
    import('element-plus/es/components/checkbox-group/style/css'),
  ]).then(([res]) => res.ElCheckboxGroup),
);
const ElDatePicker = defineAsyncComponent(() =>
  Promise.all([
    import('element-plus/es/components/date-picker/index'),
    import('element-plus/es/components/date-picker/style/css'),
  ]).then(([res]) => res.ElDatePicker),
);
const ElDivider = defineAsyncComponent(() =>
  Promise.all([
    import('element-plus/es/components/divider/index'),
    import('element-plus/es/components/divider/style/css'),
  ]).then(([res]) => res.ElDivider),
);
const ElInput = defineAsyncComponent(() =>
  Promise.all([
    import('element-plus/es/components/input/index'),
    import('element-plus/es/components/input/style/css'),
  ]).then(([res]) => res.ElInput),
);
const ElInputNumber = defineAsyncComponent(() =>
  Promise.all([
    import('element-plus/es/components/input-number/index'),
    import('element-plus/es/components/input-number/style/css'),
  ]).then(([res]) => res.ElInputNumber),
);
const ElRadio = defineAsyncComponent(() =>
  Promise.all([
    import('element-plus/es/components/radio/index'),
    import('element-plus/es/components/radio/style/css'),
  ]).then(([res]) => res.ElRadio),
);
const ElRadioButton = defineAsyncComponent(() =>
  Promise.all([
    import('element-plus/es/components/radio/index'),
    import('element-plus/es/components/radio-button/style/css'),
  ]).then(([res]) => res.ElRadioButton),
);
const ElRadioGroup = defineAsyncComponent(() =>
  Promise.all([
    import('element-plus/es/components/radio/index'),
    import('element-plus/es/components/radio-group/style/css'),
  ]).then(([res]) => res.ElRadioGroup),
);
const ElSelectV2 = defineAsyncComponent(() =>
  Promise.all([
    import('element-plus/es/components/select-v2/index'),
    import('element-plus/es/components/select-v2/style/css'),
  ]).then(([res]) => res.ElSelectV2),
);
const ElSpace = defineAsyncComponent(() =>
  Promise.all([
    import('element-plus/es/components/space/index'),
    import('element-plus/es/components/space/style/css'),
  ]).then(([res]) => res.ElSpace),
);
const ElSwitch = defineAsyncComponent(() =>
  Promise.all([
    import('element-plus/es/components/switch/index'),
    import('element-plus/es/components/switch/style/css'),
  ]).then(([res]) => res.ElSwitch),
);
const ElTimePicker = defineAsyncComponent(() =>
  Promise.all([
    import('element-plus/es/components/time-picker/index'),
    import('element-plus/es/components/time-picker/style/css'),
  ]).then(([res]) => res.ElTimePicker),
);
const ElTreeSelect = defineAsyncComponent(() =>
  Promise.all([
    import('element-plus/es/components/tree-select/index'),
    import('element-plus/es/components/tree-select/style/css'),
  ]).then(([res]) => res.ElTreeSelect),
);
const ElUpload = defineAsyncComponent(() =>
  Promise.all([
    import('element-plus/es/components/upload/index'),
    import('element-plus/es/components/upload/style/css'),
  ]).then(([res]) => res.ElUpload),
);

const withDefaultPlaceholder = <T extends Component>(
  component: T,
  type: 'input' | 'select',
  componentProps: Recordable<any> = {},
) => {
  return defineComponent({
    name: component.name,
    inheritAttrs: false,
    setup: (props: any, { attrs, expose, slots }) => {
      const placeholder =
        props?.placeholder ||
        attrs?.placeholder ||
        $t(`ui.placeholder.${type}`);
      // Method for exposing transparent components
      const innerRef = ref();
      const publicApi: Recordable<any> = {};
      expose(publicApi);
      const instance = getCurrentInstance();
      instance?.proxy?.$nextTick(() => {
        for (const key in innerRef.value) {
          if (typeof innerRef.value[key] === 'function') {
            publicApi[key] = innerRef.value[key];
          }
        }
      });
      return () =>
        h(
          component,
          { ...componentProps, placeholder, ...props, ...attrs, ref: innerRef },
          slots,
        );
    },
  });
};

// Here you need to adapt according to the business component library. The components that need to be used need to be typed here.
export type ComponentType =
  | 'ApiSelect'
  | 'ApiTreeSelect'
  | 'Checkbox'
  | 'CheckboxGroup'
  | 'DatePicker'
  | 'Divider'
  | 'IconPicker'
  | 'Input'
  | 'InputNumber'
  | 'RadioGroup'
  | 'Select'
  | 'Space'
  | 'Switch'
  | 'TimePicker'
  | 'TreeSelect'
  | 'Upload'
  | BaseFormComponentType;

async function initComponentAdapter() {
  const components: Partial<Record<ComponentType, Component>> = {
    // If your component is relatively large, you can use asynchronous loading
    // Button: () =>
    // import('xxx').then((res) => res.Button),
    ApiSelect: withDefaultPlaceholder(
      {
        ...ApiComponent,
        name: 'ApiSelect',
      },
      'select',
      {
        component: ElSelectV2,
        loadingSlot: 'loading',
        visibleEvent: 'onVisibleChange',
      },
    ),
    ApiTreeSelect: withDefaultPlaceholder(
      {
        ...ApiComponent,
        name: 'ApiTreeSelect',
      },
      'select',
      {
        component: ElTreeSelect,
        props: { label: 'label', children: 'children' },
        nodeKey: 'value',
        loadingSlot: 'loading',
        optionsPropName: 'data',
        visibleEvent: 'onVisibleChange',
      },
    ),
    Checkbox: ElCheckbox,
    CheckboxGroup: (props, { attrs, slots }) => {
      let defaultSlot;
      if (Reflect.has(slots, 'default')) {
        defaultSlot = slots.default;
      } else {
        const { options, isButton } = attrs;
        if (Array.isArray(options)) {
          defaultSlot = () =>
            options.map((option) =>
              h(isButton ? ElCheckboxButton : ElCheckbox, option),
            );
        }
      }
      return h(
        ElCheckboxGroup,
        { ...props, ...attrs },
        { ...slots, default: defaultSlot },
      );
    },
    // Customize the default button
    DefaultButton: (props, { attrs, slots }) => {
      return h(ElButton, { ...props, attrs, type: 'info' }, slots);
    },
    // Customize the main button
    PrimaryButton: (props, { attrs, slots }) => {
      return h(ElButton, { ...props, attrs, type: 'primary' }, slots);
    },
    Divider: ElDivider,
    IconPicker: withDefaultPlaceholder(IconPicker, 'select', {
      iconSlot: 'append',
      modelValueProp: 'model-value',
      inputComponent: ElInput,
    }),
    Input: withDefaultPlaceholder(ElInput, 'input'),
    InputNumber: withDefaultPlaceholder(ElInputNumber, 'input'),
    RadioGroup: (props, { attrs, slots }) => {
      let defaultSlot;
      if (Reflect.has(slots, 'default')) {
        defaultSlot = slots.default;
      } else {
        const { options } = attrs;
        if (Array.isArray(options)) {
          defaultSlot = () =>
            options.map((option) =>
              h(attrs.isButton ? ElRadioButton : ElRadio, option),
            );
        }
      }
      return h(
        ElRadioGroup,
        { ...props, ...attrs },
        { ...slots, default: defaultSlot },
      );
    },
    Select: (props, { attrs, slots }) => {
      return h(ElSelectV2, { ...props, attrs }, slots);
    },
    Space: ElSpace,
    Switch: ElSwitch,
    TimePicker: (props, { attrs, slots }) => {
      const { name, id, isRange } = props;
      const extraProps: Recordable<any> = {};
      if (isRange) {
        if (name && !Array.isArray(name)) {
          extraProps.name = [name, `${name}_end`];
        }
        if (id && !Array.isArray(id)) {
          extraProps.id = [id, `${id}_end`];
        }
      }
      return h(
        ElTimePicker,
        {
          ...props,
          ...attrs,
          ...extraProps,
        },
        slots,
      );
    },
    DatePicker: (props, { attrs, slots }) => {
      const { name, id, type } = props;
      const extraProps: Recordable<any> = {};
      if (type && type.includes('range')) {
        if (name && !Array.isArray(name)) {
          extraProps.name = [name, `${name}_end`];
        }
        if (id && !Array.isArray(id)) {
          extraProps.id = [id, `${id}_end`];
        }
      }
      return h(
        ElDatePicker,
        {
          ...props,
          ...attrs,
          ...extraProps,
        },
        slots,
      );
    },
    TreeSelect: withDefaultPlaceholder(ElTreeSelect, 'select'),
    Upload: ElUpload,
  };

  // Register the component into the global shared state
  globalShareState.setComponents(components);

  // Define message prompts in global shared state
  globalShareState.defineMessage({
    // Copy success message prompt
    copyPreferencesSuccess: (title, content) => {
      ElNotification({
        title,
        message: content,
        position: 'bottom-right',
        duration: 0,
        type: 'success',
      });
    },
  });
}

export { initComponentAdapter };
