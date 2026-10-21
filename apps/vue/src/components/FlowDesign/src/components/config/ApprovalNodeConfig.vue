<template>
  <div>
    <Form layout="vertical">
      <FormItem label="⚙ Select approval objects" name="text" class="user-type">
        <RadioGroup v-model:value="nodeProps.assignedType">
          <Radio v-for="t in state.approvalTypes" :value="t.type" :key="t.type">{{ t.name }}</Radio>
        </RadioGroup>
        <div v-if="nodeProps.assignedType === 'ASSIGN_USER'">
          <Button size="small" type="primary" @click="handleSelectUser" round>
            <template #icon>
              <PlusOutlined />
            </template>
            Select people
          </Button>
          <OrgItems v-model:value="nodeProps.assignedUser" />
        </div>
        <div v-else-if="nodeProps.assignedType === 'SELF_SELECT'">
          <RadioGroup size="small" v-model:value="nodeProps.selfSelect.multiple">
            <RadioButton :value="false">Choose a person</RadioButton>
            <RadioButton :value="true">Select multiple people</RadioButton>
          </RadioGroup>
        </div>
        <div v-else-if="nodeProps.assignedType === 'ROLE'">
          <Button size="small" type="primary" @click="handleSelectRole" round>
            <template #icon>
              <PlusOutlined />
            </template>
            Select system role
          </Button>
          <OrgItems v-model:value="nodeProps.role" />
        </div>
        <div v-else-if="nodeProps.assignedType === 'FORM_USER'">
          <FormItem label="Select form contact item" name="text" class="approve-end">
            <Select
              style="width: 80%"
              size="small"
              v-model:value="nodeProps.formUser"
              placeholder="Please select the form item containing the contact"
            >
              <SelectOption v-for="op in forms" :value="op.id">{{ op.title }}</SelectOption>
            </Select>
          </FormItem>
        </div>
        <div v-else>
          <span class="item-desc">The initiator acts as the approver himself</span>
        </div>
      </FormItem>

      <Divider></Divider>
      <FormItem label="👤 When the approver is empty" name="text" class="line-mode">
        <RadioGroup v-model:value="nodeProps.nobody.handler">
          <Radio value="TO_PASS">Auto pass</Radio>
          <Radio value="TO_REFUSE">Automatically reject</Radio>
          <Radio value="TO_ADMIN">Transfer to approval administrator</Radio>
          <Radio value="TO_USER">Forward to designated person</Radio>
        </RadioGroup>

        <div style="margin-top: 10px" v-if="nodeProps.nobody.handler === 'TO_USER'">
          <Button size="small" type="primary" @click="handleSelectNoSetUser" round>
            <template #icon>
              <PlusOutlined />
            </template>
            Select people</Button
          >
          <OrgItems v-model:value="nodeProps.assignedUser" />
        </div>
      </FormItem>

      <div v-if="showMode">
        <Divider />
        <FormItem label="👩‍👦‍👦 Approval mode when multiple people approve" name="text" class="approve-mode">
          <RadioGroup v-model:value="nodeProps.mode">
            <Radio value="NEXT">Countersign (approval in order of selection, everyone must agree)</Radio>
            <Radio value="AND">Countersign (can be approved at the same time, everyone must agree)</Radio>
            <Radio value="OR">Or sign (only one person agrees)</Radio>
          </RadioGroup>
        </FormItem>
      </div>

      <Divider>Advanced Settings</Divider>
      <FormItem label="✍ Whether a signature is required for approval" name="text">
        <Switch
          checked-children="required"
          un-checked-children="No"
          v-model:checked="nodeProps.sign"
        ></Switch>
        <Tooltip
          class="item"
          effect="dark"
          content="If signature is required globally, it will not take effect here"
          placement="top"
        >
          <QuestionOutlined style="margin-left: 10px; font-size: medium; color: #b0b0b1" />
        </Tooltip>
      </FormItem>
      <FormItem label="⏱ Approval period (if it is 0, it will not take effect)" name="timeLimit">
        <InputGroup style="width: 180px" compact>
          <Input
            style="width: 100px"
            placeholder="duration"
            size="small"
            type="number"
            v-model:value="nodeProps.timeLimit.timeout.value"
          />
          <Select
            style="width: 75px"
            v-model:value="nodeProps.timeLimit.timeout.unit"
            size="small"
            placeholder="Please select"
          >
            <SelectOption value="D">Days</SelectOption>
            <SelectOption value="H">Hour</SelectOption>
            <SelectOption value="M">Minutes</SelectOption>
          </Select>
        </InputGroup>
      </FormItem>
      <FormItem
        label="Executed after the approval period expires"
        name="level"
        v-if="nodeProps.timeLimit.timeout.value > 0"
      >
        <RadioGroup v-model:value="nodeProps.timeLimit.handler.type">
          <Radio value="PASS">Auto pass</Radio>
          <Radio value="REFUSE">Automatically reject</Radio>
          <Radio value="NOTIFY">Send reminder</Radio>
        </RadioGroup>
        <div v-if="nodeProps.timeLimit.handler.type === 'NOTIFY'">
          <div style="color: #409eef; font-size: small">Remind the current approver by default</div>
          <Switch
            checked-children="once"
            un-checked-children="loop"
            v-model:checked="nodeProps.timeLimit.handler.notify.once"
          ></Switch>
          <span style="margin-left: 20px" v-if="!nodeProps.timeLimit.handler.notify.once">
            every
            <InputNumber
              :min="0"
              :max="10000"
              :step="1"
              size="small"
              v-model:value="nodeProps.timeLimit.handler.notify.hour"
            />
            Reminder once every hour
          </span>
        </div>
      </FormItem>
      <FormItem label="🙅‍ If approval is rejected 👇">
        <RadioGroup v-model:value="nodeProps.refuse.type">
          <Radio value="TO_END">End the process directly</Radio>
          <Radio value="TO_BEFORE">Reject to superior approval node</Radio>
          <Radio value="TO_NODE">Reject to specified node</Radio>
        </RadioGroup>
        <div v-if="nodeProps.refuse.type === 'TO_NODE'">
          <span>Specify node:</span>
          <Select
            style="margin-left: 10px; width: 150px"
            placeholder="Select jump step"
            size="small"
            v-model:value="nodeProps.refuse.target"
          >
            <SelectOption v-for="(node, i) in nodeOptions" :key="i" :value="node.id">{{
              node.name
            }}</SelectOption>
          </Select>
        </div>
      </FormItem>
    </Form>
    <OrgPicker
      multiple
      :title="pickerTitle"
      :type="state.orgPickerType"
      ref="orgPickerRef"
      :selected="state.orgPickerSelected"
      @ok="selected"
    />
  </div>
