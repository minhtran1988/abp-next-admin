<script setup lang="ts">
import { computed } from 'vue';

import { ProfileSecuritySetting } from '@vben/common-ui';

const formSchema = computed(() => {
  return [
    {
      value: true,
      fieldName: 'accountPassword',
      label: 'account password',
      description: 'Current password strength: strong',
    },
    {
      value: true,
      fieldName: 'securityPhone',
      label: 'Secret mobile phone',
      description: 'Bound mobile phone: 138****8293',
    },
    {
      value: true,
      fieldName: 'securityQuestion',
      label: 'Security issue',
      description: 'No security questions are set, security questions can effectively protect account security',
    },
    {
      value: true,
      fieldName: 'securityEmail',
      label: 'Alternate email',
      description: 'Binded email address: ant***sign.com',
    },
    {
      value: false,
      fieldName: 'securityMfa',
      label: 'MFA device',
      description: 'The MFA device is not bound. After binding, you can confirm again',
    },
  ];
});
</script>
<template>
  <ProfileSecuritySetting :form-schema="formSchema" />
</template>
