using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.ExternalContact.GroupChats.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.GroupChats.Response;
/// <summary>
/// Get the customer group list response results
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/92120" />
/// </remarks>
public class WeChatWorkGetGroupChatListResponse : WeChatWorkResponse
{
    /// <summary>
    /// Customer group list
    /// </summary>
    [NotNull]
    [JsonProperty("group_chat_list")]
    [JsonPropertyName("group_chat_list")]
    public GroupChat[] GroupChatList { get; set; } = default!;
    /// <summary>
    /// Paging cursor, fill in the next request to obtain subsequent paging records. If this field returns empty it means there is no more data
    /// </summary>
    [CanBeNull]
    [JsonProperty("next_cursor")]
    [JsonPropertyName("next_cursor")]
    public string? NextCursor { get; set; }
}
