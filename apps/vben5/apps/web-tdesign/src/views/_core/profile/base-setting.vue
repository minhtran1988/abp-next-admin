<script setup lang="ts">
import type { BasicOption } from '@vben/types';

import type { VbenFormSchema } from '#/adapter/form';

import { computed, onMounted, ref } from 'vue';

import { ProfileBaseSetting } from '@vben/common-ui';

import { getUserInfoApi } from '#/api';

const profileBaseSettingRef = ref();

const MOCK_ROLES_OPTIONS: BasicOption[] = [
  {
    label: 'Administrator',
    value: 'super',
  },
  {
    label: 'user',
    value: 'user',
  },
  {
    label: 'test',
    value: 'test',
  },
];

const formSchema = computed((): VbenFormSchema[] => {
  return [
    {
      fieldName: 'realName',
      component: 'Input',
      label: 'name',
    },
    {
      fieldName: 'username',
      component: 'Input',
      label: 'username',
    },
    {
      fieldName: 'roles',
      component: 'Select',
      componentProps: {
        mode: 'tags',
        options: MOCK_ROLES_OPTIONS,
      },
      label: 'role',
    },
    {
      fieldName: 'introduction',
      component: 'Textarea',
      label: 'personal profile',
    },
  ];
});

onMounted(async () => {
  const data = await getUserInfoApi();
  profileBaseSettingRef.value.getFormApi().setValues(data);
});
</script>
<template>
  <ProfileBaseSetting ref="profileBaseSettingRef" :form-schema="formSchema" />
</template>
