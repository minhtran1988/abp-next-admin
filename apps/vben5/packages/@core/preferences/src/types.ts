import type {
  AccessModeType,
  AuthPageLayoutType,
  BreadcrumbStyleType,
  BuiltinThemeType,
  ContentCompactType,
  DeepPartial,
  LayoutHeaderMenuAlignType,
  LayoutHeaderModeType,
  LayoutType,
  LoginExpiredModeType,
  NavigationStyleType,
  PageTransitionType,
  PreferencesButtonPositionType,
  TabsStyleType,
  ThemeModeType,
} from '@vben-core/typings';

type SupportedLanguagesType = 'en-US' | 'zh-CN';

interface AppPreferences {
  /** Permission mode */
  accessMode: AccessModeType;
  /** Login registration page layout */
  authPageLayout: AuthPageLayoutType;
  /** Check update polling time */
  checkUpdatesInterval: number;
  /** Whether to enable gray mode */
  colorGrayMode: boolean;
  /** Whether to enable color weakness mode */
  colorWeakMode: boolean;
  /** Whether to enable compact mode */
  compact: boolean;
  /** Whether to enable content compact mode */
  contentCompact: ContentCompactType;
  /** Content compact width */
  contentCompactWidth: number;
  /** Content margin */
  contentPadding: number;
  /** Content bottom padding */
  contentPaddingBottom: number;
  /** Content left padding */
  contentPaddingLeft: number;
  /** Content right padding */
  contentPaddingRight: number;
  /** Content top padding */
  contentPaddingTop: number;
  // /** Apply default avatar */
  defaultAvatar: string;
  /**Default homepage address */
  defaultHomePath: string;
  // /** Enable dynamic title */
  dynamicTitle: boolean;
  /** Whether to enable checking for updates */
  enableCheckUpdates: boolean;
  /** Whether to display the copy preference button */
  enableCopyPreferences: boolean;
  /** Whether to display preferences */
  enablePreferences: boolean;
  /**
   * @zh_CN Whether to enable refreshToken
   */
  enableRefreshToken: boolean;
  /**
   * @zh_CN Whether to enable the ceiling effect of the preference navigation bar
   */
  enableStickyPreferencesNavigationBar: boolean;
  /** Whether it is mobile */
  isMobile: boolean;
  /** Layout method */
  layout: LayoutType;
  /** Supported languages */
  locale: SupportedLanguagesType;
  /** Login expiration mode */
  loginExpiredMode: LoginExpiredModeType;
  /** Application name */
  name: string;
  /** Preference button location */
  preferencesButtonPosition: PreferencesButtonPositionType;
  /**
   * @zh_CN Whether to enable watermark
   */
  watermark: boolean;
  /**
   * @zh_CN watermark copywriting
   */
  watermarkContent: string;
  /** z-index */
  zIndex: number;
}

interface BreadcrumbPreferences {
  /** Whether breadcrumbs are enabled */
  enable: boolean;
  /** Whether to hide when there is only one breadcrumb */
  hideOnlyOne: boolean;
  /** Whether the breadcrumb homepage icon is visible */
  showHome: boolean;
  /** Whether the breadcrumb icon is visible */
  showIcon: boolean;
  /** Breadcrumb style */
  styleType: BreadcrumbStyleType;
}

interface CopyrightPreferences {
  /** Copyright company name */
  companyName: string;
  /** Copyright company name link */
  companySiteLink: string;
  /** Copyright date */
  date: string;
  /** Whether the copyright is visible */
  enable: boolean;
  /** Registration number */
  icp: string;
  /** Registration number link */
  icpLink: string;
  /** Set whether the panel is displayed*/
  settingShow?: boolean;
}

interface FooterPreferences {
  /** Whether the bottom bar is visible */
  enable: boolean;
  /** Whether the bottom bar is fixed */
  fixed: boolean;
  /** Bottom bar height */
  height: number;
}

interface HeaderPreferences {
  /** Whether the top bar is enabled */
  enable: boolean;
  /** Top bar height */
  height: number;
  /** Whether the top bar is hidden, css-hide */
  hidden: boolean;
  /** Top bar menu position */
  menuAlign: LayoutHeaderMenuAlignType;
  /** header display mode */
  mode: LayoutHeaderModeType;
}

interface LogoPreferences {
  /** Whether the logo is visible */
  enable: boolean;
  /** Logo image adaptation method */
  fit: 'contain' | 'cover' | 'fill' | 'none' | 'scale-down';
  /** logo address */
  source: string;
  /** Dark theme logo address (optional, use source if not set) */
  sourceDark?: string;
}

interface NavigationPreferences {
  /** Navigation menu accordion mode */
  accordion: boolean;
  /** Whether the navigation menu is cut or not, only takes effect when layout=mixed-nav */
  split: boolean;
  /** Navigation menu style */
  styleType: NavigationStyleType;
}

