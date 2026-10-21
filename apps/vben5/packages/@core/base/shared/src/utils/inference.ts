// eslint-disable-next-line vue/prefer-import-from-vue
import { isFunction, isObject, isString } from '@vue/shared';

/**
 * Check whether the passed in value is undefined.
 *
 * @param {unknown} value The value to check.
 * @returns {boolean} If the value is undefined, return true, otherwise return false.
 */
function isUndefined(value?: unknown): value is undefined {
  return value === undefined;
}

/**
 * Check whether the passed in value is boolean
 * @param value
 * @returns If the value is a Boolean value, return true, otherwise return false.
 */
function isBoolean(value: unknown): value is boolean {
  return typeof value === 'boolean';
}

/**
 * Check whether the passed in value is empty.
 *
 * The following situations will be considered empty:
 * - The value is null.
 * - The value is undefined.
 * - The value is an empty string.
 * - The value is an array of length 0.
 * - The value is a Map or Set with no elements.
 * - The value is an object with no properties.
 *
 * @param {T} value The value to check.
 * @returns {boolean} If the value is empty, return true, otherwise return false.
 */
function isEmpty<T = unknown>(value?: T): value is T {
  if (value === null || value === undefined) {
    return true;
  }

  if (Array.isArray(value) || isString(value)) {
    return value.length === 0;
  }

  if (value instanceof Map || value instanceof Set) {
    return value.size === 0;
  }

  if (isObject(value)) {
    return Object.keys(value).length === 0;
  }

  return false;
}

/**
 * Check whether the incoming string is a valid HTTP or HTTPS URL.
 *
 * @param {string} url The string to check.
 * @return {boolean} Returns true if the string is a valid HTTP or HTTPS URL, otherwise returns false.
 */
function isHttpUrl(url?: string): boolean {
  if (!url) {
    return false;
  }
  // Use regular expressions to test whether the URL starts with http:// or https://
  const httpRegex = /^https?:\/\/.*$/;
  return httpRegex.test(url);
}

/**
 * Check whether the passed in value is a window object.
 *
 * @param {any} value The value to check.
 * @returns {boolean} If the value is a window object, return true, otherwise return false.
 */
function isWindow(value: any): value is Window {
  return (
    typeof window !== 'undefined' && value !== null && value === value.window
  );
}

/**
 * Check whether the current running environment is Mac OS.
 *
 * This function determines the current running environment by checking the navigator.userAgent string.
 * If the userAgent string contains "macintosh" or "mac os x" (case-insensitive), the current environment is considered to be Mac OS.
 *
 * @returns {boolean} If the current environment is Mac OS, return true, otherwise return false.
 */
function isMacOs(): boolean {
  const macRegex = /macintosh|mac os x/i;
  return macRegex.test(navigator.userAgent);
}

/**
 * Check whether the current running environment is Windows OS.
 *
 * This function determines the current running environment by checking the navigator.userAgent string.
 * If the userAgent string contains "windows" or "win32" (case-insensitive), the current environment is considered to be a Windows OS.
 *
 * @returns {boolean} If the current environment is Windows OS, return true, otherwise return false.
 */
function isWindowsOs(): boolean {
  const windowsRegex = /windows|win32/i;
  return windowsRegex.test(navigator.userAgent);
}

/**
 * Check whether the incoming value is a number
 * @param value
 */
function isNumber(value: any): value is number {
  return typeof value === 'number' && Number.isFinite(value);
}

/**
 * Returns the first value in the provided list that is neither `null` nor `undefined`.
 *
 * This function iterates over the input values and returns the first one that is
 * not strictly equal to `null` or `undefined`. If all values are either `null` or
 * `undefined`, it returns `undefined`.
 *
 * @template T - The type of the input values.
 * @param {...(T | null | undefined)[]} values - A list of values to evaluate.
 * @returns {T | undefined} - The first value that is not `null` or `undefined`, or `undefined` if none are found.
 *
 * @example
 * // Returns 42 because it is the first non-null, non-undefined value.
 * getFirstNonNullOrUndefined(undefined, null, 42, 'hello'); // 42
 *
 * @example
 * // Returns 'hello' because it is the first non-null, non-undefined value.
 * getFirstNonNullOrUndefined(null, undefined, 'hello', 123); // 'hello'
 *
 * @example
 * // Returns undefined because all values are either null or undefined.
 * getFirstNonNullOrUndefined(undefined, null); // undefined
 */
function getFirstNonNullOrUndefined<T>(
  ...values: (null | T | undefined)[]
): T | undefined {
  for (const value of values) {
    if (value !== undefined && value !== null) {
      return value;
    }
  }
  return undefined;
}

export {
  getFirstNonNullOrUndefined,
  isBoolean,
  isEmpty,
  isFunction,
  isHttpUrl,
  isMacOs,
  isNumber,
  isObject,
  isString,
  isUndefined,
  isWindow,
  isWindowsOs,
};
