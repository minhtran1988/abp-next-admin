<script lang="ts" setup>
import { useVbenModal, VbenButton } from '@vben/common-ui';

const [SlideModal, slideModalApi] = useVbenModal({
  animationType: 'slide',
});

const [ScaleModal, scaleModalApi] = useVbenModal({
  animationType: 'scale',
});

function openSlideModal() {
  slideModalApi.open();
}

function openScaleModal() {
  scaleModalApi.open();
}
</script>

<template>
  <div class="space-y-4">
    <div class="flex gap-4">
      <VbenButton @click="openSlideModal">Sliding animation</VbenButton>
      <VbenButton @click="openScaleModal">Scale animation</VbenButton>
    </div>

    <SlideModal title="Sliding animation example" class="w-[500px]">
      <p>This is a pop-up window that uses sliding animation, slide down from the top to enter. </p>
    </SlideModal>

    <ScaleModal title="Scale animation example" class="w-[500px]">
      <p>This is a pop-up window that uses zoom animation, displayed in a zoom-fade manner. </p>
    </ScaleModal>
  </div>
</template>
