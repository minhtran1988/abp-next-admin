using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Authorize.Models;
/// <summary>
/// Enterprise WeChat user information
/// </summary>
public class WeChatWorkUserInfo
{
    /// <summary>
    /// Member UserID
    /// </summary>
    [NotNull]
    [JsonProperty("userid")]
    [JsonPropertyName("userid")]
    public string UserId { get; set; } = default!;
    /// <summary>
    /// Member ticket, up to 512 bytes, valid for 1800s
    /// </summary>
    [NotNull]
    [JsonProperty("user_ticket")]
    [JsonPropertyName("user_ticket")]
    public string UserTicket { get; set; } = default!;
}
