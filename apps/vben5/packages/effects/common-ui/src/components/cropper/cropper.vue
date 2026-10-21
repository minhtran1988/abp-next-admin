<script setup lang="ts">
import { onMounted, onUnmounted, ref, watch } from 'vue';

// Define component parameters
const props = defineProps<{
  /** Cropping ratio format such as '1:1', '16:9', '3:4', etc. (optional) */
  aspectRatio?: string;
  /** Container height (default 400) */
  height?: number;
  /** Image address */
  img: string;
  /** Container width (default 500) */
  width?: number;
}>();

const CROPPER_CONSTANTS = {
  MIN_WIDTH: 60 as const,
  MIN_HEIGHT: 60 as const,
  DEFAULT_WIDTH: 500 as const,
  DEFAULT_HEIGHT: 400 as const,
  PADDING_RATIO: 0.1 as const,
  MAX_PADDING: 50 as const,
} as const;

type Point = [number, number]; // [clientX, clientY]
type Dimension = [number, number, number, number]; // [top, right, bottom, left]

// Drag point type
type DragAction =
  | 'bottom'
  | 'bottom-left'
  | 'bottom-right'
  | 'left'
  | 'move'
  | 'right'
  | 'top'
  | 'top-left'
  | 'top-right';

// DOM reference
const containerRef = ref<HTMLDivElement | null>(null);
const bgImageRef = ref<HTMLImageElement | null>(null);
// const maskRef = ref<HTMLDivElement | null>(null);
const maskViewRef = ref<HTMLDivElement | null>(null);
const cropperRef = ref<HTMLDivElement | null>(null);
// const cropperViewRef = ref<HTMLDivElement | null>(null);

// responsive data
const isCropperVisible = ref<boolean>(false);
const validAspectRatio = ref<null | number>(null); // Valid ratio value (null means no fixed ratio)
const containerWidth = ref<number>(
  props.width ?? CROPPER_CONSTANTS.DEFAULT_WIDTH,
);
const containerHeight = ref<number>(
  props.height ?? CROPPER_CONSTANTS.DEFAULT_HEIGHT,
);

// Cropping area size (top, right, bottom, left)
const currentDimension = ref<Dimension>([50, 50, 50, 50]);
const initDimension = ref<Dimension>([50, 50, 50, 50]);

// Drag state
const dragging = ref<boolean>(false);
const startPoint = ref<Point>([0, 0]);
const startDimension = ref<Dimension>([0, 0, 0, 0]);
const direction = ref<Dimension>([0, 0, 0, 0]);
const moving = ref<boolean>(false);

/**
 * Calculate the adaptive size of the image to ensure complete display and not exceeding the maximum width and height limit
 */
const calculateImageFitSize = () => {
  if (!bgImageRef.value) return;

  // Get the original size of the image
  const imgWidth = bgImageRef.value.naturalWidth;
  const imgHeight = bgImageRef.value.naturalHeight;

  if (imgWidth === 0 || imgHeight === 0) return;

  // Calculate the scaling ratio (using the incoming width/height, default 500/400)
  const widthRatio =
    (props.width ?? CROPPER_CONSTANTS.DEFAULT_WIDTH) / imgWidth;
  const heightRatio =
    (props.height ?? CROPPER_CONSTANTS.DEFAULT_HEIGHT) / imgHeight;
  const scaleRatio = Math.min(widthRatio, heightRatio, 1); //Do not enlarge the image, only reduce it

  // Calculate the container size after adaptation
  const fitWidth = Math.floor(imgWidth * scaleRatio);
  const fitHeight = Math.floor(imgHeight * scaleRatio);

  containerWidth.value = fitWidth;
  containerHeight.value = fitHeight;

  // Reset the initial size of the cropping box (based on the new container size)
  const padding = Math.min(
    CROPPER_CONSTANTS.MAX_PADDING,
    Math.floor(fitWidth * CROPPER_CONSTANTS.PADDING_RATIO),
    Math.floor(fitHeight * CROPPER_CONSTANTS.PADDING_RATIO),
  );

  initDimension.value = [padding, padding, padding, padding];
  currentDimension.value = [padding, padding, padding, padding];
};

