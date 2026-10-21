/**
 * Remove and destroy loading
 * It is placed here rather than in the app tag of index.html because it is less stiff and may cause flickering if the rendering is too fast.
 * Improve the experience by first adding css animation to hide, and then removing the loading node after the animation ends
 * The disadvantage is that it will increase the amount of code
 * Customized loading can be found at: https://doc.vben.pro/guide/in-depth/loading.html
 */
export function unmountGlobalLoading() {
  // Find the global loading element
  const loadingElement = document.querySelector('#__app-loading__');

  if (loadingElement) {
    // Add hidden class to trigger transition animation
    loadingElement.classList.add('hidden');

    // Find all injected loading elements that need to be removed
    const injectLoadingElements = document.querySelectorAll(
      '[data-app-loading^="inject"]',
    );

    // When the transition animation ends, remove the loading element and all injected loading elements
    loadingElement.addEventListener(
      'transitionend',
      () => {
        loadingElement.remove(); // Remove loading element
        injectLoadingElements.forEach((el) => el.remove()); // Remove all injected loading elements
      },
      { once: true },
    ); // Make sure the event is only triggered once
  }
}
