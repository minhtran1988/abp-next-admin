import type { ComputedRef, VNode } from 'vue';
import type {
  RouteLocationNormalized,
  RouteLocationNormalizedLoaded,
  RouteLocationNormalizedLoadedGeneric,
  Router,
  RouteRecordNormalized,
} from 'vue-router';

import type { TabDefinition } from '@vben-core/typings';

import { markRaw, toRaw } from 'vue';

import { preferences } from '@vben-core/preferences';
import {
  createStack,
  openRouteInNewWindow,
  Stack,
  startProgress,
  stopProgress,
} from '@vben-core/shared/utils';

import { acceptHMRUpdate, defineStore } from 'pinia';

interface RouteCached {
  component: VNode;
  key: string;
  route: RouteLocationNormalizedLoadedGeneric;
}

interface TabbarState {
  cachedRoutes: Map<string, RouteCached>;
  /**
   * @zh_CN Currently opened tab list cache
   */
  cachedTabs: Set<string>;
  /**
   * @zh_CN Index of drag end
   */
  dragEndIndex: number;
  /**
   * @zh_CN Need to exclude cached tabs
   */
  excludeCachedTabs: Set<string>;
  /**
   * @zh_CN Label right-click menu list
   */
  menuList: string[];
  /**
   * @zh_CN Whether to refresh
   */
  renderRouteView?: boolean;
  /**
   * @zh_CN List of currently opened tabs
   */
  tabs: TabDefinition[];
  /**
   * @zh_CN Update time, used in some update scenarios. If you use watch depth monitoring, performance will be lost.
   */
  updateTime?: number;
  /**
   * @zh_CN The tab opened by the previous tab page
   */
  visitHistory: Stack<string>;
}

/**
 * @zh_CN Maximum number of access history records
 */
const MAX_VISIT_HISTORY = 50;

/**
 * @zh_CN Access permission related
 */
