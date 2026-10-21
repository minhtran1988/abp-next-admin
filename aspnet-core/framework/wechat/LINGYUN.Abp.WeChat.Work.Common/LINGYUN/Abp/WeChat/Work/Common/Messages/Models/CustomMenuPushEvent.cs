using LINGYUN.Abp.WeChat.Common.Messages;
using System.Xml.Serialization;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.Common.Messages.Models;
/// <summary>
/// Click the menu to pull the message event push
/// </summary>
[EventName("custom_menu")]
public class CustomMenuPushEvent : WeChatWorkEventMessage
{
    /// <summary>
    /// Event KEY value
    /// </summary>
    [XmlElement("EventKey")]
    public string EventKey { get; set; } = default!;

    public override WeChatMessageEto ToEto()
    {
        return new WeChatWorkEventMessageEto<CustomMenuPushEvent>(this);
    }
}
