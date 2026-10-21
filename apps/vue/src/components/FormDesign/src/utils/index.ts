// import { VueConstructor } from 'vue';
import { IVFormComponent, IFormConfig, IValidationRule } from '../typings/v-form-component';
import { cloneDeep, isArray, isFunction, isNumber, uniqueId } from 'lodash-es';
// import { del } from '@vue/composition-api';
// import { withInstall } from '/@/utils';

/**
 * Component install method
 * @param comp The component that needs to be mounted with the install method
 */
// export function withInstall<T extends { name: string }>(comp: T) {
//   return Object.assign(comp, {
//     install(Vue: VueConstructor) {
//       Vue.component(comp.name, comp);
//     },
//   });
// }

/**
 * Generate key
 * @param [formItem] Control that needs to generate key, optional, if not passed, a unique key will be returned by default
 * @returns {string|boolean} returns a unique id or false
 */
export function generateKey(formItem?: IVFormComponent): string | boolean {
  if (formItem && formItem.component) {
    const key = uniqueId(`${toLine(formItem.component)}_`);
    formItem.key = key;
    formItem.field = key;

    return true;
  }
  return uniqueId('key_');
}

/**
 * Remove the specified element from the array. Value can be a numeric subscript or a function. Delete the first element whose function returns true.
 * @param array {Array<T>} Array of elements to be removed
 * @param value {number | ((item: T, index: number, array: Array<T>) => boolean}
 * @returns {T} Returns the deleted array item
 */
export function remove<T>(
  array: Array<T>,
  value: number | ((item: T, index: number, array: Array<T>) => boolean),
): T | undefined {
  let removeVal: Array<T | undefined> = [];
  if (!isArray(array)) return undefined;
  if (isNumber(value)) {
    removeVal = array.splice(value, 1);
  } else {
    const index = array.findIndex(value);
    if (index !== -1) {
      removeVal = array.splice(index, 1);
    }
  }
  return removeVal.shift();
}

/**
 * Determine data type
 * @param value
 */
export function getType(value: any): string {
  return Object.prototype.toString.call(value).slice(8, -1);
}

/**
 * Generate unique guid
 * @returns {String} unique id identifier
 */
export function randomUUID(): string {
  function S4() {
    return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
  }
  return `${S4() + S4()}-${S4()}-${S4()}-${S4()}-${S4() + S4() + S4()}`;
}

/**
 * Convert camel case to underline
 * @param str
 */
export function toLine(str: string) {
  return str.replace(/([A-Z])/g, '_$1').toLowerCase();
}

/**
 * Traverse form items
 * @param array
 * @param cb
 */
export function formItemsForEach(array: IVFormComponent[], cb: (item: IVFormComponent) => void) {
  if (!isArray(array)) return;
  const traverse = (schemas: IVFormComponent[]) => {
    schemas.forEach((formItem: IVFormComponent) => {
      if (['Grid'].includes(formItem.component)) {
        // Grid layout
        formItem.columns?.forEach((item) => traverse(item.children));
      } else {
        cb(formItem);
      }
    });
  };
  traverse(array);
}

/**
 * Find form items
 */
export const findFormItem: (
  schemas: IVFormComponent[],
  cb: (formItem: IVFormComponent) => boolean,
) => IVFormComponent | undefined = (schemas, cb) => {
  let res;
  const traverse = (schemas: IVFormComponent[]): boolean => {
    return schemas.some((formItem: IVFormComponent) => {
      const { component: type } = formItem;
      // process raster
      if (['Grid'].includes(type)) {
        return formItem.columns?.some((item) => traverse(item.children));
      }
      if (cb(formItem)) res = formItem;
      return cb(formItem);
    });
  };
  traverse(schemas);
  return res;
};

/**
 * Delete the current item attribute when opening the json modal box
 * @param formConfig {IFormConfig}
 * @returns {IFormConfig}
 */
export const removeAttrs = (formConfig: IFormConfig): IFormConfig => {
  const copyFormConfig = cloneDeep(formConfig);
  delete copyFormConfig.currentItem;
  delete copyFormConfig.activeKey;
  copyFormConfig.schemas &&
    formItemsForEach(copyFormConfig.schemas, (item) => {
      delete item.icon;
      delete item.key;
    });
  return copyFormConfig;
};

/**
 * Process asynchronous option attributes, such as select treeSelect and other option attributes. If passed as a function and return a Promise object, obtain the asynchronously returned option attributes.
 * @param {(() => Promise<any[]>) | any[]} options
 * @return {Promise<any[]>}
 */
export const handleAsyncOptions = async (
  options: (() => Promise<any[]>) | any[],
): Promise<any[]> => {
  try {
    if (isFunction(options)) return await options();
    return options;
  } catch {
    return [];
  }
};

/**
 * Formatted form item verification rule configuration
 * @param {IVFormComponent[]} schemas
 */
export const formatRules = (schemas: IVFormComponent[]) => {
  formItemsForEach(schemas, (item) => {
    if ('required' in item) {
      !isArray(item.rules) && (item.rules = []);
      item.rules.push({ required: true, message: item.message });
      delete item['required'];
      delete item['message'];
    }
  });
};

/**
 * Convert the regular string in the verification rule into a regular object
 * @param {IValidationRule[]} rules
 * @return {IValidationRule[]}
 */
export const strToReg = (rules: IValidationRule[]) => {
  const newRules = cloneDeep(rules);
  return newRules.map((item) => {
    if (item.pattern) item.pattern = runCode(item.pattern);
    return item;
  });
};

/**
 * Execute a string code and return the execution result. If an execution error occurs, this parameter will be returned.
 * @param code
 * @return {any}
 */
export const runCode = <T>(code: any): T => {
  try {
    return new Function(`return ${code}`)();
  } catch {
    return code;
  }
};