interface SidebarPreferences {
  /** Automatically activate submenu when clicking on directory */
  autoActivateChild: boolean;
  /** Whether the sidebar is collapsed */
  collapsed: boolean;
  /** Whether the sidebar collapse button is visible */
  collapsedButton: boolean;
  /** Whether to display the title when the sidebar is collapsed */
  collapsedShowTitle: boolean;
  /** Sidebar folding width */
  collapseWidth: number;
  /** Sidebar menu dragging */
  draggable: boolean;
  /** Whether the sidebar is visible */
  enable: boolean;
  /** Menu automatic expansion state */
  expandOnHover: boolean;
  /** Whether the sidebar expansion area is collapsed */
  extraCollapse: boolean;
  /** Sidebar expansion area collapsed width */
  extraCollapsedWidth: number;
  /** Whether the sidebar fixed button is visible */
  fixedButton: boolean;
  /** Whether the sidebar is hidden - css */
  hidden: boolean;
  /** Mixed sidebar widths */
  mixedWidth: number;
  /** Sidebar width */
  width: number;
}

interface ShortcutKeyPreferences {
  /** Whether to enable shortcut keys - global */
  enable: boolean;
  /** Whether to enable global lock screen shortcut keys */
  globalLockScreen: boolean;
  /** Whether to enable the global logout shortcut key */
  globalLogout: boolean;
  /** Whether to enable global preference shortcut keys */
  globalPreferences: boolean;
  /** Whether to enable global search shortcut keys */
  globalSearch: boolean;
}

interface TabbarPreferences {
  /** Whether to enable multi-tab dragging */
  draggable: boolean;
  /** Whether to enable multiple tabs */
  enable: boolean;
  /** Tab height */
  height: number;
  /** Enable tab caching function */
  keepAlive: boolean;
  /** Limit the maximum number */
  maxCount: number;
  /** Whether to close the label when clicking the middle button */
  middleClickToClose: boolean;
  /** Whether to persist the label */
  persist: boolean;
  /** Whether to enable multi-tab icons */
  showIcon: boolean;
  /** Show maximize button */
  showMaximize: boolean;
  /** Show more button */
  showMore: boolean;
  /** Show refresh button */
  showRefresh: boolean;
  /** Tab style */
  styleType: TabsStyleType;
  /** Whether to enable access history */
  visitHistory: boolean;
  /** Whether to enable mouse wheel response */
  wheelable: boolean;
}

interface ThemePreferences {
  /** Built-in theme name */
  builtinType: BuiltinThemeType;
  /** Wrong color */
  colorDestructive: string;
  /** theme color */
  colorPrimary: string;
  /** Success color */
  colorSuccess: string;
  /** Warning color */
  colorWarning: string;
  /** Font size (unit: px) */
  fontSize: number;
  /** Current topic */
  mode: ThemeModeType;
  /** rounded corners */
  radius: string;
  /** Whether to enable semi-dark header (only effective when theme='light') */
  semiDarkHeader: boolean;
  /** Whether to enable semi-dark menu (only effective when theme='light') */
  semiDarkSidebar: boolean;
  /** Whether to enable semi-dark submenu (only valid when theme='light') */
  semiDarkSidebarSub: boolean;
}

interface TransitionPreferences {
  /** Whether page switching animation is enabled */
  enable: boolean;
  // /** Whether to enable page loading */
  loading: boolean;
  /** Page switching animation */
  name: PageTransitionType | string;
  /** Whether to enable page loading progress animation */
  progress: boolean;
}

interface WidgetPreferences {
  /** Whether to enable full screen widgets */
  fullscreen: boolean;
  /** Whether to enable the global search component */
  globalSearch: boolean;
  /** Whether to enable the language switching component */
  languageToggle: boolean;
  /** Whether to enable the lock screen function */
  lockScreen: boolean;
  /** Whether to display notification widgets */
  notification: boolean;
  /** Show refresh button */
  refresh: boolean;
  /** Whether to display the sidebar show/hide widgets */
  sidebarToggle: boolean;
  /** Whether to display the theme switching widget */
  themeToggle: boolean;
  /** Whether to display the time zone component */
  timezone: boolean;
}

interface Preferences {
  /** Global configuration */
  app: AppPreferences;
  /** Top bar configuration */
  breadcrumb: BreadcrumbPreferences;
  /** Copyright configuration */
  copyright: CopyrightPreferences;
  /** Bottom bar configuration */
  footer: FooterPreferences;
  /** Breadcrumb configuration */
  header: HeaderPreferences;
  /** logo configuration */
  logo: LogoPreferences;
  /** Navigation configuration */
  navigation: NavigationPreferences;
  /** Shortcut key configuration */
  shortcutKeys: ShortcutKeyPreferences;
  /** Sidebar configuration */
  sidebar: SidebarPreferences;
  /**Tab page configuration */
  tabbar: TabbarPreferences;
  /** Theme configuration */
  theme: ThemePreferences;
  /** Animation configuration */
  transition: TransitionPreferences;
  /** Function configuration */
  widget: WidgetPreferences;
}

type PreferencesKeys = keyof Preferences;

interface InitialOptions {
  namespace: string;
  overrides?: DeepPartial<Preferences>;
}
export type {
  AppPreferences,
  BreadcrumbPreferences,
  FooterPreferences,
  HeaderPreferences,
  InitialOptions,
  LogoPreferences,
  NavigationPreferences,
  Preferences,
  PreferencesKeys,
  ShortcutKeyPreferences,
  SidebarPreferences,
  SupportedLanguagesType,
  TabbarPreferences,
  ThemePreferences,
  TransitionPreferences,
  WidgetPreferences,
};
