import type { RouteRecordRaw } from 'vue-router';

// Define module type
interface RouteModuleType {
  default: RouteRecordRaw[];
}

/**
 * Merge default export of dynamic routing module
 * @param routeModules dynamically imported routing module object
 * @returns Merged routing configuration array
 */
function mergeRouteModules(
  routeModules: Record<string, unknown>,
): RouteRecordRaw[] {
  const mergedRoutes: RouteRecordRaw[] = [];

  for (const routeModule of Object.values(routeModules)) {
    const moduleRoutes = (routeModule as RouteModuleType)?.default ?? [];
    mergedRoutes.push(...moduleRoutes);
  }

  return mergedRoutes;
}

export { mergeRouteModules };

export type { RouteModuleType };
