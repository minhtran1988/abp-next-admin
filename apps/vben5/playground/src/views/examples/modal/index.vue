<script lang="ts" setup>
import { onBeforeUnmount } from 'vue';

import {
  alert,
  clearAllAlerts,
  confirm,
  Page,
  prompt,
  useVbenModal,
} from '@vben/common-ui';

import { Button, Card, Flex, message } from 'ant-design-vue';

import DocButton from '../doc-button.vue';
import AutoHeightDemo from './auto-height-demo.vue';
import BaseDemo from './base-demo.vue';
import BlurDemo from './blur-demo.vue';
import DragDemo from './drag-demo.vue';
import DynamicDemo from './dynamic-demo.vue';
import FormModalDemo from './form-modal-demo.vue';
import InContentModalDemo from './in-content-demo.vue';
import NestedDemo from './nested-demo.vue';
import SharedDataDemo from './shared-data-demo.vue';

defineOptions({ name: 'ModalExample' });

const [BaseModal, baseModalApi] = useVbenModal({
  // Connect the detached components
  connectedComponent: BaseDemo,
});

const [InContentModal, inContentModalApi] = useVbenModal({
  // Connect the detached components
  connectedComponent: InContentModalDemo,
});

const [AutoHeightModal, autoHeightModalApi] = useVbenModal({
  connectedComponent: AutoHeightDemo,
});

const [DragModal, dragModalApi] = useVbenModal({
  connectedComponent: DragDemo,
});

const [DynamicModal, dynamicModalApi] = useVbenModal({
  connectedComponent: DynamicDemo,
});

const [SharedDataModal, sharedModalApi] = useVbenModal({
  connectedComponent: SharedDataDemo,
});

const [FormModal, formModalApi] = useVbenModal({
  connectedComponent: FormModalDemo,
});

const [NestedModal, nestedModalApi] = useVbenModal({
  connectedComponent: NestedDemo,
});

const [BlurModal, blurModalApi] = useVbenModal({
  connectedComponent: BlurDemo,
});

function openBaseModal() {
  baseModalApi.open();
}

function openInContentModal() {
  inContentModalApi.open();
}

function openAutoHeightModal() {
  autoHeightModalApi.open();
}

function openDragModal() {
  dragModalApi.open();
}

function openDynamicModal() {
  dynamicModalApi.open();
}

function openSharedModal() {
  sharedModalApi
    .setData({
      content: 'Externally passed data content',
      payload: 'Externally passed data payload',
    })
    .open();
}

function openNestedModal() {
  nestedModalApi.open();
}

function openBlurModal() {
  blurModalApi.open();
}

function handleUpdateTitle() {
  dynamicModalApi.setState({ title: 'External dynamic title' }).open();
}

function openFormModal() {
  formModalApi
    .setData({
      // form value
      values: { field1: 'abc', field2: '123' },
    })
    .open();
}

function openAlert() {
  alert({
    content: 'This is a pop-up window',
    icon: 'success',
  }).then(() => {
    message.info('The user closed the pop-up window');
  });
}

onBeforeUnmount(() => {
  // Clear all pop-ups
  clearAllAlerts();
});

function openConfirm() {
  confirm({
    beforeClose({ isConfirm }) {
      if (!isConfirm) return;
      // You can do some asynchronous operations here
      return new Promise((resolve) => {
        setTimeout(() => {
          resolve(true);
        }, 1000);
      });
    },
    centered: false,
    content: 'This is a confirmation pop-up window',
    icon: 'question',
  })
    .then(() => {
      message.success('The user confirmed the operation');
    })
    .catch(() => {
      message.error('The user canceled the operation');
    });
}

