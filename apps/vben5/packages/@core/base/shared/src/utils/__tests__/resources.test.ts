import { beforeEach, describe, expect, it } from 'vitest';

import { loadScript } from '../resources';

const testJsPath =
  'https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js';

describe('loadScript', () => {
  beforeEach(() => {
    // Clear head before each test to ensure a clean environment
    document.head.innerHTML = '';
  });

  it('should resolve when the script loads successfully', async () => {
    const promise = loadScript(testJsPath);

    // At this point the script element has been created and inserted
    const script = document.querySelector(
      `script[src="${testJsPath}"]`,
    ) as HTMLScriptElement;
    expect(script).toBeTruthy();

    // Simulation loading successful
    script.dispatchEvent(new Event('load'));

    // Wait for promise to resolve
    await expect(promise).resolves.toBeUndefined();
  });

  it('should not insert duplicate script and resolve immediately if already loaded', async () => {
    // First manually insert a script with the same src
    const existing = document.createElement('script');
    existing.src = 'bar.js';
    document.head.append(existing);

    // call again
    const promise = loadScript('bar.js');

    // resolve immediately
    await expect(promise).resolves.toBeUndefined();

    // Only one is kept in head
    const scripts = document.head.querySelectorAll('script[src="bar.js"]');
    expect(scripts).toHaveLength(1);
  });

  it('should reject when the script fails to load', async () => {
    const promise = loadScript('error.js');

    const script = document.querySelector(
      'script[src="error.js"]',
    ) as HTMLScriptElement;
    expect(script).toBeTruthy();

    // Simulation loading failed
    script.dispatchEvent(new Event('error'));

    await expect(promise).rejects.toThrow('Failed to load script: error.js');
  });

  it('should handle multiple concurrent calls and only insert one script tag', async () => {
    const p1 = loadScript(testJsPath);
    const p2 = loadScript(testJsPath);

    const script = document.querySelector(
      `script[src="${testJsPath}"]`,
    ) as HTMLScriptElement;
    expect(script).toBeTruthy();

    // Trigger a load, both promises should resolve
    script.dispatchEvent(new Event('load'));

    await expect(p1).resolves.toBeUndefined();
    await expect(p2).resolves.toBeUndefined();

    // Insert only once
    const scripts = document.head.querySelectorAll(
      `script[src="${testJsPath}"]`,
    );
    expect(scripts).toHaveLength(1);
  });
});
