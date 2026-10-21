---
outline: deep
---

#Permissions

The framework has three built-in permission control methods:

- Determine whether menus or buttons are accessible by user role
- Determine whether menus or buttons are accessible through the interface
- Mixed mode: use both front-end and back-end permission control

## Front-end access control

**Implementation Principle**: Fixed hard-coded route permissions on the front end, specifying what permissions the route has to view. Only common routes are initialized, and routes that require permission to access are not added to the routing table. After logging in or obtaining the user role in other ways, traverse the routing table through the role, obtain the routing table that the role can access, generate the routing table, and then add it to the routing instance through `router.addRoute` to implement permission filtering.

**Disadvantages**: Permissions are relatively unrestricted. If the role is changed in the background, the front desk also needs to be changed accordingly. Suitable for systems with fixed roles

### Steps

- Make sure the current mode is front-end access control mode

Adjust `preferences.ts` in the corresponding application directory to ensure `accessMode='frontend'`.

```ts
import { defineOverridesPreferences } from '@vben/preferences';

export const overridesPreferences = defineOverridesPreferences({
  // overrides
  app: {
    //Default value, optional
    accessMode: 'frontend',
  },
});
```

- Configure routing permissions

**If not configured, it will be visible by default**

```ts {3}
 {
    meta: {
      authority: ['super'],
    },
},
```

- Ensure that the role returned by the interface matches the permissions of the routing table

You can view `src/store/auth` under the application and find the following code,

```ts
// To set the login user information, you need to ensure that userInfo.roles is an array and contains the permissions in the routing table.
// For example: userInfo.roles=['super', 'admin']
authStore.setUserInfo(userInfo);
```

At this point, the configuration has been completed. You need to ensure that after logging in, the role returned by the interface matches the permissions of the routing table, otherwise you will not be able to access it.

### Menu is visible but access is prohibited

Sometimes, we need the menu to be visible, but access is prohibited. This can be achieved in the following way. Set `menuVisibleWithForbidden` to `true`. At this time, the menu is visible, but access is prohibited, and the 403 page will be redirected.

```ts
{
    meta: {
      menuVisibleWithForbidden: true,
    },
},
```

## Backend access control

**Implementation principle**: The routing table is dynamically generated through the interface and returned following a certain data structure. The front end processes the data into a recognizable structure as needed, and then adds it to the routing instance through `router.addRoute` to achieve dynamic generation of permissions.

**Disadvantages**: The backend needs to provide data structures that comply with specifications, and the frontend needs to process the data structures, which is suitable for systems with more complex permissions.

### Steps

- Make sure the current mode is backend access control mode

Adjust `preferences.ts` in the corresponding application directory to ensure `accessMode='backend'`.

```ts
import { defineOverridesPreferences } from '@vben/preferences';

export const overridesPreferences = defineOverridesPreferences({
  // overrides
  app: {
    accessMode: 'backend',
  },
});
```

- Ensure that the menu data structure returned by the interface is correct

You can view `src/router/access.ts` under the application and find the following code,

```ts {5}
async function generateAccess(options: GenerateMenuAndRoutesOptions) {
  return await generateAccessible(preferences.app.accessMode, {
    fetchMenuListAsync: async () => {
      // This interface is the menu data returned by the backend
      return await getAllMenus();
    },
  });
}
```

- The interface returns menu data, please see the comments

:::Details interface returns menu data example

```ts
const dashboardMenus = [
  {
    meta: {
      order: -1,
      title: 'page.dashboard.title',
    },
    name: 'Dashboard',
    path: '/',
    redirect: '/analytics',
    children: [
      {
        name: 'Analytics',
        path: '/analytics',
        // Here is the path to the page, you need to remove views/ and .vue
        component: '/dashboard/analytics/index',
        meta: {
          affixTab: true,
          title: 'page.dashboard.analytics',
        },
      },
      {
        name: 'Workspace',
        path: '/workspace',
        component: '/dashboard/workspace/index',
        meta: {
          title: 'page.dashboard.workspace',
        },
      },
    ],
  },
  {
    name: 'Test',
    path: '/test',
    component: '/test/index',
    meta: {
      title: 'page.test',
      // If some special pages do not require basic layout (top of page and sidebar), you can set noBasicLayout to true
      noBasicLayout: true,
    },
  },
];
```

:::

At this point, the configuration has been completed. You need to ensure that the menu returned by the interface is in the correct format after logging in, otherwise it will be inaccessible.

## Hybrid access control

**Implementation principle**: The hybrid mode combines both front-end access control and back-end access control. The system will process front-end fixed routing permissions and back-end dynamic menu data in parallel, and finally merge the two parts of routing to provide a more flexible permission control solution.

**Advantages**: It combines the performance advantages of front-end control with the flexibility of back-end control, and is suitable for permission management in complex business scenarios.

### Steps

- Make sure the current mode is mixed access control mode

Adjust `preferences.ts` in the corresponding application directory to ensure `accessMode='mixed'`.

```ts
import { defineOverridesPreferences } from '@vben/preferences';

export const overridesPreferences = defineOverridesPreferences({
  // overrides
  app: {
    accessMode: 'mixed',
  },
});
```

- Configure front-end routing permissions

