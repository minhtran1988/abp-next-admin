<script lang="ts" setup>
import { useVbenModal } from '@vben/common-ui';

import { Button, message } from 'ant-design-vue';

const [Modal, modalApi] = useVbenModal({
  draggable: true,
  onCancel() {
    modalApi.close();
  },
  onConfirm() {
    message.info('onConfirm');
    // modalApi.close();
  },
  title: 'Dynamic modification configuration example',
});

const state = modalApi.useStore();

function handleUpdateTitle() {
  modalApi.setState({ title: 'Internal dynamic title' });
}

function handleToggleFullscreen() {
  modalApi.setState((prev) => {
    return { ...prev, fullscreen: !prev.fullscreen };
  });
}
</script>
<template>
  <Modal>
    <div class="flex-col-center">
      <Button class="mb-3" type="primary" @click="handleUpdateTitle()">
        Dynamically modify the title internally
      </Button>
      <Button class="mb-3" type="primary" @click="handleToggleFullscreen()">
        {{ state.fullscreen ? 'Exit full screen' : 'Open full screen' }}
      </Button>
    </div>
  </Modal>
</template>
