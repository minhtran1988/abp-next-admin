using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Duration control value
/// </summary>
public class DateRangeControlValue : ControlValue
{
    /// <summary>
    /// Duration
    /// </summary>
    [NotNull]
    [JsonProperty("date_range")]
    [JsonPropertyName("date_range")]
    public DateRangeValue DateRange { get; set; } = default!;
    public DateRangeControlValue()
    {

    }

    public DateRangeControlValue(DateRangeValue dateRange)
    {
        DateRange = dateRange;
    }
}

public class DateRangeValue
{
    /// <summary>
    /// Duration granularity: halfday: by day hour: by hour
    /// </summary>
    [NotNull]
    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public string Type { get; set; } = default!;
    /// <summary>
    /// Start time, unix timestamp. When type is halfday, the value can only be two fixed time points: AM: 00:00:00 timestamp of the day PM: 12:00:00 timestamp of the day
    /// </summary>
    [NotNull]
    [JsonProperty("new_begin")]
    [JsonPropertyName("new_begin")]
    public long NewBegin { get; set; } = default!;
    /// <summary>
    /// End time, unix timestamp. When type is halfday, the value can only be two fixed time points: AM: 00:00:00 timestamp of the day PM: 12:00:00 timestamp of the day
    /// </summary>
    [NotNull]
    [JsonProperty("new_end")]
    [JsonPropertyName("new_end")]
    public long NewEnd { get; set; }
    /// <summary>
    /// Duration range, unit seconds
    /// </summary>
    [NotNull]
    [JsonProperty("new_duration")]
    [JsonPropertyName("new_duration")]
    public long NewDuration { get; set; }
    public DateRangeValue()
    {

    }

    public DateRangeValue(string type, long newBegin, long newEnd, long newDuration)
    {
        Type = type;
        NewBegin = newBegin;
        NewEnd = newEnd;
        NewDuration = newDuration;
    }
}
