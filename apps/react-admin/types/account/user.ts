interface UserInfo {
	[property: string]: any;
	/**
	 * Email address
	 */
	email: string;
	/**
	 * Whether the email address has been verified
	 */
	emailVerified: string;
	/**
	 * name
	 */
	givenName: string;
	/**
	 * Username
	 */
	name: string;
	/**
	 * Whether the mobile phone number has been verified
	 */
	phoneNumberVerified: string;
	/**
	 * Username
	 */
	preferredUsername: string;
	/**
	 * Role list
	 */
	role: string[];
	/**
	 *User ID
	 */
	sub: string;
	/**
	 * Username
	 */
	uniqueName: string;
}
/** oauth standard user information structure */
interface OAuthUserInfo {
	[property: string]: any;
	/**
	 * Email address
	 */
	email: string;
	/**
	 * Whether the email address has been verified
	 */
	email_verified: string;
	/**
	 * name
	 */
	given_name: string;
	/**
	 * Username
	 */
	name: string;
	/**
	 * Whether the mobile phone number has been verified
	 */
	phone_number_verified: string;
	/**
	 * Username
	 */
	preferred_username: string;
	/**
	 * Role list
	 */
	role: string[];
	/**
	 *User ID
	 */
	sub: string;
	/**
	 * Username
	 */
	unique_name: string;
}

export type { OAuthUserInfo, UserInfo };
