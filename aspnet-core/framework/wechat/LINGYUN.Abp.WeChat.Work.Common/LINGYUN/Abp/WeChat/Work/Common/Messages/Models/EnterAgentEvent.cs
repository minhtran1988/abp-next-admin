using LINGYUN.Abp.WeChat.Common.Messages;
using System.Xml.Serialization;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.Common.Messages.Models;
/// <summary>
/// Enter application events
/// </summary>
[EventName("enter_agent")]
public class EnterAgentEvent : WeChatWorkEventMessage
{
    /// <summary>
    /// Event KEY value
    /// </summary>
    [XmlElement("EventKey")]
    public string EventKey { get; set; } = default!;
    public override WeChatMessageEto ToEto()
    {
        return new WeChatWorkEventMessageEto<EnterAgentEvent>(this);
    }
}
