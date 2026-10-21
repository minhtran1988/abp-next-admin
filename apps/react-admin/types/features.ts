import type { NameValue } from "./abp-core";

type FeatureValue = NameValue<string>;

/**
 * Feature checking interface
 */
interface IFeatureChecker {
	/**
	 * Get the characteristic value
	 * @param name attribute name
	 * @returns Returns the set characteristic value, or an empty string if it does not exist
	 */
	getOrEmpty(name: string): string;
	/**
	 * Whether to enable the feature
	 * @param featureNames feature names
	 * @param requiresAll whether all are consistent
	 */
	isEnabled(featureNames: string | string[], requiresAll?: boolean): boolean;
}

interface IGlobalFeatureChecker {
	isEnabled(featureNames: string | string[], requiresAll?: boolean): boolean;
}

export type { FeatureValue, IFeatureChecker, IGlobalFeatureChecker };
