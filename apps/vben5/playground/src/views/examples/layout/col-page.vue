<script lang="ts" setup>
import { reactive, ref } from 'vue';

import { ColPage } from '@vben/common-ui';
import { IconifyIcon } from '@vben/icons';

import {
  Alert,
  Button,
  Card,
  Checkbox,
  Slider,
  Tag,
  Tooltip,
} from 'ant-design-vue';

const props = reactive({
  leftCollapsedWidth: 5,
  leftCollapsible: true,
  leftMaxWidth: 50,
  leftMinWidth: 20,
  leftWidth: 30,
  resizable: true,
  rightWidth: 70,
  splitHandle: true,
  splitLine: true,
});
const leftMinWidth = ref(props.leftMinWidth || 1);
const leftMaxWidth = ref(props.leftMaxWidth || 100);
</script>
<template>
  <ColPage
    auto-content-height
    description="ColPage is a dual-column layout component that supports folding on the left, drag-and-drop width adjustment and other functions."
    v-bind="props"
    title="ColPage dual column layout component"
  >
    <template #title>
      <span class="mr-2 text-2xl font-bold">ColPage dual-column layout component</span>
      <Tag color="hsl(var(--destructive))">Alpha</Tag>
    </template>
    <template #left="{ isCollapsed, expand }">
      <div v-if="isCollapsed" @click="expand">
        <Tooltip title="Click to expand left">
          <Button
            shape="circle"
            type="primary"
            class="flex items-center justify-center"
          >
            <template #icon>
              <IconifyIcon class="text-2xl" icon="bi:arrow-right" />
            </template>
          </Button>
        </Tooltip>
      </div>
      <div
        v-else
        :style="{ minWidth: '200px' }"
        class="mr-2 rounded-[var(--radius)] border border-border bg-card p-2"
      >
        <p>This is the content on the left</p>
        <p>This is the content on the left</p>
        <p>This is the content on the left</p>
        <p>This is the content on the left</p>
        <p>This is the content on the left</p>
      </div>
    </template>
    <Card class="ml-2" title="Basic use">
      <div class="flex flex-col gap-2">
        <div class="flex gap-2">
          <Checkbox v-model="props.resizable">Dragable to adjust width</Checkbox>
          <Checkbox v-model="props.splitLine">Show drag divider</Checkbox>
          <Checkbox v-model="props.splitHandle">Show drag handle</Checkbox>
          <Checkbox v-model="props.leftCollapsible"> Left collapsible </Checkbox>
        </div>
        <div class="flex items-center gap-2">
          <span>Left minimum width percentage:</span>
          <Slider
            v-model:value="leftMinWidth"
            :max="props.leftMaxWidth - 1"
            :min="1"
            style="width: 100px"
            @after-change="(value) => (props.leftMinWidth = value as number)"
          />
          <span>Maximum left width percentage:</span>
          <Slider
            v-model:value="props.leftMaxWidth"
            :max="100"
            :min="leftMaxWidth + 1"
            style="width: 100px"
            @after-change="(value) => (props.leftMaxWidth = value as number)"
          />
        </div>
        <Alert message="Experimental component" show-icon type="warning">
          <template #description>
            <p>
              The double-column layout component is a relatively basic layout component extended on the Page component. It supports left folding (when dragging causes the left side width to be smaller than the minimum width, you can also enter the folded state), drag and drop to adjust the width and other functions.
            </p>
            <p>The value of the above width setting is a percentage, the minimum value is 1, and the maximum value is 100. </p>
            <p class="font-bold text-red-600">
              This is an experimental component and usage may change or may not ultimately be adopted. It is not recommended for use in production environments until its usage is formally documented.
            </p>
          </template>
        </Alert>
      </div>
    </Card>
  </ColPage>
</template>
