<script lang="ts" setup>
import type { Recordable } from '@vben/types';

import { useRouter } from 'vue-router';

import { AccessControl, useAccess } from '@vben/access';
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

const { accessMode, hasAccessByCodes } = useAccess();
const authStore = useAuthStore();
const userStore = useUserStore();
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
    await authStore.authLogin(account, async () => {
      router.go(0);
    });
  }
}
</script>

<template>
  <Page
    :title="`${accessMode === 'frontend' ? 'frontend' : 'backend'} button access rights demonstration`"
    description="Switch to different accounts and observe the button changes."
  >
    <Card class="mb-5">
      <template #title>
        <span class="font-semibold">Current role:</span>
        <span class="mx-4 text-lg text-primary">
          {{ userStore.userRoles?.[0] }}
        </span>
      </template>

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

    <Card class="mb-5" title="Component form control - permission code">
      <AccessControl :codes="['AC_100100']" type="code">
        <Button class="mr-4"> Super account visible ["AC_100100"] </Button>
      </AccessControl>
      <AccessControl :codes="['AC_100030']" type="code">
        <Button class="mr-4"> Admin account visible ["AC_100030"] </Button>
      </AccessControl>
      <AccessControl :codes="['AC_1000001']" type="code">
        <Button class="mr-4"> User account visible ["AC_1000001"] </Button>
      </AccessControl>
      <AccessControl :codes="['AC_100100', 'AC_100030']" type="code">
        <Button class="mr-4">
          Super & Admin accounts are visible ["AC_100100","AC_100030"]
        </Button>
      </AccessControl>
    </Card>

    <Card
      v-if="accessMode === 'frontend'"
      class="mb-5"
      title="Component Form Control - Role"
    >
      <AccessControl :codes="['super']" type="role">
        <Button class="mr-4"> Super character is visible </Button>
      </AccessControl>
      <AccessControl :codes="['admin']" type="role">
        <Button class="mr-4"> Admin role is visible </Button>
      </AccessControl>
      <AccessControl :codes="['user']" type="role">
        <Button class="mr-4"> User role visible </Button>
      </AccessControl>
      <AccessControl :codes="['super', 'admin']" type="role">
        <Button class="mr-4"> Super & Admin roles are visible </Button>
      </AccessControl>
    </Card>

    <Card class="mb-5" title="Functional form control">
      <Button v-if="hasAccessByCodes(['AC_100100'])" class="mr-4">
        Super account is visible ["AC_100100"]
      </Button>
      <Button v-if="hasAccessByCodes(['AC_100030'])" class="mr-4">
        Admin account is visible ["AC_100030"]
      </Button>
      <Button v-if="hasAccessByCodes(['AC_1000001'])" class="mr-4">
        User account is visible ["AC_1000001"]
      </Button>
      <Button v-if="hasAccessByCodes(['AC_100100', 'AC_100030'])" class="mr-4">
        Super & Admin accounts are visible ["AC_100100","AC_100030"]
      </Button>
    </Card>

    <Card class="mb-5" title="Command mode - permission code">
      <Button class="mr-4" v-access:code="['AC_100100']">
        Super account is visible ["AC_100100"]
      </Button>
      <Button class="mr-4" v-access:code="['AC_100030']">
        Admin account is visible ["AC_100030"]
      </Button>
      <Button class="mr-4" v-access:code="['AC_1000001']">
        User account is visible ["AC_1000001"]
      </Button>
      <Button class="mr-4" v-access:code="['AC_100100', 'AC_100030']">
        Super & Admin accounts are visible ["AC_100100","AC_100030"]
      </Button>
    </Card>

    <Card v-if="accessMode === 'frontend'" class="mb-5" title="Command Mode - Role">
      <Button class="mr-4" v-access:role="['super']"> Super role is visible </Button>
      <Button class="mr-4" v-access:role="['admin']"> Admin role visible </Button>
      <Button class="mr-4" v-access:role="['user']"> User role is visible </Button>
      <Button class="mr-4" v-access:role="['super', 'admin']">
        Super & Admin roles are visible
      </Button>
    </Card>
  </Page>
</template>
