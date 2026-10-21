<script lang="ts" setup>
import type { Component } from 'vue';

import type { AnyPromiseFunction } from '@vben/types';

import { computed, nextTick, ref, unref, useAttrs, watch } from 'vue';

import { LoaderCircle } from '@vben/icons';

import { cloneDeep, get, isEqual, isFunction } from '@vben-core/shared/utils';

import { objectOmit } from '@vueuse/core';

type OptionsItem = {
  [name: string]: any;
  children?: OptionsItem[];
  disabled?: boolean;
  label?: string;
  value?: string;
};

interface Props {
  /** Components */
  component: Component;
  /** Whether to convert value from number to string */
  numberToString?: boolean;
  /** Function to obtain options data */
  api?: (arg?: any) => Promise<OptionsItem[] | Record<string, any>>;
  /** Parameters passed to api */
  params?: Record<string, any>;
  /** Extract the field names of the options array from the results returned by the api */
  resultField?: string;
  /** label field name */
  labelField?: string;
  /** children field name, available for components that require hierarchical data */
  childrenField?: string;
  /** value field name */
  valueField?: string;
  /** disabled field name */
  disabledField?: string;
  /** The attribute name of the component that receives options data */
  optionsPropName?: string;
  /** Whether to call the api immediately */
  immediate?: boolean;
  /** Request data every time a `visibleEvent` event occurs */
  alwaysLoad?: boolean;
  /** Callback function before api request */
  beforeFetch?: AnyPromiseFunction<any, any>;
  /** Callback function after api request */
  afterFetch?: AnyPromiseFunction<any, any>;
  /** Pass in option data directly and also serve as backup data when the API returns empty data */
  options?: OptionsItem[];
  /** The slot name of the component, used to display a "loading" icon */
  loadingSlot?: string;
  /** The event name that triggers the api request */
  visibleEvent?: string;
  /** The v-model attribute name of the component, the default is modelValue. Some components may be value */
  modelPropName?: string;
  /**
   * Automatic selection
   * - `first`: automatically select the first option
   * - `last`: automatically select the last option
   * - `one`: When the requested result has only one option, automatically select this option
   * - Function: Customize the selection logic. The parameter of the function is the requested result array, and the return value is the selected option.
   * - false: no automatic selection (default)
   */
  autoSelect?:
    | 'first'
    | 'last'
    | 'one'
    | ((item: OptionsItem[]) => OptionsItem)
    | false;
}

defineOptions({ name: 'ApiComponent', inheritAttrs: false });

const props = withDefaults(defineProps<Props>(), {
  labelField: 'label',
  valueField: 'value',
  disabledField: 'disabled',
  childrenField: '',
  optionsPropName: 'options',
  resultField: '',
  visibleEvent: '',
  numberToString: false,
  params: () => ({}),
  immediate: true,
  alwaysLoad: false,
  loadingSlot: '',
  beforeFetch: undefined,
  afterFetch: undefined,
  modelPropName: 'modelValue',
  api: undefined,
  autoSelect: false,
  options: () => [],
});

const emit = defineEmits<{
  optionsChange: [OptionsItem[]];
}>();

const modelValue = defineModel<any>({ default: undefined });

const attrs = useAttrs();
const innerParams = ref({});
const refOptions = ref<OptionsItem[]>([]);
const loading = ref(false);
// Has it been loaded for the first time?
const isFirstLoaded = ref(false);
// Mark if there are pending requests
const hasPendingRequest = ref(false);

const getOptions = computed(() => {
  const {
    labelField,
    valueField,
    disabledField,
    childrenField,
    numberToString,
  } = props;

  const refOptionsData = unref(refOptions);

  function transformData(data: OptionsItem[]): OptionsItem[] {
    return data.map((item) => {
      const value = get(item, valueField);
      const disabled = get(item, disabledField);
      return {
        ...objectOmit(item, [labelField, valueField, disabled, childrenField]),
        label: get(item, labelField),
        value: numberToString ? `${value}` : value,
        disabled: get(item, disabledField),
        ...(childrenField && item[childrenField]
          ? { children: transformData(item[childrenField]) }
          : {}),
      };
    });
  }

  const data: OptionsItem[] = transformData(refOptionsData);

  return data.length > 0 ? data : props.options;
});

