using LINGYUN.Abp.Aliyun.Settings;

namespace LINGYUN.Abp.Aliyun.Captcha.Settings;

public static class AliyunCaptchaSettingNames
{
    public const string CaptchaPrefix = AliyunSettingNames.Prefix + ".Captcha";
    /// <summary>
    /// The unique identifier of the verification code scenario
    /// </summary>
    public const string SceneId = CaptchaPrefix + ".SceneId";
    /// <summary>
    /// The identity obtained by the console
    /// </summary>
    public const string Prefix = CaptchaPrefix + ".Prefix";
    /// <summary>
    /// ekey obtained by console
    /// </summary>
    public const string EKey = CaptchaPrefix + ".EKey";
    /// <summary>
    /// Use encrypted verification code scene identification
    /// </summary>
    public const string UseEncryptedSceneId = CaptchaPrefix + ".UseEncryptedSceneId";
    /// <summary>
    /// Ciphertext expiration time, unit seconds, range 1~86400
    /// </summary>
    public const string EncryptedExpireTimeSec = CaptchaPrefix + ".EncryptedExpireTimeSec";
}
