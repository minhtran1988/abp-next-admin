<script lang="ts" setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';

import { Page } from '@vben/common-ui';
import { useTabs } from '@vben/hooks';

import { Button, Card, Input } from 'ant-design-vue';

const router = useRouter();
const newTabTitle = ref('');

const {
  closeAllTabs,
  closeCurrentTab,
  closeLeftTabs,
  closeOtherTabs,
  closeRightTabs,
  closeTabByKey,
  refreshTab,
  resetTabTitle,
  setTabTitle,
} = useTabs();

function openTab() {
  // Here is the route jump, you can also use path
  router.push({ name: 'VbenAbout' });
}

function openTabWithParams(id: number) {
  // Here is the route jump, you can also use path
  router.push({ name: 'FeatureTabDetailDemo', params: { id } });
}

function reset() {
  newTabTitle.value = '';
  resetTabTitle();
}
</script>

<template>
  <Page description="Used in scenarios where tab pages need to be operated" title="Tab pages">
    <Card class="mb-5" title="Open/Close tab">
      <div class="mb-3 text-foreground/80">
        If the tab page exists, jump to switch directly. If the tab does not exist, open a new tab.
      </div>
      <div class="flex flex-wrap gap-3">
        <Button type="primary" @click="openTab"> Open the "About" tab </Button>
        <Button type="primary" @click="closeTabByKey('/vben-admin/about')">
          Close the "About" tab
        </Button>
      </div>
    </Card>

    <Card class="mb-5" title="Tab operations">
      <div class="mb-3 text-foreground/80">Used to dynamically control various operations of tabs</div>
      <div class="flex flex-wrap gap-3">
        <Button type="primary" @click="closeCurrentTab()">
          Close current tab
        </Button>
        <Button type="primary" @click="closeLeftTabs()">
          Close left tab
        </Button>
        <Button type="primary" @click="closeRightTabs()">
          Close the tab on the right
        </Button>
        <Button type="primary" @click="closeAllTabs()"> Close all tabs </Button>
        <Button type="primary" @click="closeOtherTabs()">
          Close other tabs
        </Button>
        <Button type="primary" @click="refreshTab()"> Refresh the current tab </Button>
      </div>
    </Card>

    <Card class="mb-5" title="Dynamic title">
      <div class="mb-3 text-foreground/80">
        This operation will not affect the page title, only modify the Tab title
      </div>
      <div class="flex flex-wrap items-center gap-3">
        <Input
          v-model:value="newTabTitle"
          class="w-40"
          placeholder="Please enter a new title"
        />
        <Button type="primary" @click="() => setTabTitle(newTabTitle)">
          Modify
        </Button>
        <Button @click="reset"> Reset </Button>
      </div>
    </Card>

    <Card class="mb-5" title="Maximum number of openings">
      <div class="mb-3 text-foreground/80">
        Limit the maximum number of tabs opened with parameters, controlled by `route.meta.maxNumOfOpenTab`
      </div>
      <div class="flex flex-wrap items-center gap-3">
        <template v-for="item in 5" :key="item">
          <Button type="primary" @click="openTabWithParams(item)">
            Open the {{ item }} details page
          </Button>
        </template>
      </div>
    </Card>
  </Page>
</template>
