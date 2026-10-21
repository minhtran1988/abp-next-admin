using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Response;
/// <summary>
/// Enterprise WeChat sends message response
/// </summary>
public class WeChatWorkMessageResponse : WeChatWorkResponse
{
    /// <summary>
    /// Illegal userids are not case-sensitive and must be converted to lowercase.
    /// </summary>
    [JsonProperty("invaliduser")]
    [JsonPropertyName("invaliduser")]
    public string? InvalidUser { get; set; }
    /// <summary>
    /// Illegal partyid
    /// </summary>
    [JsonProperty("invalidparty")]
    [JsonPropertyName("invalidparty")]
    public string? InvalidParty { get; set; }
    /// <summary>
    /// Illegal tag id
    /// </summary>
    [JsonProperty("invalidtag")]
    [JsonPropertyName("invalidtag")]
    public string? InvalidTag { get; set; }
    /// <summary>
    /// There is no basic interface license (including expired) userid
    /// </summary>
    [JsonProperty("unlicenseduser")]
    [JsonPropertyName("unlicenseduser")]
    public string? UnLicensedUser { get; set; }
    /// <summary>
    /// Message id, used to withdraw application messages
    /// </summary>
    [JsonProperty("msgid")]
    [JsonPropertyName("msgid")]
    public string? MsgId { get; set; }
    /// <summary>
    /// Only template card messages whose message types are "button interactive", "voting selection" and "multiple selection" are returned,
    /// The application can use response_code to call the update template card message interface, which is valid within 72 hours and can only be used once.
    /// </summary>
    [JsonProperty("response_code")]
    [JsonPropertyName("response_code")]
    public string? ResponseCode { get; set; }
}
