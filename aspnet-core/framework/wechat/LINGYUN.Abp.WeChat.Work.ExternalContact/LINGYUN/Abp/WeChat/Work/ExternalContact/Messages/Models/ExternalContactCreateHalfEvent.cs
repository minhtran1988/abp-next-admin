using LINGYUN.Abp.WeChat.Common.Messages;
using LINGYUN.Abp.WeChat.Work.Common.Messages;
using System.Xml.Serialization;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Messages.Models;
/// <summary>
/// External contact adds member event without verification
/// </summary>
[EventName("external_contact_create_half")]
public class ExternalContactCreateHalfEvent : ExternalContactChangeEvent
{
    /// <summary>
    /// Add the state parameter configured in this user's "Contact Me" method, or the customer_channel parameter specified in the customer acquisition link, which can be used to identify the channel for adding this user.
    /// </summary>
    [XmlElement("State")]
    public string State { get; set; } = default!;
    /// <summary>
    /// Welcome code, which can be used to send welcome messages
    /// </summary>
    [XmlElement("WelcomeCode")]
    public string WelcomeCode { get; set; } = default!;

    public override WeChatMessageEto ToEto()
    {
        return new WeChatWorkEventMessageEto<ExternalContactCreateHalfEvent>(this);
    }
}
