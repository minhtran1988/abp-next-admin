<script lang="ts" setup>
import { ref } from 'vue';

import { EllipsisText, Page } from '@vben/common-ui';

import { Card } from 'ant-design-vue';

import DocButton from '../doc-button.vue';

const longText = `Vben Admin is a backend solution based on Vue3.0, Vite, and TypeScript. It aims to provide out-of-the-box solutions for the development of medium and large projects. Including secondary encapsulation components, utils, hooks, dynamic menus, permission verification, multi-theme configuration, button-level permission control and other functions. The project will use the latest front-end technology stack, which can be used as a startup template for the project to help you quickly build enterprise-level middle and back-end product prototypes. It can also be used as an example to learn mainstream technologies such as vue3, vite, ts, etc. The project will continue to keep up with the latest technologies and apply them in projects. Vben Admin is a backend solution based on Vue3.0, Vite, and TypeScript. It aims to provide out-of-the-box solutions for the development of medium and large projects. Including secondary encapsulation components, utils, hooks, dynamic menus, permission verification, multi-theme configuration, button-level permission control and other functions. The project will use the latest front-end technology stack, which can be used as a startup template for the project to help you quickly build enterprise-level middle and back-end product prototypes. It can also be used as an example to learn mainstream technologies such as vue3, vite, ts, etc. The project will continue to keep up with the latest technologies and apply them in projects. Vben Admin is a backend solution based on Vue3.0, Vite, and TypeScript. It aims to provide out-of-the-box solutions for the development of medium and large projects. Including secondary encapsulation components, utils, hooks, dynamic menus, permission verification, multi-theme configuration, button-level permission control and other functions. The project will use the latest front-end technology stack, which can be used as a startup template for the project to help you quickly build enterprise-level middle and back-end product prototypes. It can also be used as an example to learn mainstream technologies such as vue3, vite, ts, etc. The project will continue to keep up with the latest technologies and apply them in projects. Vben Admin is a backend solution based on Vue3.0, Vite, and TypeScript. It aims to provide out-of-the-box solutions for the development of medium and large projects. Including secondary encapsulation components, utils, hooks, dynamic menus, permission verification, multi-theme configuration, button-level permission control and other functions. The project will use the latest front-end technology stack, which can be used as a startup template for the project to help you quickly build enterprise-level middle and back-end product prototypes. It can also be used as an example to learn mainstream technologies such as vue3, vite, ts, etc. The project will continue to keep up with the latest technologies and apply them in projects. `;

const text = ref(longText);
</script>

<template>
  <Page
    description="Used for multi-line text omission, supports click expansion and custom content."
    title="Text omitted component example"
  >
    <template #extra>
      <DocButton class="mb-2" path="/components/common-ui/vben-ellipsis-text" />
    </template>
    <Card class="mb-4" title="Basic use">
      <EllipsisText :max-width="500">{{ text }}</EllipsisText>
    </Card>

    <Card class="mb-4" title="Multiple lines omitted">
      <EllipsisText :line="2">{{ text }}</EllipsisText>
    </Card>

    <Card class="mb-4" title="Click to expand">
      <EllipsisText :line="3" expand>{{ text }}</EllipsisText>
    </Card>
    <Card class="mb-4" title="Custom content">
      <EllipsisText :max-width="240">
        The lonely sea monster who lives in my heart, the king of pain, is starting to get tired of the light of the deep sea, the stagnant waves
        <template #tooltip>
          <div style="text-align: center">
            "Qinhuangdao"<br />The loneliness that lives in my heart<br />The lonely sea monster, the king of pain<br />Begins to get tired
            The light of the deep sea, the stagnant waves
          </div>
        </template>
      </EllipsisText>
    </Card>
  </Page>
</template>
