/** Authorization request data model */
interface TokenRequest {
  /** client id */
  clientId: string;
  /** Client key */
  clientSecret: string;
}
/** User password authorization request data model */
interface PasswordTokenRequest extends TokenRequest {
  /** User password */
  password: string;
  /** Authorization scope */
  scope?: string;
  /** Username */
  userName: string;
}
/** Mobile phone number authorization request data model */
interface PhoneNumberTokenRequest {
  [key: string]: any;
  /** Verification code */
  code: string;
  /** Mobile phone number */
  phoneNumber: string;
}
/** Scan QR code to log in authorization request data model */
interface QrCodeTokenRequest {
  [key: string]: any;
  /** QR code Key */
  key: string;
  /** Tenant ID */
  tenantId?: string;
}
/** User password authorization request data model */
interface PasswordTokenRequestModel {
  [key: string]: any;
  /** User password */
  password: string;
  /** Username */
  username: string;
}
/** Associated user authorization request data model */
interface LinkUserTokenRequest {
  /** Current user access token */
  accessToken?: string;
  /** Associated user tenant ID */
  linkTenantId?: string;
  /** Associated user ID */
  linkUserId: string;
}
/** Simulate user authorization request data model */
interface ImpersonationTokenRequest {
  /** Current user access token */
  accessToken?: string;
  /** Simulate tenant ID */
  tenantId?: string;
  /** Impersonate tenant username */
  tenantUserName?: string;
  /** Delegate user ID */
  userDelegationId?: string;
  /** Simulate user ID */
  userId?: string;
}
/** Token revocation request data type */
interface RevokeTokenRequest {
  /** Token */
  token: string;
  /** Token type */
  tokenType?: 'access_token' | 'refresh_token';
}
/** Token return data model */
interface TokenResult {
  /** Access token */
  accessToken: string;
  /** Expiration time */
  expiresIn: number;
  /** Refresh token */
  refreshToken: string;
  /** Token type */
  tokenType: string;
}
interface OAuthTokenRefreshModel {
  /** Refresh token */
  refreshToken: string;
}
/** oauth standard token return structure */
interface OAuthTokenResult {
  /** Access token */
  access_token: string;
  /** Expiration time */
  expires_in: number;
  /** Refresh token */
  refresh_token: string;
  /** Token type */
  token_type: string;
}

interface OAuthError {
  /** Error type */
  error: string;
  /** Error description */
  error_description: string;
  /** Error description link */
  error_uri?: string;
}

interface TwoFactorError extends OAuthError {
  twoFactorToken: string;
  userId: string;
}

interface ShouldChangePasswordError extends OAuthError {
  changePasswordToken: string;
  userId: string;
}

export type {
  ImpersonationTokenRequest,
  LinkUserTokenRequest,
  OAuthError,
  OAuthTokenRefreshModel,
  OAuthTokenResult,
  PasswordTokenRequest,
  PasswordTokenRequestModel,
  PhoneNumberTokenRequest,
  QrCodeTokenRequest,
  RevokeTokenRequest,
  ShouldChangePasswordError,
  TokenRequest,
  TokenResult,
  TwoFactorError,
};
