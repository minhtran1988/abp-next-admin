using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.MeetingRooms.Response;
/// <summary>
/// Book the conference room response parameters through the conference
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/93620#%E9%80%9A%E8%BF%87%E4%BC%9A%E8%AE%AE%E9%A2%84%E5%AE%9A%E4%BC%9A%E8%AE%AE%E5%AE%A4"/>
/// </remarks>
public class WeChatWorkBookMeetingRoomByMeetingResponse : WeChatWorkResponse
{
    /// <summary>
    /// Reservation id of the conference room
    /// </summary>
    [NotNull]
    [JsonProperty("booking_id")]
    [JsonPropertyName("booking_id")]
    public string BookingId { get; set; } = default!;
    /// <summary>
    /// Meeting room conflict date list, which is the timestamp of 0 o'clock on the day; use a repeated schedule to book a meeting room, and return when some dates conflict with the meeting room reservation status
    /// </summary>
    [NotNull]
    [JsonProperty("conflict_date")]
    [JsonPropertyName("conflict_date")]
    public long[] ConflictDate { get; set; } = default!;
}
