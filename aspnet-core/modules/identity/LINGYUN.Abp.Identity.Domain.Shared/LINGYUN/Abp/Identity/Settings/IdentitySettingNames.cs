namespace LINGYUN.Abp.Identity.Settings;

public static class IdentitySettingNames
{
    private const string Prefix = "Abp.Identity";
    public static class User
    {
        private const string UserPrefix = Prefix + ".User";
        /// <summary>
        /// User mobile phone verification SMS template
        /// </summary>
        public const string SmsPhoneNumberConfirmed = UserPrefix + ".SmsPhoneNumberConfirmed";
        /// <summary>
        /// User registration SMS verification code template number
        /// </summary>
        public const string SmsNewUserRegister = UserPrefix + ".SmsNewUserRegister";
        /// <summary>
        /// User login SMS verification code template number
        /// </summary>
        public const string SmsUserSignin = UserPrefix + ".SmsUserSignin";
        /// <summary>
        /// User reset password SMS verification code template number
        /// </summary>
        public const string SmsResetPassword = UserPrefix + ".SmsResetPassword";
        /// <summary>
        /// SMS verification code repetition interval
        /// </summary>
        public const string SmsRepetInterval = UserPrefix + ".SmsRepetInterval";
        /// <summary>
        /// Email registration verification code repetition interval
        /// </summary>
        public const string EmailRegisterRepetInterval = UserPrefix + ".EmailRegisterRepetInterval";
    }

    public static class Session
    {
        private const string SessionPrefix = Prefix + ".Session";
        /// <summary>
        /// Concurrent login strategy
        /// see: https://github.com/abpio/abp-commercial-docs/blob/dev/en/modules/identity/session-management.md#prevent-concurrent-login
        /// </summary>
        public const string ConcurrentLoginStrategy = SessionPrefix + ".ConcurrentLoginStrategy";
        /// <summary>
        /// Limit the number of logins from the same device
        /// </summary>
        public const string LogoutFromSameTypeDevicesLimit = SessionPrefix + ".LogoutFromSameTypeDevicesLimit";
    }

    public static class Security
    {
        private const string SecurityPrefix = Prefix + ".Security";

        public const string TwoFactorBehaviour = SecurityPrefix + ".TwoFactorBehaviour";

        public const string UsersCanChangeTwoFactor = SecurityPrefix + ".UsersCanChangeTwoFactor";

        public const string AdminRoleTwoFactorForceEnabled = SecurityPrefix + ".AdminRoleTwoFactorForceEnabled";

        public const string ProhibitDefaultAdminRole = SecurityPrefix + ".ProhibitDefaultAdminRole";

        public const string UseCaptcha = SecurityPrefix + ".ProhibitDefaultAdminRole";
    }

    public static class SignIn
    {
        private const string SignInPrefix = "Abp.Identity.SignIn";

        public const string RequireCaptchaVerification = SignInPrefix + ".RequireCaptchaVerification";

        public const string CaptchaComponent = SignInPrefix + ".CaptchaComponent";
    }

    public static class Link
    {
        private const string LinkPrefix = Prefix + ".Link";
        /// <summary>
        /// User login page (contains the login address string of the protocol, used to send the link in the user login email)
        /// </summary>
        public const string UserLoginUri = LinkPrefix + ".UserLoginUri";
    }
}
