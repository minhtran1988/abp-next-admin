interface FallbackProps {
  /**
   * Description
   */
  description?: string;
  /**
   * @zh_CN Home page routing address
   *  @default /
   */
  homePath?: string;
  /**
   * @zh_CN Pictures displayed by default
   * @default pageNotFoundSvg
   */
  image?: string;
  /**
   * @zh_CN built-in type
   */
  status?: '403' | '404' | '500' | 'coming-soon' | 'offline';
  /**
   * @zh_CN page prompts
   */
  title?: string;
}
export type { FallbackProps };
