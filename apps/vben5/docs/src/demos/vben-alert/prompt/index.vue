<script lang="ts" setup>
import { h } from 'vue';

import { alert, prompt, useAlertContext, VbenButton } from '@vben/common-ui';

import { Input, RadioGroup, Select } from 'ant-design-vue';
import { BadgeJapaneseYen } from 'lucide-vue-next';

function showPrompt() {
  prompt({
    content: 'Please enter something',
  })
    .then((val) => {
      alert(`Your input has been received: ${val}`);
    })
    .catch(() => {
      alert('Canceled');
    });
}

function showSlotsPrompt() {
  prompt({
    component: () => {
      // Get the pop-up window context. Note: Can only be called in setup or functional components
      const { doConfirm } = useAlertContext();
      return h(
        Input,
        {
          onKeydown(e: KeyboardEvent) {
            if (e.key === 'Enter') {
              e.preventDefault();
              // Call the confirmation method provided by the pop-up window
              doConfirm();
            }
          },
          placeholder: 'Please enter',
          prefix: 'Recharge amount:',
          type: 'number',
        },
        {
          addonAfter: () => h(BadgeJapaneseYen),
        },
      );
    },
    content:
      'This pop-up window demonstrates how to use a custom slot, and you can use useAlertContext to get the context of the pop-up window. \nPressing the Enter key in the input box will trigger the confirmation operation. ',
    icon: 'question',
    modelPropName: 'value',
  }).then((val) => {
    if (val) alert(`You entered ${val}`);
  });
}

function showSelectPrompt() {
  prompt({
    component: Select,
    componentProps: {
      options: [
        { label: 'Option A', value: 'Option A' },
        { label: 'Option B', value: 'Option B' },
        { label: 'Option C', value: 'Option C' },
      ],
      placeholder: 'Please select',
      // The pop-up window will set the body's pointer-events to none, which will affect the click event of the drop-down box.
      popupClassName: 'pointer-events-auto',
    },
    content: 'This pop-up window demonstrates how to use component to pass custom components',
    icon: 'question',
    modelPropName: 'value',
  }).then((val) => {
    if (val) {
      alert(`You selected ${val}`);
    }
  });
}

function sleep(ms: number) {
  return new Promise((resolve) => setTimeout(resolve, ms));
}

function showAsyncPrompt() {
  prompt({
    async beforeClose(scope) {
      if (scope.isConfirm) {
        if (scope.value) {
          // Simulate asynchronous operation, if unsuccessful, you can return false
          await sleep(2000);
        } else {
          alert('Please select an option');
          return false;
        }
      }
    },
    component: RadioGroup,
    componentProps: {
      class: 'flex flex-col',
      options: [
        { label: 'Option 1', value: 'option1' },
        { label: 'Option 2', value: 'option2' },
        { label: 'Option 3', value: 'option3' },
      ],
    },
    content: 'Select an option and click [Confirm]',
    icon: 'question',
    modelPropName: 'value',
  }).then((val) => {
    alert(`${val} is set.`);
  });
}
</script>
<template>
  <div class="flex gap-4">
    <VbenButton @click="showPrompt">Prompt</VbenButton>
    <VbenButton @click="showSlotsPrompt"> Prompt With slots </VbenButton>
    <VbenButton @click="showSelectPrompt">Prompt With Select</VbenButton>
    <VbenButton @click="showAsyncPrompt">Prompt With Async</VbenButton>
  </div>
</template>
