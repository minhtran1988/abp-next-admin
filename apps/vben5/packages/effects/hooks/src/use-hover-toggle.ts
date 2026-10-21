import type { Arrayable, MaybeElementRef } from '@vueuse/core';

import type { Ref } from 'vue';

import { computed, effectScope, onUnmounted, ref, unref, watch } from 'vue';

import { isFunction } from '@vben/utils';

import { useElementHover } from '@vueuse/core';

interface HoverDelayOptions {
  /** Mouse entry delay time */
  enterDelay?: (() => number) | number;
  /** Mouse leave delay time */
  leaveDelay?: (() => number) | number;
}

const DEFAULT_LEAVE_DELAY = 500; //Mouse leave delay time, default is 500ms
const DEFAULT_ENTER_DELAY = 0; // Mouse entry delay time, default is 0 (immediate response)

/**
 * Monitors whether the mouse is inside the element. If it is inside the element, it returns true, otherwise it returns false.
 * @param refElement All elements that need to be detected. Supports a single element, an array of elements, or an array of elements with reactive references. Returns true if the mouse is inside any element
 * @param delay The time to delay updating the status, which can be a number or a configuration object containing the entry/exit delay
 * @returns returns an array. The first element is a ref, indicating whether the mouse is inside the element. The second element is a controller, which can control the enabling and disabling of the listener through the enable and disable methods.
 */
export function useHoverToggle(
  refElement: Arrayable<MaybeElementRef> | Ref<HTMLElement[] | null>,
  delay: (() => number) | HoverDelayOptions | number = DEFAULT_LEAVE_DELAY,
) {
  // Compatible with old version API
  const normalizedOptions: HoverDelayOptions =
    typeof delay === 'number' || isFunction(delay)
      ? { enterDelay: DEFAULT_ENTER_DELAY, leaveDelay: delay }
      : {
          enterDelay: DEFAULT_ENTER_DELAY,
          leaveDelay: DEFAULT_LEAVE_DELAY,
          ...delay,
        };

  const value = ref(false);
  const enterTimer = ref<ReturnType<typeof setTimeout> | undefined>();
  const leaveTimer = ref<ReturnType<typeof setTimeout> | undefined>();
  const hoverScopes = ref<ReturnType<typeof effectScope>[]>([]);

  // Use computed properties to wrap refElement to make it responsive
  const refs = computed(() => {
    const raw = unref(refElement);
    if (raw === null) return [];
    return Array.isArray(raw) ? raw : [raw];
  });
  // Store all hover states
  const isHovers = ref<Array<Ref<boolean>>>([]);

  // Update the hover listening function
  function updateHovers() {
    // Stop and clean up previous scope
    hoverScopes.value.forEach((scope) => scope.stop());
    hoverScopes.value = [];

    isHovers.value = refs.value.map((refEle) => {
      if (!refEle) {
        return ref(false);
      }
      const eleRef = computed(() => {
        const ele = unref(refEle);
        return ele instanceof Element ? ele : (ele?.$el as Element);
      });

      // Create a separate scope for each element
      const scope = effectScope();
      const hoverRef = scope.run(() => useElementHover(eleRef)) || ref(false);
      hoverScopes.value.push(scope);

      return hoverRef;
    });
  }

  // Monitor changes in the number of elements to avoid excessive execution
  const elementsCount = computed(() => {
    const raw = unref(refElement);
    if (raw === null) return 0;
    return Array.isArray(raw) ? raw.length : 1;
  });

  // Initial settings
  updateHovers();

  // Only reset the listener when the number of elements changes
  const stopWatcher = watch(elementsCount, updateHovers, { deep: false });

  const isOutsideAll = computed(() => isHovers.value.every((v) => !v.value));

  function clearTimers() {
    if (enterTimer.value) {
      clearTimeout(enterTimer.value);
      enterTimer.value = undefined;
    }
    if (leaveTimer.value) {
      clearTimeout(leaveTimer.value);
      leaveTimer.value = undefined;
    }
  }

  function setValueDelay(val: boolean) {
    clearTimers();

    if (val) {
      // Mouse enter
      const enterDelay = normalizedOptions.enterDelay ?? DEFAULT_ENTER_DELAY;
      const delayTime = isFunction(enterDelay) ? enterDelay() : enterDelay;

      if (delayTime <= 0) {
        value.value = true;
      } else {
        enterTimer.value = setTimeout(() => {
          value.value = true;
          enterTimer.value = undefined;
        }, delayTime);
      }
    } else {
      // mouse leaves
      const leaveDelay = normalizedOptions.leaveDelay ?? DEFAULT_LEAVE_DELAY;
      const delayTime = isFunction(leaveDelay) ? leaveDelay() : leaveDelay;

      if (delayTime <= 0) {
        value.value = false;
      } else {
        leaveTimer.value = setTimeout(() => {
          value.value = false;
          leaveTimer.value = undefined;
        }, delayTime);
      }
    }
  }

  const hoverWatcher = watch(
    isOutsideAll,
    (val) => {
      setValueDelay(!val);
    },
    { immediate: true },
  );

  const controller = {
    enable() {
      hoverWatcher.resume();
    },
    disable() {
      hoverWatcher.pause();
    },
  };

  onUnmounted(() => {
    clearTimers();
    // Stop the listener
    stopWatcher();
    // Stop all remaining scopes
    hoverScopes.value.forEach((scope) => scope.stop());
  });

  return [value, controller] as [typeof value, typeof controller];
}
