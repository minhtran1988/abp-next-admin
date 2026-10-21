using System.ComponentModel;
using System.Xml.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Messages.Models;
/// <summary>
/// How members leave the group
/// </summary>
[Description("How members can leave the group")]
public enum ExternalChatMemberQuitScene
{
    /// <summary>
    /// Leave the group yourself
    /// </summary>
    [XmlEnum("0")]
    [Description("Leave the group yourself")]
    UserSelf = 0,
    /// <summary>
    /// Group owner/administrator moves out
    /// </summary>
    [XmlEnum("1")]
    [Description("Group owner/administrator moved out")]
    Admin = 1
}
