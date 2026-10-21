namespace LINGYUN.Abp.Identity;

public class IdentityErrorCodes
{
    /// <summary>
    /// Unable to change static declaration type
    /// </summary>
    public const string StaticClaimTypeChange = "Volo.Abp.Identity:020005";
    /// <summary>
    /// Unable to delete statically declared types
    /// </summary>
    public const string StaticClaimTypeDeletion = "Volo.Abp.Identity:020006";
    /// <summary>
    /// Mobile phone number has been used
    /// </summary>
    public const string DuplicatePhoneNumber = "Volo.Abp.Identity:020007";
    /// <summary>
    /// You cannot modify your mobile phone binding information
    /// </summary>
    public const string UsersCanNotChangePhoneNumber = "Volo.Abp.Identity:020008";
    /// <summary>
    /// You cannot modify your email binding information
    /// </summary>
    public const string UsersCanNotChangeEmailAddress = "Volo.Abp.Identity:020009";
    /// <summary>
    /// Repeat the confirmed email address
    /// </summary>
    public const string DuplicateConfirmEmailAddress = "Volo.Abp.Identity:020010";
    /// <summary>
    /// Try to enable two-step authentication when the MFA device is not bound
    /// </summary>
    public const string ChangeTwoFactorWithMFANotBound = "Volo.Abp.Identity:020011";
    /// <summary>
    /// Validator verification is invalid
    /// </summary>
    public const string AuthenticatorTokenInValid = "Volo.Abp.Identity:020012";
    /// <summary>
    /// The password cannot be the same as the password used the last {0} times
    /// </summary>
    public const string PasswordInHistoryInValid = "Volo.Abp.Identity:020013";
    /// <summary>
    /// The associated user Token is invalid
    /// </summary>
    public const string LinkUserTokenInValid = "Volo.Abp.Identity:020014";
    /// <summary>
    /// Disable setting the administrator role as the default role
    /// </summary>
    public const string ProhibitSetDefaultAdminRole = "Volo.Abp.Identity:020015";
}
