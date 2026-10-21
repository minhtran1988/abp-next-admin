using LINGYUN.Abp.WeChat.Common.Messages;
using System.Xml.Serialization;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.Common.Messages.Models;
/// <summary>
/// Event push when clicking menu jump link
/// </summary>
[EventName("menu_click_jump_link")]
public class MenuClickJumpLinkPushEvent : WeChatWorkEventMessage
{
    /// <summary>
    /// Event KEY value
    /// </summary>
    [XmlElement("EventKey")]
    public string EventKey { get; set; } = default!;
    public override WeChatMessageEto ToEto()
    {
        return new WeChatWorkEventMessageEto<MenuClickJumpLinkPushEvent>(this);
    }
}
