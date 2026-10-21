import type { Arrayable } from '@vueuse/core';
import type { FlattenedItem } from 'reka-ui';

import type { Recordable } from '@vben-core/typings';

export interface TreeProps {
  /** Allows canceling existing options when selecting a single option */
  allowClear?: boolean;
  /** When selecting non-associatively, the superior node is automatically selected */
  autoCheckParent?: boolean;
  /** Show border */
  bordered?: boolean;
  /** Cancel parent-child association selection */
  checkStrictly?: boolean;
  /** Child field name */
  childrenField?: string;
  /** Default expanded keys */
  defaultExpandedKeys?: Array<number | string>;
  /** Default expansion level (higher priority than defaultExpandedKeys) */
  defaultExpandedLevel?: number;
  /** Default value */
  defaultValue?: Arrayable<number | string>;
  /** Disable */
  disabled?: boolean;
  /** Disable field name */
  disabledField?: string;
  /** Custom node class name */
  getNodeClass?: (item: FlattenedItem<Recordable<any>>) => string;
  iconField?: string;
  /** label field */
  labelField?: string;
  /** Whether to select multiple times */
  multiple?: boolean;
  /** Display the icon specified by iconField */
  showIcon?: boolean;
  /** Enable expansion and contraction animation */
  transition?: boolean;
  /** Tree data */
  treeData: Recordable<any>[];
  /** Value field */
  valueField?: string;
}

export function treePropsDefaults() {
  return {
    allowClear: false,
    autoCheckParent: true,
    bordered: false,
    checkStrictly: false,
    defaultExpandedKeys: () => [],
    defaultExpandedLevel: 0,
    disabled: false,
    disabledField: 'disabled',
    iconField: 'icon',
    labelField: 'label',
    multiple: false,
    showIcon: true,
    transition: true,
    valueField: 'value',
    childrenField: 'children',
  };
}
