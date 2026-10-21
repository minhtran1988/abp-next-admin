<script setup lang="ts">
import { computed } from 'vue';

import { VbenAvatar } from '../avatar';

interface Props {
  /**
   * @zh_CN Whether to close the text
   */
  collapsed?: boolean;
  /**
   * @zh_CN Logo image adaptation method
   */
  fit?: 'contain' | 'cover' | 'fill' | 'none' | 'scale-down';
  /**
   * @zh_CN Logo jump address
   */
  href?: string;
  /**
   * @zh_CN Logo image size
   */
  logoSize?: number;
  /**
   * @zh_CN Logo icon
   */
  src?: string;
  /**
   * @zh_CN Dark theme Logo icon (optional, if not set, use src)
   */
  srcDark?: string;
  /**
   * @zh_CN Logo text
   */
  text: string;
  /**
   * @zh_CN Logo theme
   */
  theme?: string;
}

defineOptions({
  name: 'VbenLogo',
});

const props = withDefaults(defineProps<Props>(), {
  collapsed: false,
  href: 'javascript:void 0',
  logoSize: 32,
  src: '',
  srcDark: '',
  theme: 'light',
  fit: 'cover',
});

/**
 * @zh_CN Choose the appropriate logo icon according to the theme
 */
const logoSrc = computed(() => {
  // If it is a dark theme and srcDark is provided, use the dark theme logo
  if (props.theme === 'dark' && props.srcDark) {
    return props.srcDark;
  }
  // Otherwise use default src
  return props.src;
});
</script>

<template>
  <div :class="theme" class="flex h-full items-center text-lg">
    <a
      :class="$attrs.class"
      :href="href"
      class="flex h-full items-center gap-2 overflow-hidden px-3 text-lg leading-normal transition-all duration-500"
    >
      <VbenAvatar
        v-if="logoSrc"
        :alt="text"
        :src="logoSrc"
        :size="logoSize"
        :fit="fit"
        class="relative rounded-none bg-transparent"
      />
      <template v-if="!collapsed">
        <slot name="text">
          <span class="truncate text-nowrap font-semibold text-foreground">
            {{ text }}
          </span>
        </slot>
      </template>
    </a>
  </div>
</template>
