import type { Dictionary } from "#/abp-core";
import { format } from "@/utils/string";

/**
 * Allow external mergedResources to be passed in for translation
 * @param mergedResources precomputed resource objects
 * @param key the key that needs to be translated
 * @param args optional parameters
 * @returns translated string
 */
export function LrWithResources(
	mergedResources: Dictionary<string, string>,
	key: string,
	args?: any[] | Record<string, string>,
) {
	if (!key) return "";
	return mergedResources[key] ? format(mergedResources[key], args ?? []) : key;
}
