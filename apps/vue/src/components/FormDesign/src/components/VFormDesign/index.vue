<template>
  <Layout>
    <LayoutSider
      :class="`left ${prefixCls}-sider`"
      collapsible
      collapsedWidth="0"
      width="270"
      :zeroWidthTriggerStyle="{
        'margin-top': '-70px',
        'background-color': 'gray',
      }"
      breakpoint="md"
    >
      <CollapseContainer title="Basic Control">
        <CollapseItem
          :list="baseComponents"
          :handleListPush="handleListPushDrag"
          @add-attrs="handleAddAttrs"
          @handle-list-push="handleListPush"
        />
      </CollapseContainer>
      <CollapseContainer title="Custom control">
        <CollapseItem
          :list="customComponents"
          @add-attrs="handleAddAttrs"
          :handleListPush="handleListPushDrag"
          @handle-list-push="handleListPush"
        />
      </CollapseContainer>
      <CollapseContainer title="Layout Control">
        <CollapseItem
          :list="layoutComponents"
          :handleListPush="handleListPushDrag"
          @add-attrs="handleAddAttrs"
          @handle-list-push="handleListPush"
        />
      </CollapseContainer>
    </LayoutSider>
    <LayoutContent>
      <Toolbar
        @handle-open-json-modal="handleOpenModal(jsonModal!)"
        @handle-open-import-json-modal="handleOpenModal(importJsonModal!)"
        @handle-preview="handleOpenModal(eFormPreview!)"
        @handle-preview2="handleOpenModal(eFormPreview2!)"
        @handle-open-code-modal="handleOpenModal(codeModal!)"
        @handle-clear-form-items="handleClearFormItems"
      />
      <FormComponentPanel
        :current-item="formConfig.currentItem"
        :data="formConfig"
        @handle-set-select-item="handleSetSelectItem"
      />
    </LayoutContent>
    <LayoutSider
      :class="`right ${prefixCls}-sider`"
      collapsible
      :reverseArrow="true"
      collapsedWidth="0"
      width="270"
      :zeroWidthTriggerStyle="{ 'margin-top': '-70px', 'background-color': 'gray' }"
      breakpoint="lg"
    >
      <PropsPanel ref="propsPanel" :activeKey="formConfig.activeKey">
        <template v-for="item of formConfig.schemas" #[`${item.component}Props`]="data">
          <slot
            :name="`${item.component}Props`"
            v-bind="{ formItem: data, props: data.componentProps }"
          ></slot>
        </template>
      </PropsPanel>
    </LayoutSider>
  </Layout>

  <JsonModal ref="jsonModal" />
  <CodeModal ref="codeModal" />
  <ImportJsonModal ref="importJsonModal" />
  <VFormPreview ref="eFormPreview" :formConfig="formConfig" />
  <VFormPreview2 ref="eFormPreview2" :formConfig="formConfig" />
</template>

