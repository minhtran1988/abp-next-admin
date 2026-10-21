import { useEffect, useMemo } from "react";
import { format } from "@/utils/string";
import useLocaleStore, { useLocale } from "@/store/localeI18nStore";
import { getResources } from "@/utils/abp/localzations/get-resources";

// const { L } = useLocalizer(['AbpAuditLogging', 'AbpUi']);

/*
	const { t: $t } = useTranslation();
	const { L } = useLocalizer();
  console.log(L('DisplayName:ClaimValue')) //Different from getI18nLocales(), this one does not have a prefix key
  console.log($t('AbpIdentity.DisplayName:ClaimValue'))
*/
export function useLocalizer(resourceNames?: string | string[], callback?: () => void) {
	const localizations = useLocaleStore.getState().localizations || {};
	const locale = useLocale();

	const mergedResources = useMemo(() => getResources(resourceNames), [resourceNames]);

	useEffect(() => {
		if (callback) {
			callback();
		}
	}, [locale, callback]);

	/**
	 * Note, use react hook to store key
	 * @param key
	 * @param args
	 * @returns
	 */
	function L(key: string, args?: any[] | Record<string, string>) {
		if (!key) return "";
		return mergedResources[key] ? format(mergedResources[key], args ?? []) : key;
	}
	/**
	 * The national react hook is not actually called, it can be called in many places
	 * @param resource
	 * @param key
	 * @param args
	 * @returns
	 */
	function Lr(resource: string, key: string, args?: any[] | Record<string, string>) {
		if (!resource || !key) return "";
		// const { localizations } = useLocaleStore.getState();
		const subResource = localizations[resource] ?? {};
		return subResource[key] ? format(subResource[key], args ?? []) : key;
	}

	return { L, Lr };
}
