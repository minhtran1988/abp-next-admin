using LINGYUN.Abp.WeChat.Common.Messages;
using LINGYUN.Abp.WeChat.Work.Common.Messages;
using System.Xml.Serialization;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.OA.Messages.Models;
/// <summary>
/// Modify schedule events
/// </summary>
/// <remarks>
/// After the application creates a schedule through the API, this event is triggered when the schedule administrator modifies the schedule.
/// </remarks>
[EventName("modify_schedule")]
public class UpdateScheduleEvent : WeChatWorkEventMessage
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
        return new WeChatWorkEventMessageEto<UpdateScheduleEvent>(this);
    }
}
