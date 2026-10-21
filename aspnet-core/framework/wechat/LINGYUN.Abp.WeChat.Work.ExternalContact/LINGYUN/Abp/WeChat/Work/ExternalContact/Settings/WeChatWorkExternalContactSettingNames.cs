using LINGYUN.Abp.WeChat.Work.Settings;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Settings;

public static class WeChatWorkExternalContactSettingNames
{
    public const string Prefix = WeChatWorkSettingNames.Prefix + ".ExternalContact";
    /// <summary>
    /// Customer contact application Srcret
    /// </summary>
    public const string Secret = Prefix + ".Secret";
}
