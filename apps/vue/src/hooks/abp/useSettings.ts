import { computed } from 'vue';
import { useAbpStoreWithOut } from '/@/store/modules/abp';

type SettingValue = NameValue<string>;
/**
 * Set interface
 */
export interface ISettingProvider {
  /**
   * Query the number type setting value
   * @param name set name
   * @returns The return type is the set value of number, default 0
   */
  getNumber(name: string): number;
  /**
   * Query the boolean type setting value
   * @param name set name
   */
  isTrue(name: string): boolean;
  /**
   * Get the setting value, if it is empty, return an empty string
   * @param name set name
   */
  getOrEmpty(name: string): string;
  /**
   * Get the set value combination
   * @param names Filter setting name
   */
  getAll(...names: string[]): SettingValue[];
}

export function useSettings() {
  const getSettings = computed(() => {
    const abpStore = useAbpStoreWithOut();
    const { values: settings } = abpStore.getApplication.setting;
    const settingValues = Object.keys(settings).map((key): SettingValue => {
      return {
        name: key,
        value: settings[key],
      };
    });
    return settingValues;
  });

  function get(name: string): SettingValue | undefined {
    return getSettings.value.find((setting) => name === setting.name);
  }

  function getAll(...names: string[]): SettingValue[] {
    if (names) {
      return getSettings.value.filter((setting) => names.includes(setting.name));
    }
    return getSettings.value;
  }

  function getOrDefault<T>(name: string, defaultValue: T): T | string {
    var setting = get(name);
    if (!setting) {
      return defaultValue;
    }
    return setting.value;
  }

  const settingProvider: ISettingProvider = {
    getOrEmpty(name: string) {
      return getOrDefault(name, '');
    },
    getAll(...names: string[]) {
      return getAll(...names);
    },
    getNumber(name: string, defaultValue: number = 0) {
      var value = getOrDefault(name, defaultValue);
      const num = Number(value);
      return isNaN(num) ? defaultValue : num;
    },
    isTrue(name: string) {
      var value = getOrDefault(name, 'false');
      return value.toLowerCase() === 'true';
    },
  };

  return { settingProvider };
}
