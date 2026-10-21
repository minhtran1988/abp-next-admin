<script setup lang="ts">
import { computed, unref, watch } from 'vue';
import { useRoute } from 'vue-router';

import { preferences } from '@vben/preferences';
import { getTabKey, storeToRefs, useTabbarStore } from '@vben/stores';

import { transformComponent, useLayoutHook } from '../hooks';

const route = useRoute();

const tabbarStore = useTabbarStore();

const { getTabs, getCachedRoutes, getExcludeCachedTabs } =
  storeToRefs(tabbarStore);
const { removeCachedRoute } = tabbarStore;

const { getEnabledTransition, getTransitionName } = useLayoutHook();

/**
 * Whether to enable tabs
 */
const enableTabbar = computed(() => preferences.tabbar.enable);

const computedCachedRouteKeys = computed(() => {
  if (!unref(enableTabbar)) {
    return [];
  }
  return unref(getTabs)
    .filter((item) => item.meta.domCached)
    .map((item) => getTabKey(item));
});

/**
 * Monitor changes in cache routes and delete non-existent cache routes
 */
watch(computedCachedRouteKeys, (keys) => {
  unref(getCachedRoutes).forEach((item) => {
    if (!keys.includes(item.key)) {
      removeCachedRoute(item.key);
    }
  });
});

/**
 * All cached routes
 */
const computedCachedRoutes = computed(() => {
  if (!unref(enableTabbar)) {
    return [];
  }
  // Refresh the route to refresh the cache
  const excludeCachedTabKeys = unref(getExcludeCachedTabs);
  return [...unref(getCachedRoutes).values()].filter((item) => {
    const componentType: any = item.component.type || {};
    let componentName = componentType.name;
    if (!componentName) {
      componentName = item.route.name;
    }
    return !excludeCachedTabKeys.includes(componentName);
  });
});

/**
 * Whether to display
 */
const computedShowView = computed(() => unref(computedCachedRoutes).length > 0);

const computedCurrentRouteKey = computed(() => {
  return getTabKey(route);
});
</script>

<template>
  <template v-if="computedShowView">
    <template v-for="item in computedCachedRoutes" :key="item.key">
      <Transition
        v-if="getEnabledTransition"
        appear
        mode="out-in"
        :name="getTransitionName(item.route)"
      >
        <component
          v-show="item.key === computedCurrentRouteKey"
          :is="transformComponent(item.component, item.route)"
        />
      </Transition>
      <template v-else>
        <component
          v-show="item.key === computedCurrentRouteKey"
          :is="transformComponent(item.component, item.route)"
        />
      </template>
    </template>
  </template>
</template>

<style scoped></style>
