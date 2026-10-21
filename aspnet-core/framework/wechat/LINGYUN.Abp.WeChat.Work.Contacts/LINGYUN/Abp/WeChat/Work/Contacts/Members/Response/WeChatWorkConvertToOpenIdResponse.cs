using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Members.Response;
/// <summary>
/// userid to openid response parameter
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/90202" />
/// </remarks>
public class WeChatWorkConvertToOpenIdResponse : WeChatWorkResponse
{
    /// <summary>
    /// The openid corresponding to the userid of the enterprise WeChat member
    /// </summary>
    [NotNull]
    [JsonProperty("openid")]
    [JsonPropertyName("openid")]
    public string OpenId { get; set; } = default!;
}
