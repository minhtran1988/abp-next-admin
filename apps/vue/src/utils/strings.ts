/**
 * Format string
 * @param formatted The string to be processed
 * @param args parameter list, which can be an array or an object
 * @returns returns a formatted string
 * @example format('Hello, {0}!', ['World'])
 * @example format('Hello, {name}!', {name: 'World'})
 */
export function format(formatted: string, args: object | any[]) {
  if (Array.isArray(args)) {
    for (let i = 0; i < args.length; i++) {
      const regexp = new RegExp('\\{' + i + '\\}', 'gi');
      formatted = formatted.replace(regexp, args[i]);
    }
  } else if (typeof args === 'object') {
    Object.keys(args).forEach((key) => {
      const regexp = new RegExp('\\{' + key + '\\}', 'gi');
      formatted = formatted.replace(regexp, args[key]);
    });
  }
  return formatted;
}
/**
 * Convert string to camel case format
 * @param str string
 * @returns CamelCase notation of string
 * @example toCamelCase('hello-world')  =>  helloWorld
 * @example toCamelCase('my_variable_name')  =>  myVariableName
 */
export function toCamelCase(str: string) {
  return str.replace(/[-_\s](\w)/g, function (_, c) {
      return c ? c.toUpperCase() : '';
  });
}
/**
 *
 * @param str Whether the string is empty or space
 * @returns
 */
export function isNullOrWhiteSpace(str?: string) {
  return str === undefined || str === null || str === '' || str === ' ';
}

export function tryToJson(str: string | undefined) {
  if (!str || str.length === 0) return {};
  try {
    return JSON.parse(str);
  } catch {
    return str;
  }
}

export function getUnique(val: string) {
  const arr = val.split('');
  const newArr = [...new Set(arr)];
  return newArr.join('');
}
