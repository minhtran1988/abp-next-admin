# @vben/plugins

This directory is used to store third-party libraries integrated in the project and their related plug-ins. Each plug-in contains reusable logic, configuration and components, making it easy to manage and call them uniformly in the project.

## Note

All third-party plug-ins must be introduced in the form of `subpath`, for example:

Taking `echarts` as an example, the introduction method is as follows:

**packages.json**

```json
"exports": {
    "./echarts": {
      "types": "./src/echarts/index.ts",
      "default": "./src/echarts/index.ts"
    }
  }
```

**How to use**

```ts
import { useEcharts } from '@vben/plugins/echarts';
```

The advantage of this is that the application can choose whether to use plug-ins, and the package size will not increase due to the introduction and side effects of plug-ins. Only the required plug-ins can be introduced.