async function openPrompt() {
  prompt<string>({
    async beforeClose({ isConfirm, value }) {
      if (isConfirm && value === 'Cheese') {
        message.error('Cannot eat cheese');
        return false;
      }
    },
    componentProps: { placeholder: 'Cannot eat cheese...' },
    content: 'What did you eat for lunch? ',
    icon: 'question',
    overlayBlur: 3,
  })
    .then((res) => {
      message.success(`The user entered: ${res}`);
    })
    .catch(() => {
      message.error('The user canceled the input');
    });
}
</script>

<template>
  <Page
    auto-content-height
    description="Pop-up components are often used to display additional information, forms or operation prompts without leaving the current page. For more APIs, please view the component documentation."
    title="Pop-up component example"
  >
    <template #extra>
      <DocButton path="/components/common-ui/vben-modal" />
    </template>
    <BaseModal />
    <InContentModal />
    <AutoHeightModal />
    <DragModal />
    <DynamicModal />
    <SharedDataModal />
    <FormModal />
    <NestedModal />
    <BlurModal />
    <Flex wrap="wrap" class="w-full" gap="10">
      <Card class="w-[300px]" title="Basic use">
        <p>A basic pop-up window example</p>
        <template #actions>
          <Button type="primary" @click="openBaseModal">Open pop-up window</Button>
        </template>
      </Card>

      <Card class="w-[300px]" title="Specify container + will not be destroyed after closing">
        <p>Example of opening a pop-up window in the content area</p>
        <template #actions>
          <Button type="primary" @click="openInContentModal">Open pop-up window</Button>
        </template>
      </Card>

      <Card class="w-[300px]" title="Content highly adaptive">
        <p>The height can be automatically adjusted according to the content</p>
        <template #actions>
          <Button type="primary" @click="openAutoHeightModal">
            Open pop-up window
          </Button>
        </template>
      </Card>

      <Card class="w-[300px]" title="Dragable example">
        <p>Configure draggable to enable drag and drop function</p>
        <template #actions>
          <Button type="primary" @click="openDragModal"> Open pop-up window </Button>
        </template>
      </Card>

      <Card class="w-[300px]" title="Dynamic configuration example">
        <p>Dynamicly adjust pop-up window data through setState</p>
        <template #extra>
          <Button type="link" @click="openDynamicModal">Open pop-up window</Button>
        </template>
        <template #actions>
          <Button type="primary" @click="handleUpdateTitle">
            Modify title externally and open
          </Button>
        </template>
      </Card>

      <Card class="w-[300px]" title="Example of internal and external data sharing">
        <p>Data interaction by sharing sharedData</p>
        <template #actions>
          <Button type="primary" @click="openSharedModal">
            Open pop-up window and pass data
          </Button>
        </template>
      </Card>

      <Card class="w-[300px]" title="Form pop-up example">
        <p>Combining pop-up windows with forms</p>
        <template #actions>
          <Button type="primary" @click="openFormModal"> Open the form pop-up window </Button>
        </template>
      </Card>

      <Card class="w-[300px]" title="Nested pop-up window example">
        <p>Open the pop-up window again in the already opened pop-up window</p>
        <template #actions>
          <Button type="primary" @click="openNestedModal">Open nested pop-up window</Button>
        </template>
      </Card>

      <Card class="w-[300px]" title="Mask blur example">
        <p>Apply a blur effect similar to frosted glass to the mask layer</p>
        <template #actions>
          <Button type="primary" @click="openBlurModal">Open pop-up window</Button>
        </template>
      </Card>
      <Card class="w-[300px]" title="Lightweight prompt pop-up">
        <template #extra>
          <DocButton path="/components/common-ui/vben-alert" />
        </template>
        <p>Create dynamic prompt pop-up windows through shortcut methods, suitable for some lightweight prompts, confirmations, inputs, etc.</p>
        <template #actions>
          <Button type="primary" @click="openAlert">Alert</Button>
          <Button type="primary" @click="openConfirm">Confirm</Button>
          <Button type="primary" @click="openPrompt">Prompt</Button>
        </template>
      </Card>
    </Flex>
  </Page>
</template>
