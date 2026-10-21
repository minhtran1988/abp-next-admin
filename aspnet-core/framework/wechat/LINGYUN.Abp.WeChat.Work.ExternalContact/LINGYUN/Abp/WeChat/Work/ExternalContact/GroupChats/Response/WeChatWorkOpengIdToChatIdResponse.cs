using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.GroupChats.Response;
/// <summary>
/// Customer group opengid conversion response result
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/94822" />
/// </remarks>
public class WeChatWorkOpengIdToChatIdResponse : WeChatWorkResponse
{
    /// <summary>
    /// Customer group ID, which can be used to call to obtain customer group details
    /// </summary>
    [NotNull]
    [JsonProperty("chat_id")]
    [JsonPropertyName("chat_id")]
    public string ChatId { get; set; } = default!;
}
