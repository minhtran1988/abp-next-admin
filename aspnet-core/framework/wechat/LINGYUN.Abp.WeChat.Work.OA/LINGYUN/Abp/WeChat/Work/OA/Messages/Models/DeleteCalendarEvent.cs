using LINGYUN.Abp.WeChat.Common.Messages;
using LINGYUN.Abp.WeChat.Work.Common.Messages;
using System.Xml.Serialization;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.OA.Messages.Models;
/// <summary>
/// Delete calendar event
/// </summary>
/// <remarks>
/// This event is triggered when the calendar administrator deletes the calendar created by the API.
/// </remarks>
[EventName("delete_calendar")]
public class DeleteCalendarEvent : WeChatWorkEventMessage
{
    /// <summary>
    /// Calendar ID
    /// </summary>
    [XmlElement("CalId")]
    public string CalId { get; set; } = default!;

    public override WeChatMessageEto ToEto()
    {
        return new WeChatWorkEventMessageEto<DeleteCalendarEvent>(this);
    }
}
