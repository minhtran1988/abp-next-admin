import type { AsTag } from 'reka-ui';

import type { Component } from 'vue';

import type { ButtonVariants, ButtonVariantSize } from '../../ui';

export interface VbenButtonProps {
  /**
   * The element or component this component should render as. Can be overwrite by `asChild`
   * @defaultValue "div"
   */
  as?: AsTag | Component;
  /**
   * Change the default rendered element for the one passed as a child, merging their props and behavior.
   *
   * Read our [Composition](https://www.reka-ui.com/docs/guides/composition) guide for more details.
   */
  asChild?: boolean;
  class?: any;
  disabled?: boolean;
  loading?: boolean;
  size?: ButtonVariantSize;
  variant?: ButtonVariants;
}

export type CustomRenderType = (() => Component | string) | string;

export type ValueType = boolean | number | string;

export interface VbenButtonGroupProps extends Pick<
  VbenButtonProps,
  'disabled'
> {
  /** Allow clear selection in radio selection mode */
  allowClear?: boolean;
  /** Callback before value changes */
  beforeChange?: (
    value: ValueType,
    isChecked: boolean,
  ) => boolean | PromiseLike<boolean | undefined> | undefined;
  /** Button style */
  btnClass?: any;
  /** Button spacing distance */
  gap?: number;
  /** Limit the maximum number of selections in multi-selection mode. 0 means no limit */
  maxCount?: number;
  /** Whether to allow multiple selections */
  multiple?: boolean;
  /** Options */
  options?: { [key: string]: any; label: CustomRenderType; value: ValueType }[];
  /** Display icon */
  showIcon?: boolean;
  /** size */
  size?: 'large' | 'middle' | 'small';
}
