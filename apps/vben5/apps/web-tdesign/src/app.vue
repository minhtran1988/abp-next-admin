<script lang="ts" setup>
import type { GlobalConfigProvider } from 'tdesign-vue-next';

import { watch } from 'vue';

import { usePreferences } from '@vben/preferences';

import { merge } from 'es-toolkit/compat';
import { ConfigProvider } from 'tdesign-vue-next';
import zhConfig from 'tdesign-vue-next/es/locale/zh_CN';

defineOptions({ name: 'App' });
const { isDark } = usePreferences();

watch(
  () => isDark.value,
  (dark) => {
    document.documentElement.setAttribute('theme-mode', dark ? 'dark' : '');
  },
  { immediate: true },
);

const customConfig: GlobalConfigProvider = {
  // You can define more custom configurations here. Please refer to the API documentation for specific configurable content.
  calendar: {},
  table: {},
  pagination: {},
};
const globalConfig = merge(zhConfig, customConfig);
</script>

<template>
  <ConfigProvider :global-config="globalConfig">
    <RouterView />
  </ConfigProvider>
</template>
