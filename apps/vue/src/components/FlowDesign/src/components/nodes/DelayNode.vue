<template>
  <Node
    :title="config.name"
    :show-error="state.showError"
    :content="content"
    :error-info="state.errorInfo"
    @selected="$emit('selected')"
    @delNode="$emit('delNode')"
    @insertNode="(type) => $emit('insertNode', type)"
    placeholder="Please set the delay time"
    header-bgc="#f25643"
  >
    <template #headerIcon>
      <ClockCircleOutlined />
    </template>
  </Node>
</template>

<script lang="ts">
  export default {
    name: 'Delay',
  };
</script>

<script setup lang="ts">
  import { computed, reactive } from 'vue';
  import { ClockCircleOutlined } from '@ant-design/icons-vue';
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
  const state = reactive({
    showError: false,
    errorInfo: '',
  });

  const content = computed(() => {
    if (props.config.props.type === 'FIXED') {
      return `Wait ${props.config.props.time} ${getName(props.config.props.unit)}`;
    } else if (props.config.props.type === 'AUTO') {
      return `To the current day ${props.config.props.dateTime}`;
    } else {
      return undefined;
    }
  });

  function getName(unit: string) {
    switch (unit) {
      case 'D':
        return 'day';
      case 'H':
        return 'hour';
      case 'M':
        return 'minutes';
      default:
        return 'unknown';
    }
  }

  function validate(err: string[]) {
    state.showError = false;
    try {
      if (props.config.props.type === 'AUTO') {
        if ((props.config.props.dateTime || '') === '') {
          state.showError = true;
          state.errorInfo = 'Please select a time point';
        }
      } else {
        if (props.config.props.time <= 0) {
          state.showError = true;
          state.errorInfo = 'Please set the delay length';
        }
      }
    } catch (e) {
      state.showError = true;
      state.errorInfo = 'There is a configuration problem';
    }
    if (state.showError) {
      err.push(`${props.config.name} has no delay rule set`);
    }
    return !state.showError;
  }

  defineExpose({
    validate,
  });
</script>

<style scoped></style>
