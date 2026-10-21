<script lang="ts" setup>
import { h, ref } from 'vue';

import { alert, confirm, VbenButton } from '@vben/common-ui';

import { Checkbox, message } from 'ant-design-vue';

function showConfirm() {
  confirm('This is an alert message')
    .then(() => {
      alert('Confirmed');
    })
    .catch(() => {
      alert('Canceled');
    });
}

function showIconConfirm() {
  confirm({
    content: 'This is an alert message with icon',
    icon: 'success',
  });
}

function showfooterConfirm() {
  const checked = ref(false);
  confirm({
    cancelText: 'Do not be ridiculous',
    confirmText: 'Yes, we are all NPC',
    content:
      'Why does it seem like I have experienced what just happened before? \nI can even subconsciously predict what will happen next as it happens. \n\nIt sounds weird. Have you ever felt this way? ',
    footer: () =>
      h(
        Checkbox,
        {
          checked: checked.value,
          class: 'flex-1',
          'onUpdate:checked': (v) => (checked.value = v),
        },
        'Do not prompt again',
      ),
    icon: 'question',
    title: 'Unsolved Mystery',
  }).then(() => {
    if (checked.value) {
      message.success('I will not bother you with this problem anymore');
    } else {
      message.info('I will continue to ask you next time');
    }
  });
}

function showAsyncConfirm() {
  confirm({
    beforeClose({ isConfirm }) {
      if (isConfirm) {
        // Some asynchronous operations can be performed here. If false is eventually returned, the pop-up window will be prevented from closing.
        return new Promise((resolve) => setTimeout(resolve, 2000));
      }
    },
    content: 'This is an alert message with async confirm',
    icon: 'success',
  }).then(() => {
    alert('Confirmed');
  });
}
</script>
<template>
  <div class="flex gap-4">
    <VbenButton @click="showConfirm">Confirm</VbenButton>
    <VbenButton @click="showIconConfirm">Confirm With Icon</VbenButton>
    <VbenButton @click="showfooterConfirm">Confirm With Footer</VbenButton>
    <VbenButton @click="showAsyncConfirm">Async Confirm</VbenButton>
  </div>
</template>
