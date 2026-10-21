<script lang="ts" setup>
import type { JsonViewerAction, JsonViewerValue } from '@vben/common-ui';

import { JsonViewer, Page } from '@vben/common-ui';

import { Card, message } from 'ant-design-vue';

import { json1, json2 } from './data';

function handleKeyClick(key: string) {
  message.info(`Key ${key} was clicked`);
}

function handleValueClick(value: JsonViewerValue) {
  message.info(`Value ${JSON.stringify(value)}`);
}

function handleCopied(_event: JsonViewerAction) {
  message.success('JSON copied');
}
</script>
<template>
  <Page
    title="Json Viewer"
    description="A component that renders JSON structure data, supports copying, expansion, etc., simple and easy to use"
  >
    <Card title="Default configuration">
      <JsonViewer :value="json1" />
    </Card>
    <Card title="Copyable, expand to 3 layers by default, display borders, event handling" class="mt-4">
      <JsonViewer
        :value="json2"
        :expand-depth="3"
        copyable
        :sort="false"
        @key-click="handleKeyClick"
        @value-click="handleValueClick"
        @copied="handleCopied"
        boxed
      />
    </Card>
    <Card title="Preview Mode" class="mt-4">
      <JsonViewer
        :value="json2"
        copyable
        preview-mode
        :show-array-index="false"
      />
    </Card>
  </Page>
</template>
