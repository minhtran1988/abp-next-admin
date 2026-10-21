import type { PluginOption } from 'vite';

import fs from 'node:fs';
import fsp from 'node:fs/promises';
import { join } from 'node:path';
import { fileURLToPath } from 'node:url';

import { readPackageJSON } from '@vben/node-utils';

/**
 * Used to generate and inject loading styles into the project
 * Provide loading styles for multiple apps, no need to introduce them separately in each app -> index.html
 */
async function viteInjectAppLoadingPlugin(
  isBuild: boolean,
  env: Record<string, any> = {},
  loadingTemplate = 'loading.html',
): Promise<PluginOption | undefined> {
  const loadingHtml = await getLoadingRawByHtmlTemplate(loadingTemplate);
  const { version } = await readPackageJSON(process.cwd());
  const envRaw = isBuild ? 'prod' : 'dev';
  const cacheName = `'${env.VITE_APP_NAMESPACE}-${version}-${envRaw}-preferences-theme'`;

  // Get the cached theme
  // Ensure that under the dark theme, when refreshing the page, the loading is also a dark theme
  const injectScript = `
  <script data-app-loading="inject-js">
  var theme = localStorage.getItem(${cacheName});
  document.documentElement.classList.toggle('dark', /dark/.test(theme));
</script>
`;

  if (!loadingHtml) {
    return;
  }

  return {
    enforce: 'pre',
    name: 'vite:inject-app-loading',
    transformIndexHtml: {
      handler(html) {
        const re = /<body\s*>/;
        html = html.replace(re, `<body>${injectScript}${loadingHtml}`);
        return html;
      },
      order: 'pre',
    },
  };
}

/**
 * HTML template used to obtain loading
 */
async function getLoadingRawByHtmlTemplate(loadingTemplate: string) {
  // Supports customizing the loading template in the app, please refer to default-loading.html for the template
  let appLoadingPath = join(process.cwd(), loadingTemplate);

  if (!fs.existsSync(appLoadingPath)) {
    const __dirname = fileURLToPath(new URL('.', import.meta.url));
    appLoadingPath = join(__dirname, './default-loading.html');
  }

  return await fsp.readFile(appLoadingPath, 'utf8');
}

export { viteInjectAppLoadingPlugin };
