<script lang="ts" setup>
import type { CountToProps, TransitionPresets } from '@vben/common-ui';

import { reactive } from 'vue';

import { CountTo, Page, TransitionPresetsKeys } from '@vben/common-ui';
import { IconifyIcon } from '@vben/icons';

import {
  Button,
  Card,
  Col,
  Form,
  FormItem,
  Input,
  InputNumber,
  message,
  Row,
  Select,
  Switch,
} from 'ant-design-vue';

const props = reactive<CountToProps & { transition: TransitionPresets }>({
  decimal: '.',
  decimals: 2,
  decimalStyle: {
    fontSize: 'small',
    fontStyle: 'italic',
  },
  delay: 0,
  disabled: false,
  duration: 2000,
  endVal: 100_000,
  mainStyle: {
    color: 'hsl(var(--primary))',
    fontSize: 'xx-large',
    fontWeight: 'bold',
  },
  prefix: '￥',
  prefixStyle: {
    paddingRight: '0.5rem',
  },
  separator: ',',
  startVal: 0,
  suffix: 'yuan',
  suffixStyle: {
    paddingLeft: '0.5rem',
  },
  transition: 'easeOutQuart',
});

function changeNumber() {
  props.endVal =
    Math.floor(Math.random() * 100_000_000) / 10 ** (props.decimals || 0);
}

function openDocumentation() {
  window.open('https://vueuse.org/core/useTransition/', '_blank');
}

function onStarted() {
  message.loading({
    content: 'Animation has started',
    duration: 0,
    key: 'animator-info',
  });
}

function onFinished() {
  message.success({
    content: 'Animation has ended',
    duration: 2,
    key: 'animator-info',
  });
}
</script>
<template>
  <Page title="CountTo" description="Number scroll animation component. Use">
    <template #description>
      <span>
        Using the digital scrolling animation component encapsulated by useTransition, a transition animation will be generated every time the current value is changed.
      </span>
      <Button type="link" @click="openDocumentation">
        View useTransition documentation
      </Button>
    </template>
    <Card title="Basic usage">
      <div class="flex w-full items-center justify-center pb-4">
        <CountTo v-bind="props" @started="onStarted" @finished="onFinished" />
      </div>
      <Form :model="props">
        <Row :gutter="20">
          <Col :span="8">
            <FormItem label="Initial value" name="startVal">
              <InputNumber v-model:value="props.startVal" />
            </FormItem>
          </Col>
          <Col :span="8">
            <FormItem label="current value" name="endVal">
              <InputNumber
                v-model:value="props.endVal"
                class="w-full"
                :precision="props.decimals"
              >
                <template #addonAfter>
                  <IconifyIcon
                    v-tippy="`Set a random value`"
                    class="size-5 cursor-pointer outline-none"
                    icon="ix:random-filled"
                    @click="changeNumber"
                  />
                </template>
              </InputNumber>
            </FormItem>
          </Col>
          <Col :span="8">
            <FormItem label="Disable animation" name="disabled">
              <Switch v-model="props.disabled" />
            </FormItem>
          </Col>
          <Col :span="8">
            <FormItem label="delay animation" name="delay">
              <InputNumber v-model:value="props.delay" :min="0" />
            </FormItem>
          </Col>
          <Col :span="8">
            <FormItem label="Duration" name="duration">
              <InputNumber v-model:value="props.duration" :min="0" />
            </FormItem>
          </Col>

          <Col :span="8">
            <FormItem label="decimal places" name="decimals">
              <InputNumber
                v-model:value="props.decimals"
                :min="0"
                :precision="0"
              />
            </FormItem>
          </Col>
          <Col :span="8">
            <FormItem label="separator" name="separator">
              <Input v-model:value="props.separator" />
            </FormItem>
          </Col>
          <Col :span="8">
            <FormItem label="decimal point" name="decimal">
              <Input v-model:value="props.decimal" />
            </FormItem>
          </Col>
          <Col :span="8">
            <FormItem label="animation" name="transition">
              <Select v-model:value="props.transition">
                <Select.Option
                  v-for="preset in TransitionPresetsKeys"
                  :key="preset"
                  :value="preset"
                >
                  {{ preset }}
                </Select.Option>
              </Select>
            </FormItem>
          </Col>
          <Col :span="8">
            <FormItem label="prefix" name="prefix">
              <Input v-model:value="props.prefix" />
            </FormItem>
          </Col>
          <Col :span="8">
            <FormItem label="suffix" name="suffix">
              <Input v-model:value="props.suffix" />
            </FormItem>
          </Col>
        </Row>
      </Form>
    </Card>
  </Page>
</template>
