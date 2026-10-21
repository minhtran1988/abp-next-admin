<script lang="ts" setup>
import { useRouter } from 'vue-router';

import { Fallback } from '@vben/common-ui';

import { Button } from 'ant-design-vue';

const router = useRouter();

function details() {
  router.push({ name: 'BreadcrumbLateralDetailDemo' });
}
</script>

<template>
  <Fallback
    description="Click to view details and observe changes in breadcrumb navigation"
    status="coming-soon"
    title="Breadcrumb navigation-flat mode"
  >
    <template #action>
      <Button type="primary" @click="details">Click to view details</Button>
    </template>
  </Fallback>
</template>
