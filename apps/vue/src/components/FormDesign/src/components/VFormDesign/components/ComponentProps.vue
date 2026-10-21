<!--
 * @Description: Component property control
-->
<template>
  <div class="properties-content">
    <div class="properties-body" v-if="formConfig.currentItem">
      <Empty class="hint-box" v-if="!formConfig.currentItem.key" description="No component selected" />

      <Form label-align="left" layout="vertical">
        <!-- Loop through rendering component properties -->

        <div v-if="formConfig.currentItem && formConfig.currentItem.componentProps">
          <FormItem v-for="item in inputOptions" :key="item.name" :label="item.label">
            <!-- Processing array attributes, placeholder -->

            <div v-if="item.children">
              <component
                v-for="(child, index) of item.children"
                :key="index"
                v-bind="child.componentProps"
                :is="child.component"
                v-model:value="formConfig.currentItem.componentProps[item.name][index]"
              />
            </div>
            <!-- If it is not an array, the attribute value is processed normally -->
            <component
              v-else
              class="component-prop"
              v-bind="item.componentProps"
              :is="item.component"
              v-model:value="formConfig.currentItem.componentProps[item.name]"
            />
          </FormItem>
          <FormItem label="Control Properties">
            <Col v-for="item in controlOptions" :key="item.name">
              <Checkbox
                v-if="showControlAttrs(item.includes)"
                v-bind="item.componentProps"
                v-model:checked="formConfig.currentItem.componentProps[item.name]"
              >
                {{ item.label }}
              </Checkbox>
            </Col>
          </FormItem>
        </div>
        <FormItem label="Associated fields">
          <Select
            mode="multiple"
            v-model:value="formConfig.currentItem['link']"
            :options="linkOptions"
          />
        </FormItem>

        <FormItem
          label="options"
          v-if="
            [
              'Select',
              'ApiSelect',
              'CheckboxGroup',
              'RadioGroup',
              'TreeSelect',
              'ApiTreeSelect',
              'Cascader',
              'AutoComplete',
            ].includes(formConfig.currentItem.component)
          "
        >
          <FormOptions />
        </FormItem>

        <FormItem label="Table" v-if="['Table'].includes(formConfig.currentItem.component)">
          <FormOptions />
        </FormItem>

        <FormItem label="Grid" v-if="['Grid'].includes(formConfig.currentItem.component)">
          <FormOptions />
        </FormItem>
      </Form>
    </div>
  </div>
</template>
<script lang="ts">
  import {
    Empty,
    Input,
    Form,
    FormItem,
    Switch,
    Checkbox,
    Select,
    InputNumber,
    RadioGroup,
  } from 'ant-design-vue';
  import RadioButtonGroup from '/@/components/Form/src/components/RadioButtonGroup.vue';
  import { Col, Row } from 'ant-design-vue';
  import { computed, defineComponent, ref, watch } from 'vue';
  import { useFormDesignState } from '../../../hooks/useFormDesignState';
  import {
    baseComponentControlAttrs,
    baseComponentAttrs,
    baseComponentCommonAttrs,
    componentPropsFuncs,
  } from '../../VFormDesign/config/componentPropsConfig';
  import FormOptions from './FormOptions.vue';
  import { formItemsForEach, remove } from '../../../utils';
  import { IBaseFormAttrs } from '../config/formItemPropsConfig';
  export default defineComponent({
    name: 'ComponentProps',
    components: {
      FormOptions,
      Empty,
      Input,
      Form,
      FormItem,
      Switch,
      Checkbox,
      Select,
      InputNumber,
      RadioGroup,
      RadioButtonGroup,
      Col,
      Row,
    },
    setup() {
      // Let computed properties update automatically
      const allOptions = ref([] as Omit<IBaseFormAttrs, 'tag'>[]);
      const showControlAttrs = (includes: string[] | undefined) => {
        if (!includes) return true;
        return includes.includes(formConfig.value.currentItem!.component);
      };
      const { formConfig } = useFormDesignState();
      if (formConfig.value.currentItem) {
        formConfig.value.currentItem.componentProps =
          formConfig.value.currentItem.componentProps || {};
      }
      watch(
        () => formConfig.value.currentItem?.field,
        (_newValue, oldValue) => {
          formConfig.value.schemas &&
            formItemsForEach(formConfig.value.schemas, (item) => {
              if (item.link) {
                const index = item.link.findIndex((linkItem) => linkItem === oldValue);
                index !== -1 && remove(item.link, index);
              }
            });
        },
      );
      watch(
        () => formConfig.value.currentItem && formConfig.value.currentItem.component,
        () => {
          allOptions.value = [];
          baseComponentControlAttrs.forEach((item) => {
            item.category = 'control';
            if (!item.includes) {
              // If the property is not included, all controls are applicable
              allOptions.value.push(item);
            } else if (item.includes.includes(formConfig.value.currentItem!.component)) {
              // If there is an include, check whether the current control type is included
              allOptions.value.push(item);
            }
          });
          baseComponentCommonAttrs.forEach((item) => {
            item.category = 'input';
            if (item.includes) {
              if (item.includes.includes(formConfig.value.currentItem!.component)) {
                allOptions.value.push(item);
              }
            } else if (item.exclude) {
              if (!item.exclude.includes(formConfig.value.currentItem!.component)) {
                allOptions.value.push(item);
              }
            } else {
              allOptions.value.push(item);
            }
          });
          baseComponentAttrs[formConfig.value.currentItem!.component] &&
            baseComponentAttrs[formConfig.value.currentItem!.component].forEach(async (item) => {
              if (item.component) {
                if (['Switch', 'Checkbox', 'Radio'].includes(item.component)) {
                  item.category = 'control';
                  allOptions.value.push(item);
                } else {
                  item.category = 'input';
                  allOptions.value.push(item);
                }
              }
            });
        },
        {
          immediate: true,
        },
      );
      // Control options
      const controlOptions = computed(() => {
        return allOptions.value.filter((item) => {
          return item.category == 'control';
        });
      });
      // uncontrolled selection
      const inputOptions = computed(() => {
        return allOptions.value.filter((item) => {
          return item.category == 'input';
        });
      });
      watch(
        () => formConfig.value.currentItem!.componentProps,
        () => {
          const func = componentPropsFuncs[formConfig.value.currentItem!.component];
          if (func) {
            func(formConfig.value.currentItem!.componentProps, allOptions.value);
          }
        },
        {
          immediate: true,
          deep: true,
        },
      );
      const linkOptions = computed(() => {
        return (
          formConfig.value.schemas &&
          formConfig.value.schemas
            .filter((item) => item.key !== formConfig.value.currentItem!.key)
            .map(({ label, field }) => ({ label: label + '/' + field, value: field }))
        );
      });
      return {
        formConfig,
        showControlAttrs,
        linkOptions,
        controlOptions,
        inputOptions,
      };
    },
  });
</script>
