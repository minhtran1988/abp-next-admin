import type {
  GenerateQrCodeResult,
  QrCodeUserInfoResult,
} from '../types/qrcode';

import { useRequest } from '@abp/request';

export function useScanQrCodeApi() {
  const { cancel, request } = useRequest();

  /**
   * Generate login QR code
   * @returns QR code information
   */
  function generateApi(): Promise<GenerateQrCodeResult> {
    return request<GenerateQrCodeResult>('/api/account/qrcode/generate', {
      method: 'POST',
    });
  }

  /**
   * Check QR code status
   * @param key QR code Key
   * @returns QR code information
   */
  function checkCodeApi(key: string): Promise<QrCodeUserInfoResult> {
    return request<QrCodeUserInfoResult>(`/api/account/qrcode/${key}/check`, {
      method: 'GET',
    });
  }

  return {
    cancel,
    checkCodeApi,
    generateApi,
  };
}
