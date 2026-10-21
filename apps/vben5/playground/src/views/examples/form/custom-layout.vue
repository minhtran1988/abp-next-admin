<script lang="ts" setup>
import { h } from 'vue';

import { Page } from '@vben/common-ui';

import { Card } from 'ant-design-vue';

import { useVbenForm } from '#/adapter/form';

import DocButton from '../doc-button.vue';

const [CustomLayoutForm] = useVbenForm({
  // Common to all form items, they can be overridden individually within the form.
  commonConfig: {
    // All form items
    componentProps: {
      class: 'w-full',
    },
  },
  layout: 'horizontal',
  schema: [
    {
      component: 'Select',
      fieldName: 'field1',
      label: 'string',
    },
    {
      component: 'TreeSelect',
      fieldName: 'field2',
      label: 'string',
    },
    {
      component: 'Mentions',
      fieldName: 'field3',
      label: 'string',
    },
    {
      component: 'Input',
      fieldName: 'field4',
      label: 'string',
    },
    {
      component: 'InputNumber',
      fieldName: 'field5',
      // Starting from the third column is equivalent to an empty column in the middle
      formItemClass: 'col-start-3',
      label: 'There is an empty column in front',
    },
    {
      component: 'Divider',
      fieldName: '_divider',
      formItemClass: 'col-span-3',
      hideLabel: true,
      renderComponentContent: () => {
        return {
          default: () => h('div', 'divide line'),
        };
      },
    },
    {
      component: 'Textarea',
      fieldName: 'field6',
      // Occupy three columns of space, baseline alignment
      formItemClass: 'col-span-3 items-baseline',
      label: 'fill three columns',
    },
    {
      component: 'Input',
      fieldName: 'field7',
      // Occupy 2 columns of space, starting from the second column, which is equivalent to an empty column in front
      formItemClass: 'col-span-2 col-start-2',
      label: 'Fill 2 columns',
    },
    {
      component: 'Input',
      fieldName: 'field8',
      // Leave left and right blank
      formItemClass: 'col-start-2',
      label: 'leave left and right blank',
    },
    {
      component: 'InputPassword',
      fieldName: 'field9',
      formItemClass: 'col-start-1',
      label: 'string',
    },
  ],
  // Three columns in total
  wrapperClass: 'grid-cols-3',
});
</script>

<template>
  <Page
    content-class="flex flex-col gap-4"
    description="Use tailwind to customize the layout of form items"
    title="Form custom layout"
  >
    <template #description>
      <div class="text-muted-foreground">
        <p>Use tailwind to customize the layout of form items, and use Divider to split the form. </p>
      </div>
    </template>
    <template #extra>
      <DocButton class="mb-2" path="/components/common-ui/vben-form" />
    </template>
    <Card title="Use tailwind to customize layout">
      <CustomLayoutForm />
    </Card>
  </Page>
</template>
