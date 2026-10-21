# Check for updates

## Introduction

You may want to check for updates when updates are made to the website. The framework provides this function. By regularly checking for updates, you can configure the `checkUpdatesInterval` and `enableCheckUpdates` fields in the application's preferences.ts file to enable and set the time interval (unit: minutes) for checking for updates.

```ts
import { defineOverridesPreferences } from '@vben/preferences';

export const overridesPreferences = defineOverridesPreferences({
  // overrides
  app: {
    // Whether to enable checking for updates
    enableCheckUpdates: true,
    //The time interval for checking updates, in minutes
    checkUpdatesInterval: 1,
  },
});
```

## Effect

When an update is detected, a prompt box will pop up asking the user whether to refresh the page:

![check-updates](/guide/update-notice.png)

## Replace with other check update methods

If you need to check for updates in other ways, such as more flexibly controlling update logic through interfaces (such as forcing refresh, displaying updated content, etc.), you can do so by modifying the `src/widgets/check-updates/check-updates.vue` file under `@vben/layouts`.

```ts
//This can be replaced with your check update logic
async function getVersionTag() {
  try {
    const response = await fetch('/', {
      cache: 'no-cache',
      method: 'HEAD',
    });

    return (
      response.headers.get('etag') || response.headers.get('last-modified')
    );
  } catch {
    console.error('Failed to fetch version tag');
    return null;
  }
}
```

## Replace with third-party library to check update method

If you need to check for updates in other ways, such as using other version control methods (chunkHash, version.json), using `Web Worker` to poll for updates in the background, or customizing the timing of checking for updates (without using polling), you can use the JS library `version-polling` to achieve this.

```bash
pnpm add version-polling
```

Taking the `apps/web-antd` project as an example, add the following code to the project entry file `main.ts` or `app.vue`

```ts
import { h } from 'vue';

import { Button, notification } from 'ant-design-vue';
import { createVersionPolling } from 'version-polling';

createVersionPolling({
  silent: import.meta.env.MODE === 'development', // Not detected in development environment
  onUpdate: (self) => {
    const key = `open${Date.now()}`;
    notification.info({
      message: 'prompt',
      description: 'It has been detected that the web page has been updated. Do you want to refresh the page to load the latest version? ',
      btn: () =>
        h(
          Button,
          {
            type: 'primary',
            size: 'small',
            onClick: () => {
              notification.close(key);
              self.onRefresh();
            },
          },
          { default: () => 'Refresh' },
        ),
      key,
      duration: null,
      placement: 'bottomRight',
    });
  },
});
```
