using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.ExternalContact.GroupChats.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.GroupChats.Response;
/// <summary>
/// Get customer group details response results
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/92122" />
/// </remarks>
public class WeChatWorkGetGroupChatResponse : WeChatWorkResponse
{
    /// <summary>
    /// Customer group details
    /// </summary>
    [NotNull]
    [JsonProperty("group_chat")]
    [JsonPropertyName("group_chat")]
    public GroupChatInfo GroupChat { get; set; } = default!;
}
