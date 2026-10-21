---
outline: deep
---

# Vben Alert lightweight prompt box

Some pop-up windows for lightweight prompts provided by the framework can quickly and dynamically create prompts using only js code without writing any code in the template.

::: info application scenarios

Alert provides similar functions to Modal, but is only suitable for simple application scenarios. For example, temporarily and dynamically pop up modal confirmation boxes, input boxes, etc. If you have more complex requirements for pop-up windows, please use VbenModal

:::

::: tip note

The pop-up windows dynamically created by the shortcut methods alert, confirm, and prompt provided by Alert do not support HMR (hot update) when they are already open. After the code is changed, these pop-up windows need to be closed and reopened.

:::

::: tip README

In the sample code below, there are some problems with the theme color not being adapted and the style missing. These problems only appear in the document. In actual use, these problems will not occur. They can be ignored and do not need to worry about it.

:::

## Basic usage

Use `alert` to create a prompt with a single confirm button.

<DemoPreview dir="demos/vben-alert/alert" />

Use `confirm` to create a tooltip with confirm and cancel buttons.

<DemoPreview dir="demos/vben-alert/confirm" />

Use `prompt` to create a prompt box with confirm and cancel buttons that accepts user input.

<DemoPreview dir="demos/vben-alert/prompt" />

## useAlertContext

When the content, footer, and icon of the pop-up window use custom components, you can use `useAlertContext` in these components to obtain the context object of the current pop-up window to actively control the pop-up window.

::: tip note

`useAlertContext` can only be used in setup or functional components.

:::

### Methods

| Method | Description | Type | Version Requirements |
| --------- | ------------------ | -------- | -------- |
| doConfirm | Call the confirmation operation of the pop-up window | ()=>void | >5.5.4 |
| doCancel | Call the pop-up window's cancellation operation | ()=>void | >5.5.4 |

## Type description

```ts
/** Preset icon type */
export type IconType = 'error' | 'info' | 'question' | 'success' | 'warning';

export type BeforeCloseScope = {
  /** Whether it is a close triggered by clicking the confirm button */
  isConfirm: boolean;
};

/**
 * alert attribute
 */
export type AlertProps = {
  /** Callback before closing, if false is returned, the shutdown will be terminated */
  beforeClose?: (
    scope: BeforeCloseScope,
  ) => boolean | Promise<boolean | undefined> | undefined;
  /** border */
  bordered?: boolean;
  /** Button alignment */
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

/** prompt attribute */
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
  componentSlots?: Recordable<Component>;
  /** Default value */
  defaultValue?: T;
  /** Enter the value attribute name of the component */
  modelPropName?: string;
} & Omit<AlertProps, 'beforeClose'>;

/**
 * Function signature
 * The function signatures of alert and confirm are the same.
 * confirm will display a cancel button by default, while alert will only have one button by default.
 *  */
export function alert(options: AlertProps): Promise<void>;
export function alert(
  message: string,
  options?: Partial<AlertProps>,
): Promise<void>;
export function alert(
  message: string,
  title?: string,
  options?: Partial<AlertProps>,
): Promise<void>;

/**
 * Function signature of the pop-up input box.
 * The parameter of beforeClose will pass in the value currently entered by the user
 * component specifies the component that accepts user input, the default is Input
 * componentProps attribute data set for the input component
 * defaultValue default value
 * modelPropName Enter the value property name of the component. Default is modelValue
 */
export async function prompt<T = any>(
  options: Omit<AlertProps, 'beforeClose'> & {
    beforeClose?: (
      scope: BeforeCloseScope & {
        /** The current value of the input component */
        value: T;
      },
    ) => boolean | Promise<boolean | undefined> | undefined;
    component?: Component;
    componentProps?: Recordable<any>;
    defaultValue?: T;
    modelPropName?: string;
  },
): Promise<T | undefined>;
```
