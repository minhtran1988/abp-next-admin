import type { Component } from 'vue';

interface IContextMenuItem {
  /**
   * @zh_CN Whether to disable
   */
  disabled?: boolean;
  /**
   * @zh_CN Click event processing
   * @param data
   */
  handler?: (data: any) => void;
  /**
   * @zh_CN Whether to hide
   */
  hidden?: boolean;
  /**
   * @zh_CN icon
   */
  icon?: Component;
  /**
   * @zh_CN Whether to display the icon
   */
  inset?: boolean;
  /**
   * @zh_CN unique identifier
   */
  key: string;
  /**
   * @zh_CN Is it a dividing line?
   */
  separator?: boolean;
  /**
   * @zh_CN shortcut keys
   */
  shortcut?: string;
  /**
   * @zh_CN title
   */
  text: string;
}
export type { IContextMenuItem };