/**
 * Verify and parse the scale string
 * @returns {number|null} proportion value (width/height), null will be returned if parsing fails
 */
const parseAndValidateAspectRatio = (): null | number => {
  // If no scale parameter is passed in, null is returned directly.
  if (!props.aspectRatio) {
    return null;
  }

  // Verify scale format
  const ratioRegex = /^[1-9]\d*:[1-9]\d*$/;
  if (!ratioRegex.test(props.aspectRatio)) {
    console.warn('The cropping ratio format is wrong, it should be in the format of "number:number", such as "16:9"');
    return null;
  }

  // parse ratio
  const [width, height] = props.aspectRatio.split(':').map(Number);

  // Verify the validity of the parsing results
  if (Number.isNaN(width) || Number.isNaN(height) || !width || !height) {
    console.warn('Crop ratio parsing failed, width and height must be positive integers');
    return null;
  }

  return width / height;
};

/**
 * Set crop area size
 * @param {Dimension} dimension - [top, right, bottom, left]
 */
const setDimension = (dimension: Dimension) => {
  currentDimension.value = [...dimension];
  if (maskViewRef.value) {
    maskViewRef.value.style.clipPath = `inset(${dimension[0]}px ${dimension[1]}px ${dimension[2]}px ${dimension[3]}px)`;
  }
};

/**
 * Adjust the cropping area to the specified ratio
 */
const adjustCropperToAspectRatio = () => {
  if (!cropperRef.value) return;

  // Verify and parse the scale
  validAspectRatio.value = parseAndValidateAspectRatio();

  // If there is no valid ratio, use the initial size and do not force a fixed ratio
  if (validAspectRatio.value === null) {
    setDimension(initDimension.value);
    return;
  }

  // If there is a valid ratio, adjust the cropping frame proportionally
  const ratio = validAspectRatio.value;
  const containerWidthVal = containerWidth.value;
  const containerHeightVal = containerHeight.value;

  // Calculate the cropping frame size based on the ratio
  let newHeight: number, newWidth: number;

  // Calculate first by width
  newWidth = containerWidthVal;
  newHeight = newWidth / ratio;

  // If the height exceeds the container, the calculation is based on height priority
  if (newHeight > containerHeightVal) {
    newHeight = containerHeightVal;
    newWidth = newHeight * ratio;
  }

  // Center display
  const leftRight = (containerWidthVal - newWidth) / 2;
  const topBottom = (containerHeightVal - newHeight) / 2;

  const newDimension: Dimension = [topBottom, leftRight, topBottom, leftRight];

  setDimension(newDimension);
};

/**
 * Create a cropper
 */
const createCropper = () => {
  // Calculate image adaptation size
  calculateImageFitSize();

  isCropperVisible.value = true;
  adjustCropperToAspectRatio();
};

/**
 * Handle mouse press event
 * @param {MouseEvent} e - mouse event
 * @param {DragAction} action - action type
 */
const handleMouseDown = (e: MouseEvent, action: DragAction) => {
  dragging.value = true;
  startPoint.value = [e.clientX, e.clientY];
  startDimension.value = [...currentDimension.value];
  direction.value = [0, 0, 0, 0];
  moving.value = false;

  // handle movement
  if (action === 'move') {
    direction.value[0] = 1;
    direction.value[2] = -1;
    direction.value[3] = 1;
    direction.value[1] = -1;
    moving.value = true;
    return;
  }

  // Handle drag direction
  switch (action) {
    case 'bottom': {
      direction.value[2] = -1;
      break;
    }
    case 'bottom-left': {
      direction.value[2] = -1;
      direction.value[3] = 1;
      break;
    }
    case 'bottom-right': {
      direction.value[2] = -1;
      direction.value[1] = -1;
      break;
    }
    case 'left': {
      direction.value[3] = 1;
      break;
    }
    case 'right': {
      direction.value[1] = -1;
      break;
    }
    case 'top': {
      direction.value[0] = 1;
      break;
    }
    case 'top-left': {
      direction.value[0] = 1;
      direction.value[3] = 1;
      break;
    }
    case 'top-right': {
      direction.value[0] = 1;
      direction.value[1] = -1;
      break;
    }
  }
};

