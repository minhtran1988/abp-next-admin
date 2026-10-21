<script lang="ts" setup>
import { ref } from 'vue';

import { useVbenDrawer } from '@vben/common-ui';

import { Input, message } from 'ant-design-vue';

import { useVbenForm } from '#/adapter/form';

const value = ref('');

const [Form] = useVbenForm({
  schema: [
    {
      component: 'Input',
      componentProps: {
        placeholder: 'KeepAlive test: internal component',
      },
      fieldName: 'field1',
      hideLabel: true,
      label: 'Field 1',
    },
  ],
  showDefaultActions: false,
});

const [Drawer, drawerApi] = useVbenDrawer({
  destroyOnClose: false,
  onCancel() {
    drawerApi.close();
  },
  onConfirm() {
    message.info('onConfirm');
    // drawerApi.close();
  },
});
</script>
<template>
  <Drawer append-to-main title="Basic drawer example" title-tooltip="Title tip content">
    <template #extra> extra </template>
    This pop-up window is specified to be opened in the content area, and the pop-up window content will not be destroyed after closing.
    <Input
      v-model:value="value"
      placeholder="KeepAlive test:connectedComponent"
    />
    <Form />
  </Drawer>
</template>
