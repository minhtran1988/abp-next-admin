using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.GroupChats.Models;
/// <summary>
/// Customer base
/// </summary>
public class GroupChat
{
    /// <summary>
    /// Customer group ID
    /// </summary>
    [NotNull]
    [JsonProperty("chat_id")]
    [JsonPropertyName("chat_id")]
    public string ChatId { get; set; } = default!;
    /// <summary>
    /// Customer group follow-up status
    /// </summary>
    [NotNull]
    [JsonProperty("status")]
    [JsonPropertyName("status")]
    public GroupChatStatus Status { get; set; }
}
