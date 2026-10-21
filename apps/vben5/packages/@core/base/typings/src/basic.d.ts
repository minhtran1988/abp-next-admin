interface BasicOption {
  label: string;
  value: string;
}

type SelectOption = BasicOption;

type TabOption = BasicOption;

interface BasicUserInfo {
  /**
   * Avatar
   */
  avatar: string;
  /**
   * User nickname
   */
  realName: string;
  /**
   * User role
   */
  roles?: string[];
  /**
   * user id
   */
  userId: string;
  /**
   * Username
   */
  username: string;
}

type ClassType = Array<object | string> | object | string;

export type { BasicOption, BasicUserInfo, ClassType, SelectOption, TabOption };
