import type { Recordable } from '@vben/types';

import { requestClient } from '#/api/request';

export namespace SystemMenuApi {
  /** Logo color collection */
  export const BadgeVariants = [
    'default',
    'destructive',
    'primary',
    'success',
    'warning',
  ] as const;
  /** Logo type collection */
  export const BadgeTypes = ['dot', 'normal'] as const;
  /** Menu type collection */
  export const MenuTypes = [
    'catalog',
    'menu',
    'embedded',
    'link',
    'button',
  ] as const;
  /** System menu */
  export interface SystemMenu {
    [key: string]: any;
    /** Backend permission identification */
    authCode: string;
    /** Child */
    children?: SystemMenu[];
    /** Components */
    component?: string;
    /** Menu ID */
    id: string;
    /** Menu metadata */
    meta?: {
      /** The icon displayed when activated */
      activeIcon?: string;
      /** When used as a route, the Path of the menu that needs to be activated */
      activePath?: string;
      /** Fixed in tab bar */
      affixTab?: boolean;
      /** Fixed order in tab bar */
      affixTabOrder?: number;
      /** Logo content (valid when the logo type is normal) */
      badge?: string;
      /** Logo type */
      badgeType?: (typeof BadgeTypes)[number];
      /** Logo color */
      badgeVariants?: (typeof BadgeVariants)[number];
      /** Hide subordinates in menu */
      hideChildrenInMenu?: boolean;
      /** Hide in breadcrumbs */
      hideInBreadcrumb?: boolean;
      /** Hide in menu */
      hideInMenu?: boolean;
      /** Hide in tab bar */
      hideInTab?: boolean;
      /** Menu icon */
      icon?: string;
      /** URL of embedded Iframe */
      iframeSrc?: string;
      /** Whether to cache the page */
      keepAlive?: boolean;
      /** URL of external link page */
      link?: string;
      /** The maximum number of open tags for the same route */
      maxNumOfOpenTab?: number;
      /** No basic layout required */
      noBasicLayout?: boolean;
      /** Whether to open in a new window */
      openInNewWindow?: boolean;
      /** Menu sorting */
      order?: number;
      /** Additional routing parameters */
      query?: Recordable<any>;
      /** Menu title */
      title?: string;
    };
    /** Menu name */
    name: string;
    /** Routing path */
    path: string;
    /** Parent ID */
    pid: string;
    /** Redirect */
    redirect?: string;
    /** Menu type */
    type: (typeof MenuTypes)[number];
  }
}

/**
 * Get menu data list
 */
async function getMenuList() {
  return requestClient.get<Array<SystemMenuApi.SystemMenu>>(
    '/system/menu/list',
  );
}

async function isMenuNameExists(
  name: string,
  id?: SystemMenuApi.SystemMenu['id'],
) {
  return requestClient.get<boolean>('/system/menu/name-exists', {
    params: { id, name },
  });
}

async function isMenuPathExists(
  path: string,
  id?: SystemMenuApi.SystemMenu['id'],
) {
  return requestClient.get<boolean>('/system/menu/path-exists', {
    params: { id, path },
  });
}

/**
 * Create menu
 * @param data menu data
 */
async function createMenu(
  data: Omit<SystemMenuApi.SystemMenu, 'children' | 'id'>,
) {
  return requestClient.post('/system/menu', data);
}

/**
 * Update menu
 *
 * @param id menu ID
 * @param data menu data
 */
async function updateMenu(
  id: string,
  data: Omit<SystemMenuApi.SystemMenu, 'children' | 'id'>,
) {
  return requestClient.put(`/system/menu/${id}`, data);
}

/**
 * Delete menu
 * @param id menu ID
 */
async function deleteMenu(id: string) {
  return requestClient.delete(`/system/menu/${id}`);
}

export {
  createMenu,
  deleteMenu,
  getMenuList,
  isMenuNameExists,
  isMenuPathExists,
  updateMenu,
};
