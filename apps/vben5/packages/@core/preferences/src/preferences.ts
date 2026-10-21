import type { DeepPartial } from '@vben-core/typings';

import type { InitialOptions, Preferences } from './types';

import { markRaw, reactive, readonly, watch } from 'vue';

import { StorageManager } from '@vben-core/shared/cache';
import { isMacOs, merge } from '@vben-core/shared/utils';

import {
  breakpointsTailwind,
  useBreakpoints,
  useDebounceFn,
} from '@vueuse/core';

import { defaultPreferences } from './config';
import { updateCSSVariables } from './update-css-variables';

const STORAGE_KEYS = {
  MAIN: 'preferences',
  LOCALE: 'preferences-locale',
  THEME: 'preferences-theme',
} as const;

class PreferenceManager {
  private cache: StorageManager;
  private debouncedSave: (preference: Preferences) => void;
  private initialPreferences: Preferences = defaultPreferences;
  private isInitialized = false;
  private state: Preferences;

  constructor() {
    this.cache = new StorageManager();
    this.state = reactive<Preferences>(
      this.loadFromCache() || { ...defaultPreferences },
    );
    this.debouncedSave = useDebounceFn(
      (preference) => this.saveToCache(preference),
      150,
    );
  }

  /**
   * Clear all cached preferences
   */
  clearCache = () => {
    Object.values(STORAGE_KEYS).forEach((key) => this.cache.removeItem(key));
  };

  /**
   * Get initialization preferences
   */
  getInitialPreferences = () => {
    return this.initialPreferences;
  };

  /**
   * Get current preferences (read-only)
   */
  getPreferences = () => {
    return readonly(this.state);
  };

  /**
   * Initialize preferences
   * @param options - initialization configuration items
   * @param options.namespace - namespace, used to isolate the configuration of different applications
   * @param options.overrides - Preferences to override
   */
  initPreferences = async ({ namespace, overrides }: InitialOptions) => {
    // Prevent repeated initialization
    if (this.isInitialized) {
      return;
    }

    // Initialize the storage manager using the namespace
    this.cache = new StorageManager({ prefix: namespace });

    // Merge initial preferences
    this.initialPreferences = merge({}, overrides, defaultPreferences);

    // Load cached preferences and merge with initial configuration
    const cachedPreferences = this.loadFromCache() || {};
    const mergedPreference = merge(
      {},
      cachedPreferences,
      this.initialPreferences,
    );

    // Update preferences
    this.updatePreferences(mergedPreference);

    // Set up the listener
    this.setupWatcher();

    // Initialize platform identification
    this.initPlatform();

    this.isInitialized = true;
  };

  /**
   * Reset preferences to initial state
   */
  resetPreferences = () => {
    // Reset state to initial preferences
    Object.assign(this.state, this.initialPreferences);

    // Save preferences to cache
    this.saveToCache(this.state);

    // Directly trigger UI updates
    this.handleUpdates(this.state);
  };

  /**
   * Update preferences
   * @param updates - Preferences to update
   */
  updatePreferences = (updates: DeepPartial<Preferences>) => {
    // Deeply merge update content and current status
    const mergedState = merge({}, updates, markRaw(this.state));
    Object.assign(this.state, mergedState);

    // Perform updates based on updated values
    this.handleUpdates(updates);

    // Save to cache
    this.debouncedSave(this.state);
  };

  /**
   * Handle updates
   * @param updates - updated preferences
   */
  private handleUpdates(updates: DeepPartial<Preferences>) {
    const { theme, app } = updates;

    if (
      theme &&
      (Object.keys(theme).length > 0 || Reflect.has(theme, 'fontSize'))
    ) {
      updateCSSVariables(this.state);
    }

    if (
      app &&
      (Reflect.has(app, 'colorGrayMode') || Reflect.has(app, 'colorWeakMode'))
    ) {
      this.updateColorMode(this.state);
    }
  }

  /**
   * Initialize platform identification
   */
  private initPlatform() {
    document.documentElement.dataset.platform = isMacOs() ? 'macOs' : 'window';
  }

  /**
   * Load preferences from cache
   * @returns cached preferences, or null if they do not exist
   */
  private loadFromCache(): null | Preferences {
    return this.cache.getItem<Preferences>(STORAGE_KEYS.MAIN);
  }

  /**
   * Save preferences to cache
   * @param preference - the preference to save
   */
  private saveToCache(preference: Preferences) {
    this.cache.setItem(STORAGE_KEYS.MAIN, preference);
    this.cache.setItem(STORAGE_KEYS.LOCALE, preference.app.locale);
    this.cache.setItem(STORAGE_KEYS.THEME, preference.theme.mode);
  }

  /**
   * Listen for changes in status and system preferences
   */
  private setupWatcher() {
    if (this.isInitialized) {
      return;
    }

    // Monitor breakpoints to determine whether the mobile terminal is
    const breakpoints = useBreakpoints(breakpointsTailwind);
    const isMobile = breakpoints.smaller('md');

    watch(
      () => isMobile.value,
      (val) => {
        this.updatePreferences({
          app: { isMobile: val },
        });
      },
      { immediate: true },
    );

    // Monitor system theme preference changes
    window
      .matchMedia('(prefers-color-scheme: dark)')
      .addEventListener('change', ({ matches: isDark }) => {
        // Follow the system theme only in automatic mode
        if (this.state.theme.mode === 'auto') {
          // Apply the actual theme first
          this.updatePreferences({
            theme: { mode: isDark ? 'dark' : 'light' },
          });
          // Restore to auto mode and keep following the system
          this.updatePreferences({
            theme: { mode: 'auto' },
          });
        }
      });
  }

  /**
   * Update page color mode (gray, color weak)
   * @param preference - preference setting
   */
  private updateColorMode(preference: Preferences) {
    const { colorGrayMode, colorWeakMode } = preference.app;
    const dom = document.documentElement;

    dom.classList.toggle('invert-mode', colorWeakMode);
    dom.classList.toggle('grayscale-mode', colorGrayMode);
  }
}

const preferencesManager = new PreferenceManager();

export { PreferenceManager, preferencesManager };
