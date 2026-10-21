<script lang="ts" setup>
import { Page } from '@vben/common-ui';

import { Button, Card, message } from 'ant-design-vue';

import { useVbenForm } from '#/adapter/form';

const [Form, formApi] = useVbenForm({
  // Submit function
  handleSubmit: onSubmit,
  schema: [
    {
      component: 'Input',
      defaultValue: 'hidden value',
      dependencies: {
        show: false,
        // When any field changes, it will be triggered.
        triggerFields: ['field1Switch'],
      },
      fieldName: 'hiddenField',
      label: 'hidden field',
    },
    {
      component: 'Switch',
      defaultValue: true,
      fieldName: 'field1Switch',
      help: 'Control destruction through Dom',
      label: 'Display field 1',
    },
    {
      component: 'Switch',
      defaultValue: true,
      fieldName: 'field2Switch',
      help: 'Control hiding through css',
      label: 'Display field 2',
    },
    {
      component: 'Switch',
      fieldName: 'field3Switch',
      label: 'Disabled field 3',
    },
    {
      component: 'Switch',
      fieldName: 'field4Switch',
      label: 'Field 4 is required',
    },
    {
      component: 'Input',
      dependencies: {
        if(values) {
          return !!values.field1Switch;
        },
        // This will only be triggered when the specified field changes.
        triggerFields: ['field1Switch'],
      },
      // Field name
      fieldName: 'field1',
      // Label displayed on the interface
      label: 'Field 1',
    },
    {
      component: 'Input',
      dependencies: {
        show(values) {
          return !!values.field2Switch;
        },
        triggerFields: ['field2Switch'],
      },
      fieldName: 'field2',
      label: 'Field 2',
    },
    {
      component: 'Input',
      dependencies: {
        disabled(values) {
          return !!values.field3Switch;
        },
        triggerFields: ['field3Switch'],
      },
      fieldName: 'field3',
      label: 'Field 3',
    },
    {
      component: 'Input',
      dependencies: {
        required(values) {
          return !!values.field4Switch;
        },
        triggerFields: ['field4Switch'],
      },
      fieldName: 'field4',
      label: 'Field 4',
    },
    {
      component: 'Input',
      dependencies: {
        rules(values) {
          if (values.field1 === '123') {
            return 'required';
          }
          return null;
        },
        triggerFields: ['field1'],
      },
      fieldName: 'field5',
      help: 'Required when the value of field 1 is `123`',
      label: 'dynamic rules',
    },
    {
      component: 'Select',
      componentProps: {
        allowClear: true,
        class: 'w-full',
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
      dependencies: {
        componentProps(values) {
          if (values.field2 === '123') {
            return {
              options: [
                {
                  label: 'Option 1',
                  value: '1',
                },
                {
                  label: 'Option 2',
                  value: '2',
                },
                {
                  label: 'Option 3',
                  value: '3',
                },
              ],
            };
          }
          return {};
        },
        triggerFields: ['field2'],
      },
      fieldName: 'field6',
      help: 'Change the drop-down option when the value of field 2 is `123`',
      label: 'dynamic configuration',
    },
    {
      component: 'Input',
      fieldName: 'field7',
      label: 'Field 7',
    },
  ],
  // The large screen displays 3 items in one line, the medium screen displays 2 items in one line, and the small screen displays 1 item in one line.
  wrapperClass: 'grid-cols-1 md:grid-cols-3 lg:grid-cols-4',
});

const [SyncForm] = useVbenForm({
  handleSubmit: onSubmit,
  schema: [
    {
      component: 'Input',
      // Field name
      fieldName: 'field1',
      // Label displayed on the interface
      label: 'Field 1',
    },
    {
      component: 'Input',
      componentProps: {
        disabled: true,
      },
      dependencies: {
        trigger(values, form) {
          form.setFieldValue('field2', values.field1);
        },
        // This will only be triggered when the specified field changes.
        triggerFields: ['field1'],
      },
      // Field name
      fieldName: 'field2',
      // Label displayed on the interface
      label: 'Field 2',
    },
  ],
  // The large screen displays 3 items in one line, the medium screen displays 2 items in one line, and the small screen displays 1 item in one line.
  wrapperClass: 'grid-cols-1 md:grid-cols-3 lg:grid-cols-4',
});

function onSubmit(values: Record<string, any>) {
  message.success({
    content: `form values: ${JSON.stringify(values)}`,
  });
}

function handleDelete() {
  formApi.setState((prev) => {
    return {
      schema: prev.schema?.filter((item) => item.fieldName !== 'field7'),
    };
  });
}

function handleAdd() {
  formApi.setState((prev) => {
    return {
      schema: [
        ...(prev?.schema ?? []),
        {
          component: 'Input',
          fieldName: `field${Date.now()}`,
          label: 'Field+',
        },
      ],
    };
  });
}

function handleUpdate() {
  formApi.setState((prev) => {
    return {
      schema: prev.schema?.map((item) => {
        if (item.fieldName === 'field3') {
          return {
            ...item,
            label: 'Field 3-Modify',
          };
        }
        return item;
      }),
    };
  });
}
</script>

<template>
  <Page
    description="Examples of dynamic linkage of form components, including common scenarios. Addition, deletion, and modification are essentially modifications to the schema. You can also dynamically modify the schema through `setState`."
    title="Form component"
  >
    <Card title="Form dynamic linkage example">
      <template #extra>
        <Button class="mr-2" @click="handleUpdate">Modify field 3</Button>
        <Button class="mr-2" @click="handleDelete">Delete field 7</Button>
        <Button @click="handleAdd">Add field</Button>
      </template>
      <Form />
    </Card>

    <Card class="mt-5" title="Field synchronization, field 1 data and field 2 data synchronization">
      <SyncForm />
    </Card>
  </Page>
</template>
