using LINGYUN.Abp.WeChat.Common.Messages;
using LINGYUN.Abp.WeChat.Work.Common.Messages;
using System.Xml.Serialization;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.OA.Messages.Models;
/// <summary>
/// Schedule receipt event
/// </summary>
/// <remarks>
/// This event is triggered when the participant performs a receipt operation (accept, pending, reject) in the schedule created by the application.
/// </remarks>
[EventName("respond_schedule")]
public class RespondScheduleEvent : WeChatWorkEventMessage
{
    /// <summary>
    /// Calendar ID
    /// </summary>
    [XmlElement("CalId")]
    public string? CalId { get; set; }
    /// <summary>
    /// Schedule ID
    /// </summary>
    [XmlElement("ScheduleId")]
    public string ScheduleId { get; set; } = default!;

    public override WeChatMessageEto ToEto()
    {
        return new WeChatWorkEventMessageEto<RespondScheduleEvent>(this);
    }
}