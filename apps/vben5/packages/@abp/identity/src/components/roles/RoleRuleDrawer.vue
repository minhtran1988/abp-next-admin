<script setup lang="ts">
import type {
  DataAccessFilterGroup,
  DataAccessFilterRule,
  EntityPropertyInfoDto,
  EntityTypeInfoDto,
  RoleEntityRuleDto,
} from '@abp/data-protection';
import type { FormInstance } from 'ant-design-vue/es/form/Form';

import type { IdentityRoleDto } from '../../types';

import { h, reactive, ref, toValue, useTemplateRef } from 'vue';

import { useVbenDrawer } from '@vben/common-ui';
import { $t } from '@vben/locales';

import { useLocalization, useLocalizationSerializer } from '@abp/core';
import {
  DataAccessFilterLogic,
  DataAccessFilterOperate,
  DataAccessOperation,
  DataAccessStrategy,
  useEntityTypeInfosApi,
  useRoleEntityRulesApi,
  useSubjectStrategysApi,
} from '@abp/data-protection';
import { DeleteOutlined, PlusOutlined } from '@ant-design/icons-vue';
import {
  Button,
  Checkbox,
  DatePicker,
  Form,
  FormItem,
  FormItemRest,
  Input,
  InputNumber,
  message,
  Popconfirm,
  Select,
  Switch,
} from 'ant-design-vue';

const defaultModel: RoleEntityRuleDto = {
  accessedProperties: [],
  creationTime: new Date(),
  entityTypeFullName: '',
  entityTypeId: '',
  filterGroup: {
    groups: [],
    logic: DataAccessFilterLogic.Or,
    rules: [],
  },
  id: '',
  isEnabled: true,
  operation: DataAccessOperation.Read,
  roleId: '',
  roleName: '',
  strategy: DataAccessStrategy.All,
};

const form = useTemplateRef<FormInstance>('form');
const formModel = ref<RoleEntityRuleDto>({ ...defaultModel });
const entityTypes = ref<EntityTypeInfoDto[]>([]);
const entityTypeProps = ref<EntityPropertyInfoDto[]>([]);
const strategyOptions = reactive([
  {
    label: 'Can access all data',
    value: DataAccessStrategy.All,
  },
  {
    label: 'custom rules',
    value: DataAccessStrategy.Custom,
  },
  {
    label: 'Current user only',
    value: DataAccessStrategy.CurrentUser,
  },
  {
    label: 'Current user role only',
    value: DataAccessStrategy.CurrentRoles,
  },
  {
    label: 'Only the current user organization',
    value: DataAccessStrategy.CurrentOrganizationUnits,
  },
  {
    label: 'Only the current user organization and subordinate organizations',
    value: DataAccessStrategy.CurrentAndSubOrganizationUnits,
  },
]);
const operationOptions = reactive([
  {
    label: 'query',
    value: DataAccessOperation.Read,
  },
  {
    label: 'edit',
    value: DataAccessOperation.Write,
  },
  {
    label: 'delete',
    value: DataAccessOperation.Delete,
  },
]);
const logicOptions = reactive([
  {
    label: 'and',
    value: DataAccessFilterLogic.And,
  },
  {
    label: 'or',
    value: DataAccessFilterLogic.Or,
  },
]);
const dataAccessFilterOptions = reactive([
  {
    label: 'equal to',
    value: DataAccessFilterOperate.Equal,
  },
  {
    label: 'greater than',
    value: DataAccessFilterOperate.Greater,
  },
  {
    label: 'greater than or equal to',
    value: DataAccessFilterOperate.GreaterOrEqual,
  },
  {
    label: 'less than',
    value: DataAccessFilterOperate.Less,
  },
  {
    label: 'less than or equal to',
    value: DataAccessFilterOperate.LessOrEqual,
  },
  {
    label: 'not equal to',
    value: DataAccessFilterOperate.NotEqual,
  },
  {
    label: 'left contains',
    value: DataAccessFilterOperate.StartsWith,
  },
  {
    label: 'right contains',
    value: DataAccessFilterOperate.EndsWith,
  },
  {
    label: 'does not contain',
    value: DataAccessFilterOperate.NotContains,
  },
  {
    label: 'not equal to',
    value: DataAccessFilterOperate.NotEqual,
  },
]);
const [Drawer, drawerApi] = useVbenDrawer({
  class: 'w-1/2',
  onConfirm: onSubmit,
  async onOpenChange(isOpen) {
    entityTypeProps.value = [];
    formModel.value = { ...defaultModel };
    if (isOpen) {
      await onInitStrategys();
    }
  },
});

const { getApi: getEntityTypeInfoApi, getPagedListApi } =
  useEntityTypeInfosApi();
const {
  createApi,
  getApi: getRoleEntityRuleApi,
  updateApi,
} = useRoleEntityRulesApi();
const { getApi: getStrategysApi, setApi: setStrategysApi } =
  useSubjectStrategysApi();
