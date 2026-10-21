using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Authorize.Models;

public class WeChatWorkCode2Session
{
    /// <summary>
    /// The corpid of the company to which the user belongs
    /// </summary>
    [NotNull]
    [JsonProperty("corpid")]
    [JsonPropertyName("corpid")]
    public string CorpId { get; set; } = default!;
    /// <summary>
    /// The UserID of the user in the enterprise corresponds to the account on the management side and is unique within the enterprise. Note: The third-party applet returns the encrypted userid here
    /// </summary>
    [NotNull]
    [JsonProperty("userid")]
    [JsonPropertyName("userid")]
    public string UserId { get; set; } = default!;
    /// <summary>
    /// Session key
    /// </summary>
    [NotNull]
    [JsonProperty("session_key")]
    [JsonPropertyName("session_key")]
    public string SessionKey { get; set; } = default!;
    [CanBeNull]
    [JsonProperty("deviceid")]
    [JsonPropertyName("deviceid")]
    public string? DeviceId { get; set; } = default!;
}
