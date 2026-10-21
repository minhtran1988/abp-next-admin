<script setup lang="ts">
import { Page } from '@vben/common-ui';

import { refAutoReset } from '@vueuse/core';
import { Button, Card, Empty } from 'ant-design-vue';

import ConcurrencyCaching from './concurrency-caching.vue';
import InfiniteQueries from './infinite-queries.vue';
import PaginatedQueries from './paginated-queries.vue';
import QueryRetries from './query-retries.vue';

const showCaching = refAutoReset(true, 1000);
</script>

<template>
  <Page title="Vue Query Example">
    <div class="grid grid-cols-1 gap-4 md:grid-cols-2">
      <Card title="Page query">
        <PaginatedQueries />
      </Card>
      <Card title="infinite scroll">
        <InfiniteQueries class="h-[300px] overflow-auto" />
      </Card>
      <Card title="Retry on error">
        <QueryRetries />
      </Card>
      <Card
        title="Concurrency and Caching"
        v-spinning="!showCaching"
        :body-style="{ minHeight: '330px' }"
      >
        <template #extra>
          <Button @click="showCaching = false">Reload</Button>
        </template>
        <ConcurrencyCaching v-if="showCaching" />
        <Empty v-else description="Loading..." />
      </Card>
    </div>
  </Page>
</template>
