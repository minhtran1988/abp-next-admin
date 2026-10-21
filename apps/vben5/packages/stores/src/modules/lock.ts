import { defineStore } from 'pinia';

interface AppState {
  /**
   * Whether the screen is locked
   */
  isLockScreen: boolean;
  /**
   * Lock screen password
   */
  lockScreenPassword?: string;
}

export const useLockStore = defineStore('core-lock', {
  actions: {
    lockScreen(password: string) {
      this.isLockScreen = true;
      this.lockScreenPassword = password;
    },

    unlockScreen() {
      this.isLockScreen = false;
      this.lockScreenPassword = undefined;
    },
  },
  persist: {
    pick: ['isLockScreen', 'lockScreenPassword'],
  },
  state: (): AppState => ({
    isLockScreen: false,
    lockScreenPassword: undefined,
  }),
});