export const useTabbarStore = defineStore('core-tabbar', {
  actions: {
    /**
     * Close tabs in bulk
     */
    async _bulkCloseByKeys(keys: string[]) {
      const keySet = new Set(keys);
      this.tabs = this.tabs.filter(
        (item) => !keySet.has(getTabKeyFromTab(item)),
      );
      if (isVisitHistory()) {
        this.visitHistory.remove(...keys);
      }

      await this.updateCacheTabs();
    },
    /**
     * @zh_CN Close the tab
     * @param tab
     */
    _close(tab: TabDefinition) {
      if (isAffixTab(tab)) {
        return;
      }
      const index = this.tabs.findIndex((item) => equalTab(item, tab));
      index !== -1 && this.tabs.splice(index, 1);
    },
    /**
     * @zh_CN Jump to the default tab page
     */
    async _goToDefaultTab(router: Router) {
      if (this.getTabs.length <= 0) {
        return;
      }
      const firstTab = this.getTabs[0];
      if (firstTab) {
        await this._goToTab(firstTab, router);
      }
    },
    /**
     * @zh_CN Jump to tab page
     * @param tab
     * @param router
     */
    async _goToTab(tab: TabDefinition, router: Router) {
      const { params, path, query } = tab;
      const toParams = {
        params: params || {},
        path,
        query: query || {},
      };
      await router.replace(toParams);
    },
    /**
     * @zh_CN Add tab page
     * @param routeTab
     */
    addTab(routeTab: TabDefinition): TabDefinition {
      let tab = cloneTab(routeTab);
      if (!tab.key) {
        tab.key = getTabKey(routeTab);
      }
      if (!isTabShown(tab)) {
        return tab;
      }

      const tabIndex = this.tabs.findIndex((item) => {
        return equalTab(item, tab);
      });

      if (tabIndex === -1) {
        const maxCount = preferences.tabbar.maxCount;
        // Get the number of dynamic route openings. If it exceeds 0, it means you need to control the number of openings.
        const maxNumOfOpenTab = (routeTab?.meta?.maxNumOfOpenTab ??
          -1) as number;
        // If the dynamic routing level is greater than 0, then it is necessary to limit the number of openings of the route.
        // Get the number of opened dynamic routes and determine whether it is greater than a certain value
        if (
          maxNumOfOpenTab > 0 &&
          this.tabs.filter((tab) => tab.name === routeTab.name).length >=
            maxNumOfOpenTab
        ) {
          // Close the first one
          const index = this.tabs.findIndex(
            (item) => item.name === routeTab.name,
          );
          index !== -1 && this.tabs.splice(index, 1);
        } else if (maxCount > 0 && this.tabs.length >= maxCount) {
          // Close the first one
          const index = this.tabs.findIndex(
            (item) =>
              !Reflect.has(item.meta, 'affixTab') || !item.meta.affixTab,
          );
          index !== -1 && this.tabs.splice(index, 1);
        }
        this.tabs.push(tab);
      } else {
        // The page already exists, do not add tabs again, only update tab parameters
        const currentTab = toRaw(this.tabs)[tabIndex];
        const mergedTab = {
          ...currentTab,
          ...tab,
          meta: { ...currentTab?.meta, ...tab.meta },
        };
        if (currentTab) {
          const curMeta = currentTab.meta;
          if (Reflect.has(curMeta, 'affixTab')) {
            mergedTab.meta.affixTab = curMeta.affixTab;
          }
          if (Reflect.has(curMeta, 'newTabTitle')) {
            mergedTab.meta.newTabTitle = curMeta.newTabTitle;
          }
        }
        tab = mergedTab;
        this.tabs.splice(tabIndex, 1, mergedTab);
      }
      this.updateCacheTabs();
      // Add access history
      if (isVisitHistory()) {
        this.visitHistory.push(tab.key as string);
      }
      return tab;
    },
    /**
     * @zh_CN Close all tabs
     */
    async closeAllTabs(router: Router) {
      const newTabs = this.tabs.filter((tab) => isAffixTab(tab));
      this.tabs = newTabs.length > 0 ? newTabs : [...this.tabs].splice(0, 1);
      // Set access history
      if (isVisitHistory()) {
        this.visitHistory.retain(
          this.tabs.map((item) => getTabKeyFromTab(item)),
        );
      }
      await this._goToDefaultTab(router);
      this.updateCacheTabs();
    },
    /**
     * @zh_CN Close the left tab
     * @param tab
     */
    async closeLeftTabs(tab: TabDefinition) {
      const index = this.tabs.findIndex((item) => equalTab(item, tab));

      if (index < 1) {
        return;
      }

      const leftTabs = this.tabs.slice(0, index);
      const keys: string[] = [];

      for (const item of leftTabs) {
        if (!isAffixTab(item)) {
          keys.push(item.key as string);
        }
      }
      await this._bulkCloseByKeys(keys);
    },
    /**
     * @zh_CN Close other tabs
     * @param tab
     */
    async closeOtherTabs(tab: TabDefinition) {
      const closeKeys = this.tabs.map((item) => getTabKeyFromTab(item));

      const keys: string[] = [];

      for (const key of closeKeys) {
        if (key !== getTabKeyFromTab(tab)) {
          const closeTab = this.tabs.find(
            (item) => getTabKeyFromTab(item) === key,
          );
          if (!closeTab) {
            continue;
          }
          if (!isAffixTab(closeTab)) {
            keys.push(closeTab.key as string);
          }
        }
      }
      await this._bulkCloseByKeys(keys);
    },
    /**
     * @zh_CN Close the tab on the right
     * @param tab
     */
    async closeRightTabs(tab: TabDefinition) {
      const index = this.tabs.findIndex((item) => equalTab(item, tab));

      if (index !== -1 && index < this.tabs.length - 1) {
        const rightTabs = this.tabs.slice(index + 1);

        const keys: string[] = [];
        for (const item of rightTabs) {
          if (!isAffixTab(item)) {
            keys.push(item.key as string);
          }
        }
        await this._bulkCloseByKeys(keys);
      }
    },

    /**
     * @zh_CN Close the tab
     * @param tab
     * @param router
     */
    async closeTab(tab: TabDefinition, router: Router) {
      const { currentRoute } = router;
      const currentTabKey = getTabKey(currentRoute.value);
      // Close the tab that is not active
      if (currentTabKey !== getTabKeyFromTab(tab)) {
        this._close(tab);
        this.updateCacheTabs();
        // Remove access history
        if (isVisitHistory()) {
          this.visitHistory.remove(getTabKeyFromTab(tab));
        }
        return;
      }
      if (this.getTabs.length <= 1) {
        console.error('Failed to close the tab; only one tab remains open.');
        return;
      }
      // Remove the currently closed tab from the access history
      if (isVisitHistory()) {
        this.visitHistory.remove(currentTabKey);
        this._close(tab);

        let previousTab: TabDefinition | undefined;
        let previousTabKey: string | undefined;
        while (true) {
          previousTabKey = this.visitHistory.pop();
          if (!previousTabKey) {
            break;
          }
          previousTab = this.getTabByKey(previousTabKey);
          if (previousTab) {
            break;
          }
        }
        await (previousTab
          ? this._goToTab(previousTab, router)
          : this._goToDefaultTab(router));
        return;
      }
      // If access history is not enabled, jump directly to the next or previous tab.
      const index = this.getTabs.findIndex(
        (item) => getTabKeyFromTab(item) === getTabKey(currentRoute.value),
      );

      const before = this.getTabs[index - 1];
      const after = this.getTabs[index + 1];

      // The next tab exists, jump to the next one
      if (after) {
        this._close(tab);
        await this._goToTab(after, router);
        // The previous tab exists, jump to the previous one
      } else if (before) {
        this._close(tab);
        await this._goToTab(before, router);
      }
    },

    /**
     * @zh_CN Close the tab through key
     * @param key
     * @param router
     */
    async closeTabByKey(key: string, router: Router) {
      const originKey = decodeURIComponent(key);
      const index = this.tabs.findIndex(
        (item) => getTabKeyFromTab(item) === originKey,
      );
      if (index === -1) {
        return;
      }

      const tab = this.tabs[index];
      if (tab) {
        await this.closeTab(tab, router);
      }
    },

    /**
     * Get the tab based on the tab's key
     * @param key
     */
    getTabByKey(key: string) {
      return this.getTabs.find(
        (item) => getTabKeyFromTab(item) === key,
      ) as TabDefinition;
    },
    /**
     * @zh_CN Open tab in new window
     * @param tab
     */
    async openTabInNewWindow(tab: TabDefinition) {
      openRouteInNewWindow(tab.fullPath || tab.path);
    },

    /**
     * @zh_CN Fixed tab page
     * @param tab
     */
    async pinTab(tab: TabDefinition) {
      const index = this.tabs.findIndex((item) => equalTab(item, tab));
      if (index === -1) {
        return;
      }
      const oldTab = this.tabs[index];
      tab.meta.affixTab = true;
      tab.meta.title = oldTab?.meta?.title as string;
      // this.addTab(tab);
      this.tabs.splice(index, 1, tab);
      // Filter fixed tabs. If you change the value of affixTabOrder later, there may be problems. Currently, line 464 sorting affixTabs has no value set.
      const affixTabs = this.tabs.filter((tab) => isAffixTab(tab));
      // Get the index of fixed tabs
      const newIndex = affixTabs.findIndex((item) => equalTab(item, tab));
      // Swap positions and reorder
      await this.sortTabs(index, newIndex);
    },

    /**
     * Refresh tab page
     */
    async refresh(router: Router | string) {
      // If it is a Router route, then refresh it based on the current route.
      // If it is a string string, it is the route name, and the specified tab page is refreshed in a directed manner. It cannot be the current route name, otherwise it will not be refreshed.
      if (typeof router === 'string') {
        return await this.refreshByName(router);
      }

      const { currentRoute } = router;
      const { name } = currentRoute.value;

      this.excludeCachedTabs.add(name as string);
      this.renderRouteView = false;
      startProgress();

      await new Promise((resolve) => setTimeout(resolve, 200));

      this.excludeCachedTabs.delete(name as string);
      this.renderRouteView = true;
      stopProgress();
    },

    /**
     * Refresh the specified tab page based on the route name
     */
    async refreshByName(name: string) {
      this.excludeCachedTabs.add(name);
      await new Promise((resolve) => setTimeout(resolve, 200));
      this.excludeCachedTabs.delete(name);
    },

    /**
     * @zh_CN Reset tab title
     */
    async resetTabTitle(tab: TabDefinition) {
      if (tab?.meta?.newTabTitle) {
        return;
      }
      const findTab = this.tabs.find((item) => equalTab(item, tab));
      if (findTab) {
        findTab.meta.newTabTitle = undefined;
        await this.updateCacheTabs();
      }
    },

    /**
     * Set fixed tab page
     * @param tabs
     */
    setAffixTabs(tabs: RouteRecordNormalized[]) {
      for (const tab of tabs) {
        tab.meta.affixTab = true;
        this.addTab(routeToTab(tab));
      }
    },

    /**
     * @zh_CN Update menu list
     * @param list
     */
    setMenuList(list: string[]) {
      this.menuList = list;
    },

    /**
     * @zh_CN Set tab title
     *
     * @zh_CN supports setting static title strings or calculated properties as dynamic titles
     * @zh_CN When the title is a calculated attribute, the title will be automatically updated as the calculated attribute value changes.
     * @zh_CN Suitable for scenarios where titles need to be dynamically updated based on status or in multiple languages
     *
     * @param {TabDefinition} tab - tab page object
     * @param {ComputedRef<string> | string} title - title content, supports static string or calculated properties
     *
     * @example
     * // Set static title
     * setTabTitle(tab, 'New tab page');
     *
     * @example
     * // Set dynamic title
     * setTabTitle(tab, computed(() => t('common.dashboard')));
     */
    async setTabTitle(tab: TabDefinition, title: ComputedRef<string> | string) {
      const findTab = this.tabs.find((item) => equalTab(item, tab));

      if (findTab) {
        findTab.meta.newTabTitle = title;

        await this.updateCacheTabs();
      }
    },
    setUpdateTime() {
      this.updateTime = Date.now();
    },
    /**
     * @zh_CN Set tab page order
     * @param oldIndex
     * @param newIndex
     */
    async sortTabs(oldIndex: number, newIndex: number) {
      const currentTab = this.tabs[oldIndex];
      if (!currentTab) {
        return;
      }
      this.tabs.splice(oldIndex, 1);
      this.tabs.splice(newIndex, 0, currentTab);
      this.dragEndIndex = this.dragEndIndex + 1;
    },

    /**
     * @zh_CN Switch fixed tab page
     * @param tab
     */
    async toggleTabPin(tab: TabDefinition) {
      const affixTab = tab?.meta?.affixTab ?? false;

      await (affixTab ? this.unpinTab(tab) : this.pinTab(tab));
    },

    /**
     * @zh_CN Unpin the tab
     * @param tab
     */
    async unpinTab(tab: TabDefinition) {
      const index = this.tabs.findIndex((item) => equalTab(item, tab));
      if (index === -1) {
        return;
      }
      const oldTab = this.tabs[index];
      tab.meta.affixTab = false;
      tab.meta.title = oldTab?.meta?.title as string;
      // this.addTab(tab);
      this.tabs.splice(index, 1, tab);
      // Filter fixed tabs. If you change the value of affixTabOrder later, there may be problems. Currently, line 464 sorting affixTabs has no value set.
      const affixTabs = this.tabs.filter((tab) => isAffixTab(tab));
      // Get the index of fixed tabs, use the next position of fixed tabs which is the first position of active tabs
      const newIndex = affixTabs.length;
      // Swap positions and reorder
      await this.sortTabs(index, newIndex);
    },
    /**
     * Update the cache based on the currently open tab
     */
    async updateCacheTabs() {
      const cacheMap = new Set<string>();

      for (const tab of this.tabs) {
        // Skip tabs that do not require persistence
        const keepAlive = tab.meta?.keepAlive;
        if (!keepAlive) {
          continue;
        }
        (tab.matched || []).forEach((t, i) => {
          if (i > 0) {
            cacheMap.add(t.name as string);
          }
        });

        const name = tab.name as string;
        cacheMap.add(name);
      }
      this.cachedTabs = cacheMap;
    },
    /**
     * Add cached route
     * @param component
     * @param route
     */
    addCachedRoute(component: VNode, route: RouteLocationNormalizedLoaded) {
      const key = getTabKey(route);
      if (this.cachedRoutes.has(key)) {
        return;
      }
      this.cachedRoutes.set(key, {
        key,
        component: markRaw(component),
        route: markRaw(route),
      });
    },
    removeCachedRoute(key: string) {
      this.cachedRoutes.delete(key);
    },
  },
  getters: {
    affixTabs(): TabDefinition[] {
      const affixTabs = this.tabs.filter((tab) => isAffixTab(tab));

      return affixTabs.toSorted((a, b) => {
        const orderA = (a.meta?.affixTabOrder ?? 0) as number;
        const orderB = (b.meta?.affixTabOrder ?? 0) as number;
        return orderA - orderB;
      });
    },
    getCachedTabs(): string[] {
      return [...this.cachedTabs];
    },
    getExcludeCachedTabs(): string[] {
      return [...this.excludeCachedTabs];
    },
    getMenuList(): string[] {
      return this.menuList;
    },
    getTabs(): TabDefinition[] {
      const normalTabs = this.tabs.filter((tab) => !isAffixTab(tab));
      return [...this.affixTabs, ...normalTabs].filter(Boolean);
    },
    getCachedRoutes(): Map<string, RouteCached> {
      return this.cachedRoutes;
    },
  },
  persist: [
    // tabs do not need to be saved in localStorage
    {
      pick: ['tabs', 'visitHistory'],
      storage: sessionStorage,
      serializer: {
        serialize: JSON.stringify,
        deserialize(value: string) {
          const parsed = JSON.parse(value);
          // Stack class instances will become ordinary objects {dedup, items, maxSize} after JSON serialization,
          // All methods and getters are lost and the Stack instance needs to be rebuilt
          if (parsed.visitHistory && !(parsed.visitHistory instanceof Stack)) {
            const raw = parsed.visitHistory;
            const stack = createStack<string>(true, MAX_VISIT_HISTORY);
            if (Array.isArray(raw.items)) {
              stack.push(...raw.items);
            }
            parsed.visitHistory = stack;
          }
          return parsed;
        },
      },
    },
  ],
  state: (): TabbarState => ({
    visitHistory: createStack<string>(true, MAX_VISIT_HISTORY),
    cachedRoutes: new Map<string, RouteCached>(),
    cachedTabs: new Set(),
    dragEndIndex: 0,
    excludeCachedTabs: new Set(),
    menuList: [
      'close',
      'affix',
      'maximize',
      'reload',
      'open-in-new-window',
      'close-left',
      'close-right',
      'close-other',
      'close-all',
    ],
    renderRouteView: true,
    tabs: [],
    updateTime: Date.now(),
  }),
});

