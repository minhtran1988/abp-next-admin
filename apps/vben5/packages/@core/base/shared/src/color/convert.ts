import { TinyColor } from '@ctrl/tinycolor';

/**
 * Convert colors to HSL format.
 *
 * HSL is a color model, including Hue, Saturation and Lightness.
 *
 * @param {string} color The input color.
 * @returns {string} Color string in HSL format.
 */
function convertToHsl(color: string): string {
  const { a, h, l, s } = new TinyColor(color).toHsl();
  const hsl = `hsl(${Math.round(h)} ${Math.round(s * 100)}% ${Math.round(l * 100)}%)`;
  return a < 1 ? `${hsl} ${a}` : hsl;
}

/**
 * Convert colors to HSL CSS variables.
 *
 * This function is similar to the convertToHsl function, but the string format returned is slightly different.
 * so that it can be used as a CSS variable.
 *
 * @param {string} color The input color.
 * @returns {string} A color string in HSL format that can be used as a CSS variable.
 */
function convertToHslCssVar(color: string): string {
  const { a, h, l, s } = new TinyColor(color).toHsl();
  const hsl = `${Math.round(h)} ${Math.round(s * 100)}% ${Math.round(l * 100)}%`;
  return a < 1 ? `${hsl} / ${a}` : hsl;
}

/**
 * Convert color to RGB color string
 * TinyColor cannot handle strings containing 'deg', 'grad', 'rad' or 'turn' in hsl
 * For example, hsl(231deg 98% 65%) will be parsed as rgb(0, 0, 0)
 * Here these units are removed before conversion
 * @param str A string representing the HLS color value
 * @returns If the color value is valid, return the corresponding RGB color string; if invalid, return rgb(0, 0, 0)
 */
function convertToRgb(str: string): string {
  return new TinyColor(str.replaceAll(/deg|grad|rad|turn/g, '')).toRgbString();
}

/**
 * Check if the color is valid
 * @param {string} color - the color to be checked
 * Returns true if the color is valid, false otherwise
 */
function isValidColor(color?: string) {
  if (!color) {
    return false;
  }
  return new TinyColor(color).isValid;
}

export {
  convertToHsl,
  convertToHslCssVar,
  convertToRgb,
  isValidColor,
  TinyColor,
};
