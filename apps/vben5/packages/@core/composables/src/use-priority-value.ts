import type { ComputedRef, Ref } from 'vue';

import { computed, getCurrentInstance, unref, useAttrs, useSlots } from 'vue';

import {
  getFirstNonNullOrUndefined,
  kebabToCamelCase,
} from '@vben-core/shared/utils';

/**
 * Get values from slots, attrs, props, and state in sequence
 * @param key
 * @param props
 * @param state
 */
export function usePriorityValue<
  T extends Record<string, any>,
  S extends Record<string, any>,
  K extends keyof T = keyof T,
>(key: K, props: T, state: Readonly<Ref<NoInfer<S>>> | undefined) {
  const instance = getCurrentInstance();
  const slots = useSlots();
  const attrs = useAttrs() as T;

  const value = computed((): T[K] => {
    // Whether props are passed or not, they will have default values, which will affect the order here.
    // Determine whether to pass in by judging whether the original props have a value
    const rawProps = (instance?.vnode?.props || {}) as T;

    const standardRawProps = {} as T;

    for (const [key, value] of Object.entries(rawProps)) {
      standardRawProps[kebabToCamelCase(key) as K] = value;
    }
    const propsKey =
      standardRawProps?.[key] === undefined ? undefined : props[key];

    // slot can be closed
    return getFirstNonNullOrUndefined(
      slots[key as string],
      attrs[key],
      propsKey,
      state?.value?.[key as keyof S],
    ) as T[K];
  });

  return value;
}

/**
 * Get the values in state in batches (each value is ref)
 * @param props
 * @param state
 */
export function usePriorityValues<
  T extends Record<string, any>,
  S extends Ref<Record<string, any>> = Readonly<Ref<NoInfer<T>, NoInfer<T>>>,
>(props: T, state: S | undefined) {
  const result: { [K in keyof T]: ComputedRef<T[K]> } = {} as never;

  (Object.keys(props) as (keyof T)[]).forEach((key) => {
    result[key] = usePriorityValue(key as keyof typeof props, props, state);
  });

  return result;
}

/**
 * Get the values in state in batches (concentrated in one computed for transparent transmission)
 * @param props
 * @param state
 */
export function useForwardPriorityValues<
  T extends Record<string, any>,
  S extends Ref<Record<string, any>> = Readonly<Ref<NoInfer<T>, NoInfer<T>>>,
>(props: T, state: S | undefined) {
  const computedResult: { [K in keyof T]: ComputedRef<T[K]> } = {} as never;

  (Object.keys(props) as (keyof T)[]).forEach((key) => {
    computedResult[key] = usePriorityValue(
      key as keyof typeof props,
      props,
      state,
    );
  });

  return computed(() => {
    const unwrapResult: Record<string, any> = {};
    Object.keys(props).forEach((key) => {
      unwrapResult[key] = unref(computedResult[key]);
    });
    return unwrapResult as { [K in keyof T]: T[K] };
  });
}
