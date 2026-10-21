namespace LINGYUN.Abp.WeChat.Settings;

public static class WeChatSettingNames
{
    public const string Prefix = "Abp.WeChat";
    /// <summary>
    /// Enable quick login
    /// When logging in through WeChat code, if there is no registered user information and this configuration is enabled, create a new user directly and associate openid
    /// </summary>
    public const string EnabledQuickLogin = Prefix + ".EnabledQuickLogin";
}
