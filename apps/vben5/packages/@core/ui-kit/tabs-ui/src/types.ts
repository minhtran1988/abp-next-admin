import type { IContextMenuItem } from '@vben-core/shadcn-ui';
import type { TabDefinition, TabsStyleType } from '@vben-core/typings';

export type TabsEmits = {
  close: [string];
  sortTabs: [number, number];
  unpin: [TabDefinition];
};

export interface TabsProps {
  active?: string;
  /**
   * @zh_CN content class
   * @default tabs-chrome
   */
  contentClass?: string;
  /**
   * @zh_CN Right-click menu
   */
  contextMenus?: (data: any) => IContextMenuItem[];
  /**
   * @zh_CN Is it possible to drag and drop?
   */
  draggable?: boolean;
  /**
   * @zh_CN gap
   * @default 7
   * tabs-chrome only
   */
  gap?: number;
  /**
   * @zh_CN tab maximum width
   * tabs-chrome only
   */
  maxWidth?: number;
  /**
   * @zh_CN Close Tab when clicking the middle button
   */
  middleClickToClose?: boolean;

  /**
   * @zh_CN tab minimum width
   * tabs-chrome only
   */
  minWidth?: number;

  /**
   * @zh_CN Whether to display the icon
   */
  showIcon?: boolean;
  /**
   * @zh_CN tab page style
   */
  styleType?: TabsStyleType;

  /**
   * @zh_CN tab data
   */
  tabs?: TabDefinition[];

  /**
   * @zh_CN Whether to respond to wheel events
   */
  wheelable?: boolean;
}

export interface TabConfig extends TabDefinition {
  affixTab: boolean;
  closable: boolean;
  icon: string;
  key: string;
  title: string;
}
