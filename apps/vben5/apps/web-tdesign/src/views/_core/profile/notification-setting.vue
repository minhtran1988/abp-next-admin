<script setup lang="ts">
import { computed } from 'vue';

import { ProfileNotificationSetting } from '@vben/common-ui';

const formSchema = computed(() => {
  return [
    {
      value: true,
      fieldName: 'accountPassword',
      label: 'account password',
      description: 'Messages from other users will be notified in the form of site messages',
    },
    {
      value: true,
      fieldName: 'systemMessage',
      label: 'system message',
      description: 'System messages will be notified in the form of site messages',
    },
    {
      value: true,
      fieldName: 'todoTask',
      label: 'To-do tasks',
      description: 'To-do tasks will be notified in the form of in-site messages',
    },
  ];
});
</script>
<template>
  <ProfileNotificationSetting :form-schema="formSchema" />
</template>
