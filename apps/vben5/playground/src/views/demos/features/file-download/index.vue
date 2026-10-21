<script setup lang="ts">
import { ref } from 'vue';

import { Page } from '@vben/common-ui';
import {
  downloadFileFromBase64,
  downloadFileFromBlobPart,
  downloadFileFromImageUrl,
  downloadFileFromUrl,
} from '@vben/utils';

import { Button, Card } from 'ant-design-vue';

import { downloadFile1, downloadFile2 } from '#/api/examples/download';

import imageBase64 from './base64';

const downloadResult = ref('');

function getBlob() {
  downloadFile1().then((res) => {
    downloadResult.value = `Getting Blob successfully, length: ${res.size}`;
  });
}

function getResponse() {
  downloadFile2().then((res) => {
    downloadResult.value = `Get Response successfully, headers: ${JSON.stringify(res.headers)}, length: ${res.data.size}`;
  });
}
</script>

<template>
  <Page title="File download example">
    <Card title="Download file based on file address">
      <Button
        type="primary"
        @click="
          downloadFileFromUrl({
            source:
              'https://codeload.github.com/vbenjs/vue-vben-admin-doc/zip/main',
            target: '_self',
          })
        "
      >
        Download File
      </Button>
    </Card>

    <Card class="my-5" title="Download pictures based on address">
      <Button
        type="primary"
        @click="
          downloadFileFromImageUrl({
            source:
              'https://unpkg.com/@vbenjs/static-source@0.1.7/source/logo-v1.webp',
            fileName: 'vben-logo.png',
          })
        "
      >
        Download File
      </Button>
    </Card>

    <Card class="my-5" title="base64 streaming download">
      <Button
        type="primary"
        @click="
          downloadFileFromBase64({
            source: imageBase64,
            fileName: 'image.png',
          })
        "
      >
        Download Image
      </Button>
    </Card>
    <Card class="my-5" title="Text Download">
      <Button
        type="primary"
        @click="
          downloadFileFromBlobPart({
            source: 'text content',
            fileName: 'test.txt',
          })
        "
      >
        Download TxT
      </Button>
    </Card>

    <Card class="my-5" title="Request download">
      <Button type="primary" @click="getBlob"> Get Blob </Button>
      <Button type="primary" class="ml-4" @click="getResponse">
        GetResponse
      </Button>
      <div class="mt-4">{{ downloadResult }}</div>
    </Card>
  </Page>
</template>
