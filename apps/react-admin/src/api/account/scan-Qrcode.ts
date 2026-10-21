import type { GenerateQrCodeResult, QrCodeUserInfoResult } from "#/account/qrcode";

import requestClient from "@/api/request";

/**
 * Generate login QR code
 * @returns QR code information
 */
export function generateApi(): Promise<GenerateQrCodeResult> {
	return requestClient.post<GenerateQrCodeResult>("/api/account/qrcode/generate");
}

/**
 * Check QR code status
 * @param key QR code Key
 * @returns QR code information
 */
export function checkCodeApi(key: string): Promise<QrCodeUserInfoResult> {
	return requestClient.get<QrCodeUserInfoResult>(`/api/account/qrcode/${key}/check`);
}
