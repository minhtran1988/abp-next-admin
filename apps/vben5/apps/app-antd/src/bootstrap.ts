import { createApp, watchEffect } from 'vue';

import { registerAccessDirective } from '@vben/access';
import { registerLoadingDirective } from '@vben/common-ui';
import { preferences } from '@vben/preferences';
import { initStores } from '@vben/stores';
import '@vben/styles';
import '@vben/styles/antd';

import { useTitle } from '@vueuse/core';

import { $t, setupI18n } from '#/locales';

import { initComponentAdapter } from './adapter/component';
import { initSetupVbenForm } from './adapter/form';
import { initRequestClient } from './adapter/request';
import App from './app.vue';
import { router } from './router';
import { initTimezone } from './timezone-init';

async function bootstrap(namespace: string) {
  // Initialize component adapter
  await initComponentAdapter();

  // Initialize form components
  await initSetupVbenForm();

  // Initialize axios
  initRequestClient();

  // //Set the default configuration of the pop-up window
  // setDefaultModalProps({
  //   fullscreenButton: false,
  // });
  // //Set the default configuration of the drawer
  // setDefaultDrawerProps({
  //   zIndex: 1020,
  // });

  const app = createApp(App);

  // Register v-loading directive
  registerLoadingDirective(app, {
    loading: 'loading', // You can customize the command name here, or you can explicitly provide false to indicate not to register this command.
    spinning: 'spinning',
  });

  // Configure pinia-tore
  await initStores(app, { namespace });

  // Internationalization i18n configuration
  await setupI18n(app);

  // Initialize time zone HANDLER
  initTimezone();

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
