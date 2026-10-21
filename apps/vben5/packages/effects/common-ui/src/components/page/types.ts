export interface PageProps {
  title?: string;
  description?: string;
  contentClass?: string;
  /**
   * Adaptable according to the visible height of the content
   */
  autoContentHeight?: boolean;
  headerClass?: string;
  footerClass?: string;
  /**
   * Custom height offset value (in pixels) to adjust content area sizing
   * when used with autoContentHeight
   * @default 0
   */
  heightOffset?: number;
}
