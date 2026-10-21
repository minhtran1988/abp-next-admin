using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Schedules.Models;
/// <summary>
/// Dates not included in the repeat schedule
/// </summary>
public class ScheduleReminderExcludeTime
{
    /// <summary>
    /// Exclusive date timestamp
    /// </summary>
    [NotNull]
    [JsonProperty("start_time")]
    [JsonPropertyName("start_time")]
    public long StartTime { get; set; }
}
