<script lang="ts" setup>
import { Page } from '@vben/common-ui';

import { Card, message } from 'ant-design-vue';

import { useVbenForm } from '#/adapter/form';

const [QueryForm] = useVbenForm({
  // Expand by default
  collapsed: false,
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
        placeholder: 'Please enter username',
      },
      // Field name
      fieldName: 'username',
      // Label displayed on the interface
      label: 'string',
    },
    {
      component: 'InputPassword',
      componentProps: {
        placeholder: 'Please enter password',
      },
      fieldName: 'password',
      label: 'password',
    },
    {
      component: 'InputNumber',
      componentProps: {
        placeholder: 'Please enter',
      },
      fieldName: 'number',
      label: 'Number (with suffix)',
      suffix: () => '¥',
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
      fieldName: 'options',
      label: 'drop-down selection',
    },
    {
      component: 'DatePicker',
      fieldName: 'datePicker',
      label: 'Date selection box',
    },
  ],
  // Whether it can be expanded
  showCollapseButton: true,
  submitButtonOptions: {
    content: 'query',
  },
  // The large screen displays 3 items in one line, the medium screen displays 2 items in one line, and the small screen displays 1 item in one line.
  wrapperClass: 'grid-cols-1 md:grid-cols-2 lg:grid-cols-3',
});

const [InlineForm] = useVbenForm({
  layout: 'inline',
  schema: [
    {
      // The component needs to be registered in #/adapter.ts and add the type
      component: 'Input',
      // Parameters of the corresponding component
      componentProps: {
        placeholder: 'Please enter username',
      },
      // Field name
      fieldName: 'username',
      // Label displayed on the interface
      label: 'string',
    },
    {
      component: 'InputPassword',
      componentProps: {
        placeholder: 'Please enter password',
      },
      fieldName: 'password',
      label: 'password',
    },
    {
      component: 'InputNumber',
      componentProps: {
        placeholder: 'Please enter',
      },
      fieldName: 'number',
      label: 'Number (with suffix)',
      suffix: () => '¥',
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
      fieldName: 'options',
      label: 'drop-down selection',
    },
  ],
});

const [QueryForm1] = useVbenForm({
  // Expand by default
  collapsed: true,
  collapsedRows: 2,
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
  schema: (() => {
    const schema = [];
    for (let index = 0; index < 14; index++) {
      schema.push({
        // The component needs to be registered in #/adapter.ts and add the type
        component: 'Input',
        // Field name
        fieldName: `field${index}`,
        // Label displayed on the interface
        label: `field${index}`,
      });
    }
    return schema;
  })(),
  // Whether it can be expanded
  showCollapseButton: true,
  submitButtonOptions: {
    content: 'query',
  },
  // The large screen displays 3 items in one line, the medium screen displays 2 items in one line, and the small screen displays 1 item in one line.
  wrapperClass: 'grid-cols-1 md:grid-cols-2 lg:grid-cols-3',
});

const [QueryForm2] = useVbenForm({
  // Operation button group newLine: Displayed in a new line. rowEnd: Display within the row, aligned to the right (default). inline: use grid default style
  actionLayout: 'newLine',
  actionPosition: 'left', //The action button group is displayed on the left
  // default fold
  collapsed: true,
  collapsedRows: 3,
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
  layout: 'vertical',
  schema: [
    {
      // The component needs to be registered in #/adapter.ts and add the type
      component: 'Input',
      // Parameters of the corresponding component
      componentProps: {
        placeholder: 'Please enter username',
      },
      // Field name
      fieldName: 'username',
      // Label displayed on the interface
      label: 'string',
    },
    {
      component: 'InputPassword',
      componentProps: {
        placeholder: 'Please enter password',
      },
      fieldName: 'password',
      label: 'password',
    },
    {
      component: 'InputNumber',
      componentProps: {
        placeholder: 'Please enter',
      },
      fieldName: 'number',
      label: 'Number (with suffix)',
      suffix: () => '¥',
    },
    {
      component: 'DatePicker',
      fieldName: 'datePicker',
      label: 'Date selection box',
    },
  ],
  // Whether it can be expanded
  showCollapseButton: true,
  submitButtonOptions: {
    content: 'query',
  },
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
  <Page
    description="Query form, used in combination with common phrases and tables, can be contracted and expanded."
    title="Form component"
  >
    <Card class="mb-5" title="Query form, expanded by default">
      <QueryForm />
    </Card>

    <Card class="mb-5" title="Query form, single line form">
      <InlineForm />
    </Card>

    <Card class="mb-5" title="Query form, expanded by default, vertical layout">
      <QueryForm2 />
    </Card>

    <Card title="Query form, folded by default, retain 2 rows when folded">
      <QueryForm1 />
    </Card>
  </Page>
</template>
