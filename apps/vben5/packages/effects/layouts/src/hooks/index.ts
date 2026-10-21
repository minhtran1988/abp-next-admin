import type { VNode } from 'vue';
import type {
  RouteLocationNormalizedLoaded,
  RouteLocationNormalizedLoadedGeneric,
} from 'vue-router';

import { computed } from 'vue';

import { preferences, usePreferences } from '@vben/preferences';

/**
 * Convert component and automatically add name
 * @param component
 * @param route
 */
export function transformComponent(
  component: VNode,
  route: RouteLocationNormalizedLoadedGeneric,
) {
  // The component view is not found. If there is a backup view set, return the backup view. If not, throw an error.
  if (!component) {
    console.error(
      'Component view not found，please check the route configuration',
    );
    return undefined;
  }

  const routeName = route.name as string;
  // If the component does not have a name, return it directly
  if (!routeName) {
    return component;
  }
  const componentName = (component?.type as any)?.name;

  // If name has been set, return directly
  if (componentName) {
    return component;
  }

  // If componentName is consistent with routeName, return directly
  if (componentName === routeName) {
    return component;
  }

  // set name
  component.type ||= {};
  (component.type as any).name = routeName;

  return component;
}

/**
 * Layout related hooks
 */
export function useLayoutHook() {
  const { keepAlive } = usePreferences();
  /**
   * Whether to use animation
   */
  const getEnabledTransition = computed(() => {
    const { transition } = preferences;
    const transitionName = transition.name;
    return transitionName && transition.enable;
  });

  /**
   * Get route transition animation
   * @param _route
   */
  function getTransitionName(_route: RouteLocationNormalizedLoaded) {
    // If preference is not set, no animation is used
    const { tabbar, transition } = preferences;
    const transitionName = transition.name;
    if (!transitionName || !transition.enable) {
      return;
    }

    // If the tab is not enabled or the cache is not turned on, use the global configuration animation.
    if (!tabbar.enable || !keepAlive) {
      return transitionName;
    }

    // If the page has already been loaded, no animation is used
    // if (route.meta.loaded) {
    //   return;
    // }
    // Pages that have been opened and loaded do not use animations
    // const inTabs = getCachedTabs.value.includes(route.name as string);

    // return inTabs && route.meta.loaded ? undefined : transitionName;
    return transitionName;
  }

  return {
    getEnabledTransition,
    getTransitionName,
  };
}
