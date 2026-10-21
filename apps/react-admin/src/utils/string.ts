/**
 *
 * @param str Whether the string is empty or space
 */
export function isNullOrWhiteSpace(str?: string) {
	return str === undefined || str === null || str === "" || str === " ";
}

/**
 * Format string
 * @param formatted The string to be processed
 * @param args parameter list, which can be an array or an object
 * @returns returns a formatted string
 * @example format('Hello, {0}!', ['World'])
 * @example format('Hello, {name}!', {name: 'World'})
 */
export function format(formatted: string, args: any[] | object) {
	let resultFormatted = formatted;
	if (Array.isArray(args)) {
		for (const [i, arg] of args.entries()) {
			const regexp = new RegExp(String.raw`\{` + i + String.raw`\}`, "gi");
			resultFormatted = formatted.replace(regexp, arg);
		}
	} else if (typeof args === "object") {
		Object.keys(args).forEach((key) => {
			const regexp = new RegExp(String.raw`\{` + key + String.raw`\}`, "gi");
			const param = (args as any)[key];
			resultFormatted = formatted.replace(regexp, param);
		});
	}
	return resultFormatted;
}

export function getUnique(val: string) {
	const arr = [...val];
	const newArr = [...new Set(arr)];
	return newArr.join("");
}
