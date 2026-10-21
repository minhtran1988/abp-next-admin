using System.ComponentModel;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.GroupChats.Models;
/// <summary>
/// How to join the group
/// </summary>
[Description("Method to join the group")]
public enum GroupChatMemberJoinScene
{
    /// <summary>
    /// Invited to the group by group members (directly invited to the group)
    /// </summary>
    [Description("Invite directly to the group")]
    DirectInvitation = 1,
    /// <summary>
    /// Invited to the group by group members (join the group through the invitation link)
    /// </summary>
    [Description("Join the group via invitation link")]
    InvitationLink = 2,
    /// <summary>
    /// Join the group by scanning the group QR code
    /// </summary>
    [Description("Join the group by scanning the group QR code")]
    ScanQrCode = 3,
}