// Solve the hot update problem
const hot = import.meta.hot;
if (hot) {
  hot.accept(acceptHMRUpdate(useTabbarStore, hot));
}

/**
 * @zh_CN Clone routes to prevent routes from being modified
 * @param route
 */
function cloneTab(route: TabDefinition): TabDefinition {
  if (!route) {
    return route;
  }
  const { matched, meta, ...opt } = route;
  return {
    ...opt,
    matched: (matched
      ? matched.map((item) => ({
          meta: item.meta,
          name: item.name,
          path: item.path,
        }))
      : undefined) as RouteRecordNormalized[],
    meta: {
      ...meta,
      newTabTitle: meta.newTabTitle,
    },
  };
}

/**
 * @zh_CN Whether it is a fixed tab page
 * @param tab
 */
function isAffixTab(tab: TabDefinition) {
  return tab?.meta?.affixTab ?? false;
}

/**
 * @zh_CN Whether to display labels
 * @param tab
 */
function isTabShown(tab: TabDefinition) {
  const matched = tab?.matched ?? [];
  return !tab.meta.hideInTab && matched.every((item) => !item.meta.hideInTab);
}

/**
 * Get the key of the tab page from route
 * @param tab
 */
function getTabKey(tab: RouteLocationNormalized | RouteRecordNormalized) {
  const {
    fullPath,
    path,
    meta: { fullPathKey } = {},
    query = {},
  } = tab as RouteLocationNormalized;
  // pageKey may be an array (may occur when query parameters are repeated)
  const pageKey = Array.isArray(query.pageKey)
    ? query.pageKey[0]
    : query.pageKey;
  let rawKey;
  if (pageKey) {
    rawKey = pageKey;
  } else {
    rawKey = fullPathKey === false ? path : (fullPath ?? path);
  }
  try {
    return decodeURIComponent(rawKey);
  } catch {
    return rawKey;
  }
}

/**
 * @zh_CN Whether to enable access history
 */
function isVisitHistory() {
  return preferences.tabbar.visitHistory;
}

/**
 * Get the key of the tab page from tab
 * If the tab does not have a key, then get the key from the route
 * @param tab
 */
function getTabKeyFromTab(tab: TabDefinition): string {
  return tab.key ?? getTabKey(tab);
}

/**
 * Compare two tabs to see if they are equal
 * @param a
 * @param b
 */
function equalTab(a: TabDefinition, b: TabDefinition) {
  return getTabKeyFromTab(a) === getTabKeyFromTab(b);
}

function routeToTab(route: RouteRecordNormalized) {
  return {
    meta: route.meta,
    name: route.name,
    path: route.path,
    key: getTabKey(route),
  } as TabDefinition;
}

export { getTabKey };
