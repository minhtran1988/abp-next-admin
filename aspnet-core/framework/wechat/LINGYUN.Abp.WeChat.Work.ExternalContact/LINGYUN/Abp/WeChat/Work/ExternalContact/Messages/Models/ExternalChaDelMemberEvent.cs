using LINGYUN.Abp.WeChat.Common.Messages;
using LINGYUN.Abp.WeChat.Work.Common.Messages;
using System.Xml.Serialization;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Messages.Models;
/// <summary>
/// Customer group member withdrawal event push
/// </summary>
[EventName("external_chat_del_member")]
public class ExternalChaDelMemberEvent : ExternalChatChangeMemberEvent
{
    /// <summary>
    /// It has a value when a member leaves the group. Indicates how members leave the group<br />
    /// 0 - Leave the group by yourself<br />
    /// 1 - Group owner/administrator removed<br />
    /// </summary>
    [XmlElement("QuitScene")]
    public ExternalChatMemberQuitScene QuitScene { get; set; }

    public override WeChatMessageEto ToEto()
    {
        return new WeChatWorkEventMessageEto<ExternalChaDelMemberEvent>(this);
    }
}