/**
 * Handle mouse movement events
 * @param {MouseEvent} e - mouse event
 */
const handleMouseMove = (e: MouseEvent) => {
  if (!dragging.value || !cropperRef.value) return;

  const { clientX, clientY } = e;
  const diffX = clientX - startPoint.value[0];
  const diffY = clientY - startPoint.value[1];

  // Handle moving cropping box
  if (moving.value) {
    handleMoveCropBox(diffX, diffY);
    return;
  }

  // No valid ratio
  if (validAspectRatio.value === null) {
    handleFreeAspectResize(diffX, diffY);
  } else {
    handleFixedAspectResize(diffX, diffY);
  }
};

const handleMoveCropBox = (diffX: number, diffY: number) => {
  const newDimension = [...startDimension.value] as Dimension;

  // Calculate the position after temporary offset
  const tempTop = startDimension.value[0] + diffY;
  const tempLeft = startDimension.value[3] + diffX;

  // Calculate the fixed size of the cropping box
  const cropWidth =
    containerWidth.value - startDimension.value[3] - startDimension.value[1];
  const cropHeight =
    containerHeight.value - startDimension.value[0] - startDimension.value[2];

  // Boundary constraints: ensure that the cropping box is completely within the container and the size remains unchanged
  // Top border: top >= 0, and bottom = container height - top - clipping height >= 0
  newDimension[0] = Math.max(
    0,
    Math.min(tempTop, containerHeight.value - cropHeight),
  );
  // Bottom border: bottom = container height - top - clipping height (derived from top, no additional calculation required)
  newDimension[2] = containerHeight.value - newDimension[0] - cropHeight;
  // Left border: left >= 0, and right = container width - left - clipping width >= 0
  newDimension[3] = Math.max(
    0,
    Math.min(tempLeft, containerWidth.value - cropWidth),
  );
  // Right border: right = container width - left - cropping width (derived from left, no additional calculation required)
  newDimension[1] = containerWidth.value - newDimension[3] - cropWidth;

  // Forced to ensure that the size remains unchanged (cover the bottom line)
  const finalWidth = containerWidth.value - newDimension[3] - newDimension[1];
  const finalHeight = containerHeight.value - newDimension[0] - newDimension[2];

  if (finalWidth !== cropWidth) {
    newDimension[1] = containerWidth.value - newDimension[3] - cropWidth;
  }

  if (finalHeight !== cropHeight) {
    newDimension[2] = containerHeight.value - newDimension[0] - cropHeight;
  }

  // Update the cropping area (only the position changes, the size/proportion does not change at all)
  setDimension(newDimension);
};

