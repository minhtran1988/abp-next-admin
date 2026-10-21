namespace LINGYUN.Abp.Aliyun.Features;
public static class AliyunFeatureNames
{
    public const string GroupName = "AlibabaCloud";

    public const string Enable = GroupName + ".Enable";

    public static class Sms
    {
        public const string Default = GroupName + ".Sms";

        public const string Enable = Default + ".Enable";

        /// <summary>
        /// Maximum number of sending times
        /// </summary>
        public const string SendLimit = Default + ".SendLimit";
        /// <summary>
        /// The upper limit of sending times
        /// </summary>
        public const string SendLimitInterval = Default + ".SendLimitInterval";
        /// <summary>
        /// Default sending limit
        /// </summary>
        public const int DefaultSendLimit = 1000;
        /// <summary>
        /// Default maximum sending times
        /// </summary>
        public const int DefaultSendLimitInterval = 1;
    }

    public static class BlobStoring
    {
        public const string Default = GroupName + ".BlobStoring";

        public const string Enable = Default + ".Enable";
    }
}