/**
 * Load js file
 * @param src js file address
 */
function loadScript(src: string) {
  return new Promise<void>((resolve, reject) => {
    if (document.querySelector(`script[src="${src}"]`)) {
      // If it has already been loaded, resolve directly
      return resolve();
    }
    const script = document.createElement('script');
    script.src = src;
    script.addEventListener('load', () => resolve());
    script.addEventListener('error', () =>
      reject(new Error(`Failed to load script: ${src}`)),
    );
    document.head.append(script);
  });
}

export { loadScript };
