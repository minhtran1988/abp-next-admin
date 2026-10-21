namespace LINGYUN.Abp.Aliyun.Settings;

public static class AliyunSettingNames
{
    public const string Prefix = "Abp.Aliyun";

    /// <summary>
    /// Authentication method
    /// </summary>
    public class Authorization
    {
        public const string Prefix = AliyunSettingNames.Prefix + ".Authorization";
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
    /// SMS service
    /// </summary>
    public class Sms
    {
        public const string Prefix = AliyunSettingNames.Prefix + ".Sms";
        /// <summary>
        /// Alibaba Cloud SMS service domain name
        /// </summary>
        public const string Domain = Prefix + ".Domain";
        /// <summary>
        /// Calling method name
        /// </summary>
        public const string ActionName = Prefix + ".ActionName";
        /// <summary>
        /// Default version number
        /// </summary>
        public const string Version = Prefix + ".Version";
        /// <summary>
        /// Default signature
        /// </summary>
        public const string DefaultSignName = Prefix + ".DefaultSignName";
        /// <summary>
        /// Default SMS template number
        /// </summary>
        public const string DefaultTemplateCode = Prefix + ".DefaultTemplateCode";
        /// <summary>
        /// Default number
        /// </summary>
        public const string DefaultPhoneNumber = Prefix + ".DefaultPhoneNumber";
        /// <summary>
        /// Display errors to the client
        /// </summary>
        public const string VisableErrorToClient = Prefix + ".VisableErrorToClient";
    }

    /// <summary>
    /// Cloud communication number authentication service
    /// </summary>
    public class SmsVerifyCode
    {
        public const string Prefix = AliyunSettingNames.Prefix + ".SmsVerifyCode";
        /// <summary>
        /// Alibaba Cloud number authentication service domain name
        /// </summary>
        public const string Domain = Prefix + ".Domain";
        /// <summary>
        /// Default signature
        /// </summary>
        public const string DefaultSignName = Prefix + ".DefaultSignName";
        /// <summary>
        /// Default SMS template number
        /// </summary>
        public const string DefaultTemplateCode = Prefix + ".DefaultTemplateCode";
    }
}
