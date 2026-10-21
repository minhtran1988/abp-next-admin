import type { SupportedLanguagesType } from '@vben/locales';

import { loadLocalesMapFromDir } from '@vben/locales';

const modules = import.meta.glob('./langs/**/*.json');

const localesMap = loadLocalesMapFromDir(
  /\.\/langs\/([^/]+)\/(.*)\.json$/,
  modules,
);

/**
 * Load custom component localization resources
 * @param lang current language
 * @returns resource collection
 */
export async function loadComponentMessages(lang: SupportedLanguagesType) {
  const locales = localesMap[lang]?.();
  return locales;
}
