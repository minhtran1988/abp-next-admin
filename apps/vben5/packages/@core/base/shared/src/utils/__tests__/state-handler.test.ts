import { describe, expect, it } from 'vitest';

import { StateHandler } from '../state-handler';

describe('stateHandler', () => {
  it('should resolve when condition is set to true', async () => {
    const handler = new StateHandler();

    // Simulate asynchronous setting of condition to true
    setTimeout(() => {
      handler.setConditionTrue(); // Explicitly trigger condition to be true
    }, 10);

    // Wait for the condition to be set to true
    await handler.waitForCondition();
    expect(handler.isConditionTrue()).toBe(true);
  });

  it('should resolve immediately if condition is already true', async () => {
    const handler = new StateHandler();
    handler.setConditionTrue(); // Set to true in advance

    // resolve immediately because condition is already true
    await handler.waitForCondition();
    expect(handler.isConditionTrue()).toBe(true);
  });

  it('should reject when condition is set to false after waiting', async () => {
    const handler = new StateHandler();

    // Simulate asynchronous setting condition to false
    setTimeout(() => {
      handler.setConditionFalse(); // Explicitly trigger condition to be false
    }, 10);

    // During the waiting process, the Promise is expected to be rejected.
    await expect(handler.waitForCondition()).rejects.toThrow();
    expect(handler.isConditionTrue()).toBe(false);
  });

  it('should reset condition to false', () => {
    const handler = new StateHandler();
    handler.setConditionTrue(); // Set to true
    handler.reset(); // reset to false

    expect(handler.isConditionTrue()).toBe(false);
  });

  it('should resolve when condition is set to true after reset', async () => {
    const handler = new StateHandler();
    handler.reset(); // Make sure it is initially false

    setTimeout(() => {
      handler.setConditionTrue(); // Set to true after reset
    }, 10);

    await handler.waitForCondition();
    expect(handler.isConditionTrue()).toBe(true);
  });
});