The routing permission configuration method is the same as [front-end access control](#front-end access control) mode.

- Configure the backend menu interface

The same interface configuration method as [Backend Access Control](#Backend Access Control) mode.

- Make sure roles and permissions match

It is necessary to meet the requirements of front-end routing permission configuration and back-end menu data return at the same time, and ensure that the user role matches the permission configuration of both modes.

At this point, the configuration has been completed. The hybrid mode will automatically merge the front-end and back-end routing to provide complete permission control functions.

## Fine-grained control of buttons

In some cases, we need fine-grained control of buttons. We can use interfaces or roles to control the display of buttons.

### Permission code

The permission code is the permission code returned by the interface. The permission code is used to determine whether the button is displayed. The logic is under `src/store/auth`:

```ts
const [fetchUserInfoResult, accessCodes] = await Promise.all([
  fetchUserInfo(),
  getAccessCodes(),
]);

userInfo = fetchUserInfoResult;
authStore.setUserInfo(userInfo);
accessStore.setAccessCodes(accessCodes);
```

Find the interface corresponding to `getAccessCodes` and adjust it according to the business logic.

The data structure returned by the permission code is a string array, for example: `['AC_100100', 'AC_100110', 'AC_100120', 'AC_100010']`

With the permission code, you can use the `AccessControl` component and API provided by `@vben/access` to display and hide the buttons.

#### Component mode

```vue
<script lang="ts" setup>
import { AccessControl, useAccess } from '@vben/access';

const { accessMode, hasAccessByCodes } = useAccess();
</script>

<template>
  <!-- Type="code" needs to be specified -->
  <AccessControl :codes="['AC_100100']" type="code">
    <Button> Super account is visible ["AC_1000001"] </Button>
  </AccessControl>
  <AccessControl :codes="['AC_100030']" type="code">
    <Button> Admin account is visible ["AC_100010"] </Button>
  </AccessControl>
  <AccessControl :codes="['AC_1000001']" type="code">
    <Button> User account is visible ["AC_1000001"] </Button>
  </AccessControl>
  <AccessControl :codes="['AC_100100', 'AC_100010']" type="code">
    <Button> Super & Admin accounts are visible ["AC_100100","AC_1000001"] </Button>
  </AccessControl>
</template>
```

#### API method

```vue
<script lang="ts" setup>
import { AccessControl, useAccess } from '@vben/access';

const { hasAccessByCodes } = useAccess();
</script>

<template>
  <Button v-if="hasAccessByCodes(['AC_100100'])">
    Super account is visible ["AC_1000001"]
  </Button>
  <Button v-if="hasAccessByCodes(['AC_100030'])">
    Admin account is visible ["AC_100010"]
  </Button>
  <Button v-if="hasAccessByCodes(['AC_1000001'])">
    User account is visible ["AC_1000001"]
  </Button>
  <Button v-if="hasAccessByCodes(['AC_100100', 'AC_1000001'])">
    Super & Admin accounts are visible ["AC_100100","AC_1000001"]
  </Button>
</template>
```

#### Command mode

> The command supports binding single or multiple permission codes. A single permission code can be directly passed in as a string or an array, and multiple permission codes can be passed in as an array.

```vue
<template>
  <Button class="mr-4" v-access:code="'AC_100100'">
    Super account visible 'AC_100100'
  </Button>
  <Button class="mr-4" v-access:code="['AC_100030']">
    Admin account is visible ["AC_100010"]
  </Button>
  <Button class="mr-4" v-access:code="['AC_1000001']">
    User account is visible ["AC_1000001"]
  </Button>
  <Button class="mr-4" v-access:code="['AC_100100', 'AC_1000001']">
    Super & Admin accounts are visible ["AC_100100","AC_1000001"]
  </Button>
</template>
```

### Role

The role judgment method does not require the permission code returned by the interface, and directly determines whether the button is displayed through the role.

#### Component mode

```vue
<script lang="ts" setup>
import { AccessControl } from '@vben/access';
</script>

<template>
  <AccessControl :codes="['super']">
    <Button> Super character is visible </Button>
  </AccessControl>
  <AccessControl :codes="['admin']">
    <Button> Admin role visible </Button>
  </AccessControl>
  <AccessControl :codes="['user']">
    <Button> User role is visible </Button>
  </AccessControl>
  <AccessControl :codes="['super', 'admin']">
    <Button> Super & Admin roles are visible </Button>
  </AccessControl>
</template>
```

#### API method

```vue
<script lang="ts" setup>
import { useAccess } from '@vben/access';

const { hasAccessByRoles } = useAccess();
</script>

<template>
  <Button v-if="hasAccessByRoles(['super'])"> Super account is visible </Button>
  <Button v-if="hasAccessByRoles(['admin'])"> Admin account is visible </Button>
  <Button v-if="hasAccessByRoles(['user'])"> User account is visible </Button>
  <Button v-if="hasAccessByRoles(['super', 'admin'])">
    Super & Admin accounts are visible
  </Button>
</template>
```

#### Command mode

> The command supports binding single or multiple roles. When it is single, you can directly pass in a string or an array containing a role. If multiple roles can be accessed, pass in an array.

```vue
<template>
  <Button class="mr-4" v-access:role="'super'"> Super role is visible </Button>
  <Button class="mr-4" v-access:role="['super']"> Super role is visible </Button>
  <Button class="mr-4" v-access:role="['admin']"> Admin role visible </Button>
  <Button class="mr-4" v-access:role="['user']"> User role is visible </Button>
  <Button class="mr-4" v-access:role="['super', 'admin']">
    Super & Admin roles are visible
  </Button>
</template>
```
