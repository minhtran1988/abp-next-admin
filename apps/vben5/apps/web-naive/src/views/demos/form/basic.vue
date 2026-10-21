<script lang="ts" setup>
import { Page } from '@vben/common-ui';

import { NButton, NCard, useMessage } from 'naive-ui';

import { useVbenForm } from '#/adapter/form';
import { getAllMenusApi } from '#/api';

const message = useMessage();
const [Form, formApi] = useVbenForm({
  commonConfig: {
    // All form items
    componentProps: {
      class: 'w-full',
    },
  },
  layout: 'horizontal',
  // The large screen displays 3 items in one line, the medium screen displays 2 items in one line, and the small screen displays 1 item in one line.
  wrapperClass: 'grid-cols-1 md:grid-cols-2 lg:grid-cols-3',
  handleSubmit: (values) => {
    message.success(`Form data: ${JSON.stringify(values)}`);
  },
  schema: [
    {
      // The component needs to be registered in #/adapter.ts and add the type
      component: 'ApiSelect',
      // Parameters of the corresponding component
      componentProps: {
        // Convert menu interface to options format
        afterFetch: (data: { name: string; path: string }[]) => {
          return data.map((item: any) => ({
            label: item.name,
            value: item.path,
          }));
        },
        // Menu interface
        api: getAllMenusApi,
      },
      // Field name
      fieldName: 'api',
      // Label displayed on the interface
      label: 'ApiSelect',
      rules: 'required',
    },
    {
      component: 'ApiTreeSelect',
      // Parameters of the corresponding component
      componentProps: {
        // Menu interface
        api: getAllMenusApi,
        childrenField: 'children',
        // Convert menu interface to options format
        labelField: 'name',
        valueField: 'path',
      },
      // Field name
      fieldName: 'apiTree',
      // Label displayed on the interface
      label: 'ApiTreeSelect',
      rules: 'required',
    },
    {
      component: 'Input',
      fieldName: 'string',
      label: 'String',
      rules: 'required',
    },
    {
      component: 'InputNumber',
      fieldName: 'number',
      label: 'Number',
      rules: 'required',
    },
    {
      component: 'RadioGroup',
      fieldName: 'radio',
      label: 'Radio',
      componentProps: {
        options: [
          { value: 'A', label: 'A' },
          { value: 'B', label: 'B' },
          { value: 'C', label: 'C' },
          { value: 'D', label: 'D' },
          { value: 'E', label: 'E' },
        ],
      },
      rules: 'selectRequired',
    },
    {
      component: 'RadioGroup',
      fieldName: 'radioButton',
      label: 'RadioButton',
      componentProps: {
        isButton: true,
        class: 'flex flex-wrap', // If there are too many options, you can add a class to automatically fold
        options: [
          { value: 'A', label: 'Option A' },
          { value: 'B', label: 'Option B' },
          { value: 'C', label: 'Option C' },
          { value: 'D', label: 'Option D' },
          { value: 'E', label: 'Option E' },
        ],
      },
      rules: 'selectRequired',
    },
    {
      component: 'CheckboxGroup',
      fieldName: 'checkbox',
      label: 'Checkbox',
      componentProps: {
        options: [
          { value: 'A', label: 'Option A' },
          { value: 'B', label: 'Option B' },
          { value: 'C', label: 'Option C' },
        ],
      },
      rules: 'selectRequired',
    },
    {
      component: 'DatePicker',
      fieldName: 'date',
      label: 'Date',
      rules: 'required',
    },
    {
      component: 'Input',
      fieldName: 'textArea',
      label: 'TextArea',
      componentProps: {
        type: 'textarea',
      },
      rules: 'required',
    },
  ],
});
function setFormValues() {
  formApi.setValues({
    string: 'string',
    number: 123,
    radio: 'B',
    radioButton: 'C',
    checkbox: ['A', 'C'],
    date: Date.now(),
  });
}
</script>
<template>
  <Page
    description="The form adapter repackages CheckboxGroup and RadioGroup, and option data can be passed through the options attribute (option data will be used as a property of the child component)"
    title="Form Demonstration"
  >
    <NCard title="Basic Form">
      <template #header-extra>
        <NButton type="primary" @click="setFormValues">Set form values</NButton>
      </template>
      <Form />
    </NCard>
  </Page>
</template>
