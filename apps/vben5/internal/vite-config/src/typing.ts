import type { PluginVisualizerOptions } from 'rollup-plugin-visualizer';
import type {
  ConfigEnv,
  PluginOption,
  UserConfig,
  UserConfigFnPromise,
} from 'vite';
import type { PluginOptions } from 'vite-plugin-dts';
import type { Options as PwaPluginOptions } from 'vite-plugin-pwa';

/**
 *ImportMap configuration interface
 * @description is used to configure module import mapping and supports custom import paths and scopes.
 * @example
 * ```typescript
 * {
 *   imports: {
 *     'vue': 'https://unpkg.com/vue@3.2.47/dist/vue.esm-browser.js'
 *   },
 *   scopes: {
 *     'https://site.com/': {
 *       'vue': 'https://unpkg.com/vue@3.2.47/dist/vue.esm-browser.js'
 *     }
 *   }
 * }
 * ```
 */
interface IImportMap {
  /** Module import mapping */
  imports?: Record<string, string>;
  /** Scope-specific import mapping */
  scopes?: {
    [scope: string]: Record<string, string>;
  };
}

/**
 *Print plug-in configuration options
 * @description is used to configure console printing information
 */
interface PrintPluginOptions {
  /**
   * Printed data mapping
   * @description Data in the form of key-value pairs, which will be printed on the console
   * @example
   * ```typescript
   * {
   *   'App Version': '1.0.0',
   *   'Build Time': '2024-01-01'
   * }
   * ```
   */
  infoMap?: Record<string, string | undefined>;
}

/**
 * Nitro Mock plug-in configuration options
 * @description used to configure the behavior of the Nitro Mock server
 */
interface NitroMockPluginOptions {
  /**
   * Mock server package name
   * @default '@vbenjs/nitro-mock'
   */
  mockServerPackage?: string;

  /**
   * Mock service port
   * @default 3000
   */
  port?: number;

  /**
   * Whether to print Mock logs
   * @default false
   */
  verbose?: boolean;
}

/**
 * Archive plug-in configuration options
 * @description Compressed archive used to configure build products
 */
interface ArchiverPluginOptions {
  /**
   * Output file name
   * @default 'dist'
   */
  name?: string;
  /**
   * Output directory
   * @default '.'
   */
  outputDir?: string;
}

/**
 *ImportMap plug-in configuration
 * @description CDN import for configuring modules
 */
interface ImportmapPluginOptions {
  /**
   * CDN provider
   * @default 'jspm.io'
   * @description supports both esm.sh and jspm.io CDN providers
   */
  defaultProvider?: 'esm.sh' | 'jspm.io';
  /**
   *ImportMap configuration array
   * @description Configure the packages that need to be imported from CDN
   * @example
   * ```typescript
   * [
   *   { name: 'vue' },
   *   { name: 'pinia', range: '^2.0.0' }
   * ]
   * ```
   */
  importmap?: Array<{ name: string; range?: string }>;
  /**
   * Manually configure ImportMap
   * @description Custom ImportMap configuration
   */
  inputMap?: IImportMap;
}

/**
 *Conditional plug-in configuration
 * @description is used to dynamically load plug-ins based on conditions
 */
interface ConditionPlugin {
  /**
   * Judgment conditions
   * @description Load the plug-in when the condition is true
   */
  condition?: boolean;
  /**
   * Plug-in object
   * @description Returns the plugin array or Promise
   */
  plugins: () => PluginOption[] | PromiseLike<PluginOption[]>;
}

/**
 * Common plug-in configuration options
 * @description Basic configuration common to all plugins
 */
interface CommonPluginOptions {
  /**
   * Whether to enable development tools
   * @default false
   */
  devtools?: boolean;
  /**
   * Environment variables
   * @description Custom environment variables
   */
  env?: Record<string, any>;
  /**
   * Whether to inject metadata
   * @default true
   */
  injectMetadata?: boolean;
  /**
   * Whether it is build mode
   * @default false
   */
  isBuild?: boolean;
  /**
   * Build mode
   * @default 'development'
   */
  mode?: string;
  /**
   * Whether to enable dependency analysis
   * @default false
   * @description Use rollup-plugin-visualizer to analyze dependencies
   */
  visualizer?: boolean | PluginVisualizerOptions;
}

