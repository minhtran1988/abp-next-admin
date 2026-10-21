using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Schedules.Models;
/// <summary>
/// Schedule participant details
/// </summary>
public class ScheduleAttendeeInfo
{
    /// <summary>
    /// Schedule participant ID
    /// </summary>
    [NotNull]
    [JsonProperty("userid")]
    [JsonPropertyName("userid")]
    public string UserId { get; set; } = default!;
    /// <summary>
    /// Acceptance status of schedule participants
    /// </summary>
    [NotNull]
    [JsonProperty("response_status")]
    [JsonPropertyName("response_status")]
    public ScheduleAttendeeResponseStatus ResponseStatus { get; set; }
}
