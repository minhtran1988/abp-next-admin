<template>
  <div class="end-point">
    <Form layout="vertical">
      <Tabs v-model:activeKey="state.activeKey">
        <TabPane key="common" tab="Common">
          <FormItem label="name" extra="Name of the activity." name="name">
            <Input v-model:value="nodeProps.name" />
          </FormItem>
          <FormItem label="Display Name" extra="Display name of the activity." name="displayName">
            <Input v-model:value="nodeProps.displayName" />
          </FormItem>
          <FormItem label="Description" extra="Description of the activity." name="description">
            <TextArea v-model:value="nodeProps.description" show-count :autoSize="{ minRows: 3 }" />
          </FormItem>
        </TabPane>
        <TabPane key="properties" tab="Properties">
          <FormItem label="path" extra="The relative path that triggered this activity" name="path">
            <Input v-model:value="nodeProps.path" />
          </FormItem>
          <FormItem label="Request Method" extra="HTTP method that triggered this activity." name="methods">
            <Select v-model:value="nodeProps.methods" mode="multiple">
              <SelectOption value="GET">GET</SelectOption>
              <SelectOption value="POST">POST</SelectOption>
              <SelectOption value="PUT">PUT</SelectOption>
              <SelectOption value="PATCH">PATCH</SelectOption>
              <SelectOption value="DELETE">DELETE</SelectOption>
              <SelectOption value="OPTIONS">OPTIONS</SelectOption>
              <SelectOption value="HEAD">HEAD</SelectOption>
            </Select>
          </FormItem>
          <FormItem
            label="read content"
            extra="Indicates whether the HTTP request content body should be read and stored as part of the HTTP request model. The format of storage depends on the content-type header."
            name="readContent"
            class="user-type"
          >
            <Checkbox v-model:checked="nodeProps.readContent" />
          </FormItem>
        </TabPane>
        <TabPane key="advanced" tab="Advanced">
          <FormItem label="data type" extra="form data type" name="targetType">
            <Input v-model:value="nodeProps.targetType" />
          </FormItem>
          <FormItem label="Schema" extra="Json format data schema" name="schema">
            <CodeEditor v-model:value="nodeProps.schema" />
          </FormItem>
        </TabPane>
        <TabPane key="security" tab="Security">
          <FormItem label="Authentication" extra="Check to allow only authenticated requests" name="authorize">
            <Checkbox v-model:checked="nodeProps.authorize" />
          </FormItem>
          <FormItem
            label="strategy"
            extra="Provide a policy to be evaluated. If the policy fails, the request will be denied."
            name="policy"
          >
            <Input v-model:value="nodeProps.policy" />
          </FormItem>
        </TabPane>
      </Tabs>
    </Form>
  </div>
</template>

<script setup lang="ts">
  import { computed, reactive } from 'vue';
  import { Checkbox, Form, Input, Select, Tabs } from 'ant-design-vue';
  import { CodeEditor } from '/@/components/CodeEditor';
  import { useFlowStoreWithOut } from '/@/store/modules/flow';

  const FormItem = Form.Item;
  const SelectOption = Select.Option;
  const TabPane = Tabs.TabPane;
  const TextArea = Input.TextArea;

  defineProps({
    config: {
      type: Object,
      default: () => {
        return {};
      },
    },
  });
  const flowStore = useFlowStoreWithOut();
  const nodeProps = computed(() => {
    return flowStore.selectedNode.props;
  });
  const state = reactive({
    activeKey: 'common',
  });
</script>

<style scoped></style>
