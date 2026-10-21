<script lang="ts" setup>
import { Page } from '@vben/common-ui';

import { Button, Card, Space } from 'tdesign-vue-next';

import { message, notification } from '#/adapter/tdesign';

type NotificationType = 'error' | 'info' | 'success' | 'warning';

function info() {
  message.info('How many roads must a man walk down');
}

function error() {
  message.error({
    content: 'Once upon a time you dressed so fine',
    duration: 2500,
  });
}

function warning() {
  message.warning('How many roads must a man walk down');
}
function success() {
  message.success('Cause you walked hand in hand With another man in my place');
}

function notify(type: NotificationType) {
  notification[type]({
    duration: 2500,
    title: 'What are you talking about',
  });
}
</script>

<template>
  <Page
    description="Supports multiple languages, integrated theme function switching, etc."
    title="TDesign Vue component usage demonstration"
  >
    <Card class="!mb-5" title="Button">
      <Space>
        <Button>Default</Button>
        <Button theme="primary"> Primary </Button>
        <Button theme="default"> Info </Button>
        <Button theme="danger"> Error </Button>
      </Space>
    </Card>
    <Card class="!mb-5" title="Message">
      <Space>
        <Button @click="info"> Information </Button>
        <Button theme="danger" @click="error"> Error </Button>
        <Button @click="warning"> Warning </Button>
        <Button @click="success"> Success </Button>
      </Space>
    </Card>

    <Card class="!mb-5" title="Notification">
      <Space>
        <Button @click="notify('info')"> Information </Button>
        <Button theme="danger" @click="notify('error')"> Error </Button>
        <Button @click="notify('warning')"> Warning </Button>
        <Button @click="notify('success')"> Success </Button>
      </Space>
    </Card>
  </Page>
</template>
