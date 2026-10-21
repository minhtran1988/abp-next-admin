using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.GroupChats.Request;
/// <summary>
/// Get customer group details
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/92122" />
/// </remarks>
public class WeChatWorkGetGroupChatRequest : WeChatWorkRequest
{
    /// <summary>
    /// Customer group ID
    /// </summary>
    [NotNull]
    [JsonProperty("chat_id")]
    [JsonPropertyName("chat_id")]
    public string ChatId { get; }
    /// <summary>
    /// Whether it is necessary to return the name of the group member group_chat.member_list.name. 0-Do not return; 1-Return. Not returned by default
    /// </summary>
    [CanBeNull]
    [JsonProperty("need_name")]
    [JsonPropertyName("need_name")]
    public int? NeedName { get; }
    public WeChatWorkGetGroupChatRequest(string chatId, bool needName = false)
    {
        Check.NotNullOrWhiteSpace(chatId, nameof(chatId));

        ChatId = chatId;
        NeedName = needName ? 1 : 0;
    }
}
