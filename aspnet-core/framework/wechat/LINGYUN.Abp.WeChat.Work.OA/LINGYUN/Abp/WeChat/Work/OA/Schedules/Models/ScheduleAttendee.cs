using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.OA.Schedules.Models;
/// <summary>
/// Schedule participants
/// </summary>
public class ScheduleAttendee
{
    /// <summary>
    /// Schedule participant ID
    /// </summary>
    [NotNull]
    [JsonProperty("userid")]
    [JsonPropertyName("userid")]
    public string UserId { get; }
    public ScheduleAttendee(string userId)
    {
        UserId = Check.NotNullOrWhiteSpace(userId, nameof(userId), 64); 
    }
}
