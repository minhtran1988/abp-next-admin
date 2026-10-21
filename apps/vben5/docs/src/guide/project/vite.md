# Vite Config

The project encapsulates a layer of Vite configuration and integrates some plug-ins to facilitate reuse in multiple packages and applications. The usage is as follows:

## Application

```ts
// vite.config.mts
import { defineConfig } from '@vben/vite-config';

export default defineConfig(async () => {
  return {
    application: {},
    // Vite configuration, refer to vite official documentation for coverage
    vite: {},
  };
});
```

## Package

```ts
// vite.config.mts
import { defineConfig } from '@vben/vite-config';

export default defineConfig(async () => {
  return {
    library: {},
    // Vite configuration, refer to vite official documentation for coverage
    vite: {},
  };
});
```
