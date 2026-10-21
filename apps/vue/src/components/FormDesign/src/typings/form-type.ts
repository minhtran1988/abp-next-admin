import { Ref } from 'vue';
import { IAnyObject } from './base-type';
import { IFormConfig, IVFormComponent } from './v-form-component';

export interface IToolbarMethods {
  showModal: (jsonData: IAnyObject) => void;
}

type ChangeTabKey = 1 | 2;
export interface IPropsPanel {
  changeTab: (key: ChangeTabKey) => void;
}
export interface IState {
  // language
  locale: any;
  // Public components
  baseComponents: IVFormComponent[];
  // Custom component
  customComponents: IVFormComponent[];
  // layout component
  layoutComponents: IVFormComponent[];
  // Property panel instance
  propsPanel: Ref<null | IPropsPanel>;
  // json modal box instance
  jsonModal: Ref<null | IToolbarMethods>;
  // Import json data modal box
  importJsonModal: Ref<null | IToolbarMethods>;
  // Code preview modal box
  codeModal: Ref<null | IToolbarMethods>;
  // Preview modal box
  eFormPreview: Ref<null | IToolbarMethods>;

  eFormPreview2: Ref<null | IToolbarMethods>;
}

export interface IFormDesignMethods {
  // Set the currently selected control
  handleSetSelectItem(item: IVFormComponent): void;
  // Add controls to formConfig.formItems
  handleListPush(item: IVFormComponent): void;
  // Copy control
  handleCopy(item?: IVFormComponent, isCopy?: boolean): void;
  // Add control properties
  handleAddAttrs(schemas: IVFormComponent[], index: number): void;
  setFormConfig(config: IFormConfig): void;
  // Triggered before adding to form
  handleBeforeColAdd(
    event: { newIndex: string },
    schemas: IVFormComponent[],
    isCopy?: boolean,
  ): void;
}