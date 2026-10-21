<!-- This component is used to obtain the cached route and save it to pinia -->
<script setup lang="ts">
import type { VNode } from 'vue';
import type { RouteLocationNormalizedLoadedGeneric } from 'vue-router';

import { watch } from 'vue';

import { useTabbarStore } from '@vben/stores';

interface Props {
  component?: VNode;
  route: RouteLocationNormalizedLoadedGeneric;
}

/**
 * This is a page cache component that does not do any actual rendering.
 */
defineOptions({
  render() {
    return null;
  },
});
const props = defineProps<Props>();

const { addCachedRoute } = useTabbarStore();

watch(
  () => props.route,
  () => {
    if (props.component && props.route.meta.domCached) {
      addCachedRoute(props.component, props.route);
    }
  },
  { immediate: true },
);
</script>
