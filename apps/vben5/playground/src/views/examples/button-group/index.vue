<script lang="ts" setup>
import type { Recordable } from '@vben/types';

import { reactive, ref } from 'vue';

import {
  Page,
  VbenButton,
  VbenButtonGroup,
  VbenCheckButtonGroup,
} from '@vben/common-ui';
import { LoaderCircle, Square, SquareCheckBig } from '@vben/icons';

import { Button, Card, message } from 'ant-design-vue';

import { useVbenForm } from '#/adapter/form';

const radioValue = ref<string | undefined>('a');
const checkValue = ref(['a', 'b']);

const options = [
  { label: 'Option 1', value: 'a' },
  { label: 'Option 2', value: 'b', num: 999 },
  { label: 'Option 3', value: 'c' },
  { label: 'Option 4', value: 'd' },
  { label: 'Option 5', value: 'e' },
  { label: 'Option 6', value: 'f' },
];

function resetValues() {
  radioValue.value = undefined;
  checkValue.value = [];
}

function beforeChange(v: any, isChecked: boolean) {
  return new Promise((resolve) => {
    message.loading({
      content: `Setting ${v} to ${isChecked ? 'checked' : 'unchecked'}...`,
      duration: 0,
      key: 'beforeChange',
    });
    setTimeout(() => {
      message.success({ content: `${v} has been set successfully`, key: 'beforeChange' });
      resolve(true);
    }, 2000);
  });
}

const compProps = reactive({
  beforeChange: undefined,
  disabled: false,
  gap: 0,
  showIcon: true,
  size: 'middle',
  allowClear: false,
} as Recordable<any>);

const [Form] = useVbenForm({
  handleValuesChange(values) {
    Object.keys(values).forEach((k) => {
      if (k === 'beforeChange') {
        compProps[k] = values[k] ? beforeChange : undefined;
      } else {
        compProps[k] = values[k];
      }
    });
  },
  commonConfig: {
    labelWidth: 150,
  },
  schema: [
    {
      component: 'RadioGroup',
      componentProps: {
        options: [
          { label: 'large', value: 'large' },
          { label: 'Medium', value: 'middle' },
          { label: 'small', value: 'small' },
        ],
      },
      defaultValue: compProps.size,
      fieldName: 'size',
      label: 'size',
    },
    {
      component: 'RadioGroup',
      componentProps: {
        options: [
          { label: 'None', value: 0 },
          { label: 'small', value: 5 },
          { label: 'Medium', value: 15 },
          { label: 'big', value: 30 },
        ],
      },
      defaultValue: compProps.gap,
      fieldName: 'gap',
      label: 'spacing',
    },
    {
      component: 'Switch',
      defaultValue: compProps.showIcon,
      fieldName: 'showIcon',
      label: 'Show icon',
    },
    {
      component: 'Switch',
      defaultValue: compProps.disabled,
      fieldName: 'disabled',
      label: 'disabled',
    },
    {
      component: 'Switch',
      defaultValue: false,
      fieldName: 'beforeChange',
      label: 'pre-callback',
    },
    {
      component: 'Switch',
      defaultValue: false,
      fieldName: 'allowClear',
      label: 'Allow clearing',
      help: 'Whether deselecting is allowed during radio selection (the value is undefined)',
    },
    {
      component: 'InputNumber',
      defaultValue: 0,
      fieldName: 'maxCount',
      label: 'Maximum number of selections',
      help: 'valid for multiple selections, 0 means no limit',
    },
  ],
  showDefaultActions: false,
  submitOnChange: true,
});

function onBtnClick(value: any) {
  const opt = options.find((o) => o.value === value);
  if (opt) {
    message.success(`The button ${opt.label} was clicked, value = ${value}`);
  }
}
</script>
<template>
  <Page
    title="VbenButtonGroup button group"
    description="VbenButtonGroup is a button container used to wrap a group of buttons and coordinate the overall style. VbenCheckButtonGroup can be used as a form component to provide single-select or multi-select functions"
  >
    <Card title="Basic usage">
      <template #extra>
        <Button type="primary" @click="resetValues">Clear values</Button>
      </template>
      <p class="mt-4">Button group:</p>
      <div class="mt-2 flex flex-col gap-2">
        <VbenButtonGroup v-bind="compProps" border>
          <VbenButton
            v-for="btn in options"
            :key="btn.value"
            variant="link"
            @click="onBtnClick(btn.value)"
          >
            {{ btn.label }}
          </VbenButton>
        </VbenButtonGroup>
        <VbenButtonGroup v-bind="compProps" border>
          <VbenButton
            v-for="btn in options"
            :key="btn.value"
            variant="outline"
            @click="onBtnClick(btn.value)"
          >
            {{ btn.label }}
          </VbenButton>
        </VbenButtonGroup>
      </div>
      <p class="mt-4">Radio selection: {{ radioValue }}</p>
      <div class="mt-2 flex flex-col gap-2">
        <VbenCheckButtonGroup
          v-model="radioValue"
          :options="options"
          v-bind="compProps"
        />
      </div>
      <p class="mt-4">Radio selection slot: {{ radioValue }}</p>
      <div class="mt-2 flex flex-col gap-2">
        <VbenCheckButtonGroup
          v-model="radioValue"
          :options="options"
          v-bind="compProps"
        >
          <template #option="{ label, value, data }">
            <div class="flex items-center">
              <span>{{ label }}</span>
              <span class="ml-2 text-gray-400">{{ value }}</span>
              <span v-if="data.num" class="white ml-2">{{ data.num }}</span>
            </div>
          </template>
        </VbenCheckButtonGroup>
      </div>
      <p class="mt-4">Multiple selection{{ checkValue }}</p>
      <div class="mt-2 flex flex-col gap-2">
        <VbenCheckButtonGroup
          v-model="checkValue"
          multiple
          :options="options"
          v-bind="compProps"
        />
      </div>
      <p class="mt-4">Custom icon{{ checkValue }}</p>
      <div class="mt-2 flex flex-col gap-2">
        <VbenCheckButtonGroup
          v-model="checkValue"
          multiple
          :options="options"
          v-bind="compProps"
        >
          <template #icon="{ loading, checked }">
            <LoaderCircle class="animate-spin" v-if="loading" />
            <SquareCheckBig v-else-if="checked" />
            <Square v-else />
          </template>
        </VbenCheckButtonGroup>
      </div>
    </Card>

    <Card title="Settings" class="mt-4">
      <Form />
    </Card>
  </Page>
</template>
