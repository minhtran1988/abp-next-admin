<script lang="ts" setup>
import type { Recordable } from '@vben/types';

import { useRouter } from 'vue-router';

import { useAccess } from '@vben/access';
import { Page } from '@vben/common-ui';
import { resetAllStores, useUserStore } from '@vben/stores';

import { Button, Card } from 'ant-design-vue';

import { useAuthStore } from '#/store';

const accounts: Record<string, Recordable<any>> = {
  admin: {
    password: '123456',
    username: 'admin',
  },
  super: {
    password: '123456',
    username: 'vben',
  },
  user: {
    password: '123456',
    username: 'jack',
  },
};

const { accessMode, toggleAccessMode } = useAccess();
const userStore = useUserStore();
const accessStore = useAuthStore();
const router = useRouter();

function roleButtonType(role: string) {
  return userStore.userRoles.includes(role) ? 'primary' : 'default';
}

async function changeAccount(role: string) {
  if (userStore.userRoles.includes(role)) {
    return;
  }

  const account = accounts[role];
  resetAllStores();
  if (account) {
    await accessStore.authLogin(account, async () => {
      router.go(0);
    });
  }
}

async function handleToggleAccessMode() {
  if (!accounts.super) {
    return;
  }
  await toggleAccessMode();
  resetAllStores();

  await accessStore.authLogin(accounts.super, async () => {
    setTimeout(() => {
      router.go(0);
    }, 150);
  });
}
</script>

<template>
  <Page
    :title="`${accessMode === 'frontend' ? 'frontend' : 'backend'} page access permission demonstration`"
    description="Switch to different accounts and observe the changes in the menu on the left."
  >
    <Card class="mb-5" title="Permission Mode">
      <span class="font-semibold">Current permission mode:</span>
      <span class="mx-4 text-primary">{{
        accessMode === 'frontend' ? 'Front-end permission control' : 'Back-end permission control'
      }}</span>
      <Button type="primary" @click="handleToggleAccessMode">
        Switch to {{ accessMode === 'frontend' ? 'backend' : 'frontend' }} permission mode
      </Button>
    </Card>
    <Card title="Account switching">
      <Button :type="roleButtonType('super')" @click="changeAccount('super')">
        Switch to Super account
      </Button>

      <Button
        :type="roleButtonType('admin')"
        class="mx-4"
        @click="changeAccount('admin')"
      >
        Switch to Admin account
      </Button>
      <Button :type="roleButtonType('user')" @click="changeAccount('user')">
        Switch to User account
      </Button>
    </Card>
  </Page>
</template>
