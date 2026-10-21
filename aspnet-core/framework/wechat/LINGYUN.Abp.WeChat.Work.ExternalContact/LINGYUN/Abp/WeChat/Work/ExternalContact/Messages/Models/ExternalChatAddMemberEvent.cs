using LINGYUN.Abp.WeChat.Common.Messages;
using LINGYUN.Abp.WeChat.Work.Common.Messages;
using System.Xml.Serialization;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Messages.Models;
/// <summary>
/// Push event of customer group members joining the group
/// </summary>
[EventName("external_chat_add_member")]
public class ExternalChatAddMemberEvent : ExternalChatChangeMemberEvent
{
    /// <summary>
    /// Has value when a member joins the group. Indicates how members join the group<br />
    /// 0 - Invited to the group by a member (including direct invitation to the group and joining the group through an invitation link)<br />
    /// 3 - Join the group by scanning the group QR code<br />
    /// </summary>
    [XmlElement("JoinScene")]
    public ExternalChatMemberJoinScene JoinScene { get; set; }

    public override WeChatMessageEto ToEto()
    {
        return new WeChatWorkEventMessageEto<ExternalChatAddMemberEvent>(this);
    }
}