const { deserialize } = useLocalizationSerializer();
const { Lr } = useLocalization();

async function onInitStrategys() {
  const roleInfo = drawerApi.getData<IdentityRoleDto>();
  const result = await getStrategysApi({
    subjectId: roleInfo.name,
    subjectName: 'R',
  });
  if (result) {
    formModel.value.isEnabled = result.isEnabled;
    formModel.value.strategy = result.strategy;
    await onInitEntityTypes();
  }
}

async function onInitEntityTypes(filter?: string) {
  const { items } = await getPagedListApi({ filter });
  entityTypes.value = items.map((item) => {
    const displayName = deserialize(item.displayName);
    return {
      ...item,
      displayName: Lr(displayName.resourceName, displayName.name),
    };
  });
}

function onNewGroup(logic: DataAccessFilterLogic) {
  formModel.value.filterGroup.groups.push({
    groups: [],
    logic,
    rules: [],
  });
}

function onDeleteGroup(index: number) {
  formModel.value.filterGroup.groups.splice(index, 1);
}

function onNewRule(group: DataAccessFilterGroup) {
  group.rules.push({
    field: '',
    isLeft: false,
    javaScriptType: '',
    operate: DataAccessFilterOperate.Equal,
    typeFullName: '',
    value: '',
  });
}

function onDeleteRule(group: DataAccessFilterGroup, index: number) {
  group.rules.splice(index, 1);
}

async function onSubmit() {
  try {
    await form.value?.validate();
    drawerApi.setState({ confirmLoading: true });
    const input = toValue(formModel);
    await (input.strategy === DataAccessStrategy.Custom
      ? submitCustomRule(input)
      : submitStrategys(input));
    message.success($t('AbpUi.SavedSuccessfully'));
    drawerApi.close();
  } finally {
    drawerApi.setState({ confirmLoading: false });
  }
}

async function submitStrategys(input: RoleEntityRuleDto) {
  const roleInfo = drawerApi.getData<IdentityRoleDto>();
  await setStrategysApi({
    isEnabled: input.isEnabled,
    strategy: input.strategy,
    subjectId: roleInfo.name,
    subjectName: 'R',
  });
}

async function submitCustomRule(input: RoleEntityRuleDto) {
  const roleInfo = drawerApi.getData<IdentityRoleDto>();
  const api = input.id
    ? updateApi(input.id, {
        accessedProperties: input.accessedProperties,
        entityTypeId: input.entityTypeId,
        filterGroup: input.filterGroup,
        isEnabled: input.isEnabled,
        operation: input.operation,
        roleId: roleInfo.id,
        roleName: roleInfo.id,
      })
    : createApi({
        accessedProperties: input.accessedProperties,
        entityTypeId: input.entityTypeId,
        filterGroup: input.filterGroup,
        isEnabled: input.isEnabled,
        operation: input.operation,
        roleId: roleInfo.id,
        roleName: roleInfo.name,
      });
  await api;
}

async function onGetEntityType() {
  try {
    drawerApi.setState({ confirmLoading: true, loading: true });
    const { entityTypeId, operation } = formModel.value;
    const roleInfo = drawerApi.getData<IdentityRoleDto>();
    const ruleEntityRule = await getRoleEntityRuleApi({
      entityTypeId,
      operation,
      roleName: roleInfo.name,
    });
    formModel.value = ruleEntityRule?.id
      ? {
          ...ruleEntityRule,
          strategy: DataAccessStrategy.Custom,
        }
      : {
          ...defaultModel,
          entityTypeId,
          operation,
        };
  } finally {
    drawerApi.setState({ confirmLoading: false, loading: false });
  }
}

async function onStrategyChange(strategy: DataAccessStrategy) {
  if (strategy === DataAccessStrategy.Custom) {
    await onInitEntityTypes();
  }
}

async function onEntityTypeChange(entityTypeId: string) {
  const entityTypeInfo = await getEntityTypeInfoApi(entityTypeId);
  entityTypeProps.value = entityTypeInfo.properties.map((item) => {
    const displayName = deserialize(item.displayName);
    if (item.enums) {
      item.enums = item.enums.map((enumItem) => {
        const enumDisplayName = deserialize(enumItem.displayName);
        return {
          ...enumItem,
          displayName: Lr(enumDisplayName.resourceName, enumDisplayName.name),
        };
      });
    }
    return {
      ...item,
      displayName: Lr(displayName.resourceName, displayName.name),
    };
  });
  await onGetEntityType();
}

// Attribute selection changes trigger changes in comparison value input type
function onPropertyTypeChange(
  rule: DataAccessFilterRule,
  option: EntityPropertyInfoDto,
) {
  rule.value = '';
  rule.typeFullName = option.typeFullName;
  rule.javaScriptType = option.javaScriptType;
}
</script>

