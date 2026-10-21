<template>
  <transition>
    <div :class="prefixCls">
      <Login sessionTimeout />
    </div>
  </transition>
</template>
<script lang="ts" setup>
  import { onBeforeUnmount, onMounted, ref } from 'vue';
  import Login from './Login.vue';
  import { useRoute } from 'vue-router';
  import { useGo } from '/@/hooks/web/usePage';
  import { useDesign } from '/@/hooks/web/useDesign';
  import { useUserStore } from '/@/store/modules/user';
  import { usePermissionStore } from '/@/store/modules/permission';
  import { useAppStore } from '/@/store/modules/app';
  import { PermissionModeEnum } from '/@/enums/appEnum';

  const { prefixCls } = useDesign('st-login');
  const go = useGo();
  const { path } = useRoute();
  const userStore = useUserStore();
  const permissionStore = usePermissionStore();
  const appStore = useAppStore();
  const userId = ref<Nullable<number | string>>(0);

  const isBackMode = () => {
    return appStore.getProjectConfig.permissionMode === PermissionModeEnum.BACK;
  };

  onMounted(() => {
    // Record the current UserId
    userId.value = userStore.getUserInfo?.userId;
    console.log('Mounted', userStore.getUserInfo);
    go(`/login?redirect=${path}`);
  });

  onBeforeUnmount(() => {
    if (userId.value && userId.value !== userStore.getUserInfo.userId) {
      // If the user is not logged in, refresh the entire page to discard the previous user's page status.
      document.location.reload();
    } else if (isBackMode() && permissionStore.getLastBuildMenuTime === 0) {
      // In background permission mode, if the menu is not successfully loaded, the entire page will be reloaded. This usually occurs when the module is loaded after pressing F5 to refresh the entire page after the session has expired.
      document.location.reload();
    }
  });
</script>
<style lang="less" scoped>
  @prefix-cls: ~'@{namespace}-st-login';

  .@{prefix-cls} {
    position: fixed;
    width: 100%;
    height: 100%;
    background: @component-background;
  }
</style>
