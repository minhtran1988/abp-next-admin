<script lang="ts" setup>
import { EllipsisText } from '@vben/common-ui';

const text = `
Vben Admin is a backend solution based on Vue3.0, Vite, and TypeScript. It aims to provide out-of-the-box solutions for the development of medium and large projects.
`;
</script>
<template>
  <EllipsisText :line="2" :tooltip-when-ellipsis="true">
    {{ text }}
  </EllipsisText>

  <EllipsisText :line="3" :tooltip-when-ellipsis="true">
    {{ text }}
  </EllipsisText>
</template>
