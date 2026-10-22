/**
 * @zh_CN login page url address
 */
export const LOGIN_PATH = '/auth/login';

export interface LanguageOption {
  label: string;
  value: 'en-US' | 'zh-CN';
}

/**
 * Supported languages
 */
export const SUPPORT_LANGUAGES: LanguageOption[] = [
  {
    label: '\u7b80\u4f53\u4e2d\u6587',
    value: 'zh-CN',
  },
  {
    label: 'English',
    value: 'en-US',
  },
];
