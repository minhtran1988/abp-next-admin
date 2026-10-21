using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Members.Response;
/// <summary>
/// openid to userid response parameter
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/90202" />
/// </remarks>
public class WeChatWorkConvertToUserIdResponse : WeChatWorkResponse
{
    /// <summary>
    /// The openid’s corresponding member userid in Enterprise WeChat
    /// </summary>
    [NotNull]
    [JsonProperty("userid")]
    [JsonPropertyName("userid")]
    public string UserId { get; set; } = default!;
}
