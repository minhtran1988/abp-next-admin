using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Date/date+time control value
/// </summary>
public class DateControlValue : ControlValue
{
    /// <summary>
    /// Amount content, fill in the input value of the amount control here
    /// </summary>
    [NotNull]
    [JsonProperty("date")]
    [JsonPropertyName("date")]
    public DateValue Date { get; set; } = default!;
    public DateControlValue()
    {

    }

    public DateControlValue(DateValue date)
    {
        Date = date;
    }
}

public class DateValue
{
    /// <summary>
    /// Time display type: day-date; hour-date+time, consistent with the corresponding template control properties
    /// </summary>
    [NotNull]
    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public string Type { get; set; } = default!;
    /// <summary>
    /// Timestamp - string type, fill in the selected value of the date/date+time control here, which shall prevail
    /// </summary>
    [NotNull]
    [JsonProperty("s_timestamp")]
    [JsonPropertyName("s_timestamp")]
    public string Timestamp { get; set; } = default!;
    public DateValue()
    {

    }

    private DateValue(string type, string timestamp)
    {
        Type = type;
        Timestamp = timestamp;
    }

    public static DateValue Day(string timestamp)
    {
        return new DateValue("day", timestamp);
    }

    public static DateValue Hour(string timestamp)
    {
        return new DateValue("hour", timestamp);
    }
}
