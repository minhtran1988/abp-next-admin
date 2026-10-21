using LINGYUN.Abp.WeChat.Common.Messages;
using LINGYUN.Abp.WeChat.Work.Common.Messages;
using System.Xml.Serialization;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.OA.Messages.Models;
/// <summary>
/// Delete schedule event
/// </summary>
/// <remarks>
/// This event is triggered when the schedule administrator deletes the schedule on the calendar created by the API.
/// </remarks>
[EventName("delete_schedule")]
public class DeleteScheduleEvent : WeChatWorkEventMessage
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
        return new WeChatWorkEventMessageEto<DeleteScheduleEvent>(this);
    }
}