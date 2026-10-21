import { expect, it } from 'vitest';

import { updateCSSVariables } from '../update-css-variables';

it('updateCSSVariables should update CSS variables in :root selector', () => {
  // Simulate initial inline style sheet content
  const initialStyleContent = ':root { --primaryColor: red; }';
  document.head.innerHTML = `<style id="custom-styles">${initialStyleContent}</style>`;

  // CSS variables to update and their new values
  const updatedVariables = {
    fontSize: '16px',
    primaryColor: 'blue',
    secondaryColor: 'green',
  };

  // Call function to update CSS variables
  updateCSSVariables(updatedVariables, 'custom-styles');

  // Get the updated style content
  const styleElement = document.querySelector('#custom-styles');
  const updatedStyleContent = styleElement ? styleElement.textContent : '';

  // Check whether the updated style content contains the correct updated value
  expect(
    updatedStyleContent?.includes('primaryColor: blue;') &&
      updatedStyleContent?.includes('secondaryColor: green;') &&
      updatedStyleContent?.includes('fontSize: 16px;'),
  ).toBe(true);
});