const handleFreeAspectResize = (diffX: number, diffY: number) => {
  const cropperWidth = containerWidth.value;
  const cropperHeight = containerHeight.value;
  const currentDimensionNew: Dimension = [0, 0, 0, 0];

  // Calculate the new size to ensure it is not less than the minimum value
  currentDimensionNew[0] = Math.min(
    Math.max(startDimension.value[0] + direction.value[0] * diffY, 0),
    cropperHeight - CROPPER_CONSTANTS.MIN_HEIGHT,
  );

  currentDimensionNew[1] = Math.min(
    Math.max(startDimension.value[1] + direction.value[1] * diffX, 0),
    cropperWidth - CROPPER_CONSTANTS.MIN_WIDTH,
  );

  currentDimensionNew[2] = Math.min(
    Math.max(startDimension.value[2] + direction.value[2] * diffY, 0),
    cropperHeight - CROPPER_CONSTANTS.MIN_HEIGHT,
  );

  currentDimensionNew[3] = Math.min(
    Math.max(startDimension.value[3] + direction.value[3] * diffX, 0),
    cropperWidth - CROPPER_CONSTANTS.MIN_WIDTH,
  );

  // Ensure that the width and height of the crop area are not less than the minimum value
  const newWidth =
    cropperWidth - currentDimensionNew[3] - currentDimensionNew[1];
  const newHeight =
    cropperHeight - currentDimensionNew[0] - currentDimensionNew[2];

  if (newWidth < CROPPER_CONSTANTS.MIN_WIDTH) {
    if (direction.value[3] === 1) {
      currentDimensionNew[3] =
        cropperWidth - currentDimensionNew[1] - CROPPER_CONSTANTS.MIN_WIDTH;
    } else {
      currentDimensionNew[1] =
        cropperWidth - currentDimensionNew[3] - CROPPER_CONSTANTS.MIN_WIDTH;
    }
  }

  if (newHeight < CROPPER_CONSTANTS.MIN_HEIGHT) {
    if (direction.value[0] === 1) {
      currentDimensionNew[0] =
        cropperHeight - currentDimensionNew[2] - CROPPER_CONSTANTS.MIN_HEIGHT;
    } else {
      currentDimensionNew[2] =
        cropperHeight - currentDimensionNew[0] - CROPPER_CONSTANTS.MIN_HEIGHT;
    }
  }

  setDimension(currentDimensionNew);
};

const handleFixedAspectResize = (diffX: number, diffY: number) => {
  if (validAspectRatio.value === null) return;
  const cropperWidth = containerWidth.value;
  const cropperHeight = containerHeight.value;
  // There is a valid ratio - fixed ratio cropping
  const ratio = validAspectRatio.value;
  const currentWidth =
    cropperWidth - startDimension.value[3] - startDimension.value[1];
  const currentHeight =
    cropperHeight - startDimension.value[0] - startDimension.value[2];

  let newHeight: number, newWidth: number;
  let widthChange = 0;
  let heightChange = 0;

  // Calculate the width/height change
  if (direction.value[3] === 1) widthChange = -diffX;
  else if (direction.value[1] === -1) widthChange = diffX;

  if (direction.value[0] === 1) heightChange = -diffY;
  else if (direction.value[2] === -1) heightChange = diffY;

  const isCornerDrag =
    (direction.value[3] === 1 || direction.value[1] === -1) &&
    (direction.value[0] === 1 || direction.value[2] === -1);

  // Calculate new size
  if (isCornerDrag) {
    if (Math.abs(widthChange) > Math.abs(heightChange)) {
      newWidth = Math.max(
        CROPPER_CONSTANTS.MIN_WIDTH,
        currentWidth + widthChange,
      );
      newHeight = newWidth / ratio;
    } else {
      newHeight = Math.max(
        CROPPER_CONSTANTS.MIN_HEIGHT,
        currentHeight + heightChange,
      );
      newWidth = newHeight * ratio;
    }
  } else {
    if (direction.value[3] === 1 || direction.value[1] === -1) {
      newWidth = Math.max(
        CROPPER_CONSTANTS.MIN_WIDTH,
        currentWidth + widthChange,
      );
      newHeight = newWidth / ratio;
    } else {
      newHeight = Math.max(
        CROPPER_CONSTANTS.MIN_HEIGHT,
        currentHeight + heightChange,
      );
      newWidth = newHeight * ratio;
    }
  }

  // Limit the maximum size
  const maxWidth = cropperWidth;
  const maxHeight = cropperHeight;

  if (newWidth > maxWidth) {
    newWidth = maxWidth;
    newHeight = newWidth / ratio;
  }

  if (newHeight > maxHeight) {
    newHeight = maxHeight;
    newWidth = newHeight * ratio;
  }

  // Calculate new position
  let newLeft = startDimension.value[3];
  let newTop = startDimension.value[0];
  let newRight = startDimension.value[1];
  let newBottom = startDimension.value[2];

  // Adjust the position according to the drag direction
  if (direction.value[3] === 1) {
    newLeft = cropperWidth - newWidth - startDimension.value[1];
  } else if (direction.value[1] === -1) {
    newRight = cropperWidth - newWidth - startDimension.value[3];
  } else if (!isCornerDrag) {
    // Center adjustment
    const currentHorizontalCenter = startDimension.value[3] + currentWidth / 2;
    newLeft = Math.max(
      0,
      Math.min(cropperWidth - newWidth, currentHorizontalCenter - newWidth / 2),
    );
    newRight = cropperWidth - newWidth - newLeft;
  }

  if (direction.value[0] === 1) {
    newTop = cropperHeight - newHeight - startDimension.value[2];
  } else if (direction.value[2] === -1) {
    newBottom = cropperHeight - newHeight - startDimension.value[0];
  } else if (!isCornerDrag) {
    // Center adjustment
    const currentVerticalCenter = startDimension.value[0] + currentHeight / 2;
    newTop = Math.max(
      0,
      Math.min(
        cropperHeight - newHeight,
        currentVerticalCenter - newHeight / 2,
      ),
    );
    newBottom = cropperHeight - newHeight - newTop;
  }

  // Bounds check
  newLeft = Math.max(0, newLeft);
  newTop = Math.max(0, newTop);
  newRight = Math.max(0, newRight);
  newBottom = Math.max(0, newBottom);

  const newDimension: Dimension = [newTop, newRight, newBottom, newLeft];
  setDimension(newDimension);
};