<template>
  <Drawer title="Data Permission">
    <Form
      :model="formModel"
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 24 }"
    >
      <FormItem name="isEnabled" label="Whether it is enabled">
        <Checkbox v-model:checked="formModel.isEnabled">Whether to enable</Checkbox>
      </FormItem>
      <FormItem name="isEnabled" label="Data permission policy">
        <Select
          class="w-full"
          :options="strategyOptions"
          v-model:value="formModel.strategy"
          @change="(value) => onStrategyChange(Number(value!))"
        />
      </FormItem>
      <template v-if="formModel.strategy === DataAccessStrategy.Custom">
        <FormItem required name="entityTypeId" label="Entity Type">
          <Select
            class="w-full"
            :options="entityTypes"
            :field-names="{ label: 'displayName', value: 'id' }"
            v-model:value="formModel.entityTypeId"
            @change="(value) => onEntityTypeChange(value!.toString())"
          />
        </FormItem>
        <FormItem required name="operation" label="operation">
          <Select
            class="w-full"
            :options="operationOptions"
            v-model:value="formModel.operation"
            @change="onGetEntityType"
          />
        </FormItem>
        <FormItem name="accessedProperties" label="Accessed fields">
          <Select
            class="w-full"
            mode="multiple"
            :options="entityTypeProps"
            v-model:value="formModel.accessedProperties"
            :field-names="{ label: 'displayName', value: 'name' }"
          />
        </FormItem>
        <FormItemRest>
          <!-- Grouping -->
          <div class="flex w-full flex-col">
            <div class="w-full items-center divide-dashed border-2">
              <div class="m-2 text-sm">Data access rules</div>
              <template
                v-for="(group, gi) in formModel.filterGroup.groups"
                :key="gi"
              >
                <div class="flex flex-row items-center justify-items-center">
                  <!-- Conditions -->
                  <div class="m-2 flex w-full flex-row">
                    <div class="w-full border-2 border-dashed">
                      <template v-for="(rule, ri) in group.rules" :key="ri">
                        <div
                          class="m-2 flex flex-row items-center justify-items-center gap-1"
                        >
                          <div class="basis-2/5">
                            <Select
                              class="w-full"
                              :options="entityTypeProps"
                              v-model:value="rule.field"
                              :field-names="{
                                label: 'displayName',
                                value: 'name',
                              }"
                              @change="
                                (_, option: any) =>
                                  onPropertyTypeChange(rule, option)
                              "
                            />
                          </div>
                          <div class="basis-1/5">
                            <Select
                              class="w-full"
                              :options="dataAccessFilterOptions"
                              v-model:value="rule.operate"
                            />
                          </div>
                          <div class="basis-2/5">
                            <InputNumber
                              v-if="rule.javaScriptType === 'number'"
                              class="w-full"
                              v-model:value="rule.value"
                            />
                            <Switch
                              v-else-if="rule.javaScriptType === 'boolean'"
                              v-model:checked="rule.value"
                            />
                            <DatePicker
                              v-else-if="rule.javaScriptType === 'Date'"
                              class="w-full"
                              v-model:value="rule.value"
                              value-format="YYYY-MM-DDT00:00:00"
                            />
                            <Input
                              v-else
                              class="w-full"
                              v-model:value="rule.value"
                            />
                          </div>
                          <div class="basis-1/5">
                            <Popconfirm
                              title="Are you sure?"
                              description="This filter will be deleted"
                              @confirm="onDeleteRule(group, ri)"
                            >
                              <Button
                                type="link"
                                danger
                                :icon="h(DeleteOutlined)"
                              />
                            </Popconfirm>
                          </div>
                        </div>
                      </template>
                      <div
                        class="flex flex-row items-center justify-items-center gap-2"
                      >
                        <div class="m-2 min-w-[60px]">
                          <Select
                            size="small"
                            class="w-full"
                            v-model:value="group.logic"
                            :options="logicOptions"
                          />
                        </div>
                        <Button
                          type="link"
                          :icon="h(PlusOutlined)"
                          @click="onNewRule(group)"
                        >
                          Add conditions
                        </Button>
                        <Popconfirm
                          title="Are you sure?"
                          description="This condition group will be deleted"
                          @confirm="onDeleteGroup(gi)"
                        >
                          <Button type="link" :icon="h(DeleteOutlined)" danger>
                            Delete group
                          </Button>
                        </Popconfirm>
                      </div>
                    </div>
                  </div>
                </div>
              </template>
              <div
                class="flex flex-row items-center justify-items-center gap-2"
              >
                <div class="m-2 min-w-[60px]">
                  <Select
                    size="small"
                    class="w-full"
                    v-model:value="formModel.filterGroup.logic"
                    :options="logicOptions"
                  />
                </div>
                <div>
                  <Button
                    size="small"
                    type="link"
                    :icon="h(PlusOutlined)"
                    @click="onNewGroup(formModel.filterGroup.logic)"
                  >
                    Add group
                  </Button>
                </div>
              </div>
            </div>
          </div>
        </FormItemRest>
      </template>
    </Form>
  </Drawer>
</template>

<style scoped></style>
