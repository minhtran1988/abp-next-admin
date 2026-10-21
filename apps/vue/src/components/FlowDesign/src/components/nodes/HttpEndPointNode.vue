<template>
  <Node
    :title="config.name"
    :show-error="state.showError"
    :content="content"
    :error-info="state.errorInfo"
    @selected="$emit('selected')"
    @delNode="$emit('delNode')"
    @insertNode="(type) => $emit('insertNode', type)"
    placeholder="Please set the path to trigger the activity"
    header-bgc="#3296fa"
  >
    <template #headerIcon>
      <GlobalOutlined />
    </template>
  </Node>
</template>

<script setup lang="ts">
  import { computed, reactive } from 'vue';
  import { GlobalOutlined } from '@ant-design/icons-vue';
  import { isNullOrWhiteSpace } from '/@/utils/strings';
  import Node from './Node.vue';

  defineEmits(['delNode', 'insertNode', 'selected']);
  const props = defineProps({
    config: {
      type: Object,
      default: () => {
        return {};
      },
    },
  });
  const content = computed(() => {
    return props.config.props.path;
  });
  const state = reactive({
    showError: false,
    errorInfo: '',
  });

  function validate(err: string[]) {
    state.showError = false;
    if (isNullOrWhiteSpace(props.config.props.name)) {
      state.showError = true;
      state.errorInfo = 'Please set the name of the activity';
      return;
    }
    if (isNullOrWhiteSpace(props.config.props.path)) {
      state.showError = true;
      state.errorInfo = 'Please set the request address that triggers the activity';
      return;
    }
    if (Array.isArray(props.config.props.methods) && props.config.props.methods.length === 0) {
      state.showError = true;
      state.errorInfo = 'Please set the request method that triggers the activity';
      return;
    }
    if (state.showError) {
      err.push(`${props.config.name} activity configuration is not set up properly`);
    }
    return !state.showError;
  }

  defineExpose({
    validate,
  });
</script>

<style scoped></style>
