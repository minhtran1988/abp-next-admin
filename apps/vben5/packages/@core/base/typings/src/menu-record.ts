import type { Component } from 'vue';
import type { RouteRecordRaw } from 'vue-router';

/**
 * Extend the routing original object
 */
type ExRouteRecordRaw = RouteRecordRaw & {
  parent?: string;
  parents?: string[];
  path?: any;
};

interface MenuRecordBadgeRaw {
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
  badgeVariants?: 'destructive' | 'primary' | string;
}

/**
 * Menu original object
 */
interface MenuRecordRaw extends MenuRecordBadgeRaw {
  /**
   * Icon name when activated
   */
  activeIcon?: string;
  /**
   * Submenu
   */
  children?: MenuRecordRaw[];
  /**
   * Whether to disable the menu
   * @default false
   */
  disabled?: boolean;
  /**
   * Icon name
   */
  icon?: Component | string;
  /**
   *Menu name
   */
  name: string;
  /**
   *Sequence number
   */
  order?: number;
  /**
   * Parent path
   */
  parent?: string;
  /**
   * All parent paths
   */
  parents?: string[];
  /**
   * Menu path, unique, can be used as key
   */
  path: string;
  /**
   * Whether to display the menu
   * @default true
   */
  show?: boolean;
}

export type { ExRouteRecordRaw, MenuRecordBadgeRaw, MenuRecordRaw };
