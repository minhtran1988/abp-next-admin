import type { RouteRecordRaw } from 'vue-router';

import { filterTree, mapTree } from '@vben-core/shared/utils';

/**
 * Dynamically generate routes - front-end method
 */
async function generateRoutesByFrontend(
  routes: RouteRecordRaw[],
  roles: string[],
  forbiddenComponent?: RouteRecordRaw['component'],
): Promise<RouteRecordRaw[]> {
  // Filter the routing table based on the role ID to determine whether the current user has the specified permissions
  const finalRoutes = filterTree(routes, (route) => {
    return hasAuthority(route, roles);
  });

  if (!forbiddenComponent) {
    return finalRoutes;
  }

  // If there is a prohibited page, replace the prohibited page with a 403 page
  return mapTree(finalRoutes, (route) => {
    if (menuHasVisibleWithForbidden(route)) {
      route.component = forbiddenComponent;
    }
    return route;
  });
}

/**
 * Determine whether the route has permission to access
 * @param route
 * @param access
 */
function hasAuthority(route: RouteRecordRaw, access: string[]) {
  const authority = route.meta?.authority;
  if (!authority) {
    return true;
  }
  const canAccess = access.some((value) => authority.includes(value));

  return canAccess || (!canAccess && menuHasVisibleWithForbidden(route));
}

/**
 * Determine whether the route is displayed in the menu, but the access will be redirected to 403
 * @param route
 */
function menuHasVisibleWithForbidden(route: RouteRecordRaw) {
  return (
    !!route.meta?.authority &&
    Reflect.has(route.meta || {}, 'menuVisibleWithForbidden') &&
    !!route.meta?.menuVisibleWithForbidden
  );
}

export { generateRoutesByFrontend, hasAuthority };
