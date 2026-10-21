using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Members.Request;
/// <summary>
/// openid to userid request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/90202" />
/// </remarks>
public class WeChatWorkConvertToUserIdRequest : WeChatWorkRequest
{
    /// <summary>
    /// After using enterprise payment, return the openid of the result
    /// </summary>
    [NotNull]
    [JsonProperty("openid")]
    [JsonPropertyName("openid")]
    public string OpenId { get; }
    public WeChatWorkConvertToUserIdRequest(string openId)
    {
        OpenId = Check.NotNullOrWhiteSpace(openId, nameof(openId));
    }
}

