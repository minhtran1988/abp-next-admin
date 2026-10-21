export interface JsonViewerProps {
  /** Structural data to be displayed */
  value: any;
  /** Expand depth */
  expandDepth?: number;
  /** Whether it can be copied */
  copyable?: boolean;
  /** Whether to sort */
  sort?: boolean;
  /** Show border */
  boxed?: boolean;
  /** Topic */
  theme?: string;
  /** Whether to expand */
  expanded?: boolean;
  /** Time formatting function */
  timeformat?: (time: Date | number | string) => string;
  /** Preview mode */
  previewMode?: boolean;
  /** Display array index */
  showArrayIndex?: boolean;
  /** Display double quotes */
  showDoubleQuotes?: boolean;
}

export interface JsonViewerAction {
  action: string;
  text: string;
  trigger: HTMLElement;
}

export interface JsonViewerValue {
  value: any;
  path: string;
  depth: number;
  el: HTMLElement;
}

export interface JsonViewerToggle {
  /** Mouse events */
  event: MouseEvent;
  /** Current expansion status */
  open: boolean;
}
