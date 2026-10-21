<script lang="ts" setup>
import type { TippyProps } from '@vben/common-ui';

import { reactive } from 'vue';

import { Page, Tippy } from '@vben/common-ui';

import { Button, Card, Flex } from 'ant-design-vue';

import { useVbenForm } from '#/adapter/form';

const tippyProps = reactive<TippyProps>({
  animation: 'shift-away',
  arrow: true,
  content: 'This is a tip',
  delay: [200, 200],
  duration: 200,
  followCursor: false,
  hideOnClick: false,
  inertia: true,
  maxWidth: 'none',
  placement: 'top',
  theme: 'dark',
  trigger: 'mouseenter focusin',
});

function parseBoolean(value: string) {
  switch (value) {
    case 'false': {
      return false;
    }
    case 'true': {
      return true;
    }
    default: {
      return value;
    }
  }
}

const [Form] = useVbenForm({
  handleValuesChange(values) {
    Object.assign(tippyProps, {
      ...values,
      delay: [values.delay1, values.delay2],
      followCursor: parseBoolean(values.followCursor),
      hideOnClick: parseBoolean(values.hideOnClick),
      trigger: values.trigger.join(' '),
    });
  },
  schema: [
    {
      component: 'RadioGroup',
      componentProps: {
        buttonStyle: 'solid',
        class: 'w-full',
        options: [
          { label: 'auto', value: 'auto' },
          { label: 'dark', value: 'dark' },
          { label: 'light', value: 'light' },
        ],
        optionType: 'button',
      },
      defaultValue: tippyProps.theme,
      fieldName: 'theme',
      label: 'topic',
    },
    {
      component: 'Select',
      componentProps: {
        class: 'w-full',
        options: [
          { label: 'Slide up', value: 'shift-away' },
          { label: 'slide down', value: 'shift-toward' },
          { label: 'scale', value: 'scale' },
          { label: 'perspective', value: 'perspective' },
          { label: 'fade', value: 'fade' },
        ],
      },
      defaultValue: tippyProps.animation,
      fieldName: 'animation',
      label: 'animation type',
    },
    {
      component: 'RadioGroup',
      componentProps: {
        buttonStyle: 'solid',
        options: [
          { label: 'is', value: true },
          { label: 'No', value: false },
        ],
        optionType: 'button',
      },
      defaultValue: tippyProps.inertia,
      fieldName: 'inertia',
      label: 'animation inertia',
    },
    {
      component: 'Select',
      componentProps: {
        class: 'w-full',
        options: [
          { label: 'top', value: 'top' },
          { label: 'top-left', value: 'top-start' },
          { label: 'top right', value: 'top-end' },
          { label: 'bottom', value: 'bottom' },
          { label: 'bottom-left', value: 'bottom-start' },
          { label: 'bottom-end', value: 'bottom-end' },
          { label: 'left', value: 'left' },
          { label: 'upper left', value: 'left-start' },
          { label: 'left-bottom', value: 'left-end' },
          { label: 'right', value: 'right' },
          { label: 'upper right', value: 'right-start' },
          { label: 'right-bottom', value: 'right-end' },
        ],
      },
      defaultValue: tippyProps.placement,
      fieldName: 'placement',
      label: 'position',
    },
    {
      component: 'InputNumber',
      componentProps: {
        addonAfter: 'milliseconds',
      },
      defaultValue: tippyProps.duration,
      fieldName: 'duration',
      label: 'animation duration',
    },
    {
      component: 'InputNumber',
      componentProps: {
        addonAfter: 'milliseconds',
      },
      defaultValue: 100,
      fieldName: 'delay1',
      label: 'Display delay',
    },
    {
      component: 'InputNumber',
      componentProps: {
        addonAfter: 'milliseconds',
      },
      defaultValue: 100,
      fieldName: 'delay2',
      label: 'Hide delay',
    },
    {
      component: 'Input',
      defaultValue: tippyProps.content,
      fieldName: 'content',
      label: 'content',
    },
    {
      component: 'RadioGroup',
      componentProps: {
        buttonStyle: 'solid',
        options: [
          { label: 'is', value: true },
          { label: 'No', value: false },
        ],
        optionType: 'button',
      },
      defaultValue: tippyProps.arrow,
      fieldName: 'arrow',
      label: 'indicator arrow',
    },
    {
      component: 'Select',
      componentProps: {
        class: 'w-full',
        options: [
          { label: 'Do not follow', value: 'false' },
          { label: 'Follow completely', value: 'true' },
          { label: 'horizontal only', value: 'horizontal' },
          { label: 'Vertical only', value: 'vertical' },
          { label: 'initial only', value: 'initial' },
        ],
      },
      defaultValue: tippyProps.followCursor?.toString(),
      fieldName: 'followCursor',
      label: 'Follow pointer',
    },
    {
      component: 'Select',
      componentProps: {
        class: 'w-full',
        mode: 'multiple',
        options: [
          { label: 'mouseenter', value: 'mouseenter' },
          { label: 'clicked', value: 'click' },
          { label: 'Get focus', value: 'focusin' },
          { label: 'No trigger, only manual', value: 'manual' },
        ],
      },
      defaultValue: tippyProps.trigger?.split(' '),
      fieldName: 'trigger',
      label: 'trigger mode',
    },
    {
      component: 'Select',
      componentProps: {
        class: 'w-full',
        options: [
          { label: 'no', value: 'false' },
          { label: 'is', value: 'true' },
          { label: 'Internal only', value: 'toggle' },
        ],
      },
      defaultValue: tippyProps.hideOnClick?.toString(),
      dependencies: {
        componentProps(_, formAction) {
          return {
            disabled: !formAction.values.trigger.includes('click'),
          };
        },
        triggerFields: ['trigger'],
      },
      fieldName: 'hideOnClick',
      help: 'Only valid when the trigger mode is `click`',
      label: 'Hide after clicking',
    },
    {
      component: 'Input',
      componentProps: {
        allowClear: true,
        placeholder: 'none、200px',
      },
      defaultValue: tippyProps.maxWidth,
      fieldName: 'maxWidth',
      label: 'maximum width',
    },
  ],
  showDefaultActions: false,
  wrapperClass: 'grid-cols-1 md:grid-cols-2 lg:grid-cols-3',
});

