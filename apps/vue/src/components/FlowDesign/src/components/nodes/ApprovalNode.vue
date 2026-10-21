<template>
  <Node
    :title="config.name"
    :show-error="state.showError"
    :content="content"
    :error-info="state.errorInfo"
    @selected="$emit('selected')"
    @delNode="$emit('delNode')"
    @insertNode="(type) => $emit('insertNode', type)"
    placeholder="Please set the approver"
    header-bgc="#ff943e"
  >
    <template #headerIcon>
      <UserOutlined />
    </template>
  </Node>
</template>

<script setup lang="ts">
  import { computed, reactive } from 'vue';
  import { UserOutlined } from '@ant-design/icons-vue';
  import { useFlowStoreWithOut } from '/@/store/modules/flow';
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
    console.log('props', props);
    const config = props.config.props;
    switch (config.assignedType) {
      case 'ASSIGN_USER':
        if (config.assignedUser.length > 0) {
          let texts: string[] = [];
          config.assignedUser.forEach((org) => texts.push(org.name));
          return String(texts).replaceAll(',', '、');
        } else {
          return 'Please specify the approver';
        }
      case 'SELF':
        return 'initiator himself';
      case 'SELF_SELECT':
        return config.selfSelect.multiple ? 'The initiator selects multiple people' : 'The initiator selects one person';
      case 'FORM_USER':
        if (!config.formUser || config.formUser === '') {
          return 'Contact in the form (not selected)';
        } else {
          let text = getFormItemById(config.formUser);
          if (text && text.title) {
            return `People in the form (${text.title})`;
          } else {
            return 'The form has been removed😥';
          }
        }
      case 'ROLE':
        if (config.role.length > 0) {
          let texts: string[] = [];
          config.role.forEach((org) => texts.push(org.name));
          return String(texts).replaceAll(',', '、');
        } else {
          return 'Specified role (not set)';
        }
      default:
        return 'Unknown setting item😥';
    }
  });
  const state = reactive({
    showError: false,
    errorInfo: '',
    validate: {} as any,
  });
  const flowStore = useFlowStoreWithOut();

  function getFormItemById(id: any) {
    return flowStore.design.formItems.find((item) => item.id === id);
  }

  function validate(err: string[]) {
    try {
      return (state.showError =
        !state.validate[`validate_${props.config.props.assignedType}`](err));
    } catch (e) {
      return true;
    }
  }

  function validate_ASSIGN_USER(err: string[]) {
    if (props.config.props.assignedUser.length > 0) {
      return true;
    } else {
      state.errorInfo = 'Please specify the approver';
      err.push(`${props.config.name} No approver specified`);
      return false;
    }
  }

  function validate_SELF_SELECT(err: string[]) {
    console.log(err);
    return true;
  }

  function validate_LEADER_TOP(err: string[]) {
    console.log(err);
    return true;
  }

  function validate_LEADER(err: string[]) {
    console.log(err);
    return true;
  }

  function validate_ROLE(err: string[]) {
    if (props.config.props.role.length <= 0) {
      state.errorInfo = 'Please specify the system role responsible for approval';
      err.push(`${props.config.name} does not specify an approval role`);
      return false;
    }
    return true;
  }

  function validate_SELF(err: string[]) {
    console.log(err);
    return true;
  }

  function validate_FORM_USER(err: string[]) {
    if (props.config.props.formUser === '') {
      state.errorInfo = 'Please specify the personnel component in the form';
      err.push(`${props.config.name} The approver is the person in the form, but it is not specified`);
      return false;
    }
    return true;
  }

  function validate_REFUSE(err: string[]) {
    console.log(err);
    return true;
  }

  defineExpose({
    validate,
    validate_ASSIGN_USER,
    validate_SELF_SELECT,
    validate_LEADER_TOP,
    validate_LEADER,
    validate_ROLE,
    validate_SELF,
    validate_FORM_USER,
    validate_REFUSE,
  });
</script>

<style scoped></style>
