using System.ComponentModel;
using System.Xml.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Messages.Models;
/// <summary>
/// How members join the group
/// </summary>
[Description("How members join the group")]
public enum ExternalChatMemberJoinScene
{
    /// <summary>
    /// Invited to the group by members
    /// </summary>
    [XmlEnum("0")]
    [Description("Invited to the group by members")]
    MemberInvitation = 0,
    /// <summary>
    /// Join the group by scanning the group QR code
    /// </summary>
    [XmlEnum("3")]
    [Description("Join the group by scanning the group QR code")]
    ScanQrCode = 3
}
