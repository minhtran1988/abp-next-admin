using System.ComponentModel;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.GroupChats.Models;
/// <summary>
/// Member type
/// </summary>
[Description("Member Type")]
public enum GroupChatMemberType
{
    /// <summary>
    /// Corporate members
    /// </summary>
    [Description("Corporate Member")]
    Internal = 1,
    /// <summary>
    /// External contact
    /// </summary>
    [Description("External Contact")]
    External = 2
}
