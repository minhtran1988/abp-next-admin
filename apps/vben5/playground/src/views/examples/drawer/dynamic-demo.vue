<script lang="ts" setup>
import { useVbenDrawer } from '@vben/common-ui';

import { Button, message } from 'ant-design-vue';

const [Drawer, drawerApi] = useVbenDrawer({
  onCancel() {
    drawerApi.close();
  },
  onConfirm() {
    message.info('onConfirm');
    // drawerApi.close();
  },
  title: 'Dynamic modification configuration example',
});

// const state = drawerApi.useStore();

function handleUpdateTitle() {
  drawerApi.setState({ title: 'Internal dynamic title' });
}
</script>
<template>
  <Drawer>
    <div class="flex-col-center">
      <Button class="mb-3" type="primary" @click="handleUpdateTitle()">
        Dynamically modify the title internally
      </Button>
    </div>
  </Drawer>
</template>
