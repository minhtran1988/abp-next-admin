<!--
 Access control component for fine-grained access control.
 TODO: More complete functions can be extended:
 1. Supports multiple permission codes, as long as one permission code is satisfied or multiple permission codes are satisfied.
 2. Support multiple roles, as long as one role is satisfied or multiple roles are satisfied.
 3. Support custom permission codes and role judgment logic
-->
<script lang="ts" setup>
import { computed } from 'vue';

import { useAccess } from './use-access';

interface Props {
  /**
   * Specified codes is visible
   * @default []
   */
  codes?: string[];

  /**
   * How to control the component, if it is role, pass in the role, if it is code, pass in the permission code
   * @default 'role'
   */
  type?: 'code' | 'role';
}

defineOptions({
  name: 'AccessControl',
});

const props = withDefaults(defineProps<Props>(), {
  codes: () => [],
  type: 'role',
});

const { hasAccessByCodes, hasAccessByRoles } = useAccess();

const hasAuth = computed(() => {
  const { codes, type } = props;
  return type === 'role' ? hasAccessByRoles(codes) : hasAccessByCodes(codes);
});
</script>

<template>
  <slot v-if="!codes"></slot>
  <slot v-else-if="hasAuth"></slot>
</template>
