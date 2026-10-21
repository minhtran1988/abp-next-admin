import type { NameValue } from './global';

type SettingValue = NameValue<string>;
/**
 * Set interface
 */
interface ISettingProvider {
  /**
   * Get the set value combination
   * @param names Filter setting name
   */
  getAll(...names: string[]): SettingValue[];
  /**
   * Query the number type setting value
   * @param name set name
   * @returns The return type is the set value of number, default 0
   */
  getNumber(name: string): number;
  /**
   * Get the setting value, if it is empty, return an empty string
   * @param name set name
   */
  getOrEmpty(name: string): string;
  /**
   * Query the boolean type setting value
   * @param name set name
   */
  isTrue(name: string): boolean;
}

export type { ISettingProvider, SettingValue };
