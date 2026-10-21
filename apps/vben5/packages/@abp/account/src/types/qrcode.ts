export enum QrCodeStatus {
  /** Confirmed */
  Confirmed = 10,
  /** Create */
  Created = 0,
  /** Invalid */
  Invalid = -1,
  /** Scanned */
  Scaned = 5,
}

interface GenerateQrCodeResult {
  key: string;
}

interface QrCodeInfoResult {
  key: string;
  status: QrCodeStatus;
}

interface QrCodeUserInfoResult extends QrCodeInfoResult {
  picture?: string;
  tenantId?: string;
  userId?: string;
  userName?: string;
}

export type { GenerateQrCodeResult, QrCodeUserInfoResult };
