/**
 * Capitalize the first letter of the string
 * @param string
 */
function capitalizeFirstLetter(string: string): string {
  return string.charAt(0).toUpperCase() + string.slice(1);
}

/**
 * Convert the first letter of the string to lowercase.
 *
 * @param str The string to be converted
 * @returns string with lowercase first letter
 */
function toLowerCaseFirstLetter(str: string): string {
  if (!str) return str; // If the string is empty, return directly
  return str.charAt(0).toLowerCase() + str.slice(1);
}

/**
 * Generate camel case key names
 * @param key
 * @param parentKey
 */
function toCamelCase(key: string, parentKey: string): string {
  if (!parentKey) {
    return key;
  }
  return parentKey + key.charAt(0).toUpperCase() + key.slice(1);
}

function kebabToCamelCase(str: string): string {
  return str
    .split('-')
    .filter(Boolean)
    .map((word, index) =>
      index === 0 ? word : word.charAt(0).toUpperCase() + word.slice(1),
    )
    .join('');
}

export {
  capitalizeFirstLetter,
  kebabToCamelCase,
  toCamelCase,
  toLowerCaseFirstLetter,
};
