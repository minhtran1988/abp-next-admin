using System.ComponentModel;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Customers.Models;
/// <summary>
/// External contact type
/// </summary>
[Description("External Contact Type")]
public enum ExternalContactType
{
    /// <summary>
    /// WeChat users
    /// </summary>
    [Description("WeChat user")]
    WeChat = 1,
    /// <summary>
    /// Enterprise WeChat users
    /// </summary>
    [Description("Enterprise WeChat User")]
    WeChatWork = 2,
}
