<script lang="ts" setup>
import { useRouter } from 'vue-router';

import { Fallback } from '@vben/common-ui';

import { Button } from 'ant-design-vue';

const router = useRouter();
</script>

<template>
  <Fallback
    description="Breadcrumb navigation-flat mode-details page"
    status="coming-soon"
    title="Pay attention to changes in breadcrumb navigation"
  >
    <template #action>
      <Button @click="router.go(-1)">Return</Button>
    </template>
  </Fallback>
</template>
