using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Members.Response;
/// <summary>
/// Get userid response parameter
/// </summary>
public class WeChatWorkGetUserIdResponse : WeChatWorkResponse
{
    /// <summary>
    /// Member UserID. The account corresponding to the management end must be unique within the enterprise. It is not case sensitive and the length is 1~64 bytes. Note: The value obtained by the third-party application is the userid of the ciphertext.
    /// </summary>
    [NotNull]
    [JsonProperty("userid")]
    [JsonPropertyName("userid")]
    public string UserId { get; set; } = default!;
}
