<script lang="ts" setup>
import { Loading, Page, Spinner } from '@vben/common-ui';
import { IconifyIcon } from '@vben/icons';

import { refAutoReset } from '@vueuse/core';
import { Button, Card, Spin } from 'ant-design-vue';

const spinning = refAutoReset(false, 3000);
const loading = refAutoReset(false, 3000);

const spinningV = refAutoReset(false, 3000);
const loadingV = refAutoReset(false, 3000);
</script>
<template>
  <Page
    title="Vben Loading"
    description="Loading status component. This component can add a loading mask layer to other components that are used as containers. When using them, the container needs to be positioned relative."
  >
    <Card title="Antd Spin">
      <template #actions>This is a demonstration of the Spin component that comes with the Antd component library</template>
      <Spin :spinning="spinning" tip="Loading...">
        <Button type="primary" @click="spinning = true">Show Spin</Button>
      </Spin>
    </Card>

    <Card title="Vben Loading" v-loading="loadingV" class="mt-4">
      <template #extra>
        <Button type="primary" @click="loadingV = true">
          v-loading command
        </Button>
      </template>
      <template #actions>
        The Loading component can set text and also provides an icon slot to replace the loading icon.
      </template>
      <div class="flex gap-4">
        <div class="size-40">
          <Loading
            :spinning="loading"
            text="Loading..."
            class="flex h-full w-full items-center justify-center"
          >
            <Button type="primary" @click="loading = true">Default animation</Button>
          </Loading>
        </div>
        <div class="size-40">
          <Loading
            :spinning="loading"
            class="flex h-full w-full items-center justify-center"
          >
            <Button type="primary" @click="loading = true">Custom animation 1</Button>
            <template #icon>
              <IconifyIcon
                icon="svg-spinners:ring-resize"
                class="size-10 text-primary"
              />
            </template>
          </Loading>
        </div>
        <div class="size-40">
          <Loading
            :spinning="loading"
            class="flex h-full w-full items-center justify-center"
          >
            <Button type="primary" @click="loading = true">Custom animation 2</Button>
            <template #icon>
              <IconifyIcon
                icon="svg-spinners:bars-scale"
                class="size-10 text-primary"
              />
            </template>
          </Loading>
        </div>
      </div>
    </Card>

    <Card
      title="Vben Spinner"
      v-spinning="spinningV"
      class="mt-4 overflow-hidden"
      :body-style="{
        position: 'relative',
        overflow: 'hidden',
      }"
    >
      <template #extra>
        <Button type="primary" @click="spinningV = true">
          v-spinning directive
        </Button>
      </template>
      <template #actions>
        The Spinner component is a special case of the Loading component and has only a fixed unified style.
      </template>
      <Spinner
        :spinning="spinning"
        class="flex size-40 items-center justify-center"
      >
        <Button type="primary" @click="spinning = true">Show Spinner</Button>
      </Spinner>
    </Card>
  </Page>
</template>
