<script lang="ts" setup>
import { useVbenModal } from '@vben/common-ui';

import { Button } from 'ant-design-vue';

import DragDemo from './drag-demo.vue';

const [Modal] = useVbenModal({
  destroyOnClose: true,
});
const [BaseModal, baseModalApi] = useVbenModal({
  connectedComponent: DragDemo,
});

function openNestedModal() {
  baseModalApi.open();
}
</script>
<template>
  <Modal title="Nested pop-up window example">
    <Button @click="openNestedModal" type="primary">Open bullet window</Button>
    <BaseModal />
  </Modal>
</template>
