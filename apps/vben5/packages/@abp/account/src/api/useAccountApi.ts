import type { ListResultDto } from '@abp/core';

import type {
  GetTwoFactorProvidersInput,
  PhoneResetPasswordDto,
  SendEmailSigninCodeDto,
  SendPhoneResetPasswordCodeDto,
  SendPhoneSigninCodeDto,
  TwoFactorProvider,
} from '../types/account';

import { useRequest } from '@abp/request';

export function useAccountApi() {
  const { cancel, request } = useRequest();

  /**
   * Get available two-factor authentication verifiers
   * @param input parameter
   */
  function getTwoFactorProvidersApi(
    input: GetTwoFactorProvidersInput,
  ): Promise<ListResultDto<TwoFactorProvider>> {
    return request<ListResultDto<TwoFactorProvider>>(
      '/api/account/two-factor-providers',
      {
        method: 'GET',
        params: input,
      },
    );
  }

  /**
   *Send login verification email
   * @param input parameter
   */
  function sendEmailSigninCodeApi(
    input: SendEmailSigninCodeDto,
  ): Promise<void> {
    return request('/api/account/email/send-signin-code', {
      data: input,
      method: 'POST',
    });
  }

  /**
   *Send login verification SMS
   * @param input parameter
   */
  function sendPhoneSigninCodeApi(
    input: SendPhoneSigninCodeDto,
  ): Promise<void> {
    return request('/api/account/phone/send-signin-code', {
      data: input,
      method: 'POST',
    });
  }

  /**
   *Send password reset verification SMS
   * @param input parameter
   */
  function sendPhoneResetPasswordCodeApi(
    input: SendPhoneResetPasswordCodeDto,
  ): Promise<void> {
    return request('/api/account/phone/send-password-reset-code', {
      data: input,
      method: 'POST',
    });
  }

  /**
   *Reset password
   * @param input parameter
   */
  function resetPasswordApi(input: PhoneResetPasswordDto): Promise<void> {
    return request('/api/account/phone/reset-password', {
      data: input,
      method: 'PUT',
    });
  }

  return {
    cancel,
    getTwoFactorProvidersApi,
    resetPasswordApi,
    sendEmailSigninCodeApi,
    sendPhoneResetPasswordCodeApi,
    sendPhoneSigninCodeApi,
  };
}
