<script setup lang="ts">
import type { CSSProperties } from 'vue';

import {
  computed,
  onBeforeUnmount,
  onMounted,
  onUpdated,
  ref,
  watchEffect,
} from 'vue';

import { VbenTooltip } from '@vben-core/shadcn-ui';

import { useElementSize } from '@vueuse/core';

interface Props {
  /**
   * Whether to enable clicking on text to expand all
   * @default false
   */
  expand?: boolean;
  /**
   * Maximum number of lines of text
   * @default 1
   */
  line?: number;
  /**
   *Maximum width of text
   * @default '100%'
   */
  maxWidth?: number | string;
  /**
   * Prompt box position
   * @default 'top'
   */
  placement?: 'bottom' | 'left' | 'right' | 'top';
  /**
   * Whether to enable text prompt box
   * @default true
   */
  tooltip?: boolean;
  /**
   * Whether to only display the prompt box when the text is truncated
   * @default false
   */
  tooltipWhenEllipsis?: boolean;
  /**
   * Pixel difference threshold for text truncation detection. The larger the value, the stricter the judgment.
   * @default 3
   */
  ellipsisThreshold?: number;
  /**
   * Prompt box background color, priority is higher than overlayStyle
   */
  tooltipBackgroundColor?: string;
  /**
   * Prompt text font color, priority is higher than overlayStyle
   */
  tooltipColor?: string;
  /**
   * Prompt text font size, unit px, priority is higher than overlayStyle
   */
  tooltipFontSize?: number;
  /**
   * The maximum width of the prompt box content, in px. When not set by default, the prompt text content will automatically be consistent with the display text width.
   */
  tooltipMaxWidth?: number;
  /**
   * Prompt box content area style
   * @default { textAlign: 'justify' }
   */
  tooltipOverlayStyle?: CSSProperties;
}

const props = withDefaults(defineProps<Props>(), {
  expand: false,
  line: 1,
  maxWidth: '100%',
  placement: 'top',
  tooltip: true,
  tooltipWhenEllipsis: false,
  ellipsisThreshold: 3,
  tooltipBackgroundColor: '',
  tooltipColor: '',
  tooltipFontSize: 14,
  tooltipMaxWidth: undefined,
  tooltipOverlayStyle: () => ({ textAlign: 'justify' }),
});

const emit = defineEmits<{ expandChange: [boolean] }>();

const textMaxWidth = computed(() => {
  if (typeof props.maxWidth === 'number') {
    return `${props.maxWidth}px`;
  }
  return props.maxWidth;
});
const ellipsis = ref();
const isExpand = ref(false);
const defaultTooltipMaxWidth = ref();
const isEllipsis = ref(false);

const { width: eleWidth } = useElementSize(ellipsis);

// Check if the text is truncated
const checkEllipsis = () => {
  if (!ellipsis.value || !props.tooltipWhenEllipsis) return;

  const element = ellipsis.value;

  const originalText = element.textContent || '';
  const originalTrimmed = originalText.trim();

  // Return false directly for empty text
  if (!originalTrimmed) {
    isEllipsis.value = false;
    return;
  }

  const widthDiff = element.scrollWidth - element.clientWidth;
  const heightDiff = element.scrollHeight - element.clientHeight;

  // Use a large enough difference threshold to ensure that only truly truncated text is displayed in the tooltip
  isEllipsis.value =
    props.line === 1
      ? widthDiff > props.ellipsisThreshold
      : heightDiff > props.ellipsisThreshold;
};

// Use ResizeObserver to monitor size changes
let resizeObserver: null | ResizeObserver = null;

onMounted(() => {
  if (typeof ResizeObserver !== 'undefined' && props.tooltipWhenEllipsis) {
    resizeObserver = new ResizeObserver(() => {
      checkEllipsis();
    });

    if (ellipsis.value) {
      resizeObserver.observe(ellipsis.value);
    }
  }

  // Initial detection
  checkEllipsis();
});

// Use onUpdated hook to detect content changes
onUpdated(() => {
  if (props.tooltipWhenEllipsis) {
    checkEllipsis();
  }
});

onBeforeUnmount(() => {
  if (resizeObserver) {
    resizeObserver.disconnect();
    resizeObserver = null;
  }
});

watchEffect(
  () => {
    if (props.tooltip && eleWidth.value) {
      defaultTooltipMaxWidth.value =
        props.tooltipMaxWidth ?? eleWidth.value + 24;
    }
  },
  { flush: 'post' },
);

function onExpand() {
  isExpand.value = !isExpand.value;
  emit('expandChange', isExpand.value);
  if (props.tooltipWhenEllipsis) {
    checkEllipsis();
  }
}

function handleExpand() {
  props.expand && onExpand();
}
</script>
<template>
  <div>
    <VbenTooltip
      :content-style="{
        ...tooltipOverlayStyle,
        maxWidth: `${defaultTooltipMaxWidth}px`,
        fontSize: `${tooltipFontSize}px`,
        color: tooltipColor,
        backgroundColor: tooltipBackgroundColor,
      }"
      :disabled="
        !props.tooltip || isExpand || (props.tooltipWhenEllipsis && !isEllipsis)
      "
      :side="placement"
    >
      <slot name="tooltip">
        <slot></slot>
      </slot>

      <template #trigger>
        <div
          ref="ellipsis"
          :class="{
            '!cursor-pointer': expand,
            ['block truncate']: line === 1,
            [$style.ellipsisMultiLine]: line > 1,
          }"
          :style="{
            '-webkit-line-clamp': isExpand ? '' : line,
            'max-width': textMaxWidth,
          }"
          class="cursor-text overflow-hidden"
          @click="handleExpand"
          v-bind="$attrs"
        >
          <slot></slot>
        </div>
      </template>
    </VbenTooltip>
  </div>
</template>

<style module>
.ellipsisMultiLine {
  display: -webkit-box;
  -webkit-box-orient: vertical;
}
</style>
