import type { Component, Ref } from 'vue';

import type { MaybePromise } from '@vben-core/typings';

import type { ModalApi } from './modal-api';

export interface ModalProps {
  /**
   * Animation type
   * @default 'slide'
   */
  animationType?: 'scale' | 'slide';
  /**
   * Whether to mount to the content area
   * @default false
   */
  appendToMain?: boolean;
  /**
   * Whether to display borders
   * @default false
   */
  bordered?: boolean;
  /**
   * Cancel button text
   */
  cancelText?: string;
  /**
   * Whether it is centered
   * @default false
   */
  centered?: boolean;

  class?: string;

  /**
   * Whether to display the close button in the upper right corner
   * @default true
   */
  closable?: boolean;
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
   * Disable confirm button
   */
  confirmDisabled?: boolean;
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
   * Destroy pop-ups when closing
   */
  destroyOnClose?: boolean;
  /**
   * Whether it can be dragged and dropped
   * @default false
   */
  draggable?: boolean;
  /**
   * Whether to display the bottom
   * @default true
   */
  footer?: boolean;
  footerClass?: string;
  /**
   * Whether full screen
   * @default false
   */
  fullscreen?: boolean;
  /**
   * Whether to display the full screen button
   * @default true
   */
  fullscreenButton?: boolean;
  /**
   * Whether to display the top bar
   * @default true
   */
  header?: boolean;
  headerClass?: string;
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
   * Submitting (lock pop-up window status)
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
   * Pop-up window level
   */
  zIndex?: number;
}

export interface ModalState extends ModalProps {
  /** Pop-up window open status */
  isOpen?: boolean;
  /**
   * Share data
   */
  sharedData?: Record<string, any>;
}

export type ExtendedModalApi = ModalApi & {
  useStore: <T = NoInfer<ModalState>>(
    selector?: (state: NoInfer<ModalState>) => T,
  ) => Readonly<Ref<T>>;
};

export interface ModalApiOptions extends ModalState {
  /**
   * Independent pop-up component
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
