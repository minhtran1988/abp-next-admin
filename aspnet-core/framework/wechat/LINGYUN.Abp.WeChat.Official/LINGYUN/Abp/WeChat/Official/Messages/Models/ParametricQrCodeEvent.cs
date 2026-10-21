using LINGYUN.Abp.WeChat.Common.Messages;
using System.Xml.Serialization;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Official.Messages.Models;
/// <summary>
/// Scan QR code event with parameters
/// </summary>
[EventName("parametric_qr_code")]
public class ParametricQrCodeEvent : WeChatEventMessage
{
    /// <summary>
    /// Event KEY value
    /// </summary>
    [XmlElement("EventKey")]
    public string EventKey { get; set; } = default!;
    /// <summary>
    /// The QR code ticket can be used to exchange for the QR code image
    /// </summary>
    [XmlElement("Ticket")]
    public string Ticket { get; set; } = default!;
    public override WeChatMessageEto ToEto()
    {
        return new WeChatOfficialEventMessageEto<ParametricQrCodeEvent>(this);
    }
}
