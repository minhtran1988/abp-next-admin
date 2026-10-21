using LINGYUN.Abp.WeChat.Common.Messages;
using System.Xml.Serialization;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Official.Messages.Models;
/// <summary>
/// Event push when clicking menu jump link
/// </summary>
[EventName("menu_click_jump_link")]
public class MenuClickJumpLinkEvent : WeChatEventMessage
{
    /// <summary>
    /// Event KEY value
    /// </summary>
    [XmlElement("EventKey")]
    public string EventKey { get; set; } = default!;
    public override WeChatMessageEto ToEto()
    {
        return new WeChatOfficialEventMessageEto<MenuClickJumpLinkEvent>(this);
    }
}
