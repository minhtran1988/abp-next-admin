using LINGYUN.Abp.WeChat.Common.Messages;
using System.Xml.Serialization;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.Common.Messages.Models;
/// <summary>
/// Menu event push in the upper right corner of the universal template card
/// </summary>
[EventName("template_card_menu_event")]
public class TemplateCardMenuPushEvent : WeChatWorkEventMessage
{
    /// <summary>
    /// The same as the task_id specified when sending the template card message
    /// </summary>
    [XmlElement("TaskId")]
    public string TaskId { get; set; } = default!;
    /// <summary>
    /// Type of universal template card,
    /// The type is
    /// "text_notice", 
    /// "news_notice", 
    /// "button_interaction"
    /// Three types
    /// </summary>
    [XmlElement("CardType")]
    public string CardType { get; set; } = default!;
    /// <summary>
    /// ResponseCode used to call the update card interface
    /// </summary>
    [XmlElement("ResponseCode")]
    public string ResponseCode { get; set; } = default!;
    /// <summary>
    /// The same key value as the button key in the menu in the upper right corner of the send template card
    /// </summary>
    [XmlElement("EventKey")]
    public string EventKey { get; set; } = default!;
    public override WeChatMessageEto ToEto()
    {
        return new WeChatWorkEventMessageEto<TemplateCardMenuPushEvent>(this);
    }
}
