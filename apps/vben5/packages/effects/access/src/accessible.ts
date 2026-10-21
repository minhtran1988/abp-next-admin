import type { Component, DefineComponent } from 'vue';

import type {
  AccessModeType,
  GenerateMenuAndRoutesOptions,
  RouteRecordRaw,
} from '@vben/types';

import { defineComponent, h } from 'vue';

import {
  cloneDeep,
  generateMenus,
  generateRoutesByBackend,
  generateRoutesByFrontend,
  isFunction,
  isString,
  mapTree,
} from '@vben/utils';

async function generateAccessible(
  mode: AccessModeType,
  options: GenerateMenuAndRoutesOptions,
) {
  const { router } = options;

  options.routes = cloneDeep(options.routes);
  // Generate route
  const accessibleRoutes = await generateRoutes(mode, options);

  const root = router.getRoutes().find((item) => item.path === '/');

  // Get the list of existing route names
  const names = root?.children?.map((item) => item.name) ?? [];

  // Dynamically added to the router instance
  accessibleRoutes.forEach((route) => {
    if (root && !route.meta?.noBasicLayout) {
      // In order to be compatible with the previous version usage, if it contains sub-routes, remove the component to avoid multi-layer BasicLayout
      // If your project has followed this modification and removed the BasicLayout at the first level of all custom menus, you can delete this if code
      if (route.children && route.children.length > 0) {
        delete route.component;
      }
      // Based on the router name, if the route already exists, it will not be added again.
      if (names?.includes(route.name)) {
        // Find the existing routing index and update it. Failure to update will cause 404 problems caused by the first-level directory not being updated when switching users, and homePath being in the second-level directory.
        const index = root.children?.findIndex(
          (item) => item.name === route.name,
        );
        if (index !== undefined && index !== -1 && root.children) {
          root.children[index] = route;
        }
      } else {
        root.children?.push(route);
      }
    } else {
      router.addRoute(route);
    }
  });

  if (root) {
    if (root.name) {
      router.removeRoute(root.name);
    }
    router.addRoute(root);
  }

  // Generate menu
  const accessibleMenus = generateMenus(accessibleRoutes, options.router);

  return { accessibleMenus, accessibleRoutes };
}

/**
 * Generate routes
 * @param mode
 * @param options
 */
async function generateRoutes(
  mode: AccessModeType,
  options: GenerateMenuAndRoutesOptions,
) {
  const { forbiddenComponent, roles, routes } = options;

  let resultRoutes: RouteRecordRaw[] = routes;
  switch (mode) {
    case 'backend': {
      resultRoutes = await generateRoutesByBackend(options);
      break;
    }
    case 'frontend': {
      resultRoutes = await generateRoutesByFrontend(
        routes,
        roles || [],
        forbiddenComponent,
      );
      break;
    }
    case 'mixed': {
      const [frontend_resultRoutes, backend_resultRoutes] = await Promise.all([
        generateRoutesByFrontend(routes, roles || [], forbiddenComponent),
        generateRoutesByBackend(options),
      ]);

      resultRoutes = [...frontend_resultRoutes, ...backend_resultRoutes];
      break;
    }
  }

  /**
   * Adjust the routing tree and do the following:
   * 1. Add redirect to routes that have not been redirected
   * 2. Change the name of the lazy loaded component to the name of the current route (if keep-alive is enabled)
   */
  resultRoutes = mapTree(resultRoutes, (route) => {
    // Repackage the component and use the same name as the route name to support keep-alive conditional caching.
    if (
      route.meta?.keepAlive &&
      isFunction(route.component) &&
      route.name &&
      isString(route.name)
    ) {
      const originalComponent = route.component as () => Promise<{
        default: Component | DefineComponent;
      }>;
      route.component = async () => {
        const component = await originalComponent();
        if (!component.default) return component;
        return defineComponent({
          name: route.name as string,
          setup(props, { attrs, slots }) {
            return () => h(component.default, { ...props, ...attrs }, slots);
          },
        });
      };
    }

    // If there is a redirect or no sub-route, return directly
    if (route.redirect || !route.children || route.children.length === 0) {
      return route;
    }
    const firstChild = route.children[0];

    // If the child route does not start with /, return directly. In this case, you need to calculate the paths of all parents to get the correct path, which is not processed here.
    if (!firstChild?.path || !firstChild.path.startsWith('/')) {
      return route;
    }

    route.redirect = firstChild.path;
    return route;
  });

  return resultRoutes;
}

export { generateAccessible };
