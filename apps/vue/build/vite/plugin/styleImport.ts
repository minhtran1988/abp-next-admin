/**
 *  Introduces component library styles on demand.
 * https://github.com/anncwb/vite-plugin-style-import
 */
import { createStyleImportPlugin, VxeTableResolve } from 'vite-plugin-style-import';

export function configStyleImportPlugin(_isBuild: boolean) {
  // if (!isBuild) {
  //   return [];
  // }
  const styleImportPlugin = createStyleImportPlugin({
    libs: [
      {
        libraryName: 'ant-design-vue',
        esModule: true,
        resolveStyle: (name) => {
          // Here is a list of "subcomponents" that do not need to introduce additional style files
          const ignoreList = [
            'anchor-link',
            'sub-menu',
            'menu-item',
            'menu-divider',
            'menu-item-group',
            'breadcrumb-item',
            'breadcrumb-separator',
            'form-item',
            'step',
            'select-option',
            'select-opt-group',
            'card-grid',
            'card-meta',
            'collapse-panel',
            'descriptions-item',
            'list-item',
            'list-item-meta',
            'table-column',
            'table-column-group',
            'tab-pane',
            'tab-content',
            'timeline-item',
            'tree-node',
            'skeleton-input',
            'skeleton-avatar',
            'skeleton-title',
            'skeleton-paragraph',
            'skeleton-image',
            'skeleton-button',
          ];
          // Here is a list of subcomponents that need to introduce additional styles
          // When introducing subcomponents separately, component styles need to be introduced, otherwise the subcomponent styles will be lost after packaging.
          const replaceList = {
            textarea: 'input',
            'typography-text': 'typography',
            'typography-title': 'typography',
            'typography-paragraph': 'typography',
            'typography-link': 'typography',
            'dropdown-button': 'dropdown',
            'input-password': 'input',
            'input-search': 'input',
            'input-group': 'input',
            'radio-group': 'radio',
            'checkbox-group': 'checkbox',
            'layout-sider': 'layout',
            'layout-content': 'layout',
            'layout-footer': 'layout',
            'layout-header': 'layout',
            'month-picker': 'date-picker',
            'range-picker': 'date-picker',
            'image-preview-group': 'image',
          };

          return ignoreList.includes(name)
            ? ''
            : replaceList.hasOwnProperty(name)
            ? `ant-design-vue/es/${replaceList[name]}/style/index`
            : `ant-design-vue/es/${name}/style/index`;
        },
      },
    ],
    resolves: [VxeTableResolve()],
  });
  return styleImportPlugin;
}
