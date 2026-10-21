<script lang="ts" setup>
import { ref } from 'vue';

import { Page } from '@vben/common-ui';

import { Button, Card, message, Step, Steps, Switch } from 'ant-design-vue';

import { useVbenForm } from '#/adapter/form';

const currentTab = ref(0);
function onFirstSubmit(values: Record<string, any>) {
  message.success({
    content: `form1 values: ${JSON.stringify(values)}`,
  });
  currentTab.value = 1;
}
function onSecondReset() {
  currentTab.value = 0;
}
function onSecondSubmit(values: Record<string, any>) {
  message.success({
    content: `form2 values: ${JSON.stringify(values)}`,
  });
}

const [FirstForm, firstFormApi] = useVbenForm({
  commonConfig: {
    componentProps: {
      class: 'w-full',
    },
  },
  handleSubmit: onFirstSubmit,
  layout: 'horizontal',
  resetButtonOptions: {
    show: false,
  },
  schema: [
    {
      component: 'Input',
      componentProps: {
        placeholder: 'Please enter',
      },
      fieldName: 'formFirst',
      label: 'Form 1 Field',
      rules: 'required',
    },
  ],
  submitButtonOptions: {
    content: 'Next',
  },
  wrapperClass: 'grid-cols-1 md:grid-cols-1 lg:grid-cols-1',
});
const [SecondForm, secondFormApi] = useVbenForm({
  commonConfig: {
    componentProps: {
      class: 'w-full',
    },
  },
  handleReset: onSecondReset,
  handleSubmit: onSecondSubmit,
  layout: 'horizontal',
  resetButtonOptions: {
    content: 'Previous step',
  },
  schema: [
    {
      component: 'Input',
      componentProps: {
        placeholder: 'Please enter',
      },
      fieldName: 'formSecond',
      label: 'Form 2 Field',
      rules: 'required',
    },
  ],
  wrapperClass: 'grid-cols-1 md:grid-cols-1 lg:grid-cols-1',
});
const needMerge = ref(true);
async function handleMergeSubmit() {
  const values = await firstFormApi
    .merge(secondFormApi)
    .submitAllForm(needMerge.value);
  message.success({
    content: `merged form values: ${JSON.stringify(values)}`,
  });
}
</script>

<template>
  <Page
    description="Example of merging form components: In some scenarios, such as step-by-step forms, multiple forms need to be merged and submitted uniformly. By default, the Object.assign rule is used to merge the forms. If special processing of data is required, false can be passed in."
    title="Form component"
  >
    <Card title="Basic Example">
      <template #extra>
        <Switch
          v-model="needMerge"
          checked-children="Enable field merging"
          class="mr-4"
          un-checked-children="Close field merging"
        />
        <Button type="primary" @click="handleMergeSubmit">Merge submission</Button>
      </template>
      <div class="mx-auto max-w-lg">
        <Steps :current="currentTab" class="steps">
          <Step title="Form 1" />
          <Step title="Form 2" />
        </Steps>
        <div class="p-20">
          <FirstForm v-show="currentTab === 0" />
          <SecondForm v-show="currentTab === 1" />
        </div>
      </div>
    </Card>
  </Page>
</template>
