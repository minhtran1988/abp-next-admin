import type { Component, Ref } from 'vue';

import type { MenuRecordBadgeRaw, ThemeModeType } from '@vben-core/typings';

interface MenuProps {
  /**
   * @zh_CN Whether to enable accordion mode
   * @default true
   */
  accordion?: boolean;
  /**
   * @zh_CN Whether the menu is collapsed
   * @default false
   */
  collapse?: boolean;

  /**
   * @zh_CN Whether to display the menu name when the menu is collapsed
   * @default false
   */
  collapseShowTitle?: boolean;

  /**
   * @zh_CN Default activated menu
   */
  defaultActive?: string;

  /**
   * @zh_CN Default expanded menu
   */
  defaultOpeneds?: string[];

  /**
   * @zh_CN Menu mode
   * @default vertical
   */
  mode?: 'horizontal' | 'vertical';

  /**
   * @zh_CN Is it round style?
   * @default true
   */
  rounded?: boolean;

  /**
   * @zh_CN Whether to automatically scroll to the activated menu item
   * @default false
   */
  scrollToActive?: boolean;

  /**
   * @zh_CN Menu theme
   * @default dark
   */
  theme?: ThemeModeType;
}

interface SubMenuProps extends MenuRecordBadgeRaw {
  /**
   * @zh_CN activation icon
   */
  activeIcon?: string;
  /**
   * @zh_CN Whether to disable
   */
  disabled?: boolean;
  /**
   * @zh_CN icon
   */
  icon?: Component | string;
  /**
   * @zh_CN submenu name
   */
  path: string;
}

interface MenuItemProps extends MenuRecordBadgeRaw {
  /**
   * @zh_CN icon
   */
  activeIcon?: string;
  /**
   * @zh_CN Whether to disable
   */
  disabled?: boolean;
  /**
   * @zh_CN icon
   */
  icon?: Component | string;
  /**
   * @zh_CN menuitem name
   */
  path: string;
}

interface MenuItemRegistered {
  active: boolean;
  parentPaths: string[];
  path: string;
}

interface MenuItemClicked {
  parentPaths: string[];
  path: string;
}

interface MenuProvider {
  activePath?: string;
  addMenuItem: (item: MenuItemRegistered) => void;

  addSubMenu: (item: MenuItemRegistered) => void;
  closeMenu: (path: string, parentLinks: string[]) => void;
  handleMenuItemClick: (item: MenuItemClicked) => void;
  handleSubMenuClick: (subMenu: MenuItemRegistered) => void;
  isMenuPopup: boolean;
  items: Record<string, MenuItemRegistered>;

  openedMenus: string[];
  openMenu: (path: string, parentLinks: string[]) => void;
  props: MenuProps;
  removeMenuItem: (item: MenuItemRegistered) => void;

  removeSubMenu: (item: MenuItemRegistered) => void;

  subMenus: Record<string, MenuItemRegistered>;
  theme: string;
}

interface SubMenuProvider {
  addSubMenu: (item: MenuItemRegistered) => void;
  handleMouseleave?: (deepDispatch: boolean) => void;
  level: number;
  mouseInChild: Ref<boolean>;
  removeSubMenu: (item: MenuItemRegistered) => void;
}

export type {
  MenuItemClicked,
  MenuItemProps,
  MenuItemRegistered,
  MenuProps,
  MenuProvider,
  SubMenuProps,
  SubMenuProvider,
};
