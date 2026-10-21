using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Request;
/// <summary>
/// Withdraw message request carrier
/// </summary>
public class WeChatWorkMessageReCallRequest : WeChatWorkRequest
{
    /// <summary>
    /// Call interface credentials
    /// </summary>
    [System.Text.Json.Serialization.JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    public string AccessToken { get; set; }
    /// <summary>
    /// Message ID
    /// </summary>
    [JsonProperty("msgid")]
    [JsonPropertyName("msgid")]
    public string MessageId { get; set; }
    public WeChatWorkMessageReCallRequest(string accessToken, string messageId)
    {
        AccessToken = accessToken;
        MessageId = messageId;
    }
}
