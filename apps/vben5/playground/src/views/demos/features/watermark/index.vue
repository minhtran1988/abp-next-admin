<script lang="ts" setup>
import { Page } from '@vben/common-ui';
import { useWatermark } from '@vben/hooks';

import { Button, Card } from 'ant-design-vue';

const { destroyWatermark, updateWatermark, watermark } = useWatermark();

async function recreateWaterMark() {
  destroyWatermark();
  await createWaterMark();
}

async function createWaterMark() {
  await updateWatermark({
    advancedStyle: {
      colorStops: [
        {
          color: 'red',
          offset: 0,
        },
        {
          color: 'blue',
          offset: 1,
        },
      ],
      type: 'linear',
    },
    content: `hello my watermark\n${new Date().toLocaleString()}`,
    globalAlpha: 0.5,
    gridLayoutOptions: {
      cols: 2,
      gap: [20, 20],
      matrix: [
        [1, 0],
        [0, 1],
      ],
      rows: 2,
    },
    height: 200,
    layout: 'grid',
    rotate: 22,
    width: 200,
  });
}
</script>

<template>
  <Page title="watermark">
    <template #description>
      <div class="mt-2 text-foreground/80">
        watermark used
        <a
          class="text-primary"
          href="https://zhensherlock.github.io/watermark-js-plus/"
          target="_blank"
        >
          watermark-js-plus
        </a>
        Open source plug-in, detailed configuration can be found in the plug-in configuration.
      </div>
    </template>

    <Card title="Use">
      <Button
        :disabled="!!watermark"
        class="mr-2"
        type="primary"
        @click="recreateWaterMark"
      >
        Create watermark
      </Button>
      <Button
        :disabled="!watermark"
        class="mr-2"
        type="primary"
        @click="createWaterMark"
      >
        Update watermark
      </Button>
      <Button :disabled="!watermark" danger @click="destroyWatermark">
        Remove watermark
      </Button>
    </Card>
  </Page>
</template>
