using LINGYUN.Abp.WeChat.Common.Messages;
using LINGYUN.Abp.WeChat.Work.Common.Messages;
using System.Xml.Serialization;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.OA.Messages.Models;
/// <summary>
/// Meeting room reservation event
/// </summary>
/// <remarks>
/// When a user reserves a conference room on Enterprise WeChat, this event will be triggered to call back to the conference room system application.
/// </remarks>
[EventName("book_meeting_room")]
public class BookMeetingRoomEvent : WeChatWorkEventMessage
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
        return new WeChatWorkEventMessageEto<BookMeetingRoomEvent>(this);
    }
}
