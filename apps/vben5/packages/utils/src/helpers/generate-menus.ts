import type { Router, RouteRecordRaw } from 'vue-router';

import type {
  ExRouteRecordRaw,
  MenuRecordRaw,
  RouteMeta,
} from '@vben-core/typings';

import { filterTree, mapTree, sortTree } from '@vben-core/shared/utils';

/**
 * Generate menu list based on routes
 * @param routes - route configuration list
 * @param router - Vue Router instance
 * @returns generated menu list
 */
function generateMenus(
  routes: RouteRecordRaw[],
  router: Router,
): MenuRecordRaw[] {
  // Convert the route list into an object map with name as the key
  const finalRoutesMap: { [key: string]: string } = Object.fromEntries(
    router.getRoutes().map(({ name, path }) => [name, path]),
  );

  let menus = mapTree<ExRouteRecordRaw, MenuRecordRaw>(routes, (route) => {
    // Get the final routing path
    const path = finalRoutesMap[route.name as string] ?? route.path ?? '';

    const {
      meta = {} as RouteMeta,
      name: routeName,
      redirect,
      children = [],
    } = route;
    const {
      activeIcon,
      badge,
      badgeType,
      badgeVariants,
      hideChildrenInMenu = false,
      icon,
      link,
      order,
      title = '',
    } = meta;

    // Make sure the menu name is not empty
    const name = (title || routeName || '') as string;

    // handle submenu
    const resultChildren = hideChildrenInMenu
      ? []
      : ((children as MenuRecordRaw[]) ?? []);

    // Set the parent-child relationship of the submenu
    if (resultChildren.length > 0) {
      resultChildren.forEach((child) => {
        child.parents = [...(route.parents ?? []), path];
        child.parent = path;
      });
    }

    // Determine the final path
    const resultPath = hideChildrenInMenu ? redirect || path : link || path;

    return {
      activeIcon,
      badge,
      badgeType,
      badgeVariants,
      icon,
      name,
      order,
      parent: route.parent,
      parents: route.parents,
      path: resultPath,
      show: !meta.hideInMenu,
      children: resultChildren,
    };
  });

  // Sort the menu to avoid the problem of order=0 being replaced by 999
  menus = sortTree(menus, (a, b) => (a?.order ?? 999) - (b?.order ?? 999));

  // Filter out hidden menu items
  return filterTree(menus, (menu) => !!menu.show);
}

export { generateMenus };
