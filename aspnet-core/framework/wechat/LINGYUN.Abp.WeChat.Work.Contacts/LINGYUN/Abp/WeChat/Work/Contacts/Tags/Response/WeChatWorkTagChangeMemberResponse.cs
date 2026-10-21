using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Tags.Response;
/// <summary>
/// Tag member change response parameters
/// </summary>
public class WeChatWorkTagChangeMemberResponse : WeChatWorkResponse
{
    /// <summary>
    /// If part of the userid is illegal, return
    /// </summary>
    [CanBeNull]
    [JsonProperty("invalidlist")]
    [JsonPropertyName("invalidlist")]
    public string? InvalidList { get; set; }
    /// <summary>
    /// If part of the partylist is illegal, return
    /// </summary>
    [CanBeNull]
    [JsonProperty("invalidparty")]
    [JsonPropertyName("invalidparty")]
    public int[]? InvalidPart { get; set; }
}
