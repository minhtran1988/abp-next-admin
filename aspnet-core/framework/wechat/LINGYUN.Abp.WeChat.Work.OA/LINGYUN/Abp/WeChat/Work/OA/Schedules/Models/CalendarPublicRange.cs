using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Schedules.Models;
/// <summary>
/// Calendar visibility
/// </summary>
public class CalendarPublicRange
{
    /// <summary>
    /// Public member list range
    /// </summary>
    [CanBeNull]
    [JsonProperty("userids")]
    [JsonPropertyName("userids")]
    public string[]? UserIds { get; set; }
    /// <summary>
    /// Public department list range
    /// </summary>
    [CanBeNull]
    [JsonProperty("partyids")]
    [JsonPropertyName("partyids")]
    public int[]? PartyIds { get; set; }
    public CalendarPublicRange(string[]? userIds = null, int[]? partyIds = null)
    {
        UserIds = userIds;
        PartyIds = partyIds;
    }
}
