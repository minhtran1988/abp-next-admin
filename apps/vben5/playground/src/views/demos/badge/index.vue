<script lang="ts" setup>
import { reactive } from 'vue';
import { useRoute } from 'vue-router';

import { Page } from '@vben/common-ui';
import { useAccessStore } from '@vben/stores';

import { MenuBadge } from '@vben-core/menu-ui';

import { Button, Card, Radio, RadioGroup } from 'ant-design-vue';

import { useVbenForm } from '#/adapter/form';

const colors = [
  { label: 'Default: default', value: 'default' },
  { label: 'Default: key', value: 'destructive' },
  { label: 'Default: primary', value: 'primary' },
  { label: 'Default: success', value: 'success' },
  { label: 'custom', value: 'bg-gray-200 text-black' },
];

const route = useRoute();
const accessStore = useAccessStore();
const menu = accessStore.getMenuByPath(route.path);
const badgeProps = reactive({
  badge: menu?.badge as string,
  badgeType: menu?.badge ? 'normal' : (menu?.badgeType as 'dot' | 'normal'),
  badgeVariants: menu?.badgeVariants as string,
});

const [Form] = useVbenForm({
  handleValuesChange(values) {
    badgeProps.badge = values.badge;
    badgeProps.badgeType = values.badgeType;
    badgeProps.badgeVariants = values.badgeVariants;
  },
  schema: [
    {
      component: 'RadioGroup',
      componentProps: {
        buttonStyle: 'solid',
        options: [
          { label: 'dot logo', value: 'dot' },
          { label: 'text logo', value: 'normal' },
        ],
        optionType: 'button',
      },
      defaultValue: badgeProps.badgeType,
      fieldName: 'badgeType',
      label: 'type',
    },
    {
      component: 'Input',
      componentProps: {
        maxLength: 4,
        placeholder: 'Please enter logo content',
        style: { width: '200px' },
      },
      defaultValue: badgeProps.badge,
      fieldName: 'badge',
      label: 'Logo content',
    },
    {
      component: 'RadioGroup',
      defaultValue: badgeProps.badgeVariants,
      fieldName: 'badgeVariants',
      label: 'color',
    },
    {
      component: 'Input',
      fieldName: 'action',
    },
  ],
  showDefaultActions: false,
});

function updateMenuBadge() {
  if (menu) {
    menu.badge = badgeProps.badge;
    menu.badgeType = badgeProps.badgeType;
    menu.badgeVariants = badgeProps.badgeVariants;
  }
}
</script>

<template>
  <Page
    description="Logos can be displayed on menu items, and these logos can be actively updated"
    title="menu logo"
  >
    <Card title="Logo Update">
      <Form>
        <template #badgeVariants="slotProps">
          <RadioGroup v-bind="slotProps">
            <Radio
              v-for="color in colors"
              :key="color.value"
              :value="color.value"
            >
              <div
                :title="color.label"
                class="flex h-[14px] w-[50px] items-center justify-start"
              >
                <MenuBadge
                  v-bind="{ ...badgeProps, badgeVariants: color.value }"
                />
              </div>
            </Radio>
          </RadioGroup>
        </template>
        <template #action>
          <Button type="primary" @click="updateMenuBadge">Update logo</Button>
        </template>
      </Form>
    </Card>
  </Page>
</template>
