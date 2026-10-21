using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.MeetingRooms.Models;
/// <summary>
/// Conference room reservation status
/// </summary>
public class MeetingRoomSchedule
{
    /// <summary>
    /// Timestamp of start time
    /// </summary>
    [NotNull]
    [JsonProperty("start_time")]
    [JsonPropertyName("start_time")]
    public long StartTime { get; set; }
    /// <summary>
    /// Timestamp of end time
    /// </summary>
    [NotNull]
    [JsonProperty("end_time")]
    [JsonPropertyName("end_time")]
    public long EndTime { get; set; }
    /// <summary>
    /// The userid of the reservation person
    /// </summary>
    [NotNull]
    [JsonProperty("booker")]
    [JsonPropertyName("booker")]
    public string Booker { get; set; } = default!;
    /// <summary>
    /// Reservation status of the conference room
    /// </summary>
    [NotNull]
    [JsonProperty("status")]
    [JsonPropertyName("status")]
    public MeetingRoomScheduleStatus Status { get; set; }
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
    [CanBeNull]
    [JsonProperty("schedule_id")]
    [JsonPropertyName("schedule_id")]
    public string? ScheduleId { get; set; }
}