<script lang="ts" setup>
  import CollapseItem from './modules/CollapseItem.vue';
  import FormComponentPanel from './modules/FormComponentPanel.vue';
  import JsonModal from './components/JsonModal.vue';
  import VFormPreview from '../VFormPreview/index.vue';
  import VFormPreview2 from '../VFormPreview/useForm.vue';
  import Toolbar from './modules/Toolbar.vue';
  import PropsPanel from './modules/PropsPanel.vue';
  import ImportJsonModal from './components/ImportJsonModal.vue';
  import CodeModal from './components/CodeModal.vue';
  import 'codemirror/mode/javascript/javascript';
  import { ref, provide, Ref } from 'vue';
  import { Layout, LayoutContent, LayoutSider } from 'ant-design-vue';
  import { IVFormComponent, IFormConfig, PropsTabKey } from '../../typings/v-form-component';
  import { formItemsForEach, generateKey } from '../../utils';
  import { cloneDeep } from 'lodash-es';
  import { baseComponents, customComponents, layoutComponents } from '../../core/formItemConfig';
  import { useRefHistory, UseRefHistoryReturn } from '@vueuse/core';
  import { globalConfigState } from './config/formItemPropsConfig';
  import { IFormDesignMethods, IPropsPanel, IToolbarMethods } from '../../typings/form-type';
  import { useDesign } from '/@/hooks/web/useDesign';
  import { CollapseContainer } from '/@/components/Container/index';
  defineProps({
    title: {
      type: String,
      default: 'v-form-antd form designer',
    },
  });
  const { prefixCls } = useDesign('form-design');
  // Subcomponent instance
  const propsPanel = ref<null | IPropsPanel>(null);
  const jsonModal = ref<null | IToolbarMethods>(null);
  const importJsonModal = ref<null | IToolbarMethods>(null);
  const eFormPreview = ref<null | IToolbarMethods>(null);
  const eFormPreview2 = ref<null | IToolbarMethods>(null);
  const codeModal = ref<null | IToolbarMethods>(null);
  const formModel = ref({});
  // endregion
  const formConfig = ref<IFormConfig>({
    // form configuration
    schemas: [],
    layout: 'horizontal',
    labelLayout: 'flex',
    labelWidth: 100,
    labelCol: {},
    wrapperCol: {},
    currentItem: {
      component: '',
      componentProps: {},
    },
    activeKey: 1,
  });
  const setFormConfig = (config: IFormConfig) => {
    // When importing externally, necessary information may be missing.
    config.schemas = config.schemas || [];
    config.schemas.forEach((item) => {
      item.colProps = item.colProps || { span: 24 };
      item.componentProps = item.componentProps || {};
      item.itemProps = item.itemProps || {};
    });
    formConfig.value = config;
  };
  // Get history records for undo and reconstruction
  const historyReturn = useRefHistory(formConfig, {
    deep: true,
    capacity: 20,
    parse: (val: IFormConfig) => {
      // Use lodash.cloneDeep to re-copy the data and point currentItem to the selected item
      const formConfig = cloneDeep(val);
      const { currentItem, schemas } = formConfig;
      // Find the selected item from formItems
      const item = schemas && schemas.find((item) => item.key === currentItem?.key);
      // If there is, assign the value to the current item, if not, switch the property panel
      if (item) {
        formConfig.currentItem = item;
      }
      return formConfig;
    },
  });
  /**
   * Select form item
   * @param schema The currently selected form item
   */
  const handleSetSelectItem = (schema: IVFormComponent) => {
    formConfig.value.currentItem = schema;
    handleChangePropsTabs(
      schema.key ? (formConfig.value.activeKey! === 1 ? 2 : formConfig.value.activeKey!) : 1,
    );
  };
  const setGlobalConfigState = (formItem: IVFormComponent) => {
    formItem.colProps = formItem.colProps || {};
    formItem.colProps.span = globalConfigState.span;
    // console.log('setGlobalConfigState', formItem);
  };
  /**
   * Add attributes
   * @param schemas
   * @param index
   */
  const handleAddAttrs = (_formItems: IVFormComponent[], _index: number) => {};
  const handleListPushDrag = (item: IVFormComponent) => {
    const formItem = cloneDeep(item);
    setGlobalConfigState(formItem);
    generateKey(formItem);
    return formItem;
  };
  /**
   * Added to the panel when the control is clicked
   * @param item {IVFormComponent} currently clicked component
   */
  const handleListPush = (item: IVFormComponent) => {
    // console.log('handleListPush', item);
    const formItem = cloneDeep(item);
    setGlobalConfigState(formItem);
    generateKey(formItem);
    if (!formConfig.value.currentItem?.key) {
      handleSetSelectItem(formItem);
      formConfig.value.schemas && formConfig.value.schemas.push(formItem);
      return;
    }
    handleCopy(formItem, false);
  };
  /**
   * Copy the form item. If the form item is in grid layout, traverse all self-form items to regenerate the key.
   * @param {IVFormComponent} formItem
   * @return {IVFormComponent}
   */
  const copyFormItem = (formItem: IVFormComponent) => {
    const newFormItem = cloneDeep(formItem);
    if (newFormItem.component === 'Grid') {
      formItemsForEach([formItem], (item) => {
        generateKey(item);
      });
    }
    return newFormItem;
  };
  /**
   * Copy or add a form, copy the form when isCopy is true
   * @param item {IVFormComponent} currently clicked component
   * @param isCopy {boolean} whether to copy
   */
  const handleCopy = (
    item: IVFormComponent = formConfig.value.currentItem as IVFormComponent,
    isCopy = true,
  ) => {
    const key = formConfig.value.currentItem?.key;
    /**
     * Traverse the form item configuration. If it is a copy, copy the form item. If it is not a copy, add it directly to the form item.
     * @param schemas
     */
    const traverse = (schemas: IVFormComponent[]) => {
      // Use some traversal, stop traversing after finding the target
      schemas.some((formItem: IVFormComponent, index: number) => {
        if (formItem.key === key) {
          // Determine whether it is copied
          isCopy
            ? schemas.splice(index, 0, copyFormItem(formItem))
            : schemas.splice(index + 1, 0, item);
          const event = {
            newIndex: index + 1,
          };
          // Add to form item
          handleBeforeColAdd(event, schemas, isCopy);
          return true;
        }
        if (['Grid', 'Tabs'].includes(formItem.component)) {
          // Grid layout
          formItem.columns?.forEach((item) => {
            traverse(item.children);
          });
        }
      });
    };
    if (formConfig.value.schemas) {
      traverse(formConfig.value.schemas);
    }
  };
  /**
   * Add to form
   * @param newIndex {object} event object
   * @param schemas {IVFormComponent[]} form item list
   * @param isCopy {boolean} whether to copy
   */
  const handleBeforeColAdd = ({ newIndex }: any, schemas: IVFormComponent[], isCopy = false) => {
    const item = schemas[newIndex];
    isCopy && generateKey(item);
    handleSetSelectItem(item);
  };
  /**
   * Open modal box
   * @param Modal {IToolbarMethods}
   */
  const handleOpenModal = (Modal: IToolbarMethods) => {
    const config = cloneDeep(formConfig.value);
    Modal?.showModal(config);
  };
  /**
   * Switch property panel
   * @param key
   */
  const handleChangePropsTabs = (key: PropsTabKey) => {
    formConfig.value.activeKey = key;
  };
  /**
   * Clear the form item list
   */
  const handleClearFormItems = () => {
    formConfig.value.schemas = [];
    handleSetSelectItem({ component: '' });
  };
  const setFormModel = (key, value) => (formModel.value[key] = value);
  provide('formModel', formModel);
  // Inject the method items of the ancestor component into the subcomponent. The subcomponent can be obtained through inject.
  provide<(key: String, value: any) => void>('setFormModelMethod', setFormModel);
  // region attributes injected into subcomponents
  // provide('currentItem', formConfig.value.currentItem)
  // Inject the form configuration items into the subcomponent. The subcomponent can be obtained through inject. The obtained data is responsive.
  provide<Ref<IFormConfig>>('formConfig', formConfig);
  // Inject history
  provide<UseRefHistoryReturn<any, any>>('historyReturn', historyReturn);
  // Inject the method items of the ancestor component into the subcomponent. The subcomponent can be obtained through inject.
  provide<IFormDesignMethods>('formDesignMethods', {
    handleBeforeColAdd,
    handleCopy,
    handleListPush,
    handleSetSelectItem,
    handleAddAttrs,
    setFormConfig,
  });
  // endregion
</script>

<style lang="less" scoped>
  @prefix-cls: ~'@{namespace}-form-design';

  [data-theme='dark'] {
  .@{prefix-cls}-sider{
    background-color: #1f1f1f;
  }}

  [data-theme='light'] {
    .@{prefix-cls}-sider{
    background-color: #fff;
  }
  }
</style>
