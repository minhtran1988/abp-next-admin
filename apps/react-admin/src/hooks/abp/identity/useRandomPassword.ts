import { useAbpSettings } from "@/store/abpSettingStore";

/**
 * Taken from https://www.html5tricks.com/demo/js-passwd-generator/index.html
 * Please contact us for deletion if there is any infringement.
 */
export function useRandomPassword() {
	const randomFunc: { [key: string]: () => string } = {
		defaultNumber: getRandomNumber,
		lower: getRandomLower,
		number: getRandomNumber,
		symbol: getRandomSymbol,
		upper: getRandomUpper,
	};

	function getRandomLower() {
		return String.fromCodePoint(Math.floor(Math.random() * 26) + 97);
	}

	function getRandomUpper() {
		return String.fromCodePoint(Math.floor(Math.random() * 26) + 65);
	}

	function getRandomNumber() {
		return String.fromCodePoint(Math.floor(Math.random() * 10) + 48);
	}

	function getRandomSymbol() {
		const symbols = '~!@#$%^&*()_+{}":?><;.,';
		return symbols[Math.floor(Math.random() * symbols.length)] ?? "";
	}

	function generatePassword() {
		const { getNumber, isTrue } = useAbpSettings();
		// Generate a random password based on configuration items
		// Password length
		const length = getNumber("Abp.Identity.Password.RequiredLength");
		// requires lowercase letters
		const lower = isTrue("Abp.Identity.Password.RequireLowercase");
		// Requires uppercase letters
		const upper = isTrue("Abp.Identity.Password.RequireUppercase");
		// requires numbers
		const number = isTrue("Abp.Identity.Password.RequireDigit");
		// requires symbols
		const symbol = isTrue("Abp.Identity.Password.RequireNonAlphanumeric");
		// generate numbers by default
		const defaultNumber = !lower && !upper && !number && !symbol;

		let generatedPassword = "";
		const typesArr = [{ lower }, { upper }, { number }, { symbol }, { defaultNumber }].filter(
			(item) => Object.values(item)[0],
		);
		for (let i = 0; i < length; i++) {
			typesArr.forEach((type) => {
				const funcName = Object.keys(type)[0];
				if (funcName && randomFunc[funcName]) {
					generatedPassword += randomFunc[funcName]();
				}
			});
		}
		return generatedPassword.slice(0, length);
	}

	return { generatePassword };
}
