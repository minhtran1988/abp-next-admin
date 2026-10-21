using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Schedules.Models;
/// <summary>
/// Invalid input content
/// </summary>
public class CalendarFailResult
{
    /// <summary>
    /// Invalid calendar notification scope member list
    /// </summary>
    [CanBeNull]
    [JsonProperty("shares")]
    [JsonPropertyName("shares")]
    public CalendarFailShare[] Shares { get; set; } = default!;
}
