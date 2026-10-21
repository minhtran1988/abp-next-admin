<script lang="ts" setup>
import { ref } from 'vue';

import { useVbenModal } from '@vben/common-ui';

import { Input, message } from 'ant-design-vue';

const [Modal, modalApi] = useVbenModal({
  destroyOnClose: false,
  onCancel() {
    modalApi.close();
  },
  onConfirm() {
    message.info('onConfirm');
    // modalApi.close();
  },
});
const value = ref();
</script>
<template>
  <Modal
    append-to-main
    class="w-[600px]"
    title="Basic pop-up window example"
    title-tooltip="Title tip content"
  >
    This pop-up window is specified to be opened in the content area, and the pop-up window content will not be destroyed after closing.
    <Input v-model:value="value" placeholder="KeepAlive test" />
  </Modal>
</template>
