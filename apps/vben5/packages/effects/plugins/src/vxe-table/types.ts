import type {
  VxeGridListeners,
  VxeGridPropTypes,
  VxeGridProps as VxeTableGridProps,
  VxeUIExport,
} from 'vxe-table';

import type { Ref } from 'vue';

import type { ClassType, DeepPartial } from '@vben/types';

import type { BaseFormComponentType, VbenFormProps } from '@vben-core/form-ui';

import type { VxeGridApi } from './api';

import { useVbenForm } from '@vben-core/form-ui';

export interface VxePaginationInfo {
  currentPage: number;
  pageSize: number;
  total: number;
}

interface ToolbarConfigOptions extends VxeGridPropTypes.ToolbarConfig {
  /** Whether to display the button to switch the search form */
  search?: boolean;
}

export interface VxeTableGridOptions<T = any> extends VxeTableGridProps<T> {
  /** Toolbar configuration */
  toolbarConfig?: ToolbarConfigOptions;
}

export interface SeparatorOptions {
  show?: boolean;
  backgroundColor?: string;
}

export interface VxeGridProps<
  T extends Record<string, any> = any,
  D extends BaseFormComponentType = BaseFormComponentType,
> {
  /**
   * Title
   */
  tableTitle?: string;
  /**
   *Title help
   */
  tableTitleHelp?: string;
  /**
   * component class
   */
  class?: ClassType;
  /**
   * vxe-grid class
   */
  gridClass?: ClassType;
  /**
   * vxe-grid configuration
   */
  gridOptions?: DeepPartial<VxeTableGridOptions<T>>;
  /**
   * vxe-grid event
   */
  gridEvents?: DeepPartial<VxeGridListeners<T>>;
  /**
   * Form configuration
   */
  formOptions?: VbenFormProps<D>;
  /**
   * Show search form
   */
  showSearchForm?: boolean;
  /**
   * Separator bar between search form and table body
   */
  separator?: boolean | SeparatorOptions;
}

export type ExtendedVxeGridApi<
  D extends Record<string, any> = any,
  F extends BaseFormComponentType = BaseFormComponentType,
> = VxeGridApi<D> & {
  useStore: <T = NoInfer<VxeGridProps<D, F>>>(
    selector?: (state: NoInfer<VxeGridProps<any, any>>) => T,
  ) => Readonly<Ref<T>>;
};

export interface SetupVxeTable {
  configVxeTable: (ui: VxeUIExport) => void;
  useVbenForm: typeof useVbenForm;
}
