interface AuthenticationProps {
  /**
   * @zh_CN Verification code login path
   */
  codeLoginPath?: string;
  /**
   * @zh_CN Forgot password path
   */
  forgetPasswordPath?: string;

  /**
   * @zh_CN Whether it is in loading processing state
   */
  loading?: boolean;

  /**
   * @zh_CN QR code login path
   */
  qrCodeLoginPath?: string;

  /**
   * @zh_CN registration path
   */
  registerPath?: string;

  /**
   * @zh_CN Whether to display verification code to log in
   */
  showCodeLogin?: boolean;
  /**
   * @zh_CN Whether to display forgotten password?
   */
  showForgetPassword?: boolean;

  /**
   * @zh_CN Whether to display QR code to log in
   */
  showQrcodeLogin?: boolean;

  /**
   * @zh_CN Whether to display the registration button
   */
  showRegister?: boolean;

  /**
   * @zh_CN Whether to display remember account
   */
  showRememberMe?: boolean;

  /**
   * @zh_CN Whether to display third-party login
   */
  showThirdPartyLogin?: boolean;

  /**
   * @zh_CN Login box subtitle
   */
  subTitle?: string;

  /**
   * @zh_CN Login box title
   */
  title?: string;
  /**
   * @zh_CN Submit button text
   */
  submitButtonText?: string;
}

export type { AuthenticationProps };
