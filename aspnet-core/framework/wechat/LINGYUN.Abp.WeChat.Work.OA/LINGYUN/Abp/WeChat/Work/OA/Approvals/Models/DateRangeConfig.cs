using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Duration configuration
/// </summary>
public class DateRangeConfig
{
    /// <summary>
    /// Time display type: halfday-date; hour-date+time
    /// </summary>
    [NotNull]
    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public string Type { get; set; } = default!;
    /// <summary>
    /// 0-natural day; 1-working day
    /// </summary>
    [NotNull]
    [JsonProperty("official_holiday")]
    [JsonPropertyName("official_holiday")]
    public byte OfficialHoliday { get; set; }
    /// <summary>
    /// The length of a day (in seconds), must be greater than 0 and less than or equal to 86400
    /// </summary>
    [NotNull]
    [JsonProperty("perday_duration")]
    [JsonPropertyName("perday_duration")]
    public int PerdayDuration { get; set; }
    public DateRangeConfig()
    {

    }

    public DateRangeConfig(string type, byte officialHoliday, int perdayDuration)
    {
        Type = type;
        OfficialHoliday = officialHoliday;
        PerdayDuration = perdayDuration;
    }
}