/**
 * Handle mouse lift event
 */
const handleMouseUp = () => {
  dragging.value = false;
  moving.value = false;
  direction.value = [0, 0, 0, 0];
};

/**
 * Processing image loading is completed
 */
const handleImageLoad = () => {
  createCropper();
};

/**
 * Crop pictures
 * @param {'image/jpeg' | 'image/png'} format - output image format
 * @param {number} quality - compression quality (0-1)
 * @param {'blob' | 'base64'} outputType - output type
 * @param {number} targetWidth - target width (optional, if not passed, the original crop width will be used)
 * @param {number} targetHeight - target height (optional, if not passed, it will be the original clipping height)
 */
const getCropImage = async (
  format: 'image/jpeg' | 'image/png' = 'image/jpeg',
  quality: number = 0.92,
  outputType: 'base64' | 'blob' = 'blob',
  targetWidth?: number,
  targetHeight?: number,
): Promise<Blob | string | undefined> => {
  if (!props.img || !bgImageRef.value || !containerRef.value) return;

  // Quality parameter boundary correction: forcedly limited to the 0-1 range to prevent errors from being passed in illegal values
  const validQuality = Math.max(0, Math.min(1, quality));

  // Create a temporary image object to obtain the original size
  const tempImg = new Image();
  // Cross-domain image processing: only set cross-domain anonymity for non-original network images
  if (props.img.startsWith('http://') || props.img.startsWith('https://')) {
    try {
      const url = new URL(props.img);
      if (url.origin !== location.origin) {
        tempImg.crossOrigin = 'anonymous';
      }
    } catch {
      // Invalid URL, skip cross-domain configuration, do not interrupt execution
    }
  }

  // Wait for the temporary image to be loaded
  await new Promise<void>((resolve, reject) => {
    const timeout = setTimeout(() => {
      tempImg.removeEventListener('load', handleLoad);
      tempImg.removeEventListener('error', handleError);
      reject(new Error('Image loading timeout, timeout time 10 seconds'));
    }, 10_000);
    const handleLoad = () => {
      clearTimeout(timeout);
      tempImg.removeEventListener('load', handleLoad);
      tempImg.removeEventListener('error', handleError);
      resolve();
    };

    const handleError = (err: ErrorEvent) => {
      clearTimeout(timeout);
      tempImg.removeEventListener('load', handleLoad);
      tempImg.removeEventListener('error', handleError);
      reject(new Error(`Image loading failed: ${err.message}`));
    };

    tempImg.addEventListener('load', handleLoad);
    tempImg.addEventListener('error', handleError);
    tempImg.src = props.img;
  });

  const containerRect = containerRef.value.getBoundingClientRect();
  const imgRect = bgImageRef.value.getBoundingClientRect();

  // 1. Calculate the rendering parameters of the image in the container
  const containerWidth = containerRect.width;
  const containerHeight = containerRect.height;
  const renderedImgWidth = imgRect.width;
  const renderedImgHeight = imgRect.height;
  const imgOffsetX = (containerWidth - renderedImgWidth) / 2;
  const imgOffsetY = (containerHeight - renderedImgHeight) / 2;

  // 2. Calculate the actual coordinates of the cropping box within the container
  const [cropTop, cropRight, cropBottom, cropLeft] = currentDimension.value;
  const cropBoxWidth = containerWidth - cropLeft - cropRight;
  const cropBoxHeight = containerHeight - cropTop - cropBottom;

  // 3. Convert the cropping box coordinates to the coordinates on the picture (considering the picture offset)
  const cropOnImgX = cropLeft - imgOffsetX;
  const cropOnImgY = cropTop - imgOffsetY;

  // 4. Calculate the scaling ratio of the rendered image to the original image (retaining the original pixels)
  const scaleX = tempImg.width / renderedImgWidth;
  const scaleY = tempImg.height / renderedImgHeight;

  // 5. Map to the cropped area of the original image (accurate to the original pixels to prevent cross-border)
  const originalCropX = Math.max(0, Math.floor(cropOnImgX * scaleX));
  const originalCropY = Math.max(0, Math.floor(cropOnImgY * scaleY));
  const originalCropWidth = Math.min(
    Math.floor(cropBoxWidth * scaleX),
    tempImg.width - originalCropX,
  );
  const originalCropHeight = Math.min(
    Math.floor(cropBoxHeight * scaleY),
    tempImg.height - originalCropY,
  );

  // Boundary check: illegal cropping size returns
  if (originalCropWidth <= 0 || originalCropHeight <= 0) return;

  // 6. Handle HD screen adaptation (solve Retina screen blur)
  const dpr = window.devicePixelRatio || 1;

  // Final canvas size (the passed target size is used first, otherwise the original crop size is used)
  const finalWidth = targetWidth ? Math.max(1, targetWidth) : originalCropWidth;
  const finalHeight = targetHeight
    ? Math.max(1, targetHeight)
    : originalCropHeight;

  // Create the canvas and get the drawing context
  const canvas = document.createElement('canvas');
  const ctx = canvas.getContext('2d');
  if (!ctx) return;

  // The physical size of the canvas (multiplied by the device pixel ratio to ensure high definition and no blur)
  canvas.width = finalWidth * dpr;
  canvas.height = finalHeight * dpr;

  // Canvas display size (visual size, consistent with final display)
  canvas.style.width = `${finalWidth}px`;
  canvas.style.height = `${finalHeight}px`;

  // Scale canvas context to adapt to HD screen DPR
  ctx.scale(dpr, dpr);

  // 7. Draw the cropped picture (draw using original pixels to ensure clarity)
  ctx.drawImage(
    tempImg,
    originalCropX, // Original image cropping starting point X (exact pixels)
    originalCropY, // Original image cropping starting Y (exact pixels)
    originalCropWidth, // Original image crop width (exact pixels)
    originalCropHeight, // Original image crop height (exact pixels)
    0, // Canvas drawing starting point X
    0, // Canvas drawing starting Y
    finalWidth, // Canvas drawing width (target size)
    finalHeight, // Canvas drawing height (target size)
  );

  try {
    return outputType === 'base64'
      ? canvas.toDataURL(format, validQuality)
      : new Promise<Blob>((resolve) => {
          canvas.toBlob(
            (blob) => {
              // Bottom line: If blob generation fails, return an empty blob (to prevent null)
              resolve(blob || new Blob([], { type: format }));
            },
            format,
            validQuality,
          );
        });
  } catch (error) {
    console.error('Picture export failed:', error);
  }
};

