<script lang="ts" setup>
import type { LoginExpiredModeType } from '@vben/types';

import { Page } from '@vben/common-ui';
import { preferences, updatePreferences } from '@vben/preferences';

import { Button, Card } from 'ant-design-vue';

import { getMockStatusApi } from '#/api';

async function handleClick(type: LoginExpiredModeType) {
  const loginExpiredMode = preferences.app.loginExpiredMode;

  updatePreferences({ app: { loginExpiredMode: type } });
  await getMockStatusApi('401');
  updatePreferences({ app: { loginExpiredMode } });
}
</script>

<template>
  <Page title="Login expired demo">
    <template #description>
      <div class="mt-2 text-foreground/80">
        When an interface request encounters a 401 status code, you need to log in again. There are two ways:
        <p>1. Go to the login page and jump back to the original page after successful login</p>
        <p>
          2. A re-login pop-up window will pop up. After logging in, close the pop-up window without any page jump (the login page will still jump after refreshing)
        </p>
      </div>
    </template>

    <Card class="mb-5" title="How to jump to the login page">
      <Button type="primary" @click="handleClick('page')"> Click trigger </Button>
    </Card>
    <Card class="mb-5" title="Login pop-up window">
      <Button type="primary" @click="handleClick('modal')"> Click trigger </Button>
    </Card>
  </Page>
</template>
