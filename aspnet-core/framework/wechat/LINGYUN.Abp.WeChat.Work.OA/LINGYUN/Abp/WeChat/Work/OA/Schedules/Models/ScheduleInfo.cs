using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Schedules.Models;
/// <summary>
/// Schedule details
/// </summary>
public class ScheduleInfo
{
    /// <summary>
    /// Schedule ID
    /// </summary>
    [NotNull]
    [JsonProperty("schedule_id")]
    [JsonPropertyName("schedule_id")]
    public string ScheduleId { get; set; } = default!;
    /// <summary>
    /// Administrator userid list
    /// </summary>
    [NotNull]
    [JsonProperty("admins")]
    [JsonPropertyName("admins")]
    public string[] Admins { get; set; } = default!;
    /// <summary>
    /// Schedule participant list
    /// </summary>
    [NotNull]
    [JsonProperty("attendees")]
    [JsonPropertyName("attendees")]
    public ScheduleAttendeeInfo[] Attendees { get; set; } = default!;
    /// <summary>
    /// Schedule title
    /// </summary>
    [NotNull]
    [JsonProperty("summary")]
    [JsonPropertyName("summary")]
    public string Summary { get; set; } = default!;
    /// <summary>
    /// Schedule description
    /// </summary>
    [CanBeNull]
    [JsonProperty("description")]
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    /// <summary>
    /// Reminder related information
    /// </summary>
    [CanBeNull]
    [JsonProperty("reminders")]
    [JsonPropertyName("reminders")]
    public ScheduleReminder? Reminders { get; set; }
    /// <summary>
    /// Schedule address
    /// </summary>
    /// <remarks>
    /// No more than 128 characters
    /// </remarks>
    [CanBeNull]
    [JsonProperty("location")]
    [JsonPropertyName("location")]
    public string? Location { get; set; }
    /// <summary>
    /// Schedule status
    /// </summary>
    [NotNull]
    [JsonProperty("status")]
    [JsonPropertyName("status")]
    public ScheduleStatus Status { get; set; }
    /// <summary>
    /// Schedule start time, Unix timestamp
    /// </summary>
    [NotNull]
    [JsonProperty("start_time")]
    [JsonPropertyName("start_time")]
    public long StartTime { get; set; }
    /// <summary>
    /// Schedule end time, Unix timestamp
    /// </summary>
    [NotNull]
    [JsonProperty("end_time")]
    [JsonPropertyName("end_time")]
    public long EndTime { get; set; }
    /// <summary>
    /// Whether the whole day schedule
    /// </summary>
    [NotNull]
    [JsonProperty("is_whole_day")]
    [JsonPropertyName("is_whole_day")]
    [Newtonsoft.Json.JsonConverter(typeof(IntToBoolConverter))]
    [System.Text.Json.Serialization.JsonConverter(typeof(IntToBoolConverter))]
    public bool IsWholeDay { get; set; }
    /// <summary>
    /// Calendar ID to which the schedule belongs
    /// </summary>
    /// <remarks>
    /// No more than 64 bytes
    /// </remarks>
    [CanBeNull]
    [JsonProperty("cal_id")]
    [JsonPropertyName("cal_id")]
    public string? CalId { get; set; }
}
