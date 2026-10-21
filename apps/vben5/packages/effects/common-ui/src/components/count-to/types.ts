import type { CubicBezierPoints, EasingFunction } from '@vueuse/core';

import type { StyleValue } from 'vue';

import { TransitionPresets as TransitionPresetsData } from '@vueuse/core';

export type TransitionPresets = keyof typeof TransitionPresetsData;

export const TransitionPresetsKeys = Object.keys(
  TransitionPresetsData,
) as TransitionPresets[];

export interface CountToProps {
  /** Initial value */
  startVal?: number;
  /** Current value */
  endVal: number;
  /** Whether to disable animation */
  disabled?: boolean;
  /** Delay animation start time */
  delay?: number;
  /** duration */
  duration?: number;
  /** Decimal places */
  decimals?: number;
  /** Decimal point */
  decimal?: string;
  /** delimiter */
  separator?: string;
  /** prefix */
  prefix?: string;
  /** Suffix */
  suffix?: string;
  /** Transition effect */
  transition?: CubicBezierPoints | EasingFunction | TransitionPresets;
  /** Class name of integer part */
  mainClass?: string;
  /** Class name of decimal part */
  decimalClass?: string;
  /** The class name of the prefix part */
  prefixClass?: string;
  /** The class name of the suffix part */
  suffixClass?: string;

  /** Style of integer part */
  mainStyle?: StyleValue;
  /** Style of decimal part */
  decimalStyle?: StyleValue;
  /** Style of prefix part */
  prefixStyle?: StyleValue;
  /** Style of suffix part */
  suffixStyle?: StyleValue;
}
