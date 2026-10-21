using LINGYUN.Abp.WeChat.Features;

namespace LINGYUN.Abp.WeChat.MiniProgram.Features;

public static class WeChatMiniProgramFeatures
{
    public const string GroupName = WeChatFeatures.GroupName + ".MiniProgram";

    public const string Enable = GroupName + ".Enable";

    public const string EnableAuthorization = GroupName + ".EnableAuthorization";

    public static class Messages
    {
        public const string Default = GroupName + ".Messages";

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
        /// Default maximum number of releases
        /// </summary>
        public const int DefaultSendLimit = 1000;
        /// <summary>
        /// Default maximum number of releases and duration
        /// </summary>
        public const int DefaultSendLimitInterval = 1;
    }
}
