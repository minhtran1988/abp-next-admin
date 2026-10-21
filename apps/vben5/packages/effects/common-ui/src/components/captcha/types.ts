import type { CSSProperties } from 'vue';

import type { ClassType } from '@vben/types';

export interface CaptchaData {
  /**
   * x
   */
  x: number;
  /**
   * y
   */
  y: number;
  /**
   * timestamp
   */
  t: number;
}
export interface CaptchaPoint extends CaptchaData {
  /**
   *Data index
   */
  i: number;
}
export interface PointSelectionCaptchaCardProps {
  /**
   *Verification code picture
   */
  captchaImage: string;
  /**
   * Verification code image height
   * @default '220px'
   */
  height?: number | string;
  /**
   * Horizontal padding
   * @default '12px'
   */
  paddingX?: number | string;
  /**
   * vertical padding
   * @default '16px'
   */
  paddingY?: number | string;
  /**
   * Title
   * @default 'Please click on the pictures one by one'
   */
  title?: string;
  /**
   * Verification code image width
   * @default '300px'
   */
  width?: number | string;
}

export interface PointSelectionCaptchaProps extends PointSelectionCaptchaCardProps {
  /**
   * Whether to display the OK button
   * @default false
   */
  showConfirm?: boolean;
  /**
   * Tip picture
   * @default ''
   */
  hintImage?: string;
  /**
   * Prompt text
   * @default ''
   */
  hintText?: string;
}

export interface SliderCaptchaProps {
  class?: ClassType;
  /**
   * @description slider style
   * @default {}
   */
  actionStyle?: CSSProperties;

  /**
   * @description The style of the slider bar
   * @default {}
   */
  barStyle?: CSSProperties;

  /**
   * @description Content style
   * @default {}
   */
  contentStyle?: CSSProperties;

  /**
   * @description component style
   * @default {}
   */
  wrapperStyle?: CSSProperties;

  /**
   * @description Whether to use it as a slot for linkage components, please refer to the rotation verification component
   * @default false
   */
  isSlot?: boolean;

  /**
   * @description Prompt for successful verification
   * @default 'verification passed'
   */
  successText?: string;

  /**
   * @description prompt text
   * @default 'Please hold down the slider and drag'
   */
  text?: string;
}

export interface SliderRotateCaptchaProps {
  /**
   * @description Angle of rotation
   * @default 20
   */
  diffDegree?: number;

  /**
   * @description The width of the image
   * @default 260
   */
  imageSize?: number;

  /**
   * @description The style of the image
   * @default {}
   */
  imageWrapperStyle?: CSSProperties;

  /**
   * @description maximum rotation angle
   * @default 270
   */
  maxDegree?: number;

  /**
   * @description minimum rotation angle
   * @default 90
   */
  minDegree?: number;

  /**
   * @description The address of the image
   */
  src?: string;
  /**
   * @description default prompt text
   */
  defaultTip?: string;
}

export interface SliderTranslateCaptchaProps {
  /**
   * @description The width of the puzzle
   * @default 420
   */
  canvasWidth?: number;
  /**
   * @description Height of the puzzle
   * @default 280
   */
  canvasHeight?: number;
  /**
   * @description The length of the square on the cut piece
   * @default 42
   */
  squareLength?: number;
  /**
   * @description The radius of the circle on the cut piece
   * @default 10
   */
  circleRadius?: number;
  /**
   * @description The address of the image
   */
  src?: string;
  /**
   * @description The maximum gap allowed
   * @default 3
   */
  diffDistance?: number;
  /**
   * @description default prompt text
   */
  defaultTip?: string;
}

export interface CaptchaVerifyPassingData {
  isPassing: boolean;
  time: number | string;
}

export interface SliderCaptchaActionType {
  resume: () => void;
}

export interface SliderRotateVerifyPassingData {
  event: MouseEvent | TouchEvent;
  moveDistance: number;
  moveX: number;
}