const bindProps = computed(() => {
  return {
    [props.modelPropName]: unref(modelValue),
    [props.optionsPropName]: unref(getOptions),
    [`onUpdate:${props.modelPropName}`]: (val: string) => {
      modelValue.value = val;
    },
    ...objectOmit(attrs, [`onUpdate:${props.modelPropName}`]),
    ...(props.visibleEvent
      ? {
          [props.visibleEvent]: handleFetchForVisible,
        }
      : {}),
  };
});

async function fetchApi() {
  const { api, beforeFetch, afterFetch, resultField } = props;

  if (!api || !isFunction(api)) {
    return;
  }

  // If loading, mark pending requests and return
  if (loading.value) {
    hasPendingRequest.value = true;
    return;
  }

  refOptions.value = [];
  try {
    loading.value = true;
    let finalParams = unref(mergedParams);
    if (beforeFetch && isFunction(beforeFetch)) {
      finalParams = (await beforeFetch(cloneDeep(finalParams))) || finalParams;
    }
    let res = await api(finalParams);
    if (afterFetch && isFunction(afterFetch)) {
      res = (await afterFetch(res)) || res;
    }
    isFirstLoaded.value = true;
    if (Array.isArray(res)) {
      refOptions.value = res;
      emitChange();
      return;
    }
    if (resultField) {
      refOptions.value = get(res, resultField) || [];
    }
    emitChange();
  } catch (error) {
    console.warn(error);
    // reset status
    isFirstLoaded.value = false;
  } finally {
    loading.value = false;
    // If there is a pending request, trigger a new request immediately
    if (hasPendingRequest.value) {
      hasPendingRequest.value = false;
      // Use nextTick to ensure that the status update is completed before triggering a new request
      await nextTick();
      fetchApi();
    }
  }
}

async function handleFetchForVisible(visible: boolean) {
  if (visible) {
    if (props.alwaysLoad) {
      await fetchApi();
    } else if (!props.immediate && !unref(isFirstLoaded)) {
      await fetchApi();
    }
  }
}

const mergedParams = computed(() => {
  return {
    ...props.params,
    ...unref(innerParams),
  };
});

watch(
  mergedParams,
  (value, oldValue) => {
    if (isEqual(value, oldValue)) {
      return;
    }
    fetchApi();
  },
  { deep: true, immediate: props.immediate },
);

function emitChange() {
  if (
    modelValue.value === undefined &&
    props.autoSelect &&
    unref(getOptions).length > 0
  ) {
    let firstOption;
    if (isFunction(props.autoSelect)) {
      firstOption = props.autoSelect(unref(getOptions));
    } else {
      switch (props.autoSelect) {
        case 'first': {
          firstOption = unref(getOptions)[0];
          break;
        }
        case 'last': {
          firstOption = unref(getOptions)[unref(getOptions).length - 1];
          break;
        }
        case 'one': {
          if (unref(getOptions).length === 1) {
            firstOption = unref(getOptions)[0];
          }
          break;
        }
      }
    }

    if (firstOption) modelValue.value = firstOption.value;
  }
  emit('optionsChange', unref(getOptions));
}
const componentRef = ref();
defineExpose({
  /** Get options data */
  getOptions: () => unref(getOptions),
  /** Get the current value */
  getValue: () => unref(modelValue),
  /** Get the packaged component instance */
  getComponentRef: <T = any,>() => componentRef.value as T,
  /** Update API parameters */
  updateParam(newParams: Record<string, any>) {
    innerParams.value = newParams;
  },
});
</script>
<template>
  <component
    :is="component"
    v-bind="bindProps"
    :placeholder="$attrs.placeholder"
    ref="componentRef"
  >
    <template v-for="item in Object.keys($slots)" #[item]="data">
      <slot :name="item" v-bind="data || {}"></slot>
    </template>
    <template v-if="loadingSlot && loading" #[loadingSlot]>
      <LoaderCircle class="animate-spin" />
    </template>
  </component>
</template>
