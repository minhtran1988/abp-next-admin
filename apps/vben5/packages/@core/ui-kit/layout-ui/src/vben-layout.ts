import type {
  ContentCompactType,
  LayoutHeaderModeType,
  LayoutType,
  ThemeModeType,
} from '@vben-core/typings';

interface VbenLayoutProps {
  /**
   *Content area fixed width
   * @default 'wide'
   */
  contentCompact?: ContentCompactType;
  /**
   * Fixed width layout width
   * @default 1200
   */
  contentCompactWidth?: number;
  /**
   * padding
   * @default 16
   */
  contentPadding?: number;
  /**
   * paddingBottom
   * @default 16
   */
  contentPaddingBottom?: number;
  /**
   * paddingLeft
   * @default 16
   */
  contentPaddingLeft?: number;
  /**
   * paddingRight
   * @default 16
   */
  contentPaddingRight?: number;
  /**
   * paddingTop
   * @default 16
   */
  contentPaddingTop?: number;
  /**
   * Is footer visible?
   * @default false
   */
  footerEnable?: boolean;
  /**
   * Is footer fixed?
   * @default true
   */
  footerFixed?: boolean;
  /**
   * footer height
   * @default 32
   */
  footerHeight?: number;

  /**
   * header height
   * @default 48
   */
  headerHeight?: number;
  /**
   * Whether the top bar is hidden
   * @default false
   */
  headerHidden?: boolean;
  /**
   * header display mode
   * @default 'fixed'
   */
  headerMode?: LayoutHeaderModeType;
  /**
   * header top bar theme
   */
  headerTheme?: ThemeModeType;
  /**
   * Whether to display the header switching sidebar button
   * @default
   */
  headerToggleSidebarButton?: boolean;
  /**
   * Whether the header is displayed
   * @default true
   */
  headerVisible?: boolean;
  /**
   * Whether to display on mobile terminal
   * @default false
   */
  isMobile?: boolean;
  /**
   * Layout method
   * sidebar-nav side menu layout
   * header-nav top menu layout
   * mixed-nav side & top menu layout
   * sidebar-mixed-nav side mixed menu layout
   * full-content full-screen content layout
   * @default sidebar-nav
   */
  layout?: LayoutType;
  /**
   * Side menu collapsed state
   * @default false
   */
  sidebarCollapse?: boolean;
  /**
   * Side menu collapse button
   * @default true
   */
  sidebarCollapsedButton?: boolean;
  /**
   * Whether to display the title when the side menu is collapsed or not
   * @default true
   */
  sidebarCollapseShowTitle?: boolean;
  /**
   * Whether the sidebar is visible
   * @default true
   */
  sidebarEnable?: boolean;
  /**
   * Extra width for side menu collapse
   * @default 48
   */
  sidebarExtraCollapsedWidth?: number;
  /**
   * Whether the side menu fold button is fixed
   * @default true
   */
  sidebarFixedButton?: boolean;
  /**
   * Whether the sidebar is hidden
   * @default false
   */
  sidebarHidden?: boolean;
  /**
   * Mixed sidebar widths
   * @default 80
   */
  sidebarMixedWidth?: number;
  /**
   * Sidebar
   * @default dark
   */
  sidebarTheme?: ThemeModeType;
  /**
   * Sidebar sub-bar
   * @default dark
   */
  sidebarThemeSub?: ThemeModeType;
  /**
   * Sidebar width
   * @default 210
   */
  sidebarWidth?: number;
  /**
   * Side menu collapse width
   * @default 48
   */
  sideCollapseWidth?: number;
  /**
   * Is the tab visible?
   * @default true
   */
  tabbarEnable?: boolean;
  /**
   *tab height
   * @default 30
   */
  tabbarHeight?: number;
  /**
   * zIndex
   * @default 100
   */
  zIndex?: number;
}
export type { VbenLayoutProps };
