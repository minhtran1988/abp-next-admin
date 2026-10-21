import type { BasicUserInfo } from '@vben-core/typings';

/** User information */
interface UserInfo extends BasicUserInfo {
  /**
   * User description
   */
  desc: string;
  /**
   * Home page address
   */
  homePath: string;

  /**
   * accessToken
   */
  token: string;
}

export type { UserInfo };
