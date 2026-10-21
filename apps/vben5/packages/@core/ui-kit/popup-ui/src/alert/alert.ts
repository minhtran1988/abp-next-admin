import type { Component, VNode, VNodeArrayChildren } from 'vue';

import type { Recordable } from '@vben-core/typings';

import { createContext } from '@vben-core/shadcn-ui';

export type IconType = 'error' | 'info' | 'question' | 'success' | 'warning';

export type BeforeCloseScope = {
  isConfirm: boolean;
};

export type AlertProps = {
  /** Callback before closing, if false is returned, the shutdown will be terminated */
  beforeClose?: (
    scope: BeforeCloseScope,
  ) => boolean | Promise<boolean | undefined> | undefined;
  /** border */
  bordered?: boolean;
  /**
   * Button alignment
   * @default 'end'
   */
  buttonAlign?: 'center' | 'end' | 'start';
  /** Title of the cancel button */
  cancelText?: string;
  /** Whether to display in the center */
  centered?: boolean;
  /** Title of confirmation button */
  confirmText?: string;
  /** Additional styles for popup containers */
  containerClass?: string;
  /** Pop-up window prompt content */
  content: Component | string;
  /** Additional styles for pop-up content */
  contentClass?: string;
  /** During the execution of the beforeClose callback, a loading mask is displayed in the content area*/
  contentMasking?: boolean;
  /** Content at the bottom of the pop-up window (in the same container as the button) */
  footer?: Component | string;
  /** The icon of the pop-up window (in front of the title) */
  icon?: Component | IconType;
  /**
   * Pop-up window mask blur effect
   */
  overlayBlur?: number;
  /** Whether to display the cancel button */
  showCancel?: boolean;
  /** Pop-up window title */
  title?: string;
};

/** Prompt attribute */
export type PromptProps<T = any> = {
  /** Callback before closing, if false is returned, the shutdown will be terminated */
  beforeClose?: (scope: {
    isConfirm: boolean;
    value: T | undefined;
  }) => boolean | Promise<boolean | undefined> | undefined;
  /** Component for accepting user input */
  component?: Component;
  /** Input component attributes */
  componentProps?: Recordable<any>;
  /** Input component slot */
  componentSlots?:
    | (() => any)
    | Recordable<unknown>
    | VNode
    | VNodeArrayChildren;
  /** Default value */
  defaultValue?: T;
  /** Enter the value attribute name of the component */
  modelPropName?: string;
} & Omit<AlertProps, 'beforeClose'>;

/**
 * Alert context
 */
export type AlertContext = {
  /** Perform cancellation operation */
  doCancel: () => void;
  /** Perform confirmation operation */
  doConfirm: () => void;
};

export const [injectAlertContext, provideAlertContext] =
  createContext<AlertContext>('VbenAlertContext');

/**
 * Get Alert context
 * @returns AlertContext
 */
export function useAlertContext() {
  const context = injectAlertContext();
  if (!context) {
    throw new Error('useAlertContext must be used within an AlertProvider');
  }
  return context;
}
