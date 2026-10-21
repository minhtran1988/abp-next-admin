using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.MsgAudits.Request;
/// <summary>
/// Get single chat session consent request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/91782" />
/// </remarks>
public class WeChatWorkCheckSingleAgreeRequest : WeChatWorkRequest
{
    /// <summary>
    /// The userid of the internal member
    /// </summary>
    [NotNull]
    [JsonProperty("userid")]
    [JsonPropertyName("userid")]
    public string UserId { get; }
    /// <summary>
    /// externalopenid of external member
    /// </summary>
    [NotNull]
    [JsonProperty("exteranalopenid")]
    [JsonPropertyName("exteranalopenid")]
    public string ExteranalOpenId { get; }
    public WeChatWorkCheckSingleAgreeRequest(string userId, string exteranalOpenId)
    {
        UserId = userId;
        ExteranalOpenId = exteranalOpenId;
    }
}
