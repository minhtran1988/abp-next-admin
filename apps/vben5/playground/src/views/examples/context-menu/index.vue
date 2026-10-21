<script setup lang="ts">
import { Page } from '@vben/common-ui';

import { VbenContextMenu } from '@vben-core/shadcn-ui';

import { Button, Card, message } from 'ant-design-vue';

const needHidden = (role: string) => {
  return role === 'user';
};

const contextMenus = () => {
  return [
    {
      text: 'Refresh',
      key: 'refresh',
      handler: (data: any) => {
        message.success('Refresh successful', data);
      },
      hidden: needHidden('admin'),
    },
    {
      text: 'Close current',
      key: 'close-current',
      handler: (data: any) => {
        message.success('Close current', data);
      },
      hidden: needHidden('user'),
    },
    {
      text: 'Close others',
      key: 'close-other',
      handler: (data: any) => {
        message.success('Close others', data);
      },
    },
    {
      text: 'Close all',
      key: 'close-all',
      handler: (data: any) => {
        message.success('Close all', data);
      },
    },
  ];
};
</script>

<template>
  <Page title="Context Menu context menu">
    <Card title="Basic use">
      <div>A total of four menus (refresh, close current, close other, close all)</div>
      <br />
      <br />
      <VbenContextMenu :menus="contextMenus" :modal="true" item-class="pr-6">
        <Button> Right-click me to open the context menu (with hidden items) </Button>
      </VbenContextMenu>
    </Card>
  </Page>
</template>
