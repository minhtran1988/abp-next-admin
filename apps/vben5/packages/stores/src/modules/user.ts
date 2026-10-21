import { acceptHMRUpdate, defineStore } from 'pinia';

interface BasicUserInfo {
  [key: string]: any;
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

interface AccessState {
  /**
   * User information
   */
  userInfo: BasicUserInfo | null;
  /**
   * User role
   */
  userRoles: string[];
}

/**
 * @zh_CN user information related
 */
export const useUserStore = defineStore('core-user', {
  actions: {
    setUserInfo(userInfo: BasicUserInfo | null) {
      // Set user information
      this.userInfo = userInfo;
      // Set role information
      const roles = userInfo?.roles ?? [];
      this.setUserRoles(roles);
    },
    setUserRoles(roles: string[]) {
      this.userRoles = roles;
    },
  },
  state: (): AccessState => ({
    userInfo: null,
    userRoles: [],
  }),
});

// Solve the hot update problem
const hot = import.meta.hot;
if (hot) {
  hot.accept(acceptHMRUpdate(useUserStore, hot));
}
