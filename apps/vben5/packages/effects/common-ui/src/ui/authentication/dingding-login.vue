<script setup lang="ts">
import { useRoute } from 'vue-router';

import { SvgDingDingIcon } from '@vben/icons';
import { $t } from '@vben/locales';

import { alert, useVbenModal } from '@vben-core/popup-ui';
import { VbenIconButton } from '@vben-core/shadcn-ui';
import { loadScript } from '@vben-core/shared/utils';

interface Props {
  clientId: string;
  corpId: string;
  // Login callback address
  redirectUri?: string;
  // Whether to embed QR code to log in
  isQrCode?: boolean;
}

const props = defineProps<Props>();

const route = useRoute();

const [Modal, modalApi] = useVbenModal({
  header: false,
  footer: false,
  fullscreenButton: false,
  class: 'w-[302px] h-[302px] dingding-qrcode-login-modal',
  onOpened() {
    handleQrCodeLogin();
  },
});

const getRedirectUri = () => {
  const { redirectUri } = props;
  if (redirectUri) {
    return redirectUri;
  }
  return window.location.origin + route.fullPath;
};

/**
 * Embedded QR code login
 */
const handleQrCodeLogin = async () => {
  const { clientId, corpId } = props;
  if (!(window as any).DTFrameLogin) {
    // QR code login to load resources
    await loadScript(
      'https://g.alicdn.com/dingding/h5-dingtalk-login/0.21.0/ddlogin.js',
    );
  }
  (window as any).DTFrameLogin(
    {
      id: 'dingding_qrcode_login_element',
      width: 300,
      height: 300,
    },
    {
      // Note: redirect_uri needs to be a complete URL. After scanning the code, DingTalk will jump here with the code.
      redirect_uri: encodeURIComponent(getRedirectUri()),
      client_id: clientId,
      scope: 'openid corpid',
      response_type: 'code',
      state: '1',
      prompt: 'consent',
      corpId,
    },
    (loginResult: any) => {
      const { redirectUrl } = loginResult;
      // You can redirect directly here
      window.location.href = redirectUrl;
    },
    (errorMsg: string) => {
      // Here it is generally necessary to display the specific reasons for login failure.
      alert(`Login Error: ${errorMsg}`);
    },
  );
};

const handleLogin = () => {
  const { clientId, corpId, isQrCode } = props;
  if (isQrCode) {
    // Embedded QR code login
    modalApi.open();
  } else {
    window.location.href = `https://login.dingtalk.com/oauth2/auth?redirect_uri=${encodeURIComponent(getRedirectUri())}&response_type=code&client_id=${clientId}&scope=openid&corpid=${corpId}&prompt=consent`;
  }
};
</script>

<template>
  <div>
    <VbenIconButton
      @click="handleLogin"
      :tooltip="$t('authentication.dingdingLogin')"
      tooltip-side="top"
    >
      <SvgDingDingIcon />
    </VbenIconButton>
    <Modal>
      <div id="dingding_qrcode_login_element"></div>
    </Modal>
  </div>
</template>

<style>
.dingding-qrcode-login-modal {
  .relative {
    padding: 0 !important;
  }
}
</style>
