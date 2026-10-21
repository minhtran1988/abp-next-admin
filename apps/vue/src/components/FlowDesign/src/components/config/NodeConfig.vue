<template>
  <div>
    <Tabs v-model="state.active" v-if="name && formConfig.length > 0">
      <TabPane :label="name" name="properties">
        <component
          :is="componentRef[(selectNode.type || '').toLowerCase()]"
          :config="selectNode.props"
        />
      </TabPane>
      <!-- <TabPane label="Form permission settings" name="permissions">
        <form-authority-config/>
      </TabPane> -->
    </Tabs>
    <component
      :is="componentRef[(selectNode.type || '').toLowerCase()]"
      v-else
      :config="selectNode.props"
    />
  </div>
</template>

<script lang="ts" setup>
  import { computed, reactive, shallowRef } from 'vue';
  import { Tabs } from 'ant-design-vue';
  import { useFlowStoreWithOut } from '/@/store/modules/flow';
  import Approval from './ApprovalNodeConfig.vue';
  import Trigger from './TriggerNodeConfig.vue';
  // import FormAuthorityConfig from './FormAuthorityConfig.vue'
  import Root from './RootNodeConfig.vue';
  import HttpEndPoint from './HttpEndPointNodeConfig.vue';

  const TabPane = Tabs.TabPane;
  const componentRef = shallowRef({
    root: Root,
    approval: Approval,
    trigger: Trigger,
    httpendpoint: HttpEndPoint,
  });
  const flowStore = useFlowStoreWithOut();
  const selectNode = computed(() => {
    return flowStore.selectedNode;
  });
  const formConfig = computed(() => {
    return flowStore.design.formItems;
  });
  const name = computed(() => {
    switch (selectNode.value.type) {
      case 'ROOT':
        return 'Set initiator';
      case 'APPROVAL':
        return 'Set approver';
      case 'CC':
        return 'Set CC';
      default:
        return undefined;
    }
  });
  const state = reactive({
    active: 'properties',
  });
</script>

<style lang="less" scoped></style>
