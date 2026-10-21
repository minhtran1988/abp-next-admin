import type { Component, Ref } from 'vue';

import type { ClassType, MaybePromise } from '@vben-core/typings';

import type { DrawerApi } from './drawer-api';

export type DrawerPlacement = 'bottom' | 'left' | 'right' | 'top';

export type CloseIconPlacement = 'left' | 'right';

export interface DrawerProps {
  /**
   * Whether to mount to the content area
   * @default false
   */
  appendToMain?: boolean;
  /**
   * Cancel button text
   */
  cancelText?: string;
  class?: ClassType;
  /**
   * Whether to display the close button
   * @default true
   */
  closable?: boolean;
  /**
   * Position of close button
   */
  closeIconPlacement?: CloseIconPlacement;
  /**
   * Click on the pop-up window mask to close the pop-up window
   * @default true
   */
  closeOnClickModal?: boolean;
  /**
   * Whether to close the pop-up window by pressing the ESC key
   * @default true
   */
  closeOnPressEscape?: boolean;
  /**
   * OK button loading
   * @default false
   */
  confirmLoading?: boolean;
  /**
   * OK button text
   */
  confirmText?: string;
  contentClass?: string;
  /**
   * Pop-up window description
   */
  description?: string;
  /**
   * Destroy the drawer on close
   */
  destroyOnClose?: boolean;
  /**
   * Whether to display the bottom
   * @default true
   */
  footer?: boolean;
  /**
   * Pop-up window bottom style
   */
  footerClass?: ClassType;
  /**
   * Whether to display the top bar
   * @default true
   */
  header?: boolean;
  /**
   * Pop-up window header style
   */
  headerClass?: ClassType;
  /**
   * Whether the pop-up window is displayed
   * @default false
   */
  loading?: boolean;
  /**
   * Whether to display the mask
   * @default true
   */
  modal?: boolean;

  /**
   * Whether to focus automatically
   */
  openAutoFocus?: boolean;
  /**
   * Pop-up window mask blur effect
   */
  overlayBlur?: number;
  /**
   * Drawer location
   * @default right
   */
  placement?: DrawerPlacement;

  /**
   * Whether to display the cancel button
   * @default true
   */
  showCancelButton?: boolean;
  /**
   * Whether to display the confirmation button
   * @default true
   */
  showConfirmButton?: boolean;
  /**
   * Submitting (locked drawer status)
   */
  submitting?: boolean;
  /**
   * Pop-up window title
   */
  title?: string;
  /**
   * Pop-up window title prompt
   */
  titleTooltip?: string;
  /**
   * Drawer level
   */
  zIndex?: number;
}

export interface DrawerState extends DrawerProps {
  /** Pop-up window open status */
  isOpen?: boolean;
  /**
   * Share data
   */
  sharedData?: Record<string, any>;
}

export type ExtendedDrawerApi = DrawerApi & {
  useStore: <T = NoInfer<DrawerState>>(
    selector?: (state: NoInfer<DrawerState>) => T,
  ) => Readonly<Ref<T>>;
};

export interface DrawerApiOptions extends DrawerState {
  /**
   *Separate drawer components
   */
  connectedComponent?: Component;
  /**
   * Callback before closing, return false to prevent closing
   * @returns
   */
  onBeforeClose?: () => MaybePromise<boolean | undefined>;
  /**
   *Callback when the cancel button is clicked
   */
  onCancel?: () => void;
  /**
   * The callback for the end of pop-up window closing animation
   * @returns
   */
  onClosed?: () => void;
  /**
   *Callback when click OK button
   */
  onConfirm?: () => void;
  /**
   * Pop-up window status change callback
   * @param isOpen
   * @returns
   */
  onOpenChange?: (isOpen: boolean) => void;
  /**
   * The pop-up window opens the animation end callback
   * @returns
   */
  onOpened?: () => void;
}
