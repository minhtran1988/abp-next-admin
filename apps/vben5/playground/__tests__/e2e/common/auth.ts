import type { Page } from '@playwright/test';

import { expect } from '@playwright/test';

export async function authLogin(page: Page) {
  // Make sure the login form is normal
  const usernameInput = await page.locator(`input[name='username']`);
  await expect(usernameInput).toBeVisible();

  const passwordInput = await page.locator(`input[name='password']`);
  await expect(passwordInput).toBeVisible();

  const sliderCaptcha = await page.locator(`div[name='captcha']`);
  const sliderCaptchaAction = await page.locator(`div[name='captcha-action']`);
  await expect(sliderCaptcha).toBeVisible();
  await expect(sliderCaptchaAction).toBeVisible();

  // Drag the verification code slider
  // Get the position of the drag button
  const sliderCaptchaBox = await sliderCaptcha.boundingBox();
  if (!sliderCaptchaBox) throw new Error('Slider not found');

  const actionBoundingBox = await sliderCaptchaAction.boundingBox();
  if (!actionBoundingBox) throw new Error('The button to be dragged was not found');

  // Calculate starting position and target position
  const startX = actionBoundingBox.x + actionBoundingBox.width / 2; // x coordinate of div center
  const startY = actionBoundingBox.y + actionBoundingBox.height / 2; // y coordinate of the center of the div

  const targetX = startX + sliderCaptchaBox.width + actionBoundingBox.width; // Drag the width of the container to the right
  const targetY = startY; // y coordinate remains unchanged

  // Simulate mouse drag
  await page.mouse.move(startX, startY); // Move to the center of the action
  await page.mouse.down(); // Press mouse
  await page.mouse.move(targetX, targetY, { steps: 20 }); // Drag to the target position
  await page.mouse.up(); // Release the mouse

  // Make assertions after dragging to check whether the action is at the expected location,
  const newActionBoundingBox = await sliderCaptchaAction.boundingBox();
  expect(newActionBoundingBox?.x).toBeGreaterThan(actionBoundingBox.x);

  // The verification has been successful here, click to log in
  await page.waitForTimeout(300);
  await page.getByRole('button', { name: 'login' }).click();
}
