namespace LINGYUN.Abp.Tencent.Settings;

public static class TencentCloudSettingNames
{
    public const string Prefix = "Abp.TencentCloud";
    /// <summary>
    /// SecretId
    /// </summary>
    public const string SecretId = Prefix + ".SecretId";
    /// <summary>
    /// SecretKey
    /// </summary>
    public const string SecretKey = Prefix + ".SecretKey";
    /// <summary>
    /// Connect region
    /// </summary>
    public const string EndPoint = Prefix + ".EndPoint";
    /// <summary>
    /// Session duration
    /// </summary>
    public const string DurationSecond = Prefix + ".DurationSecond";
    /// <summary>
    /// Authentication method
    /// </summary>
    public class Authorization
    {
        public const string Prefix = TencentCloudSettingNames.Prefix + ".Authorization";
        /// <summary>
        /// Region ID
        /// </summary>
        public const string RegionId = Prefix + ".RegionId";
        /// <summary>
        /// AccessKey ID of RAM account
        /// </summary>
        public const string AccessKeyId = Prefix + ".AccessKeyId";
        /// <summary>
        /// AccessKey Secret of RAM account
        /// </summary>
        public const string AccessKeySecret = Prefix + ".AccessKeySecret";
        /// <summary>
        /// Use STS Token to access
        /// </summary>
        public const string UseSecurityTokenService = Prefix + ".UseSecurityTokenService";
        /// <summary>
        /// Access using AssumeRole method of RAM sub-account
        /// </summary>
        public const string RamRoleArn = Prefix + ".RamRoleArn";
        /// <summary>
        /// User-defined parameters. This parameter is used to distinguish different tokens and can be used for user-level access auditing.
        /// </summary>
        public const string RoleSessionName = Prefix + ".RoleSessionName";
        /// <summary>
        /// Expiration time, unit is seconds.
        /// </summary>
        public const string DurationSeconds = Prefix + ".DurationSeconds";
        /// <summary>
        /// Permission policy.
        /// </summary>
        public const string Policy = Prefix + ".Policy";
    }
    /// <summary>
    /// Connection settings
    /// </summary>
    public class Connection
    {
        public const string Prefix = TencentCloudSettingNames.Prefix + ".Connection";
        /// <summary>
        /// proxy server
        /// </summary>
        public const string WebProxy = Prefix + ".WebProxy";
        /// <summary>
        /// Connect region
        /// According to Tencent Cloud documentation, a domain name needs to be specified for financial district services.
        /// </summary>
        public const string EndPoint = Prefix + ".EndPoint";
        /// <summary>
        /// Http method, according to Tencent Cloud documentation, different methods have restrictions on request size
        /// Default: POST
        /// </summary>
        public const string HttpMethod = Prefix + ".HttpMethod";
        /// <summary>
        /// Timeout
        /// </summary>
        public const string Timeout = Prefix + ".Timeout";
    }

    public static class Sms
    {
        private const string Prefix = TencentCloudSettingNames.Prefix + ".Sms";
        /// <summary>
        /// SMS SdkAppId
        /// The actual SdkAppId generated after adding the application in the SMS console, for example, 1400006666.
        /// </summary>
        public const string AppId = Prefix + ".AppId";
        /// <summary>
        /// SMS signature content
        /// </summary>
        public const string DefaultSignName = Prefix + ".DefaultSignName";
        /// <summary>
        /// Default SMS template ID
        /// </summary>
        public const string DefaultTemplateId = Prefix + ".DefaultTemplateId";
    }
}
