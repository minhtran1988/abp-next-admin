using System.ComponentModel;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Members.Models;
/// <summary>
/// Email type
/// </summary>
[Description("Mailbox Type")]
public enum EmailType
{
    /// <summary>
    /// Business email
    /// </summary>
    [Description("Business Email")]
    BizEmail = 1,
    /// <summary>
    /// Personal email
    /// </summary>
    [Description("Personal Email")]
    PersonEmail = 2,
}
