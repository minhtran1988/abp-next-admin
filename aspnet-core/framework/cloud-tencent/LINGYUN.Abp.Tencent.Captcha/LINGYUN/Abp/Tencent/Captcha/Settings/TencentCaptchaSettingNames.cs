using LINGYUN.Abp.Tencent.Settings;

namespace LINGYUN.Abp.Tencent.Captcha.Settings;

public static class TencentCaptchaSettingNames
{
    private const string Prefix = TencentCloudSettingNames.Prefix + ".Captcha";
    /// <summary>
    /// Verification code business ID
    /// </summary>
    public const string CaptchaAppId = Prefix + ".CaptchaAppId";
    /// <summary>
    /// Original key
    /// </summary>
    public const string AppSecretKey = Prefix + ".AppSecretKey";
    /// <summary>
    /// Encryption method
    /// </summary>
    public const string CaptchaAppIdEncryptedType = Prefix + ".CaptchaAppIdEncryptedType";
    /// <summary>
    /// Expiration time (seconds), maximum value 86400 seconds
    /// </summary>
    public const string CaptchaAppIdEncryptedExpireTime = Prefix + ".CaptchaAppIdEncryptedExpireTime";
}
