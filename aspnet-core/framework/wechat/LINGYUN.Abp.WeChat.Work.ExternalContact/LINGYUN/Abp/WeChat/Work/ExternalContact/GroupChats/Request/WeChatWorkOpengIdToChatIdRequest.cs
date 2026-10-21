using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.GroupChats.Request;
/// <summary>
/// Customer group opengid conversion request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/94822" />
/// </remarks>
public class WeChatWorkOpengIdToChatIdRequest : WeChatWorkRequest
{
    /// <summary>
    /// The group ID obtained by the mini program on WeChat
    /// </summary>
    [NotNull]
    [JsonProperty("opengid")]
    [JsonPropertyName("opengid")]
    public string OpengId { get; }
    public WeChatWorkOpengIdToChatIdRequest(string opengId)
    {
        Check.NotNullOrWhiteSpace(opengId, nameof(opengId));

        OpengId = opengId;
    }
}
