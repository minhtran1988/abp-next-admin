namespace LINGYUN.Abp.WxPusher.Features;

public static class WxPusherFeatureNames
{
    public const string GroupName = "WxPusher";

    /// <summary>
    /// Enable WxPusher
    /// </summary>
    public const string Enable = GroupName + ".Enable";

    public static class Message
    {
        public const string GroupName = WxPusherFeatureNames.GroupName + ".Message";
        /// <summary>
        /// Enable message push
        /// </summary>
        public const string Enable = GroupName + ".Enable";
        /// <summary>
        /// Maximum number of sending times
        /// </summary>
        public const string SendLimit = GroupName + ".SendLimit";
        /// <summary>
        /// The upper limit of sending times
        /// </summary>
        public const string SendLimitInterval = GroupName + ".SendLimitInterval";
    }
}
