#Configuration

## Environment variable configuration

The project's environment variable configuration is located in `.env`, `.env.development`, and `.env.production` in the application directory.

The rules are consistent with [Vite Env Variables and Modes](https://vitejs.dev/guide/env-and-mode.html). The format is as follows:

```bash
.env # loaded in all environments
.env.local # Loaded in all environments, but ignored by git
.env.[mode] # Only loaded in the specified mode
.env.[mode].local # Only loaded in the specified mode, but will be ignored by git
```

::: tip

- Only variables starting with `VITE_` will be embedded in the client-side package, and you can access them in the project code like this:

  ```ts
  console.log(import.meta.env.VITE_PROT);
  ```

- Variables starting with `VITE_GLOB_*` will be added to the `_app.config.js` configuration file when packaging.

:::

## Environment configuration instructions

::: code-group

```bash [.env]
#App title
VITE_APP_TITLE=Vben Admin

# Application namespace, used as prefix for cache, store and other functions to ensure isolation
VITE_APP_NAMESPACE=vben-web-antd
```

```bash [.env.development]
# port number
VITE_PORT=5555

# Resource public path, needs to start and end with /
VITE_BASE=/

#Interface address
VITE_GLOB_API_URL=/api

# Whether to enable Nitro Mock service, true means open, false means closed
VITE_NITRO_MOCK=true

# Whether to open devtools, true means open, false means closed
VITE_DEVTOOLS=true

# Whether to inject global loading
VITE_INJECT_APP_LOADING=true

```

```bash [.env.production]
# Resource public path, needs to start and end with /
VITE_BASE=/

#Interface address
VITE_GLOB_API_URL=https://mock-napi.vben.pro/api

# Whether to enable compression, can be set to none, brotli, gzip
VITE_COMPRESS=gzip

# Whether to enable PWA
VITE_PWA=false

# vue-router mode
VITE_ROUTER_HISTORY=hash

# Whether to inject global loading
VITE_INJECT_APP_LOADING=true

# Whether to generate dist.zip after packaging
VITE_ARCHIVER=true

```

:::

## Dynamic configuration of production environment

After executing `pnpm build` to build the project in the Okura root directory, the `dist/_app.config.js` file will be automatically generated under the corresponding application and inserted into `index.html`.

`_app.config.js` is a dynamic configuration file that can dynamically modify the configuration according to different environments after the project is built. The content is as follows:

```ts
window._VBEN_ADMIN_PRO_APP_CONF_ = {
  VITE_GLOB_API_URL: 'https://mock-napi.vben.pro/api',
};
Object.freeze(window._VBEN_ADMIN_PRO_APP_CONF_);
Object.defineProperty(window, '_VBEN_ADMIN_PRO_APP_CONF_', {
  configurable: false,
  writable: false,
});
```

### Function

`_app.config.js` is used when the project needs to dynamically modify the configuration after packaging, such as the interface address. There is no need to repackage, you can modify the variables in /`dist/_app.config.js` after packaging, and refresh to update the local variables in the code. The `js` file is used here to ensure that the configuration file loading order remains at the front.

### Use

To obtain the variables in `_app.config.js`, you need to use the `useAppConfig` method provided by `@vben/hooks`.

```ts
const { apiURL } = useAppConfig(import.meta.env, import.meta.env.PROD);
```

### New

To add a dynamically modifyable configuration item, just follow the steps below:

- First, add variables that need to be dynamically configurable in `.env` or the corresponding development environment configuration file. Variables that start with `VITE_GLOB_*` are required, such as:

  ```bash
  VITE_GLOB_OTHER_API_URL=https://mock-napi.vben.pro/other-api
  ```

- In `packages/types/global.d.ts`, add the corresponding type definition, such as:

  ```ts
  export interface VbenAdminProAppConfigRaw {
    VITE_GLOB_API_URL: string;
    VITE_GLOB_OTHER_API_URL: string; // [!code ++]
  }

  export interface ApplicationConfig {
    apiURL: string;
    otherApiURL: string; // [!code ++]
  }
  ```

- In `packages/effects/hooks/src/use-app-config.ts`, add corresponding configuration items, such as:

  ```ts
  export function useAppConfig(
    env: Record<string, any>,
    isProduction: boolean,
  ): ApplicationConfig {
    // In a production environment, use the window._VBEN_ADMIN_PRO_APP_CONF_ global variable directly
    const config = isProduction
      ? window._VBEN_ADMIN_PRO_APP_CONF_
      : (env as VbenAdminProAppConfigRaw);

    const { VITE_GLOB_API_URL, VITE_GLOB_OTHER_API_URL } = config; // [!code ++]

    return {
      apiURL: VITE_GLOB_API_URL,
      otherApiURL: VITE_GLOB_OTHER_API_URL, // [!code ++]
    };
  }
  ```

At this point, you can use the `useAppConfig` method in the project to obtain the new configuration items.

```ts
const { otherApiURL } = useAppConfig(import.meta.env, import.meta.env.PROD);
```

::: warning note

The `useAppConfig` method can only be used within the application and should not be coupled to the package for use. `import.meta.env` and `import.meta.env.PROD` are passed in here to avoid this situation. A pure package should avoid using variables of specific build tools.

:::

## Preferences

The project provides a very rich set of preferences for dynamically configuring various features of the project:

![](/guide/preferences.png)

If you can't find the documentation, you can try configuring it yourself and click `Copy Preferences` to overwrite the project default. The configuration file is located in `preferences.ts` in the application directory. Here, you can override the framework's default configuration and implement custom configuration.

```ts
import { useAppConfig } from '@vben/hooks';
import { defineOverridesPreferences } from '@vben/preferences';

/**
 * @description project configuration file
 * Only part of the configuration in the project needs to be overwritten. Unnecessary configurations do not need to be overwritten. The default configuration will be automatically used.
 *!!! Please clear the cache after changing the configuration, otherwise it may not take effect
 */
export const overridesPreferences = defineOverridesPreferences({
  // overrides
});
```

### Framework default configuration

::: details View the framework default configuration

```ts
const defaultPreferences: Preferences = {
  app: {
    accessMode: 'frontend',
    authPageLayout: 'panel-right',
    checkUpdatesInterval: 1,
    colorGrayMode: false,
    colorWeakMode: false,
    compact: false,
    contentCompact: 'wide',
    contentCompactWidth: 1200,
    contentPadding: 0,
    contentPaddingBottom: 0,
    contentPaddingLeft: 0,
    contentPaddingRight: 0,
    contentPaddingTop: 0,
    defaultAvatar:
      'https://unpkg.com/@vbenjs/static-source@0.1.7/source/avatar-v1.webp',
    defaultHomePath: '/analytics',
    dynamicTitle: true,
    enableCheckUpdates: true,
    enablePreferences: true,
    enableCopyPreferences: true,
    enableRefreshToken: false,
    isMobile: false,
    layout: 'sidebar-nav',
    locale: 'zh-CN',
    loginExpiredMode: 'page',
    name: 'Vben Admin',
    preferencesButtonPosition: 'auto',
    watermark: false,
    zIndex: 200,
  },
  breadcrumb: {
    enable: true,
    hideOnlyOne: false,
    showHome: false,
    showIcon: true,
    styleType: 'normal',
  },
  copyright: {
    companyName: 'Vben',
    companySiteLink: 'https://www.vben.pro',
    date: '2024',
    enable: true,
    icp: '',
    icpLink: '',
    settingShow: true,
  },
  footer: {
    enable: false,
    fixed: false,
    height: 32,
  },
  header: {
    enable: true,
    height: 50,
    hidden: false,
    menuAlign: 'start',
    mode: 'fixed',
  },
  logo: {
    enable: true,
    fit: 'contain',
    source: 'https://unpkg.com/@vbenjs/static-source@0.1.7/source/logo-v1.webp',
    // sourceDark: 'https://unpkg.com/@vbenjs/static-source@0.1.7/source/logo-dark.webp', // Optional: dark theme logo
  },
  navigation: {
    accordion: true,
    split: true,
    styleType: 'rounded',
  },
  shortcutKeys: {
    enable: true,
    globalLockScreen: true,
    globalLogout: true,
    globalPreferences: true,
    globalSearch: true,
  },
  sidebar: {
    autoActivateChild: false,
    collapsed: false,
    collapsedButton: true,
    collapsedShowTitle: false,
    collapseWidth: 60,
    enable: true,
    expandOnHover: true,
    extraCollapse: false,
    extraCollapsedWidth: 60,
    fixedButton: true,
    hidden: false,
    mixedWidth: 80,
    width: 224,
  },
  tabbar: {
    draggable: true,
    enable: true,
    height: 38,
    keepAlive: true,
    maxCount: 0,
    middleClickToClose: false,
    persist: true,
    showIcon: true,
    showMaximize: true,
    showMore: true,
    styleType: 'chrome',
    wheelable: true,
  },
  theme: {
    builtinType: 'default',
    colorDestructive: 'hsl(348 100% 61%)',
    colorPrimary: 'hsl(212 100% 45%)',
    colorSuccess: 'hsl(144 57% 58%)',
    colorWarning: 'hsl(42 84% 61%)',
    mode: 'dark',
    radius: '0.5',
    semiDarkHeader: false,
    semiDarkSidebar: false,
  },
  transition: {
    enable: true,
    loading: true,
    name: 'fade-slide',
    progress: true,
  },
  widget: {
    fullscreen: true,
    globalSearch: true,
    languageToggle: true,
    lockScreen: true,
    notification: true,
    refresh: true,
    sidebarToggle: true,
    themeToggle: true,
  },
};
```

:::

::: details View the framework default configuration type

```ts
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
  /** Whether to display preferences */
  enablePreferences: boolean;
  /** Whether to display the copy preference button */
  enableCopyPreferences: boolean;
  /**
   * @zh_CN Whether to enable refreshToken
   */
  enableRefreshToken: boolean;
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
  /** Tab style */
  styleType: TabsStyleType;
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
  /** Current topic */
  mode: ThemeModeType;
  /** rounded corners */
  radius: string;
  /** Whether to enable semi-dark header (only effective when theme='light') */
  semiDarkHeader: boolean;
  /** Whether to enable semi-dark menu (only effective when theme='light') */
  semiDarkSidebar: boolean;
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
```

:::

::: warning note

- The `overridesPreferences` method only needs to override part of the configuration in the project. Unnecessary configurations do not need to be overwritten, and the default configuration will be used automatically.
- Any configuration item can be overridden. You only need to override it in the `overridesPreferences` method. Do not modify the default configuration file.
- Please clear the cache after changing the configuration, otherwise it may not take effect.

:::
