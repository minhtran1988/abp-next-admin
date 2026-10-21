import type {
  AuthenticatorDto,
  AuthenticatorRecoveryCodeDto,
  ChangePasswordInput,
  ChangePhoneNumberInput,
  ChangePictureInput,
  ConfirmEmailInput,
  ProfileDto,
  SendChangePhoneNumberCodeInput,
  SendEmailConfirmCodeDto,
  TwoFactorEnabledDto,
  UpdateProfileDto,
  VerifyAuthenticatorCodeInput,
} from '../types/profile';

import { useRequest } from '@abp/request';

export function useProfileApi() {
  const { cancel, request } = useRequest();

  /**
   * Query personal information
   * @returns personal information
   */
  function getApi(): Promise<ProfileDto> {
    return request<ProfileDto>('/api/account/my-profile', {
      method: 'GET',
    });
  }

  /**
   * Update personal information
   * @param input parameter
   * @returns personal information
   */
  function updateApi(input: UpdateProfileDto): Promise<ProfileDto> {
    return request<ProfileDto>('/api/account/my-profile', {
      data: input,
      method: 'PUT',
    });
  }

  /**
   * Change password
   * @param input parameter
   */
  function changePasswordApi(input: ChangePasswordInput): Promise<void> {
    return request('/api/account/my-profile/change-password', {
      data: input,
      method: 'POST',
    });
  }

  /**
   *Send verification code to modify mobile phone number
   * @param input parameter
   */
  function sendChangePhoneNumberCodeApi(
    input: SendChangePhoneNumberCodeInput,
  ): Promise<void> {
    return request('/api/account/my-profile/send-phone-number-change-code', {
      data: input,
      method: 'POST',
    });
  }

  /**
   * Modify mobile phone number
   * @param input parameter
   */
  function changePhoneNumberApi(input: ChangePhoneNumberInput): Promise<void> {
    return request('/api/account/my-profile/change-phone-number', {
      data: input,
      method: 'PUT',
    });
  }

  /**
   * Modify avatar
   * @param input parameter
   */
  function changePictureApi(input: ChangePictureInput): Promise<void> {
    return request('/api/account/my-profile/picture', {
      data: input,
      headers: {
        'Content-Type': 'multipart/form-data',
      },
      method: 'POST',
    });
  }

  /**
   * Get avatar
   * @returns avatar file stream
   */
  function getPictureApi(): Promise<Blob> {
    return request('/api/account/my-profile/picture', {
      method: 'GET',
      responseType: 'blob',
    });
  }

  /**
   * Get the two-step authentication activation status
   */
  function getTwoFactorEnabledApi(): Promise<TwoFactorEnabledDto> {
    return request<TwoFactorEnabledDto>('/api/account/my-profile/two-factor', {
      method: 'GET',
    });
  }

  /**
   *Set the secondary authentication enabled status
   */
  function changeTwoFactorEnabledApi(
    input: TwoFactorEnabledDto,
  ): Promise<void> {
    return request('/api/account/my-profile/change-two-factor', {
      data: input,
      method: 'PUT',
    });
  }

  /**
   * Get authenticator configuration
   * @returns authenticator
   */
  function getAuthenticatorApi(): Promise<AuthenticatorDto> {
    return request<AuthenticatorDto>('/api/account/my-profile/authenticator', {
      method: 'GET',
    });
  }

  /**
   * Verify authentication code
   * @param input parameter
   * @returns reset code
   */
  function verifyAuthenticatorCodeApi(
    input: VerifyAuthenticatorCodeInput,
  ): Promise<AuthenticatorRecoveryCodeDto> {
    return request<AuthenticatorRecoveryCodeDto>(
      '/api/account/my-profile/verify-authenticator-code',
      {
        data: input,
        method: 'POST',
      },
    );
  }

  /**
   * Reset validator
   */
  function resetAuthenticatorApi(): Promise<void> {
    return request('/api/account/my-profile/reset-authenticator', {
      method: 'POST',
    });
  }

  /**
   *Send email confirmation link
   * @param input parameter
   */
  function sendEmailConfirmLinkApi(
    input: SendEmailConfirmCodeDto,
  ): Promise<void> {
    return request('/api/account/my-profile/send-email-confirm-link', {
      data: input,
      method: 'POST',
    });
  }

  /**
   * Confirmation email
   * @param input parameter
   */
  function confirmEmailApi(input: ConfirmEmailInput) {
    return request('/api/account/my-profile/confirm-email', {
      data: input,
      method: 'PUT',
    });
  }

  return {
    cancel,
    changePasswordApi,
    changePhoneNumberApi,
    changePictureApi,
    changeTwoFactorEnabledApi,
    confirmEmailApi,
    getApi,
    getAuthenticatorApi,
    getPictureApi,
    getTwoFactorEnabledApi,
    resetAuthenticatorApi,
    sendChangePhoneNumberCodeApi,
    sendEmailConfirmLinkApi,
    updateApi,
    verifyAuthenticatorCodeApi,
  };
}
