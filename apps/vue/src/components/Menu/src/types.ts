// import { ComputedRef } from 'vue';
// import { ThemeEnum } from '/@/enums/appEnum';
// import { MenuModeEnum } from '/@/enums/menuEnum';
export interface MenuState {
  // Default selected list
  defaultSelectedKeys: string[];

  // mode
  // mode: MenuModeEnum;

  // // Topic
  // theme: ComputedRef<ThemeEnum> | ThemeEnum;

  // indent
  inlineIndent?: number;

  // Expand the array
  openKeys: string[];

  // Currently selected menu item key array
  selectedKeys: string[];

  // Array expanded in contracted state
  collapsedOpenKeys: string[];
}
