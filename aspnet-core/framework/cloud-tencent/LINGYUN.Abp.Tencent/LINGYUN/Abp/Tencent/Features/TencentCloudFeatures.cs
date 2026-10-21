namespace LINGYUN.Abp.Tencent.Features;

public static class TencentCloudFeatures
{
    public const string GroupName = "Abp.TencentCloud";

    public static class Sms
    {
        public const string GroupName = TencentCloudFeatures.GroupName + ".Sms";
        /// <summary>
        /// Enable SMS
        /// </summary>
        public const string Enable = GroupName + ".Enable";
    }

    public static class BlobStoring
    {
        public const string GroupName = TencentCloudFeatures.GroupName + ".BlobStoring";
        /// <summary>
        /// Enable object storage
        /// </summary>
        public const string Enable = GroupName + ".Enable";
        /// <summary>
        /// Maximum stream size limit, invalid if less than or equal to 0, unit (MB)
        /// Default: 0
        /// </summary>
        public const string MaximumStreamSize = GroupName + ".MaximumStreamSize";
    }
}
