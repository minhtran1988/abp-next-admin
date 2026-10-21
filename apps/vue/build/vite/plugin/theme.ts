/**
 * Vite plugin for website theme color switching
 * https://github.com/anncwb/vite-plugin-theme
 */
import type { PluginOption } from 'vite';
import path from 'path';
import {
  viteThemePlugin,
  antdDarkThemePlugin,
  mixLighten,
  mixDarken,
  tinycolor,
} from 'vite-plugin-theme';
import { getThemeColors, generateColors } from '../../config/themeConfig';
import { generateModifyVars } from '../../generate/generateModifyVars';

export function configThemePlugin(isBuild: boolean): PluginOption[] {
  const colors = generateColors({
    mixDarken,
    mixLighten,
    tinycolor,
  });
  // Extract the viteThemePlugin plug-in, and enforce will be set according to different environments below.
  const vite_theme_plugin = viteThemePlugin({
    resolveSelector: (s) => {
      s = s.trim();
      switch (s) {
        case '.ant-steps-item-process .ant-steps-item-icon > .ant-steps-icon':
          return '.ant-steps-item-icon > .ant-steps-icon';
        case '.ant-radio-button-wrapper-checked:not(.ant-radio-button-wrapper-disabled)':
        case '.ant-radio-button-wrapper-checked:not(.ant-radio-button-wrapper-disabled):hover':
        case '.ant-radio-button-wrapper-checked:not(.ant-radio-button-wrapper-disabled):active':
          return s;
        case '.ant-steps-item-icon > .ant-steps-icon':
          return s;
        case '.ant-select-item-option-selected:not(.ant-select-item-option-disabled)':
          return s;
        default:
          if (s.indexOf('.ant-btn') >= -1) {
            // The button has been recustomized and needs to be filtered out to prevent overwriting.
            return s;
          }
      }
      return s.startsWith('[data-theme') ? s : `[data-theme] ${s}`;
    },
    colorVariables: [...getThemeColors(), ...colors],
  });
  vite_theme_plugin.forEach(function (item) {
    // Special configuration for vite:theme plug-in
    if ('vite:theme' === item.name) {
      // Remove enforce: "post" when packaging, adapt to vite 2.6.x, otherwise the generated app-theme-style will be empty because the code of async transform(code, id) { is not obtained correctly
      if (isBuild) {
        delete item.enforce;
      }
    }
  });
  const plugin = [
    vite_theme_plugin,
    antdDarkThemePlugin({
      preloadFiles: [
        path.resolve(process.cwd(), 'node_modules/ant-design-vue/dist/antd.less'),
        // path.resolve(process.cwd(), 'node_modules/ant-design-vue/dist/antd.dark.less'),
        path.resolve(process.cwd(), 'src/design/index.less'),
      ],
      filter: (id) => (isBuild ? !id.endsWith('antd.less') : true),
      // extractCss: false,
      darkModifyVars: {
        ...generateModifyVars(true),
        'text-color': '#c9d1d9',
        'primary-1': 'rgb(255 255 255 / 8%)',
        'text-color-base': '#c9d1d9',
        'component-background': '#151515',
        'heading-color': 'rgb(255 255 255 / 65%)',
        // black: '#0e1117',
        // #8b949e
        'text-color-secondary': '#8b949e',
        'border-color-base': '#303030',
        // 'border-color-split': '#30363d',
        'item-active-bg': '#111b26',
        'app-content-background': '#1e1e1e',
        'tree-node-selected-bg': '#11263c',

        'alert-success-border-color': '#274916',
        'alert-success-bg-color': '#162312',
        'alert-success-icon-color': '#49aa19',
        'alert-info-border-color': '#153450',
        'alert-info-bg-color': '#111b26',
        'alert-info-icon-color': '#177ddc',
        'alert-warning-border-color': '#594214',
        'alert-warning-bg-color': '#2b2111',
        'alert-warning-icon-color': '#d89614',
        'alert-error-border-color': '#58181c',
        'alert-error-bg-color': '#2a1215',
        'alert-error-icon-color': '#a61d24',
      },
    }),
  ];

  return plugin as unknown as PluginOption[];
}
