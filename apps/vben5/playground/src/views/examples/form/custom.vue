<script lang="ts" setup>
import { h, markRaw } from 'vue';

import { Page } from '@vben/common-ui';

import { Card, Input, message } from 'ant-design-vue';

import { useVbenForm, z } from '#/adapter/form';

import TwoFields from './modules/two-fields.vue';

const [Form] = useVbenForm({
  // Common to all form items, they can be overridden individually within the form.
  commonConfig: {
    // All form items
    componentProps: {
      class: 'w-full',
    },
    labelClass: 'w-2/6',
  },
  fieldMappingTime: [['field4', ['phoneType', 'phoneNumber'], null]],
  // Submit function
  handleSubmit: onSubmit,
  // Vertical layout, label and input are on different lines, the value is vertical
  // Horizontal layout, label and input are on the same line
  layout: 'horizontal',
  schema: [
    {
      // The component needs to be registered in #/adapter.ts and add the type
      component: 'Input',
      fieldName: 'field',
      label: 'custom suffix',
      suffix: () => h('span', { class: 'text-red-600' }, 'CNY'),
    },
    {
      component: 'Input',
      fieldName: 'field1',
      label: 'custom component slot',
      renderComponentContent: () => ({
        prefix: () => 'prefix',
        suffix: () => 'suffix',
      }),
    },
    {
      component: h(Input, { placeholder: 'Please enter Field2' }),
      fieldName: 'field2',
      label: 'custom component',
      modelPropName: 'value',
      rules: 'required',
    },
    {
      component: 'Input',
      fieldName: 'field3',
      label: 'Custom component (slot)',
      rules: 'required',
    },
    {
      component: markRaw(TwoFields),
      defaultValue: [undefined, ''],
      disabledOnChangeListener: false,
      fieldName: 'field4',
      formItemClass: 'col-span-1',
      label: 'Combined field',
      rules: z
        .array(z.string().optional())
        .length(2, 'Please select the type and enter your mobile phone number')
        .refine((v) => !!v[0], {
          message: 'Please select a type',
        })
        .refine((v) => !!v[1] && v[1] !== '', {
          message: 'Enter mobile phone number',
        })
        .refine((v) => v[1]?.match(/^1[3-9]\d{9}$/), {
          // Use full-width spaces to place the error text below the mobile phone number input box.
          message: 'The number format is incorrect',
        }),
    },
  ],
  // The medium screen displays 2 in one line, the small screen displays 1 in one line.
  wrapperClass: 'grid-cols-1 md:grid-cols-2',
});

function onSubmit(values: Record<string, any>) {
  message.success({
    content: `form values: ${JSON.stringify(values)}`,
  });
}
</script>

<template>
  <Page description="Form component customization example" title="Form component">
    <Card title="Basic Example">
      <Form>
        <template #field3="slotProps">
          <Input placeholder="Please enter" v-bind="slotProps" />
        </template>
      </Form>
    </Card>
  </Page>
</template>