function goDoc() {
  window.open('https://atomiks.github.io/tippyjs/v6/all-props/');
}
</script>
<template>
  <Page title="Tippy">
    <template #description>
      <div class="flex items-center">
        <p>
          Tippy
          It is a lightweight prompt tool library that can be used to create various interactive prompts, such as tool tips, boot tips, etc.
        </p>
        <Button type="link" size="small" @click="goDoc">View documentation</Button>
      </div>
    </template>
    <Card title="Use in command form">
      <p class="mb-4">
        The instruction form is relatively simple to use. Use v-tippy to pass the configuration directly on the component that needs to display the tooltip. It is suitable for tooltips with fixed content.
      </p>
      <Flex warp="warp" gap="20" align="center">
        <Button v-tippy="'This is a tip, the default configuration is used'">Default configuration</Button>

        <Button
          v-tippy="{ theme: 'light', content: 'This is a tip, always light theme' }"
        >
          Specify topic
        </Button>
        <Button
          v-tippy="{
            theme: 'light',
            content: 'This prompt will be activated 100 milliseconds after igniting the component',
            delay: 100,
          }"
        >
          Specify delay
        </Button>
        <Button
          v-tippy="{
            content: 'The animation of this prompt is `scale`',
            animation: 'scale',
          }"
        >
          Specify animation
        </Button>
      </Flex>
    </Card>
    <Card title="Use in component form" class="mt-4">
      <div class="flex w-full justify-center">
        <Tippy v-bind="tippyProps">
          <Button>Move the mouse over this component to experience the effect</Button>
        </Tippy>
      </div>

      <Form class="mt-4" />
      <template #actions>
        <p
          class="cursor-default text-secondary-foreground hover:text-secondary-foreground"
        >
          More configuration please
          <Button type="link" size="small" @click="goDoc">View documentation</Button>
          , only some common configurations are listed here
        </p>
      </template>
    </Card>
  </Page>
</template>
