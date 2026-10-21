<script setup lang="ts">
import { Page, SliderTranslateCaptcha } from '@vben/common-ui';

import { Card, message } from 'ant-design-vue';

function handleSuccess() {
  message.success('success!');
}
</script>

<template>
  <Page
    description="Used for front-end simple puzzle slider horizontal drag verification scenario"
    title="Puzzle Slider Verification"
  >
    <Card class="mb-5" title="Basic example">
      <div class="flex items-center justify-center p-4">
        <SliderTranslateCaptcha
          src="https://unpkg.com/@vbenjs/static-source@0.1.7/source/pro-avatar.webp"
          :canvas-width="420"
          :canvas-height="420"
          @success="handleSuccess"
        />
      </div>
    </Card>
  </Page>
</template>