</template>

<script setup lang="ts">
  import { computed, nextTick, reactive, ref, unref } from 'vue';
  import {
    Button,
    Divider,
    Form,
    Radio,
    Input,
    InputNumber,
    Select,
    Switch,
    Tooltip,
  } from 'ant-design-vue';
  import { PlusOutlined, QuestionOutlined } from '@ant-design/icons-vue';
  import { useFlowStoreWithOut } from '/@/store/modules/flow';
  import OrgPicker from '../OrgPicker.vue';
  import OrgItems from '../OrgItems.vue';

  const FormItem = Form.Item;
  const InputGroup = Input.Group;
  const RadioGroup = Radio.Group;
  const RadioButton = Radio.Button;
  const SelectOption = Select.Option;

  const props = defineProps({
    config: {
      type: Object,
      default: () => {
        return {};
      },
    },
  });
  const orgPickerRef = ref<any>();
  const flowStore = useFlowStoreWithOut();

  const nodeProps = computed(() => {
    return flowStore.selectedNode.props;
  });

  const selectUser = computed(() => {
    return props.config.assignedUser || [];
  });

  const selectRole = computed(() => {
    return props.config.role || [];
  });

  const forms = computed(() => {
    return flowStore.design.formItems.filter((f) => {
      return f.name === 'UserPicker';
    });
  });

  const pickerTitle = computed(() => {
    switch (state.orgPickerType) {
      case 'user':
        return 'Please select a person';
      case 'role':
        return 'Please select a system role';
      default:
        return undefined;
    }
  });

  const nodeOptions = computed(() => {
    let values: any[] = [];
    const excType = ['ROOT', 'EMPTY', 'CONDITION', 'CONDITIONS', 'CONCURRENT', 'CONCURRENTS'];
    flowStore.nodeMap.forEach((v) => {
      if (excType.indexOf(v.type) === -1) {
        values.push({ id: v.id, name: v.name });
      }
    });
    return values;
  });

  const showMode = computed(() => {
    const node = unref(nodeProps);
    switch (node.assignedType) {
      case 'ASSIGN_USER':
        return node.assignedUser.length > 0;
      case 'SELF_SELECT':
        return node.selfSelect.multiple;
      case 'FORM_USER':
        return true;
      case 'ROLE':
        return true;
      default:
        return false;
    }
  });

  const state = reactive({
    showOrgSelect: false,
    orgPickerSelected: [] as any[],
    orgPickerType: 'user',
    approvalTypes: [
      { name: 'Specified Person', type: 'ASSIGN_USER' },
      { name: 'Initiator choice', type: 'SELF_SELECT' },
      { name: 'role', type: 'ROLE' },
      { name: 'initiator himself', type: 'SELF' },
      { name: 'Contact in the form', type: 'FORM_USER' },
    ],
  });

  function handleSelectUser() {
    state.orgPickerSelected = unref(selectUser);
    state.orgPickerType = 'user';
    nextTick(() => {
      const orgPicker = unref(orgPickerRef);
      orgPicker?.show();
    });
  }

  function handleSelectNoSetUser() {
    state.orgPickerSelected = props.config.nobody.assignedUser;
    state.orgPickerType = 'user';
    nextTick(() => {
      const orgPicker = unref(orgPickerRef);
      orgPicker?.show();
    });
  }

  function handleSelectRole() {
    state.orgPickerSelected = unref(selectRole);
    state.orgPickerType = 'role';
    nextTick(() => {
      const orgPicker = unref(orgPickerRef);
      orgPicker?.show();
    });
  }

  function selected(select) {
    switch (state.orgPickerType) {
      case 'user':
        nodeProps.value.role = [];
        nodeProps.value.assignedUser = select;
        state.orgPickerSelected.length = 0;
        select.forEach((val) => state.orgPickerSelected.push(val));
        break;
      case 'role':
        nodeProps.value.assignedUser = [];
        nodeProps.value.role = select;
        state.orgPickerSelected.length = 0;
        select.forEach((val) => state.orgPickerSelected.push(val));
        break;
    }
  }

  function removeUserItem(index) {
    selectUser.value.splice(index, 1);
  }

  function removeRoleItem(index) {
    selectRole.value.splice(index, 1);
  }

  defineExpose({
    removeUserItem,
    removeRoleItem,
  });
</script>

<style lang="less" scoped>
  .user-type {
    :deep(.a-radio) {
      width: 110px;
      margin-top: 10px;
      margin-bottom: 20px;
    }
  }

  :deep(.line-mode) {
    .a-radio {
      width: 150px;
      margin: 5px;
    }
  }

  :deep(.a-form-item__label) {
    line-height: 25px;
  }

  :deep(.approve-mode) {
    .a-radio {
      float: left;
      width: 100%;
      display: block;
      margin-top: 15px;
    }
  }

  :deep(.approve-end) {
    position: relative;

    .a-radio-group {
      width: 160px;
    }

    .a-radio {
      margin-bottom: 5px;
      width: 100%;
    }

    .approve-end-leave {
      position: absolute;
      bottom: -5px;
      left: 150px;
    }
  }

  :deep(.a-divider--horizontal) {
    margin: 10px 0;
  }
</style>
