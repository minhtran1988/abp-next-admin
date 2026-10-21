import type NProgress from 'nprogress';

// Create a variable of an NProgress instance with an initial value of null
let nProgressInstance: null | typeof NProgress = null;

/**
 * Dynamically load the NProgress library and configure it.
 * This function first checks whether the NProgress library has been loaded. If it has been loaded, it returns the NProgress instance directly.
 * Otherwise, dynamically import the NProgress library, configure it, and then return the NProgress instance.
 *
 * @returns Promise object of NProgress instance.
 */
async function loadNprogress() {
  if (nProgressInstance) {
    return nProgressInstance;
  }
  nProgressInstance = await import('nprogress');
  nProgressInstance.configure({
    showSpinner: true,
    speed: 300,
  });
  return nProgressInstance;
}

/**
 * Start displaying the progress bar.
 * This function first loads the NProgress library, and then calls the start method of NProgress to start displaying the progress bar.
 */
async function startProgress() {
  const nprogress = await loadNprogress();
  nprogress?.start();
}

/**
 * Stop showing the progress bar and hide the progress bar.
 * This function first loads the NProgress library, and then calls the done method of NProgress to stop and hide the progress bar.
 */
async function stopProgress() {
  const nprogress = await loadNprogress();
  nprogress?.done();
}

export { startProgress, stopProgress };
