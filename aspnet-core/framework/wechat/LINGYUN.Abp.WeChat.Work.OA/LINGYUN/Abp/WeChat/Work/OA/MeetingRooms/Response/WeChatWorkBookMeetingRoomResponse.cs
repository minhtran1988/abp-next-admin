using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.MeetingRooms.Response;
/// <summary>
/// Booking conference room response parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/93620#%E9%A2%84%E5%AE%9A%E4%BC%9A%E8%AE%AE%E5%AE%A4"/>
/// </remarks>
public class WeChatWorkBookMeetingRoomResponse : WeChatWorkResponse
{
    /// <summary>
    /// Reservation id of the conference room
    /// </summary>
    [NotNull]
    [JsonProperty("booking_id")]
    [JsonPropertyName("booking_id")]
    public string BookingId { get; set; } = default!;
    /// <summary>
    /// The id of the conference associated schedule
    /// </summary>
    [NotNull]
    [JsonProperty("schedule_id")]
    [JsonPropertyName("schedule_id")]
    public string ScheduleId { get; set; } = default!;
}
