<script setup lang="ts">
import { ref } from 'vue';

import { Page } from '@vben/common-ui';

import { NDataTable } from 'naive-ui';

const columns = ref([
  {
    key: 'no',
    title: 'No',
  },
  {
    key: 'title',
    title: 'Title',
  },
  {
    key: 'length',
    title: 'Length',
  },
]);
const data = [
  { length: '4:18', no: 3, title: 'Wonderwall' },
  { length: '4:48', no: 4, title: "Don't Look Back in Anger" },
  { length: '7:27', no: 12, title: 'Champagne Supernova' },
];
</script>

<template>
  <Page
    description="Form pages are used to collect or verify information from users. Basic forms are commonly used in form scenarios with few data items."
    title="NDataTable"
  >
    <NDataTable :columns="columns" :data="data" />
  </Page>
</template>

<style scoped></style>
