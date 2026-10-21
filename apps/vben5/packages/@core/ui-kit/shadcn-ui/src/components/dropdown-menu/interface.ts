import type { Component } from 'vue';

interface VbenDropdownMenuItem {
  disabled?: boolean;
  /**
   * @zh_CN Click event processing
   * @param data
   */
  handler?: (data: any) => void;
  /**
   * @zh_CN icon
   */
  icon?: Component;
  /**
   * @zh_CN title
   */
  label: string;
  /**
   * @zh_CN Is it a dividing line?
   */
  separator?: boolean;
  /**
   * @zh_CN unique identifier
   */
  value: string;
}

interface DropdownMenuProps {
  menus: VbenDropdownMenuItem[];
}

export type { DropdownMenuProps, VbenDropdownMenuItem };
