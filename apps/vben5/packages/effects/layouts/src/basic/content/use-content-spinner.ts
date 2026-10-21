import { computed, ref } from 'vue';
import { useRouter } from 'vue-router';

import { preferences } from '@vben/preferences';

function useContentSpinner() {
  const spinning = ref(false);
  const startTime = ref(0);
  const router = useRouter();
  const minShowTime = 500; // Minimum display time
  const enableLoading = computed(() => preferences.transition.loading);

  // End loading animation
  const onEnd = () => {
    if (!enableLoading.value) {
      return;
    }
    const processTime = performance.now() - startTime.value;
    if (processTime < minShowTime) {
      setTimeout(() => {
        spinning.value = false;
      }, minShowTime - processTime);
    } else {
      spinning.value = false;
    }
  };

  // Route pre-guard
  router.beforeEach((to) => {
    if (to.meta.loaded || !enableLoading.value || to.meta.iframeSrc) {
      return true;
    }
    startTime.value = performance.now();
    spinning.value = true;
    return true;
  });

  // Route post guard
  router.afterEach((to) => {
    if (to.meta.loaded || !enableLoading.value || to.meta.iframeSrc) {
      return true;
    }
    onEnd();
    return true;
  });

  return { spinning };
}

export { useContentSpinner };
