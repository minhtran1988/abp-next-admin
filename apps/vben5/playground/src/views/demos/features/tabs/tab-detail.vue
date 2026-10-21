<script lang="ts" setup>
import { computed } from 'vue';
import { useRoute } from 'vue-router';

import { Page } from '@vben/common-ui';
import { useTabs } from '@vben/hooks';

const route = useRoute();

const { setTabTitle } = useTabs();

const index = computed(() => {
  return route.params?.id ?? -1;
});

setTabTitle(`No.${index.value} - detailed information`);
</script>

<template>
  <Page :title="`Tab page ${index} details page`">
    <template #description> {{ index }} - Details page content is here </template>
  </Page>
</template>
