import type { OAuthError } from '../types/token';

import { $t } from '@vben/locales';

export function useOAuthError() {
  function formatError(error: OAuthError) {
    switch (error.error_description) {
      // Invalid username or password
      case 'Invalid username or password!': {
        return $t('abp.oauth.errors.invalidUserNameOrPassword');
      }
      // Need to change password
      case 'PeriodicallyChangePassword':
      case 'ShouldChangePasswordOnNextLogin': {
        return $t('abp.oauth.errors.shouldChangePassword');
      }
      // requires secondary authentication
      case 'RequiresTwoFactor': {
        return $t('abp.oauth.errors.requiresTwoFactor');
      }
      // User refuses authorization
      case 'The authorization was denied by the end user.': {
        return $t('abp.oauth.errors.accessDenied');
      }
      // Token has expired
      case 'The token is no longer valid.':
      case 'The user is no longer allowed to sign in.': {
        return $t('abp.oauth.errors.tokenHasExpired');
      }
      // The user attempts to log in too many times and the user is locked out
      case 'The user account has been locked out due to invalid login attempts. Please wait a while and try again.': {
        return $t('abp.oauth.errors.accountLockedByInvalidLoginAttempts');
      }
      // User is not enabled
      case 'You are not allowed to login! Your account is inactive.': {
        return $t('abp.oauth.errors.accountInactive');
      }
      // Other uncommon error messages return the original string
      default: {
        return error.error_description;
      }
    }
  }

  return {
    formatError,
  };
}