/**
 * Apply plug-in configuration options
 * @description used to configure plug-in options when building the application
 */
interface ApplicationPluginOptions extends CommonPluginOptions {
  /**
   * Whether to enable compressed archiving
   * @default false
   * @description When enabled, a zip file will be generated in the packaging directory.
   */
  archiver?: boolean;
  /**
   * Compression archive plug-in configuration
   * @description Configure the behavior of compressed archives
   */
  archiverPluginOptions?: ArchiverPluginOptions;
  /**
   * Whether to enable compression
   * @default false
   * @description supports gzip and brotli compression
   */
  compress?: boolean;
  /**
   * Compression type
   * @default ['gzip']
   * @description Optional compression type
   */
  compressTypes?: ('brotli' | 'gzip')[];
  /**
   * Whether to extract the configuration file
   * @default false
   * @description Extract configuration files when building
   */
  extraAppConfig?: boolean;
  /**
   * Whether to enable HTML plug-in
   * @default true
   */
  html?: boolean;
  /**
   * Whether to enable internationalization
   * @default false
   */
  i18n?: boolean;
  /**
   * Whether to enable ImportMap CDN
   * @default false
   */
  importmap?: boolean;
  /**
   *ImportMap plug-in configuration
   */
  importmapOptions?: ImportmapPluginOptions;
  /**
   * Whether to inject application loading animation
   * @default true
   */
  injectAppLoading?: boolean;
  /**
   * Whether to inject global SCSS
   * @default true
   */
  injectGlobalScss?: boolean;
  /**
   * Whether to inject copyright information
   * @default true
   */
  license?: boolean;
  /**
   * Whether to enable Nitro Mock
   * @default false
   */
  nitroMock?: boolean;
  /**
   * Nitro Mock plug-in configuration
   */
  nitroMockOptions?: NitroMockPluginOptions;
  /**
   * Whether to enable console printing
   * @default false
   */
  print?: boolean;
  /**
   *Print plug-in configuration
   */
  printInfoMap?: PrintPluginOptions['infoMap'];
  /**
   * Whether to enable PWA
   * @default false
   */
  pwa?: boolean;
  /**
   * PWA plug-in configuration
   */
  pwaOptions?: Partial<PwaPluginOptions>;
  /**
   * Whether to enable VXE Table lazy loading
   * @default false
   */
  vxeTableLazyImport?: boolean;
}

/**
 * Library plug-in configuration options
 * @description used to configure plug-in options when building the library
 */
interface LibraryPluginOptions extends CommonPluginOptions {
  /**
   * Whether to enable DTS output
   * @default true
   * @description Generate TypeScript type declaration file
   */
  dts?: boolean | PluginOptions;
}

/**
 * Application configuration option type
 */
type ApplicationOptions = ApplicationPluginOptions;

/**
 * Library configuration option type
 */
type LibraryOptions = LibraryPluginOptions;

/**
 * Application configuration definition function type
 * @description is used to define the application build configuration
 */
type DefineApplicationOptions = (config?: ConfigEnv) => Promise<{
  /** Application plug-in configuration */
  application?: ApplicationOptions;
  /** Vite configuration */
  vite?: UserConfig;
}>;

/**
 * Library configuration defines function type
 * @description is used to define the library build configuration
 */
type DefineLibraryOptions = (config?: ConfigEnv) => Promise<{
  /** Library plug-in configuration */
  library?: LibraryOptions;
  /** Vite configuration */
  vite?: UserConfig;
}>;

/**
 * Configuration definition type
 * @description Configuration definition for application or library
 */
type DefineConfig = DefineApplicationOptions | DefineLibraryOptions;

type VbenViteConfig = Promise<UserConfig> | UserConfig | UserConfigFnPromise;

export type {
  ApplicationPluginOptions,
  ArchiverPluginOptions,
  CommonPluginOptions,
  ConditionPlugin,
  DefineApplicationOptions,
  DefineConfig,
  DefineLibraryOptions,
  IImportMap,
  ImportmapPluginOptions,
  LibraryPluginOptions,
  NitroMockPluginOptions,
  PrintPluginOptions,
  VbenViteConfig,
};
