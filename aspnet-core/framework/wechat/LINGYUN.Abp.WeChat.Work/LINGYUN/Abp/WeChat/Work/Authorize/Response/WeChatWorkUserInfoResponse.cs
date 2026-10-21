using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Authorize.Response;
/// <summary>
/// Enterprise WeChat user information response
/// </summary>
public class WeChatWorkUserInfoResponse : WeChatWorkResponse
{
    /// <summary>
    /// Member UserID
    /// </summary>
    [JsonProperty("userid")]
    [JsonPropertyName("userid")]
    public string UserId { get; set; } = default!;
    /// <summary>
    /// Member ticket, up to 512 bytes, valid for 1800s
    /// </summary>
    [JsonProperty("user_ticket")]
    [JsonPropertyName("user_ticket")]
    public string UserTicket { get; set; } = default!;
}
