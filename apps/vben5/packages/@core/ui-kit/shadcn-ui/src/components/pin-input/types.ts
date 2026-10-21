interface PinInputProps {
  class?: any;
  /**
   * Verification code length
   */
  codeLength?: number;
  /**
   * Send verification code button text
   */
  createText?: (countdown: number) => string;
  /**
   * Whether to disable
   */
  disabled?: boolean;
  /**
   * Customize verification code sending logic
   * @returns
   */
  handleSendCode?: () => Promise<void>;
  /**
   * Send verification code button loading
   */
  loading?: boolean;
  /**
   * Maximum retry time
   */
  maxTime?: number;
}

export type { PinInputProps };
