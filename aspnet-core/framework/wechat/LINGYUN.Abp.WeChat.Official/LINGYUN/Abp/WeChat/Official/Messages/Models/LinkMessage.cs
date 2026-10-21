using LINGYUN.Abp.WeChat.Common.Messages;
using System.Xml.Serialization;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Official.Messages.Models;
/// <summary>
/// Link message
/// </summary>
[EventName("link")]
public class LinkMessage : WeChatOfficialGeneralMessage
{
    /// <summary>
    /// Message title
    /// </summary>
    [XmlElement("Title")]
    public string Title { get; set; } = default!;
    /// <summary>
    /// Message description
    /// </summary>
    [XmlElement("Description")]
    public string? Description { get; set; }
    /// <summary>
    /// Message link
    /// </summary>
    [XmlElement("Url")]
    public string Url { get; set; } = default!;
    public override WeChatMessageEto ToEto()
    {
        return new WeChatOfficialGeneralMessageEto<LinkMessage>(this);
    }
}
