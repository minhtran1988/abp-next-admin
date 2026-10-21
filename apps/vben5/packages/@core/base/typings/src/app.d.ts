type LayoutType =
  | 'full-content'
  | 'header-mixed-nav'
  | 'header-nav'
  | 'header-sidebar-nav'
  | 'mixed-nav'
  | 'sidebar-mixed-nav'
  | 'sidebar-nav';

type ThemeModeType = 'auto' | 'dark' | 'light';

/**
 * Preference button location
 * fixed fixed on the right side
 * header top bar
 * auto automatic
 */
type PreferencesButtonPositionType = 'auto' | 'fixed' | 'header';

type BuiltinThemeType =
  | 'custom'
  | 'deep-blue'
  | 'deep-green'
  | 'default'
  | 'gray'
  | 'green'
  | 'neutral'
  | 'orange'
  | 'pink'
  | 'red'
  | 'rose'
  | 'sky-blue'
  | 'slate'
  | 'stone'
  | 'violet'
  | 'yellow'
  | 'zinc'
  | (Record<never, never> & string);

type ContentCompactType = 'compact' | 'wide';

type LayoutHeaderModeType = 'auto' | 'auto-scroll' | 'fixed' | 'static';
type LayoutHeaderMenuAlignType = 'center' | 'end' | 'start';

/**
 * Login expiration mode
 * modal pop-up mode
 * page page mode
 */
type LoginExpiredModeType = 'modal' | 'page';

/**
 * Breadcrumb style
 * background background
 * normal default
 */
type BreadcrumbStyleType = 'background' | 'normal';

/**
 * Permission mode
 * backend backend permission mode
 * frontend front-end permission mode
 * mixed mixed permission mode
 */
type AccessModeType = 'backend' | 'frontend' | 'mixed';

/**
 *Navigation style
 * plain plain
 * rounded
 */
type NavigationStyleType = 'plain' | 'rounded';

/**
 * Tab bar style
 * brisk brisk
 * card card
 * chrome google
 * plain plain
 */
type TabsStyleType = 'brisk' | 'card' | 'chrome' | 'plain';

/**
 * Page switching animation
 */
type PageTransitionType = 'fade' | 'fade-down' | 'fade-slide' | 'fade-up';

/**
 * Page switching animation
 * panel-center centered layout
 * panel-left layout on the left
 * panel-right right layout
 */
type AuthPageLayoutType = 'panel-center' | 'panel-left' | 'panel-right';

/**
 * Time zone options
 */
interface TimezoneOption {
  label: string;
  offset: number;
  timezone: string;
}

export type {
  AccessModeType,
  AuthPageLayoutType,
  BreadcrumbStyleType,
  BuiltinThemeType,
  ContentCompactType,
  LayoutHeaderMenuAlignType,
  LayoutHeaderModeType,
  LayoutType,
  LoginExpiredModeType,
  NavigationStyleType,
  PageTransitionType,
  PreferencesButtonPositionType,
  TabsStyleType,
  ThemeModeType,
  TimezoneOption,
};
