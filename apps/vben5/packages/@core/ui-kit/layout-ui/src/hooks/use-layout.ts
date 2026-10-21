import type { LayoutType } from '@vben-core/typings';

import type { VbenLayoutProps } from '../vben-layout';

import { computed } from 'vue';

export function useLayout(props: VbenLayoutProps) {
  const currentLayout = computed(() =>
    props.isMobile ? 'sidebar-nav' : (props.layout as LayoutType),
  );

  /**
   * Whether to display content in full screen, no need for side, bottom, top, or tab areas
   */
  const isFullContent = computed(() => currentLayout.value === 'full-content');

  /**
   * Whether to use side blending mode
   */
  const isSidebarMixedNav = computed(
    () => currentLayout.value === 'sidebar-mixed-nav',
  );

  /**
   * Whether it is head navigation mode
   */
  const isHeaderNav = computed(() => currentLayout.value === 'header-nav');

  /**
   * Whether it is mixed navigation mode
   */
  const isMixedNav = computed(
    () =>
      currentLayout.value === 'mixed-nav' ||
      currentLayout.value === 'header-sidebar-nav',
  );

  /**
   * Whether it is the head blending mode
   */
  const isHeaderMixedNav = computed(
    () => currentLayout.value === 'header-mixed-nav',
  );

  return {
    currentLayout,
    isFullContent,
    isHeaderMixedNav,
    isHeaderNav,
    isMixedNav,
    isSidebarMixedNav,
  };
}
