<script lang="ts" setup>
import type { NotificationType } from 'naive-ui';

import { Page } from '@vben/common-ui';

import { NButton, NCard, NSpace, useMessage, useNotification } from 'naive-ui';

const notification = useNotification();

const message = useMessage();
function error() {
  message.error('Once upon a time you dressed so fine');
}

function warning() {
  message.warning('How many roads must a man walk down');
}
function success() {
  message.success('Cause you walked hand in hand With another man in my place');
}
function loading() {
  message.loading(
    'If I were you, I will realize that I love you more than any other guy',
  );
}

function notify(type: NotificationType) {
  notification[type]({
    content: 'What are you talking about',
    duration: 2500,
    keepAliveOnHover: true,
    meta: 'Cannot think of it',
  });
}
</script>

<template>
  <Page description="Supports multiple languages, integrated theme function switching, etc." title="Naive component usage demonstration">
    <NCard class="mb-5" title="Button">
      <NSpace>
        <NButton>Default</NButton>
        <NButton type="tertiary"> Tertiary </NButton>
        <NButton type="primary"> Primary </NButton>
        <NButton type="info"> Info </NButton>
        <NButton type="success"> Success </NButton>
        <NButton type="warning"> Warning </NButton>
        <NButton type="error"> Error </NButton>
      </NSpace>
    </NCard>

    <NCard class="mb-5" title="Message">
      <NSpace>
        <NButton type="error" @click="error"> Error </NButton>
        <NButton type="warning" @click="warning"> Warning </NButton>
        <NButton type="success" @click="success"> Success </NButton>
        <NButton type="primary" @click="loading"> Loading </NButton>
      </NSpace>
    </NCard>

    <NCard class="mb-5" title="Notification">
      <NSpace>
        <NButton type="error" @click="notify('error')"> Error </NButton>
        <NButton type="warning" @click="notify('warning')"> Warning </NButton>
        <NButton type="success" @click="notify('success')"> Success </NButton>
        <NButton type="primary" @click="notify('info')"> Loading </NButton>
      </NSpace>
    </NCard>
  </Page>
</template>
