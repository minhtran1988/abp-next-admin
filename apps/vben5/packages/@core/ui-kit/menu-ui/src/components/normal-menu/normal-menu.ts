import type { MenuRecordRaw } from '@vben-core/typings';

interface NormalMenuProps {
  /**
   * Menu data
   */
  activePath?: string;
  /**
   * Whether to fold
   */
  collapse?: boolean;
  /**
   * Menu items
   */
  menus?: MenuRecordRaw[];
  /**
   * @zh_CN Is it round style?
   * @default true
   */
  rounded?: boolean;
  /**
   * theme
   */
  theme?: 'dark' | 'light';
}

export type { NormalMenuProps };
