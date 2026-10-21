import { Ref, SetupContext } from 'vue';
import { IVFormComponent, IFormConfig, AForm } from '../typings/v-form-component';
import { findFormItem, formItemsForEach } from '../utils';
import { cloneDeep, isFunction } from 'lodash-es';
import { IAnyObject } from '../typings/base-type';

interface IFormInstanceMethods extends AForm {
  submit: () => Promise<any>;
}

export interface IProps {
  formConfig: IFormConfig;
  formModel: IAnyObject;
}

type ISet = <T extends keyof IVFormComponent>(
  field: string,
  key: T,
  value: IVFormComponent[T],
) => void;
// Get the form item bound to the current field
type IGet = (field: string) => IVFormComponent | undefined;
// Get the value of field in formData
type IGetValue = (field: string) => any;
// Set the field value in formData and trigger verification
type ISetValue = (field: string | IAnyObject, value?: any) => void;
// Hide the form item corresponding to the field
type IHidden = (field: string) => void;
// Display the form items corresponding to the field
type IShow = (field: string) => void;
// Set the props attribute bound to the form item corresponding to the field
type ISetProps = (field: string, key: string, value: any) => void;
// Get the value in formData
type IGetData = () => Promise<IAnyObject>;
// Disable the form. If the field is empty, the entire form is disabled.
type IDisable = (field?: string | boolean) => void;
// Set form configuration method
type ISetFormConfig = (key: string, value: any) => void;
interface ILinkOn {
  [key: string]: Set<IVFormComponent>;
}

export interface IVFormMethods extends Partial<IFormInstanceMethods> {
  set: ISet;
  get: IGet;
  getValue: IGetValue;
  setValue: ISetValue;
  hidden: IHidden;
  show: IShow;
  setProps: ISetProps;
  linkOn: ILinkOn;
  getData: IGetData;
  disable: IDisable;
}
export function useVFormMethods(
  props: IProps,
  _context: Partial<SetupContext>,
  formInstance: Ref<AForm | null>,
  formInstanceMethods: Partial<IFormInstanceMethods>,
): IVFormMethods {
  /**
   * Get form items based on field
   * @param {string} field
   * @return {IVFormComponent | undefined}
   */
  const get: IGet = (field) =>
    findFormItem(props.formConfig.schemas, (item) => item.field === field);

  /**
   * Set form item field value based on form field
   * @param {string} field
   * @param {keyof IVFormComponent} key
   * @param {never} value
   */
  const set: ISet = (field, key, value) => {
    const formItem = get(field);
    if (formItem) formItem[key] = value;
  };

  /**
   * Set the props of the form item
   * @param {string} field The form item field that needs to be set
   * @param {string} key The key that needs to be set
   * @param value The value that needs to be set
   */
  const setProps: ISetProps = (field, key, value) => {
    const formItem = get(field);
    if (formItem?.componentProps) {
      ['options', 'treeData'].includes(key) && setValue(field, undefined);

      formItem.componentProps[key] = value;
    }
  };
  /**
   * Set the value of the field and trigger verification after setting
   * @param {string} field The field that needs to be set
   * @param value The value that needs to be set
   */
  const setValue: ISetValue = (field, value) => {
    if (typeof field === 'string') {
      // props.formData[field] = value
      props.formModel[field] = value;
      formInstance.value?.validateField(field, value, []);
    } else {
      const keys = Object.keys(field);
      keys.forEach((key) => {
        props.formModel[key] = field[key];
        formInstance.value?.validateField(key, field[key], []);
      });
    }
  };
  /**
   * Set form configuration method
   * @param {string} key
   * @param value
   */
  const setFormConfig: ISetFormConfig = (key, value) => {
    props.formConfig[key] = value;
  };
  /**
   * Get the field value based on the form item field, if the field is empty, then
   * @param {string} field The field that needs to be set
   */
  const getValue: IGetValue = (field) => {
    const formData = cloneDeep(props.formModel);
    return formData[field];
  };

  /**
   * Get the value in formData
   * @return {Promise<IAnyObject<any>>}
   */
  const getData: IGetData = async () => {
    return cloneDeep(props.formModel);
  };
  /**
   * Hide specified form items
   * @param {string} field The field of the form item that needs to be hidden
   */
  const hidden: IHidden = (field) => {
    set(field, 'hidden', true);
  };

  /**
   * Disable form
   * @param {string | undefined} field
   */
  const disable: IDisable = (field) => {
    typeof field === 'string'
      ? setProps(field, 'disabled', true)
      : setFormConfig('disabled', field !== false);
  };

  /**
   * Show form items
   * @param {string} field The field of the form item that needs to be displayed
   */
  const show: IShow = (field) => {
    set(field, 'hidden', false);
  };

  /**
   * Triggered when monitoring form field linkage
   * @type {ILinkOn}
   */
  const linkOn: ILinkOn = {};
  const initLink = (schemas: IVFormComponent[]) => {
    // First traverse to find the form that requires associated fields
    formItemsForEach(schemas, (formItem) => {
      // If association is required, perform a second level traversal, find the associated fields in the form, and store them in Set.
      formItemsForEach(schemas, (item) => {
        if (!linkOn[item.field!]) linkOn[item.field!] = new Set<IVFormComponent>();
        if (formItem.link?.includes(item.field!) && isFunction(formItem.update)) {
          linkOn[item.field!].add(formItem);
        }
      });
      linkOn[formItem.field!].add(formItem);
    });
  };
  initLink(props.formConfig.schemas);

  return {
    linkOn,
    setValue,
    getValue,
    hidden,
    show,
    set,
    get,
    setProps,
    getData,
    disable,
    ...formInstanceMethods,
  };
}