<script lang="ts" setup>
import type { DrawerPlacement, DrawerState } from '@vben/common-ui';

import { Page, useVbenDrawer } from '@vben/common-ui';

import { Button, Card } from 'ant-design-vue';

import DocButton from '../doc-button.vue';
import AutoHeightDemo from './auto-height-demo.vue';
import BaseDemo from './base-demo.vue';
import DynamicDemo from './dynamic-demo.vue';
import FormDrawerDemo from './form-drawer-demo.vue';
import inContentDemo from './in-content-demo.vue';
import SharedDataDemo from './shared-data-demo.vue';

defineOptions({ name: 'DrawerExample' });
const [BaseDrawer, baseDrawerApi] = useVbenDrawer({
  // Connect the detached components
  connectedComponent: BaseDemo,
  // placement: 'left',
});

const [InContentDrawer, inContentDrawerApi] = useVbenDrawer({
  // Connect the detached components
  connectedComponent: inContentDemo,
  // placement: 'left',
});

const [AutoHeightDrawer, autoHeightDrawerApi] = useVbenDrawer({
  connectedComponent: AutoHeightDemo,
});

const [DynamicDrawer, dynamicDrawerApi] = useVbenDrawer({
  connectedComponent: DynamicDemo,
});

const [SharedDataDrawer, sharedDrawerApi] = useVbenDrawer({
  connectedComponent: SharedDataDemo,
});

const [FormDrawer, formDrawerApi] = useVbenDrawer({
  connectedComponent: FormDrawerDemo,
});

function openBaseDrawer(placement: DrawerPlacement = 'right') {
  baseDrawerApi.setState({ placement }).open();
}

function openBlurDrawer() {
  baseDrawerApi.setState({ overlayBlur: 5 }).open();
}

function openInContentDrawer(placement: DrawerPlacement = 'right') {
  const state: Partial<DrawerState> = { class: '', placement };
  if (placement === 'top') {
    // The level of the top area of the page is only 200, so setting a value lower than 200 is more appropriate when the drawer slides out from the top.
    state.zIndex = 199;
  }
  inContentDrawerApi.setState(state).open();
}

function openMaxContentDrawer() {
  // This is just for demonstration convenience. In fact, when you use it yourself, you can directly uninstall these configurations in the Drawer properties.
  inContentDrawerApi.setState({ class: 'w-full', placement: 'right' }).open();
}

function openAutoHeightDrawer() {
  autoHeightDrawerApi.open();
}

function openDynamicDrawer() {
  dynamicDrawerApi.open();
}

function handleUpdateTitle() {
  dynamicDrawerApi.setState({ title: 'External dynamic title' }).open();
}

function openSharedDrawer() {
  sharedDrawerApi
    .setData({
      content: 'Externally passed data content',
      payload: 'Externally passed data payload',
    })
    .open();
}

function openFormDrawer() {
  formDrawerApi
    .setData({
      // form value
      values: { field1: 'abc', field2: '123' },
    })
    .open();
}
</script>

<template>
  <Page
    auto-content-height
    description="The drawer component is usually used to display an overlay on the current page to display important information or provide a user interaction interface."
    title="Drawer component example"
  >
    <template #extra>
      <DocButton path="/components/common-ui/vben-drawer" />
    </template>
    <BaseDrawer />
    <InContentDrawer />
    <AutoHeightDrawer />
    <DynamicDrawer />
    <SharedDataDrawer />
    <FormDrawer />

    <Card class="mb-4" title="Basic use">
      <p class="mb-3">A basic drawer example</p>
      <Button class="mb-2" type="primary" @click="openBaseDrawer('right')">
        Open on the right
      </Button>
      <Button
        class="mb-2 ml-2"
        type="primary"
        @click="openBaseDrawer('bottom')"
      >
        bottom open
      </Button>
      <Button class="mb-2 ml-2" type="primary" @click="openBaseDrawer('left')">
        Open left
      </Button>
      <Button class="mb-2 ml-2" type="primary" @click="openBaseDrawer('top')">
        top open
      </Button>
      <Button class="mb-2 ml-2" type="primary" @click="openBlurDrawer">
        Mask layer blur effect
      </Button>
    </Card>

    <Card class="mb-4" title="Open in content area">
      <p class="mb-3">Specify that the drawer opens in the content area and does not cover areas such as the top and left menus</p>
      <Button class="mb-2" type="primary" @click="openInContentDrawer('right')">
        Open on the right
      </Button>
      <Button
        class="mb-2 ml-2"
        type="primary"
        @click="openInContentDrawer('bottom')"
      >
        bottom open
      </Button>
      <Button
        class="mb-2 ml-2"
        type="primary"
        @click="openInContentDrawer('left')"
      >
        Open left
      </Button>
      <Button
        class="mb-2 ml-2"
        type="primary"
        @click="openInContentDrawer('top')"
      >
        top open
      </Button>
      <Button class="mb-2 ml-2" type="primary" @click="openMaxContentDrawer">
        Open content area full screen
      </Button>
    </Card>

    <Card class="mb-4" title="Content height adaptive scrolling">
      <p class="mb-3">Can automatically calculate scroll height based on content</p>
      <Button type="primary" @click="openAutoHeightDrawer">Open drawer</Button>
    </Card>

    <Card class="mb-4" title="Dynamic configuration example">
      <p class="mb-3">Dynamicly adjust drawer data through setState</p>
      <Button type="primary" @click="openDynamicDrawer">Open drawer</Button>
      <Button class="ml-2" type="primary" @click="handleUpdateTitle">
        Modify title externally and open
      </Button>
    </Card>

    <Card class="mb-4" title="Examples of internal and external data sharing">
      <p class="mb-3">Data interaction by sharing sharedData</p>
      <Button type="primary" @click="openSharedDrawer">
        Open the drawer and pass the data
      </Button>
    </Card>

    <Card class="mb-4" title="Form drawer example">
      <p class="mb-3">Open the drawer and set the form schema and data</p>
      <Button type="primary" @click="openFormDrawer">
        Open the drawer and set the form schema and data
      </Button>
    </Card>
  </Page>
</template>
