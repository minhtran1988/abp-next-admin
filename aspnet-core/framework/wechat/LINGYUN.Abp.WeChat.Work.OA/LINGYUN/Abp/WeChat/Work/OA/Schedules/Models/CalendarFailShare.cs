using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Schedules.Models;
/// <summary>
/// Invalid calendar notification scope member
/// </summary>
public class CalendarFailShare
{
    /// <summary>
    /// Error code
    /// </summary>
    [NotNull]
    [JsonProperty("errcode")]
    [JsonPropertyName("errcode")]
    public int ErrorCode { get; set; }
    /// <summary>
    /// Error message
    /// </summary>
    [NotNull]
    [JsonProperty("errmsg")]
    [JsonPropertyName("errmsg")]
    public string ErrorMessage { get; set; } = default!;
    /// <summary>
    /// ID of calendar notification scope member
    /// </summary>
    [NotNull]
    [JsonProperty("userid")]
    [JsonPropertyName("userid")]
    public string UserId { get; set; } = default!;
}
