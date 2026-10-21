using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Schedules.Models;
/// <summary>
/// Calendar notification range members
/// </summary>
public class CalendarShare
{
    /// <summary>
    /// ID of calendar notification scope member
    /// </summary>
    [NotNull]
    [JsonProperty("userid")]
    [JsonPropertyName("userid")]
    public string UserId { get; set; } = default!;
    /// <summary>
    /// Calendar notification range member permissions
    /// </summary>
    [NotNull]
    [JsonProperty("permission")]
    [JsonPropertyName("permission")]
    public CalendarSharePermission Permission { get; set; }
}