// Monitor the proportion changes and re-adjust the cropping frame
watch(() => props.aspectRatio, adjustCropperToAspectRatio);

// Monitor width/height changes and recalculate the size
watch([() => props.width, () => props.height], () => {
  calculateImageFitSize();
  adjustCropperToAspectRatio();
});

// Register global events when the component is mounted
onMounted(() => {
  document.addEventListener('mousemove', handleMouseMove);
  document.addEventListener('mouseup', handleMouseUp);

  // If the image has been loaded, manually trigger the creation of the cropper
  if (
    bgImageRef.value &&
    bgImageRef.value.complete &&
    bgImageRef.value.naturalWidth > 0
  ) {
    createCropper();
  }
});

// Clean up when component is uninstalled
onUnmounted(() => {
  document.removeEventListener('mousemove', handleMouseMove);
  document.removeEventListener('mouseup', handleMouseUp);
});

defineExpose({ getCropImage });
</script>

<template>
  <div
    :style="{
      width: `${width || CROPPER_CONSTANTS.DEFAULT_WIDTH}px`,
      height: `${height || CROPPER_CONSTANTS.DEFAULT_HEIGHT}px`,
    }"
    class="cropper-action-wrapper"
  >
    <div
      ref="containerRef"
      class="cropper-container"
      :style="{
        width: `${containerWidth}px`,
        height: `${containerHeight}px`,
      }"
    >
      <!-- Original image display - adaptive size -->
      <img
        ref="bgImageRef"
        class="cropper-image"
        :src="img"
        @load="handleImageLoad"
        :style="{
          maxWidth: '100%',
          maxHeight: '100%',
          objectFit: 'contain',
        }"
        alt="Crop original image"
      />

      <!-- Mask layer -->
      <div
        class="cropper-mask"
        :style="{
          display: isCropperVisible ? 'block' : 'none',
          width: '100%',
          height: '100%',
        }"
      >
        <div
          ref="maskViewRef"
          class="cropper-mask-view"
          :style="{
            backgroundImage: `url(${img})`,
            backgroundSize: 'contain',
            backgroundPosition: 'center',
            backgroundRepeat: 'no-repeat',
            clipPath: `inset(${currentDimension[0]}px ${currentDimension[1]}px ${currentDimension[2]}px ${currentDimension[3]}px)`,
            width: '100%',
            height: '100%',
          }"
        ></div>
      </div>

      <!-- Cropping box -->
      <div
        ref="cropperRef"
        class="cropper-box"
        :style="{
          display: isCropperVisible ? 'block' : 'none',
          width: '100%',
          height: '100%',
        }"
      >
        <div
          class="cropper-view"
          :style="{
            inset: `${currentDimension[0]}px ${currentDimension[1]}px ${currentDimension[2]}px ${currentDimension[3]}px`,
          }"
        >
          <!-- Cropping frame auxiliary lines -->
          <span class="cropper-dashed-h"></span>
          <span class="cropper-dashed-v"></span>

          <!-- Cropping frame drag area -->
          <span
            class="cropper-move-area"
            @mousedown="handleMouseDown($event, 'move')"
          ></span>

          <!-- Border line -->
          <span class="cropper-line-e"></span>
          <span class="cropper-line-n"></span>
          <span class="cropper-line-w"></span>
          <span class="cropper-line-s"></span>

          <!-- Corner drag point -->
          <span
            class="cropper-point cropper-point-ne"
            @mousedown="handleMouseDown($event, 'top-right')"
          >
            <span class="cropper-point-inner"></span>
          </span>
          <span
            class="cropper-point cropper-point-nw"
            @mousedown="handleMouseDown($event, 'top-left')"
          >
            <span class="cropper-point-inner"></span>
          </span>
          <span
            class="cropper-point cropper-point-sw"
            @mousedown="handleMouseDown($event, 'bottom-left')"
          >
            <span class="cropper-point-inner"></span>
          </span>
          <span
            class="cropper-point cropper-point-se"
            @mousedown="handleMouseDown($event, 'bottom-right')"
          >
            <span class="cropper-point-inner"></span>
          </span>

          <!-- Edge midpoint drag point -->
          <span
            class="cropper-point cropper-point-e"
            @mousedown="handleMouseDown($event, 'right')"
          >
            <span class="cropper-point-inner"></span>
          </span>
          <span
            class="cropper-point cropper-point-n"
            @mousedown="handleMouseDown($event, 'top')"
          >
            <span class="cropper-point-inner"></span>
          </span>
          <span
            class="cropper-point cropper-point-w"
            @mousedown="handleMouseDown($event, 'left')"
          >
            <span class="cropper-point-inner"></span>
          </span>
          <span
            class="cropper-point cropper-point-s"
            @mousedown="handleMouseDown($event, 'bottom')"
          >
            <span class="cropper-point-inner"></span>
          </span>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.cropper-action-wrapper {
  @apply box-border flex items-center justify-center;

  background-color: transparent;

  /* Mosaic background */
  background-image:
    linear-gradient(45deg, #ccc 25%, transparent 25%),
    linear-gradient(-45deg, #ccc 25%, transparent 25%),
    linear-gradient(45deg, transparent 75%, #ccc 75%),
    linear-gradient(-45deg, transparent 75%, #ccc 75%);
  background-position:
    0 0,
    0 10px,
    10px -10px,
    -10px 0;
  background-size: 20px 20px;
}

.cropper-container {
  @apply relative;
}

.cropper-image {
  @apply block;
}

/* Mask layer */
.cropper-mask {
  @apply absolute left-0 top-0 bg-black/50;
}

.cropper-mask-view {
  @apply absolute left-0 top-0;
}

/* Cropping frame */
.cropper-box {
  @apply absolute left-0 top-0 z-10;
}

.cropper-view {
  @apply absolute bottom-0 left-0 right-0 top-0 select-none outline outline-1 outline-blue-500;
}

/* Cropping frame auxiliary lines */
.cropper-dashed-h {
  @apply absolute left-0 top-1/3 block h-1/3 w-full border-b border-t border-dashed border-gray-200/50;
}

.cropper-dashed-v {
  @apply absolute left-1/3 top-0 block h-full w-1/3 border-l border-r border-dashed border-gray-200/50;
}

/* Cropping frame drag area */
.cropper-move-area {
  @apply absolute left-0 top-0 block h-full w-full cursor-move bg-white/10;
}

/* Border drag line */
.cropper-line-e,
.cropper-line-n,
.cropper-line-w,
.cropper-line-s {
  @apply absolute block bg-blue-500/10;
}

.cropper-line-e {
  @apply right-[-3px] top-0 h-full w-1;
}

.cropper-line-n {
  @apply left-0 top-[-3px] h-1 w-full;
}

.cropper-line-w {
  @apply left-[-3px] top-0 h-full w-1;
}

.cropper-line-s {
  @apply bottom-[-3px] left-0 h-1 w-full;
}

/* Drag point */
.cropper-point {
  @apply absolute flex h-2 w-2 items-center justify-center bg-blue-500;
}

.cropper-point-inner {
  @apply block h-1.5 w-1.5 bg-white;
}

/* Corner drag point position and cursor */
.cropper-point-ne {
  @apply right-[-5px] top-[-5px] cursor-ne-resize;
}

.cropper-point-nw {
  @apply left-[-5px] top-[-5px] cursor-nw-resize;
}

.cropper-point-sw {
  @apply bottom-[-5px] left-[-5px] cursor-sw-resize;
}

.cropper-point-se {
  @apply bottom-[-5px] right-[-5px] cursor-se-resize;
}

/* Edge midpoint drag point position and cursor */
.cropper-point-e {
  @apply right-[-5px] top-1/2 -mt-1 cursor-e-resize;
}

.cropper-point-n {
  @apply left-1/2 top-[-5px] -ml-1 cursor-n-resize;
}

.cropper-point-w {
  @apply left-[-5px] top-1/2 -mt-1 cursor-w-resize;
}

.cropper-point-s {
  @apply bottom-[-5px] left-1/2 -ml-1 cursor-s-resize;
}
</style>
