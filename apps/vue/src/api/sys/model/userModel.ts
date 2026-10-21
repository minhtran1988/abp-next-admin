/**
 * @description: Login interface parameters
 */
export interface LoginParams {
  username: string;
  password: string;
  twoFactorProvider?: string;
  twoFactorCode?: string;
  enterpriseId?: string;
}

export interface LoginByPhoneParams {
  phoneNumber: string;
  code: string;
}

export interface RoleInfo {
  roleName: string;
  value: string;
}

/**
 * @description: Login interface return value
 */
export interface LoginResultModel {
  /** Access token */
  access_token: string;
  /** Expiration time */
  expires_in: number;
  /** Token type */
  token_type: string;
  /** Refresh token */
  refresh_token: string;
}

export interface PortalLoginModel {
  id: string;
  name: string;
  logo?: string;
}

/**
 * @description: Get user information return value
 */
export interface GetUserInfoModel {
  [key: string]: string;
}
