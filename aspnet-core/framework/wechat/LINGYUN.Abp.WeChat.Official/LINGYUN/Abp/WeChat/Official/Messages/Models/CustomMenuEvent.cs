using LINGYUN.Abp.WeChat.Common.Messages;
using System.Xml.Serialization;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Official.Messages.Models;
/// <summary>
/// Custom menu events
/// </summary>
[EventName("custom_menu")]
public class CustomMenuEvent : WeChatEventMessage
{
    /// <summary>
    /// Event KEY value
    /// </summary>
    [XmlElement("EventKey")]
    public string EventKey { get; set; } = default!;

    public override WeChatMessageEto ToEto()
    {
        return new WeChatOfficialEventMessageEto<CustomMenuEvent>(this);
    }
}
