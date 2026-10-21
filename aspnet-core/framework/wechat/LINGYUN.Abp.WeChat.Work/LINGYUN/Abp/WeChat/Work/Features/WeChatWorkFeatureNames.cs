namespace LINGYUN.Abp.WeChat.Work.Features;

public static class WeChatWorkFeatureNames
{
    public const string GroupName = "WeChat.Work";
    /// <summary>
    /// Enable corporate WeChat
    /// </summary>
    public const string Enable = GroupName + ".Enable";

    public static class Message
    {
        public const string GroupName = WeChatWorkFeatureNames.GroupName + ".Message";
        /// <summary>
        /// Enable message push
        /// </summary>
        public const string Enable = GroupName + ".Enable";
        /// <summary>
        /// Maximum number of sending times
        /// </summary>
        public const string Limit = GroupName + ".Limit";
        /// <summary>
        /// The upper limit of sending times
        /// </summary>
        public const string LimitInterval = GroupName + ".LimitInterval";
    }

    public static class AppChat
    {
        public const string GroupName = WeChatWorkFeatureNames.GroupName + ".AppChat";
        public static class Message
        {
            /// <summary>
            /// Enable message push
            /// </summary>
            public const string Enable = GroupName + ".Enable";
            /// <summary>
            /// Maximum number of sending times
            /// </summary>
            public const string Limit = GroupName + ".Limit";
            /// <summary>
            /// The upper limit of sending times
            /// </summary>
            public const string LimitInterval = GroupName + ".LimitInterval";
        }
    }

    public static class Webhook
    {
        public const string GroupName = WeChatWorkFeatureNames.GroupName + ".Webhook";
        /// <summary>
        /// Enable message push
        /// </summary>
        public const string Enable = GroupName + ".Enable";
        /// <summary>
        /// Maximum number of sending times
        /// </summary>
        public const string Limit = GroupName + ".Limit";
        /// <summary>
        /// The upper limit of sending times
        /// </summary>
        public const string LimitInterval = GroupName + ".LimitInterval";
    }
}
