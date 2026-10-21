import { createApp, watchEffect } from 'vue';

import { registerAccessDirective } from '@vben/access';
import { registerLoadingDirective } from '@vben/common-ui';
import { preferences } from '@vben/preferences';
import { initStores } from '@vben/stores';
import '@vben/styles';
import '@vben/styles/ele';

import { useTitle } from '@vueuse/core';
import { ElLoading } from 'element-plus';

import { $t, setupI18n } from '#/locales';

import { initComponentAdapter } from './adapter/component';
import { initSetupVbenForm } from './adapter/form';
import App from './app.vue';
import { router } from './router';

async function bootstrap(namespace: string) {
  // Initialize component adapter
  await initComponentAdapter();

  // Initialize form components
  await initSetupVbenForm();

  // //Set the default configuration of the pop-up window
  // setDefaultModalProps({
  //   fullscreenButton: false,
  // });
  // //Set the default configuration of the drawer
  // setDefaultDrawerProps({
  //   zIndex: 2000,
  // });
  const app = createApp(App);

  // Register the v-loading instruction provided by Element Plus
  app.directive('loading', ElLoading.directive);

  // Register the v-loading and v-spinning instructions provided by Vben
  registerLoadingDirective(app, {
    loading: false, // You can choose between the v-loading instruction provided by Vben and the v-loading instruction provided by Element Plus. False here means not to register the v-loading instruction provided by Vben.
    spinning: 'spinning',
  });

  // Internationalization i18n configuration
  await setupI18n(app);

  // Configure pinia-tore
  await initStores(app, { namespace });

  // Install permission instructions
  registerAccessDirective(app);

  // Initialize tippy
  const { initTippy } = await import('@vben/common-ui/es/tippy');
  initTippy(app);

  // Configure routing and routing guards
  app.use(router);

  // Configure Motion plug-in
  const { MotionPlugin } = await import('@vben/plugins/motion');
  app.use(MotionPlugin);

  // Dynamically update title
  watchEffect(() => {
    if (preferences.app.dynamicTitle) {
      const routeTitle = router.currentRoute.value.meta?.title;
      const pageTitle =
        (routeTitle ? `${$t(routeTitle)} - ` : '') + preferences.app.name;
      useTitle(pageTitle);
    }
  });

  app.mount('#app');
}

export { bootstrap };
