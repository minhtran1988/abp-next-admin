using LINGYUN.Abp.WeChat.Common.Messages;
using LINGYUN.Abp.WeChat.Work.Common.Messages;
using System.Xml.Serialization;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Messages.Models;
/// <summary>
/// Delete enterprise customer event
/// </summary>
[EventName("external_contact_delete")]
public class ExternalContactDeleteEvent : ExternalContactChangeEvent
{
    /// <summary>
    /// The operation source of deleting the customer. DELETE_BY_TRANSFER indicates that the customer was automatically deleted by the transfer member due to in-service inheritance.
    /// </summary>
    [XmlElement("Source")]
    public string Source { get; set; } = default!;

    public override WeChatMessageEto ToEto()
    {
        return new WeChatWorkEventMessageEto<ExternalContactDeleteEvent>(this);
    }
}
