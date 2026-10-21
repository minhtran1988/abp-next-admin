using LINGYUN.Abp.WeChat.Common.Messages;
using LINGYUN.Abp.WeChat.Work.Common.Messages;
using System.Xml.Serialization;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.OA.Messages.Models;
/// <summary>
/// Conference room cancellation event
/// </summary>
/// <remarks>
/// When the user cancels the reservation of a conference room on Enterprise WeChat, this event will be triggered to call back to the conference room system application; if the conference room is booked by a self-built application, in addition to the conference room system application, a callback will also be made to the corresponding self-built application.
/// </remarks>
[EventName("cancel_meeting_room")]
public class CancelMeetingRoomEvent : WeChatWorkEventMessage
{
    /// <summary>
    /// Conference room id
    /// </summary>
    [XmlElement("MeetingRoomId")]
    public int MeetingRoomId { get; set; }
    /// <summary>
    /// Booking id, you can query the specific meeting booking status based on this ID
    /// </summary>
    [XmlElement("BookingId")]
    public string BookingId { get; set; } = default!;

    public override WeChatMessageEto ToEto()
    {
        return new WeChatWorkEventMessageEto<CancelMeetingRoomEvent>(this);
    }
}
