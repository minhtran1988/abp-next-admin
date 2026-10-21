<script setup lang="ts">
import { computed } from 'vue';

import { Page, SliderRotateCaptcha } from '@vben/common-ui';
import { preferences } from '@vben/preferences';
import { useUserStore } from '@vben/stores';

import { Card, message } from 'ant-design-vue';

const userStore = useUserStore();
function handleSuccess() {
  message.success('success!');
}

const avatar = computed(() => {
  return userStore.userInfo?.avatar || preferences.app.defaultAvatar;
});
</script>

<template>
  <Page description="For front-end simple drag verification scenario" title="Slider rotation verification">
    <Card class="mb-5" title="Basic example">
      <div class="flex items-center justify-center p-4">
        <SliderRotateCaptcha :src="avatar" @success="handleSuccess" />
      </div>
    </Card>
  </Page>
</template>
