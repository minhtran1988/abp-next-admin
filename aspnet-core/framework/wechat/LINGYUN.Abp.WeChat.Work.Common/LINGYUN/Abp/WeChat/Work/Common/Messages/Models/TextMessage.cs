using LINGYUN.Abp.WeChat.Common.Messages;
using System.Xml.Serialization;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.Common.Messages.Models;
/// <summary>
/// Text message
/// </summary>
[EventName("text")]
public class TextMessage : WeChatWorkGeneralMessage
{
    /// <summary>
    /// Text message content
    /// </summary>
    [XmlElement("Content")]
    public string Content { get; set; } = default!;
    public override WeChatMessageEto ToEto()
    {
        return new WeChatWorkGeneralMessageEto<TextMessage>(this);
    }
}
