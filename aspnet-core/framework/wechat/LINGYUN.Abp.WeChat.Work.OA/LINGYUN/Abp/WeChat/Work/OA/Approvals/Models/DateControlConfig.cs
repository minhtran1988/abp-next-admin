using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Date/date+time control configuration
/// </summary>
public class DateControlConfig : ControlConfig
{
    /// <summary>
    /// Date/date+time content
    /// </summary>
    [NotNull]
    [JsonProperty("date")]
    [JsonPropertyName("date")]
    public DateConfig Date { get; set; } = default!;
    public DateControlConfig()
    {

    }

    public DateControlConfig(DateConfig date)
    {
        Date = date;
    }
}

public class DateConfig
{
    /// <summary>
    /// Time display type: day-date; hour-date+time, consistent with the corresponding template control properties
    /// </summary>
    [NotNull]
    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public string Type { get; set; } = default!;
}
