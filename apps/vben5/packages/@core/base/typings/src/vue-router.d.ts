import type { Component } from 'vue';
import type { Router, RouteRecordRaw } from 'vue-router';

interface RouteMeta {
  /**
   * Activation icon (menu/tab)
   */
  activeIcon?: string;
  /**
   * The currently activated menu, sometimes you don’t want to activate the existing menu and need to activate the parent menu.
   */
  activePath?: string;
  /**
   * Whether to pin the tab page
   * @default false
   */
  affixTab?: boolean;
  /**
   * Fixed tab order
   * @default 0
   */
  affixTabOrder?: number;
  /**
   * Requires specific role identification to access
   * @default []
   */
  authority?: string[];
  /**
   * Logo
   */
  badge?: string;
  /**
   * Logo type
   */
  badgeType?: 'dot' | 'normal';
  /**
   * Logo color
   */
  badgeVariants?:
    | 'default'
    | 'destructive'
    | 'primary'
    | 'success'
    | 'warning'
    | string;
  /**
   * Whether the route corresponding to the dom is cached
   */
  domCached?: boolean;
  /**
   * The full path of the route as key (default true)
   */
  fullPathKey?: boolean;
  /**
   * The children of the current route are not displayed in the menu
   * @default false
   */
  hideChildrenInMenu?: boolean;
  /**
   * The current route is not displayed in breadcrumbs
   * @default false
   */
  hideInBreadcrumb?: boolean;
  /**
   * The current route is not displayed in the menu
   * @default false
   */
  hideInMenu?: boolean;
  /**
   * The current route is not displayed on the tab page
   * @default false
   */
  hideInTab?: boolean;
  /**
   * Icon (menu/tab)
   */
  icon?: Component | string;
  /**
   * iframe address
   */
  iframeSrc?: string;
  /**
   * Ignore permissions and can access directly
   * @default false
   */
  ignoreAccess?: boolean;
  /**
   * Turn on KeepAlive cache
   */
  keepAlive?: boolean;
  /**
   * External link-jump path
   */
  link?: string;
  /**
   * Whether the route has been loaded
   */
  loaded?: boolean;
  /**
   * Maximum number of open tabs
   * @default -1
   */
  maxNumOfOpenTab?: number;
  /**
   * The menu can be seen, but access will be redirected to 403
   */
  menuVisibleWithForbidden?: boolean;
  /**
   * Do not use base layout (only takes effect at top level)
   */
  noBasicLayout?: boolean;
  /**
   *Opens in new window
   */
  openInNewWindow?: boolean;
  /**
   * Used for routing->menu sorting
   */
  order?: number;
  /**
   * Parameters carried by the menu
   */
  query?: Recordable;
  /**
   * title name
   */
  title: string;
}

// Define a recursive type to change the component property of RouteRecordRaw to string
type RouteRecordStringComponent<T = string> = Omit<
  RouteRecordRaw,
  'children' | 'component'
> & {
  children?: RouteRecordStringComponent<T>[];
  component: T;
};

type ComponentRecordType = Record<string, () => Promise<Component>>;

interface GenerateMenuAndRoutesOptions {
  fetchMenuListAsync?: () => Promise<RouteRecordStringComponent[]>;
  forbiddenComponent?: RouteRecordRaw['component'];
  layoutMap?: ComponentRecordType;
  pageMap?: ComponentRecordType;
  roles?: string[];
  router: Router;
  routes: RouteRecordRaw[];
}

export type {
  ComponentRecordType,
  GenerateMenuAndRoutesOptions,
  RouteMeta,
  RouteRecordRaw,
  RouteRecordStringComponent,
};
