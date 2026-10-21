using LINGYUN.Abp.WeChat.Work.Settings;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Settings;

public static class WeChatWorkContactSettingNames
{
    public const string Prefix = WeChatWorkSettingNames.Prefix + ".Contacts";
    /// <summary>
    /// Address book application Srcret
    /// </summary>
    public const string Secret = Prefix + ".Secret";
}
