using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Schedules.Models;
/// <summary>
/// Calendar schedule details
/// </summary>
public class CalendarScheduleInfo : ScheduleInfo
{
    /// <summary>
    /// The schedule number is an auto-incrementing number
    /// </summary>
    [NotNull]
    [JsonProperty("sequence")]
    [JsonPropertyName("sequence")]
    public ulong Sequence { get; set; }
}
