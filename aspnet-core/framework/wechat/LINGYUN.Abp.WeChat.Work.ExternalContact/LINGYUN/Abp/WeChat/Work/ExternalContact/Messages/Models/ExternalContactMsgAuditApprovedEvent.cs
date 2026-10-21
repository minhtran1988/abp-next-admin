using LINGYUN.Abp.WeChat.Common.Messages;
using LINGYUN.Abp.WeChat.Work.Common.Messages;
using System.Xml.Serialization;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Messages.Models;
/// <summary>
/// The customer agrees to push the chat content archive event
/// </summary>
[EventName("external_contact_msg_audit_approved")]
public class ExternalContactMsgAuditApprovedEvent : ExternalContactChangeEvent
{
    /// <summary>
    /// Welcome code, which can be used to send welcome messages
    /// </summary>
    [XmlElement("WelcomeCode")]
    public string WelcomeCode { get; set; } = default!;

    public override WeChatMessageEto ToEto()
    {
        return new WeChatWorkEventMessageEto<ExternalContactMsgAuditApprovedEvent>(this);
    }
}
