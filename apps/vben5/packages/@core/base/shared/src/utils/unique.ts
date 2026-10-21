/**
 * Deduplicate the object array according to the specified field
 * @param arr The object array to be duplicated
 * @param key field name based on which to remove duplicates
 * @returns object array after deduplication
 */
function uniqueByField<T>(arr: T[], key: keyof T): T[] {
  const seen = new Map<any, T>();
  return arr.filter((item) => {
    const value = item[key];
    return seen.has(value) ? false : (seen.set(value, item), true);
  });
}

export { uniqueByField };
