import type { Router, RouteRecordName, RouteRecordRaw } from 'vue-router';

import { traverseTreeValues } from '@vben-core/shared/utils';

/**
 * @zh_CN Reset all routes, except whitelist if specified
 */
export function resetStaticRoutes(router: Router, routes: RouteRecordRaw[]) {
  // Get the names of all nodes of the static route that contain the child nodes, and exclude routes that do not have the name field
  const staticRouteNames = traverseTreeValues<
    RouteRecordRaw,
    RouteRecordName | undefined
  >(routes, (route) => {
    // These routes need to specify a name to prevent routes without a specified name from being deleted when the route is reset.
    if (!route.name) {
      console.warn(
        `The route with the path ${route.path} needs to have the field name specified.`,
      );
    }
    return route.name;
  });

  const { getRoutes, hasRoute, removeRoute } = router;
  const allRoutes = getRoutes();
  allRoutes.forEach(({ name }) => {
    // Only if it exists in the routing table and is not in the whitelist, it needs to be deleted.
    if (name && !staticRouteNames.includes(name) && hasRoute(name)) {
      removeRoute(name);
    }
  });
}
