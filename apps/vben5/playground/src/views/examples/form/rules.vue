<script lang="ts" setup>
import { Page } from '@vben/common-ui';

import { Button, Card, message } from 'ant-design-vue';

import { useVbenForm, z } from '#/adapter/form';

const [Form, formApi] = useVbenForm({
  // Common to all form items, they can be overridden individually within the form.
  commonConfig: {
    // All form items
    componentProps: {
      class: 'w-full',
    },
  },
  // Submit function
  handleSubmit: onSubmit,
  // Vertical layout, label and input are on different lines, the value is vertical
  // Horizontal layout, label and input are on the same line
  layout: 'horizontal',
  schema: [
    {
      // The component needs to be registered in #/adapter.ts and add the type
      component: 'Input',
      // Parameters of the corresponding component
      componentProps: {
        placeholder: 'Please enter',
      },
      // Field name
      fieldName: 'field1',
      // Label displayed on the interface
      label: 'Field 1',
      rules: 'required',
    },
    {
      component: 'Input',
      componentProps: {
        placeholder: 'Please enter',
      },
      defaultValue: 'default value',
      fieldName: 'field2',
      label: 'Default value (required)',
      rules: 'required',
    },
    {
      component: 'Input',
      componentProps: {
        placeholder: 'Please enter',
      },
      fieldName: 'field3',
      label: 'Default value (not required)',
      rules: z.string().default('default value').optional(),
    },
    {
      component: 'Input',
      componentProps: {
        placeholder: 'Please enter',
      },
      fieldName: 'field31',
      label: 'custom information',
      rules: z.string().min(1, { message: 'Enter at least 1 character' }),
    },
    {
      component: 'Input',
      // Parameters of the corresponding component
      componentProps: {
        placeholder: 'Please enter',
      },
      // Field name
      fieldName: 'field4',
      // Label displayed on the interface
      label: 'mailbox',
      rules: z.string().email('Please enter the correct email'),
    },
    {
      component: 'InputNumber',
      componentProps: {
        placeholder: 'Please enter',
      },
      fieldName: 'number',
      label: 'number',
      rules: 'required',
    },
    {
      component: 'Select',
      componentProps: {
        allowClear: true,
        filterOption: true,
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
        placeholder: 'Please select',
        showSearch: true,
      },
      defaultValue: undefined,
      fieldName: 'options',
      label: 'drop-down selection',
      rules: 'selectRequired',
    },
    {
      component: 'RadioGroup',
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
      fieldName: 'radioGroup',
      label: 'radio group',
      rules: 'selectRequired',
    },
    {
      component: 'CheckboxGroup',
      componentProps: {
        name: 'cname',
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
      fieldName: 'checkboxGroup',
      label: 'Multiple selection group',
      rules: 'selectRequired',
    },
    {
      component: 'Checkbox',
      fieldName: 'checkbox',
      label: '',
      renderComponentContent: () => {
        return {
          default: () => ['I have read and agree'],
        };
      },
      rules: z.boolean().refine((value) => value, {
        message: 'Please check',
      }),
    },
    {
      component: 'DatePicker',
      defaultValue: undefined,
      fieldName: 'datePicker',
      label: 'Date selection box',
      rules: 'selectRequired',
    },
    {
      component: 'RangePicker',
      defaultValue: undefined,
      fieldName: 'rangePicker',
      label: 'Interval selection box',
      rules: 'selectRequired',
    },
    {
      component: 'InputPassword',
      componentProps: {
        placeholder: 'Please enter',
      },
      fieldName: 'password',
      label: 'password',
      rules: 'required',
    },
    {
      component: 'Input',
      componentProps: {
        placeholder: 'Please enter',
      },
      fieldName: 'input-blur',
      formFieldProps: {
        validateOnChange: false,
        validateOnModelUpdate: false,
      },
      help: 'Verification will be triggered only when blur',
      label: 'blur trigger',
      rules: 'required',
    },
    {
      component: 'Input',
      componentProps: {
        placeholder: 'Please enter',
      },
      fieldName: 'input-async',
      label: 'Asynchronous verification',
      rules: z
        .string()
        .min(3, 'Username must be at least 3 characters')
        .refine(
          async (username) => {
            // Assuming this is an asynchronous function, simulate checking whether the username already exists
            const checkUsernameExists = async (
              username: string,
            ): Promise<boolean> => {
              await new Promise((resolve) => setTimeout(resolve, 1000));
              return username === 'existingUser';
            };
            const exists = await checkUsernameExists(username);
            return !exists;
          },
          {
            message: 'Username already exists',
          },
        ),
    },
  ],
  // The large screen displays 3 items in one line, the medium screen displays 2 items in one line, and the small screen displays 1 item in one line.
  wrapperClass: 'grid-cols-1 md:grid-cols-2 lg:grid-cols-3',
});

function onSubmit(values: Record<string, any>) {
  message.success({
    content: `form values: ${JSON.stringify(values)}`,
  });
}
</script>

<template>
  <Page description="Form Validation Example" title="Form Component">
    <Card title="Basic component verification example">
      <template #extra>
        <Button @click="() => formApi.validate()">Validate form</Button>
        <Button class="mx-2" @click="() => formApi.resetValidate()">
          Clear verification information
        </Button>
      </template>
      <Form />
    </Card>
  </Page>
</template>
