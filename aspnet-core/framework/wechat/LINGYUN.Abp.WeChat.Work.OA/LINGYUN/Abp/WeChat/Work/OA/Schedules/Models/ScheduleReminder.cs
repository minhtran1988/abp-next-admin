using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Schedules.Models;
/// <summary>
/// Schedule reminder
/// </summary>
public class ScheduleReminder
{
    /// <summary>
    /// Do you need to be reminded?
    /// </summary>
    [NotNull]
    [JsonProperty("is_remind")]
    [JsonPropertyName("is_remind")]
    [Newtonsoft.Json.JsonConverter(typeof(IntToBoolConverter))]
    [System.Text.Json.Serialization.JsonConverter(typeof(IntToBoolConverter))]
    public bool IsRemind { get; set; }
    /// <summary>
    /// Whether to repeat the schedule
    /// </summary>
    [NotNull]
    [JsonProperty("is_repeat")]
    [JsonPropertyName("is_repeat")]
    [Newtonsoft.Json.JsonConverter(typeof(IntToBoolConverter))]
    [System.Text.Json.Serialization.JsonConverter(typeof(IntToBoolConverter))]
    public bool IsRepeat { get; set; }
    /// <summary>
    /// How many seconds before the schedule starts (start_time) to be reminded, effective when is_remind is 1. For example: 300 means a reminder 5 minutes before the start of the schedule. Currently only the following values are supported:
    /// <list type="number">
    /// <item>0 - When the event starts</item>
    /// <item>300 - 5 minutes before the event starts</item>
    /// <item>900 - 15 minutes before the event starts</item>
    /// <item>3600 - 1 hour before the event starts</item>
    /// <item>86400 - 1 day before the event starts</item>
    /// </list>
    /// </summary>
    /// <remarks>
    /// Note: It is recommended to use the remind_time_diffs field, which will be abandoned later.
    /// </remarks>
    [CanBeNull]
    [JsonProperty("remind_before_event_secs")]
    [JsonPropertyName("remind_before_event_secs")]
    public uint? RemindBeforeEventSecs { get; set; }
    /// <summary>
    /// The difference between the reminder time and the schedule start time (start_time) is valid when IsRemind is true. For example: -300 means a reminder 5 minutes before the start of the schedule.
    /// </summary>
    /// <remarks>
    /// Special case: For the "all day" type schedule set by the enterprise WeChat terminal, since start_time is a 0 o'clock timestamp, the reminder will appear as a positive number 32400 if it is set to 9 o'clock that day.
    /// Value range: -604800 ~ 86399
    /// </remarks>
    [CanBeNull]
    [JsonProperty("remind_time_diffs")]
    [JsonPropertyName("remind_time_diffs")]
    public int[]? RemindTimeDiffs { get; set; }
    /// <summary>
    /// Repeat type, valid when IsRepeat is true
    /// </summary>
    [CanBeNull]
    [JsonProperty("repeat_type")]
    [JsonPropertyName("repeat_type")]
    public ScheduleReminderRepeatType? RepeatType { get; set; }
    /// <summary>
    /// Repeat end time, Unix timestamp. Leave blank or fill in 0 to indicate repeating
    /// </summary>
    [CanBeNull]
    [JsonProperty("repeat_until")]
    [JsonPropertyName("repeat_until")]
    public long? RepeatUntil { get; set; }
    /// <summary>
    /// Whether to customize repetition
    /// </summary>
    [NotNull]
    [JsonProperty("is_custom_repeat")]
    [JsonPropertyName("is_custom_repeat")]
    [Newtonsoft.Json.JsonConverter(typeof(IntToBoolConverter))]
    [System.Text.Json.Serialization.JsonConverter(typeof(IntToBoolConverter))]
    public bool IsCustomRepeat { get; set; }
    /// <summary>
    /// Repeat interval
    /// </summary>
    /// <remarks>
    /// Only valid when specified as a custom repeat.
    /// This field has different meanings depending on the repeat_type.
    /// For example:
    /// Repeat_interval is specified as 2, repeat_type is specified as weekly, then it repeats every 2 weeks;
    /// Repeat_interval is specified as 2, repeat_type is specified as monthly, then it will be repeated every 2 months
    /// </remarks>
    [CanBeNull]
    [JsonProperty("repeat_interval")]
    [JsonPropertyName("repeat_interval")]
    public uint? RepeatInterval { get; set; }
    /// <summary>
    /// Repeat every day of the week
    /// </summary>
    /// <remarks>
    /// Only valid when specified as custom recurrence and the recurrence type is weekly.
    /// Value range: 1 ~ 7, indicating Monday to Sunday respectively
    /// </remarks>
    [CanBeNull]
    [JsonProperty("repeat_day_of_week")]
    [JsonPropertyName("repeat_day_of_week")]
    public uint[]? RepeatDayOfWeek { get; set; }
    /// <summary>
    /// Which days of the month are repeated?
    /// </summary>
    /// <remarks>
    /// Only valid when specified as a custom recurrence and the recurrence type is monthly.
    /// Value range: 1 ~ 31, indicating numbers 1 to 31 respectively.
    /// </remarks>
    [CanBeNull]
    [JsonProperty("repeat_day_of_month")]
    [JsonPropertyName("repeat_day_of_month")]
    public uint[]? RepeatDayOfMonth { get; set; }
    /// <summary>
    /// Time zone.
    /// </summary>
    /// <remarks>
    /// UTC offset representation (that is, the number of hours away from the zero time zone), the east zone is a positive number, and the west zone is a negative number.
    /// For example: +8 means East Eighth District, Beijing time
    /// The default is Dongba District, Beijing time
    /// Value range: -12 ~ +12
    /// </remarks>
    [CanBeNull]
    [JsonProperty("timezone")]
    [JsonPropertyName("timezone")]
    public uint? TimeZone { get; set; }
    /// <summary>
    /// A list of dates not included in the repeating schedule.
    /// </summary>
    /// <remarks>
    /// If you modify/delete a specific day or multiple days in a duplicate schedule, the original schedule will exclude the corresponding dates.
    /// </remarks>
    [CanBeNull]
    [JsonProperty("exclude_time_list")]
    [JsonPropertyName("exclude_time_list")]
    public ScheduleReminderExcludeTime[]? ExcludeTimeList { get; set; }
}
