/**
 * Function to update CSS variables
 * @param variables Mapping of CSS variables to be updated and their new values
 */
function updateCSSVariables(
  variables: { [key: string]: string },
  id = '__vben-styles__',
): void {
  // Get or create an inline style sheet element
  const styleElement =
    document.querySelector(`#${id}`) || document.createElement('style');

  styleElement.id = id;

  // Construct the style text of the CSS variable to be updated
  let cssText = ':root {';
  for (const key in variables) {
    if (Object.prototype.hasOwnProperty.call(variables, key)) {
      cssText += `${key}: ${variables[key]};`;
    }
  }
  cssText += '}';

  // Assign style text to the inline style sheet
  styleElement.textContent = cssText;

  // Add an inline style sheet to the head of the document
  if (!document.querySelector(`#${id}`)) {
    setTimeout(() => {
      document.head.append(styleElement);
    });
  }
}

export { updateCSSVariables };
