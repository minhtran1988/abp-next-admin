import type { ExtensibleObject, IHasConcurrencyStamp } from "#/abp-core";

interface ProfileDto extends ExtensibleObject, IHasConcurrencyStamp {
	/** Email */
	email: string;
	hasPassword: boolean;
	/** Whether it is an external user */
	isExternal: boolean;
	/** name */
	name?: string;
	/** Mobile phone number */
	phoneNumber?: string;
	/** Last name */
	surname?: string;
	/** Username */
	userName: string;
}

interface UpdateProfileDto extends ExtensibleObject, IHasConcurrencyStamp {
	/** Email */
	email: string;
	/** name */
	name?: string;
	/** Mobile phone number */
	phoneNumber?: string;
	/** Last name */
	surname?: string;
	/** Username */
	userName: string;
}

interface ChangePasswordInput {
	/** Current password */
	currentPassword: string;
	/** New password */
	newPassword: string;
}

interface ChangePhoneNumberInput {
	code: string;
	newPhoneNumber: string;
}

interface SendChangePhoneNumberCodeInput {
	newPhoneNumber: string;
}

interface ChangePictureInput {
	file: File;
}

interface TwoFactorEnabledDto {
	/** Whether to enable two-step authentication */
	enabled: boolean;
}

interface AuthenticatorDto {
	authenticatorUri: string;
	isAuthenticated: boolean;
	sharedKey: string;
}

interface VerifyAuthenticatorCodeInput {
	authenticatorCode: string;
}

interface AuthenticatorRecoveryCodeDto {
	recoveryCodes: string[];
}

interface SendEmailConfirmCodeDto {
	appName: string;
	email: string;
	returnUrl?: string;
}

interface ConfirmEmailInput {
	confirmToken: string;
}

export type {
	AuthenticatorDto,
	AuthenticatorRecoveryCodeDto,
	ChangePasswordInput,
	ChangePhoneNumberInput,
	ChangePictureInput,
	ConfirmEmailInput,
	ProfileDto,
	SendChangePhoneNumberCodeInput,
	SendEmailConfirmCodeDto,
	TwoFactorEnabledDto,
	UpdateProfileDto,
	VerifyAuthenticatorCodeInput,
};
