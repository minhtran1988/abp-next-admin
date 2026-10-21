using JetBrains.Annotations;
using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.OA.MeetingRooms.Request;
/// <summary>
/// Cancel reservation meeting room request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/93620#%E5%8F%96%E6%B6%88%E9%A2%84%E5%AE%9A%E4%BC%9A%E8%AE%AE%E5%AE%A4"/>
/// </remarks>
public class WeChatWorkCancelBookMeetingRoomRequest : WeChatWorkRequest
{
    /// <summary>
    /// Reservation id of the conference room
    /// </summary>
    [NotNull]
    [JsonProperty("booking_id")]
    [JsonPropertyName("booking_id")]
    public string BookingId { get; }
    /// <summary>
    /// Whether to retain the schedule, 0 - delete simultaneously, 1 - retain, only valid for non-duplicate schedules
    /// </summary>
    [NotNull]
    [JsonProperty("keep_schedule")]
    [JsonPropertyName("keep_schedule")]
    public int KeepSchedule { get; }
    /// <summary>
    /// For repeated schedules, if this parameter is not filled in, it means canceling all repeated reservations; if filled in, it means canceling the conference room reservation on the corresponding day.
    /// </summary>
    [CanBeNull]
    [JsonProperty("cancel_date")]
    [JsonPropertyName("cancel_date")]
    public long? CancelDate { get; private set; }
    public WeChatWorkCancelBookMeetingRoomRequest(
        string bookingId,
        bool keepSchedule = false, 
        DateTime? cancelDate = null)
    {
        BookingId = Check.NotNullOrWhiteSpace(bookingId, nameof(bookingId));
        KeepSchedule = keepSchedule ? 1 : 0;
        if (cancelDate.HasValue)
        {
            CancelDate = (long)(cancelDate.Value - new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }
}
