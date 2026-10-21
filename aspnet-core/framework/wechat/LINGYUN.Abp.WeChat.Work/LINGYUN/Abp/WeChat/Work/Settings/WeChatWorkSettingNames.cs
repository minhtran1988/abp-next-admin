namespace LINGYUN.Abp.WeChat.Work.Settings;

public static class WeChatWorkSettingNames
{
    public const string Prefix = "Abp.WeChat.Work";

    /// <summary>
    /// Enable quick login
    /// </summary>
    public const string EnabledQuickLogin = Prefix + ".EnabledQuickLogin";

    public static class Connection
    {
        public const string Prefix = WeChatWorkSettingNames.Prefix + ".Connection";
        /// <summary>
        /// Enterprise ID
        /// </summary>
        public static string CorpId = Prefix + ".CorpId";
        /// <summary>
        /// ApplicationId
        /// </summary>
        public static string AgentId = Prefix + ".AgentId";
        /// <summary>
        /// Application key
        /// </summary>
        public static string Secret = Prefix + ".Secret";
        /// <summary>
        /// Token
        /// </summary>
        public static string Token = Prefix + ".Token";
        /// <summary>
        /// EncodingAESKey
        /// </summary>
        public static string EncodingAESKey = Prefix + ".EncodingAESKey";
    }
}
