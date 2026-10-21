using JetBrains.Annotations;
using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.OA.Schedules.Models;

public abstract class CreateOrUpdateSchedule
{
    /// <summary>
    /// Schedule administrator userid list, the administrator must be in the list of shared members
    /// </summary>
    /// <remarks>
    /// Specify up to 3 people
    /// </remarks>
    [CanBeNull]
    [JsonProperty("admins")]
    [JsonPropertyName("admins")]
    public string[]? Admins { get; set; }
    /// <summary>
    /// Schedule participant list
    /// </summary>
    /// <remarks>
    /// Supports up to 1,000 people
    /// </remarks>
    [CanBeNull]
    [JsonProperty("attendees")]
    [JsonPropertyName("attendees")]
    public ScheduleAttendee[]? Attendees { get; set; }
    /// <summary>
    /// Schedule title
    /// </summary>
    /// <remarks>
    /// 0 ~ 128 characters. If left blank, "New Event" will be displayed by default.
    /// </remarks>
    [CanBeNull]
    [JsonProperty("summary")]
    [JsonPropertyName("summary")]
    public string? Summary { get; set; }
    /// <summary>
    /// Schedule description
    /// </summary>
    /// <remarks>
    /// No more than 1000 characters
    /// </remarks>
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
    /// Schedule start time, Unix timestamp
    /// </summary>
    [NotNull]
    [JsonProperty("start_time")]
    [JsonPropertyName("start_time")]
    public long StartTime { get; }
    /// <summary>
    /// Schedule end time, Unix timestamp
    /// </summary>
    [NotNull]
    [JsonProperty("end_time")]
    [JsonPropertyName("end_time")]
    public long EndTime { get; }
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
    /// <summary>
    /// Whether the whole day schedule
    /// </summary>
    [NotNull]
    [JsonProperty("is_whole_day")]
    [JsonPropertyName("is_whole_day")]
    [Newtonsoft.Json.JsonConverter(typeof(IntToBoolConverter))]
    [System.Text.Json.Serialization.JsonConverter(typeof(IntToBoolConverter))]
    public bool? IsWholeDay { get; set; }
    protected CreateOrUpdateSchedule(DateTime startTime, DateTime endTime)
    {
        StartTime = startTime.GetUnixTimeSeconds();
        EndTime = endTime.GetUnixTimeSeconds();
    }

    /// <summary>
    /// Validate input
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    public void Validate()
    {
        Check.Length(Summary, nameof(Summary), 128);
        Check.Length(Description, nameof(Description), 1000);
        Check.Length(Location, nameof(Location), 128);
        Check.Length(CalId, nameof(CalId), 64);

        if (Admins?.Length > 3)
        {
            throw new ArgumentException("Admins list. Up to 3!", nameof(Admins));
        }

        if (Attendees?.Length > 1000)
        {
            throw new ArgumentException("Attendees list. Up to 1000!", nameof(Attendees));
        }
    }
}
