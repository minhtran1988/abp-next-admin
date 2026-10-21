using LINGYUN.Abp.WeChat.Common.Messages;
using LINGYUN.Abp.WeChat.Work.Common.Messages;
using System.Xml.Serialization;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Messages.Models;
/// <summary>
/// Customer takeover failure event
/// </summary>
[EventName("external_contact_transfer_fail")]
public class ExternalContactTransferFailEvent : ExternalContactChangeEvent
{
    /// <summary>
    /// The reason for the failure of the replacement, customer_refused-the customer refused, customer_limit_exceed-the number of customers who replaced the member reached the upper limit
    /// </summary>
    [XmlElement("FailReason")]
    public string FailReason { get; set; } = default!;

    public override WeChatMessageEto ToEto()
    {
        return new WeChatWorkEventMessageEto<ExternalContactTransferFailEvent>(this);
    }
}
