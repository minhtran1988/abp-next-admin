import { computed } from 'vue';

import { diff } from '@vben-core/shared/utils';

import { preferencesManager } from './preferences';
import { isDarkTheme } from './update-css-variables';

function usePreferences() {
  const preferences = preferencesManager.getPreferences();
  const initialPreferences = preferencesManager.getInitialPreferences();
  /**
   * @zh_CN Changes to computing preferences
   */
  const diffPreference = computed(() => {
    return diff(initialPreferences, preferences);
  });

  const appPreferences = computed(() => preferences.app);

  const shortcutKeysPreferences = computed(() => preferences.shortcutKeys);

  /**
   * @zh_CN Determine whether it is dark mode
   * @param preferences - the current preference object, its theme value will be used to determine whether it is dark mode.
   * @returns If the theme is dark mode, returns true, otherwise returns false.
   */
  const isDark = computed(() => {
    return isDarkTheme(preferences.theme.mode);
  });

  const locale = computed(() => {
    return preferences.app.locale;
  });

  const isMobile = computed(() => {
    return appPreferences.value.isMobile;
  });

  const theme = computed(() => {
    return isDark.value ? 'dark' : 'light';
  });

  /**
   * @zh_CN layout method
   */
  const layout = computed(() =>
    isMobile.value ? 'sidebar-nav' : appPreferences.value.layout,
  );

  /**
   * @zh_CN Whether to display the top bar
   */
  const isShowHeaderNav = computed(() => {
    return preferences.header.enable;
  });

  /**
   * @zh_CN Whether to display content in full screen without side, bottom, top, and tab areas
   */
  const isFullContent = computed(
    () => appPreferences.value.layout === 'full-content',
  );

  /**
   * @zh_CN Whether side navigation mode
   */
  const isSideNav = computed(
    () => appPreferences.value.layout === 'sidebar-nav',
  );

  /**
   * @zh_CN Whether side blending mode
   */
  const isSideMixedNav = computed(
    () => appPreferences.value.layout === 'sidebar-mixed-nav',
  );

  /**
   * @zh_CN Whether it is head navigation mode
   */
  const isHeaderNav = computed(
    () => appPreferences.value.layout === 'header-nav',
  );

  /**
   * @zh_CN Whether it is head hybrid navigation mode
   */
  const isHeaderMixedNav = computed(
    () => appPreferences.value.layout === 'header-mixed-nav',
  );

  /**
   * @zh_CN Whether it is top bar + side navigation mode
   */
  const isHeaderSidebarNav = computed(
    () => appPreferences.value.layout === 'header-sidebar-nav',
  );

  /**
   * @zh_CN Whether it is mixed navigation mode
   */
  const isMixedNav = computed(
    () => appPreferences.value.layout === 'mixed-nav',
  );

  /**
   * @zh_CN Whether to include side navigation mode
   */
  const isSideMode = computed(() => {
    return (
      isMixedNav.value ||
      isSideMixedNav.value ||
      isSideNav.value ||
      isHeaderMixedNav.value ||
      isHeaderSidebarNav.value
    );
  });

  const sidebarCollapsed = computed(() => {
    return preferences.sidebar.collapsed;
  });

  /**
   * @zh_CN Whether to enable keep-alive
   * Only enabled when tabs are visible and keep-alive is enabled
   */
  const keepAlive = computed(
    () => preferences.tabbar.enable && preferences.tabbar.keepAlive,
  );

  /**
   * @zh_CN Whether the login registration page layout is on the left side
   */
  const authPanelLeft = computed(() => {
    return appPreferences.value.authPageLayout === 'panel-left';
  });

  /**
   * @zh_CN Whether the login registration page layout is on the right side
   */
  const authPanelRight = computed(() => {
    return appPreferences.value.authPageLayout === 'panel-right';
  });

  /**
   * @zh_CN Whether the layout of the login registration page is in the middle
   */
  const authPanelCenter = computed(() => {
    return appPreferences.value.authPageLayout === 'panel-center';
  });

  /**
   * @zh_CN Whether the content has been maximized
   * Exclude full-content mode
   */
  const contentIsMaximize = computed(() => {
    const headerIsHidden = preferences.header.hidden;
    const sidebarIsHidden = preferences.sidebar.hidden;
    return headerIsHidden && sidebarIsHidden && !isFullContent.value;
  });

  /**
   * @zh_CN Whether to enable global search shortcut keys
   */
  const globalSearchShortcutKey = computed(() => {
    const { enable, globalSearch } = shortcutKeysPreferences.value;
    return enable && globalSearch;
  });

  /**
   * @zh_CN Whether to enable the global logout shortcut key
   */
  const globalLogoutShortcutKey = computed(() => {
    const { enable, globalLogout } = shortcutKeysPreferences.value;
    return enable && globalLogout;
  });

  const globalLockScreenShortcutKey = computed(() => {
    const { enable, globalLockScreen } = shortcutKeysPreferences.value;
    return enable && globalLockScreen;
  });

  /**
   * @zh_CN Preference button location
   */
  const preferencesButtonPosition = computed(() => {
    const { enablePreferences, preferencesButtonPosition } = preferences.app;

    // If the preference button is not enabled
    if (!enablePreferences) {
      return {
        fixed: false,
        header: false,
      };
    }

    const { header, sidebar } = preferences;
    const headerHidden = header.hidden;
    const sidebarHidden = sidebar.hidden;

    const contentIsMaximize = headerHidden && sidebarHidden;

    const isHeaderPosition = preferencesButtonPosition === 'header';

    // If a fixed position is set
    if (preferencesButtonPosition !== 'auto') {
      return {
        fixed: preferencesButtonPosition === 'fixed',
        header: isHeaderPosition,
      };
    }

    // If it is in full screen mode or not fixed to the top,
    const fixed =
      contentIsMaximize ||
      isFullContent.value ||
      isMobile.value ||
      !isShowHeaderNav.value;

    return {
      fixed,
      header: !fixed,
    };
  });

  return {
    authPanelCenter,
    authPanelLeft,
    authPanelRight,
    contentIsMaximize,
    diffPreference,
    globalLockScreenShortcutKey,
    globalLogoutShortcutKey,
    globalSearchShortcutKey,
    isDark,
    isFullContent,
    isHeaderMixedNav,
    isHeaderNav,
    isHeaderSidebarNav,
    isMixedNav,
    isMobile,
    isSideMixedNav,
    isSideMode,
    isSideNav,
    keepAlive,
    layout,
    locale,
    preferencesButtonPosition,
    sidebarCollapsed,
    theme,
  };
}

export { usePreferences };
