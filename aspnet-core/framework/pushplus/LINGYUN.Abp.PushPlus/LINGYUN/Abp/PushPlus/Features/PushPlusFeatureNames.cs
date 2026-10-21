namespace LINGYUN.Abp.PushPlus.Features;

public static class PushPlusFeatureNames
{
    public const string GroupName = "PushPlus";

    public static class Message
    {
        public const string GroupName = PushPlusFeatureNames.GroupName + ".Message";

        public const string Enable = GroupName + ".Enable";
    }

    public static class Channel
    {
        public const string GroupName = PushPlusFeatureNames.GroupName + ".Channel";

        public static class WeChat
        {
            public const string GroupName = Channel.GroupName + ".WeChat";
            /// <summary>
            /// Enable WeChat channel
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

        public static class WeWork
        {
            public const string GroupName = Channel.GroupName + ".WeWork";
            /// <summary>
            /// Enable corporate WeChat channel
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

        public static class Webhook
        {
            public const string GroupName = Channel.GroupName + ".Webhook";
            /// <summary>
            /// Enable Webhook channel
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

        public static class Email
        {
            public const string GroupName = Channel.GroupName + ".Email";
            /// <summary>
            /// Enable Email channel
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

        public static class Sms
        {
            public const string GroupName = Channel.GroupName + ".Sms";
            /// <summary>
            /// Enable SMS channel
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
}
