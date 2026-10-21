import { create } from "zustand";
import { createJSONStorage, persist } from "zustand/middleware";
import en_US from "antd/locale/en_US";
import zh_CN from "antd/locale/zh_CN";
import { LocalEnum } from "#/enum";
import { getLocalizationApi } from "@/api/abp-core";

import type { Locale as AntdLocal } from "antd/es/locale";
import useAbpStore from "./abpCoreStore";
import { mapLocaleToAbpLanguageFormat } from "@/utils";
import type { Dictionary } from "#/abp-core";

type Locale = keyof typeof LocalEnum;
type Language = {
	locale: keyof typeof LocalEnum;
	icon: string;
	label: string;
	antdLocal: AntdLocal;
};

export const LANGUAGE_MAP: Record<Locale, Language> = {
	[LocalEnum.zh_CN]: {
		locale: LocalEnum.zh_CN,
		label: "Chinese",
		icon: "ic-locale_zh_CN",
		antdLocal: zh_CN,
	},
	[LocalEnum.en_US]: {
		locale: LocalEnum.en_US,
		label: "English",
		icon: "ic-locale_en_US",
		antdLocal: en_US,
	},
};

type LocaleStore = {
	locale: Locale;
	language: Language;
	localizations: Dictionary<string, Dictionary<string, string>>;

	actions: {
		setLocale: (locale: Locale, i18n: any) => Promise<void>;
	};
};

const useLocaleStore = create<LocaleStore>()(
	persist(
		(set) => ({
			locale: LocalEnum.en_US, //Default language
			language: LANGUAGE_MAP[LocalEnum.en_US],
			localizations: {},
			actions: {
				setLocale: async (locale: Locale, i18n: any) => {
					let { localization } = useAbpStore.getState();
					const setLocalization = useAbpStore.getState().actions.setLocalization;
					const getI18nLocales = useAbpStore.getState().actions.getI18nLocales;

					const lang = mapLocaleToAbpLanguageFormat(locale);
					if (lang !== localization?.currentCulture.cultureName) {
						localization = await getLocalizationApi({
							cultureName: lang, //api language conversion
							onlyDynamics: false,
						});
					}

					if (localization) {
						setLocalization(localization); //Save in
					}
					const locales = getI18nLocales(); //Just saved it, clean it up
					i18n.changeLanguage(locale);
					Object.keys(locales).forEach((resource) => {
						const translations = { [resource]: locales[resource] }; // Preserve nested structure
						i18n.addResourceBundle(locale, "translation", translations, true, true); // Add resources to i18n
					});
					// Update Zustand status
					set({
						locale,
						language: LANGUAGE_MAP[locale],
						localizations: locales,
					});
				},
			},
		}),
		{
			name: "localeI18nStore",
			storage: createJSONStorage(() => localStorage),
			// Keep the style, but do not persist the data temporarily
			partialize: () => ({}),
		},
	),
);

export const useLocale = () => useLocaleStore((state) => state.locale);
export const useLanguage = () => useLocaleStore((state) => state.language);
export const useSetLocale = () => useLocaleStore((state) => state.actions.setLocale);

export default useLocaleStore;
