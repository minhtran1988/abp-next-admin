using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.GroupChats.Models;
/// <summary>
/// Customer group details
/// </summary>
public class GroupChatInfo
{
    /// <summary>
    /// Customer group ID
    /// </summary>
    [NotNull]
    [JsonProperty("chat_id")]
    [JsonPropertyName("chat_id")]
    public string ChatId { get; set; } = default!;
    /// <summary>
    /// Group name
    /// </summary>
    [NotNull]
    [JsonProperty("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
    /// <summary>
    /// Group owner ID
    /// </summary>
    [NotNull]
    [JsonProperty("owner")]
    [JsonPropertyName("owner")]
    public string Owner { get; set; } = default!;
    /// <summary>
    /// Group creation time
    /// </summary>
    [NotNull]
    [JsonProperty("create_time")]
    [JsonPropertyName("create_time")]
    public long CreateTime { get; set; }
    /// <summary>
    /// Group announcement
    /// </summary>
    [CanBeNull]
    [JsonProperty("notice")]
    [JsonPropertyName("notice")]
    public string? Notice { get; set; }
    /// <summary>
    /// Group member list
    /// </summary>
    [NotNull]
    [JsonProperty("member_list")]
    [JsonPropertyName("member_list")]
    public GroupChatMember[] MemberList { get; set; } = default!;
    /// <summary>
    /// Group administrator list
    /// </summary>
    [NotNull]
    [JsonProperty("admin_list")]
    [JsonPropertyName("admin_list")]
    public GroupChatManager[] AdminList { get; set; } = default!;
    /// <summary>
    /// Current group member version number. It can cooperate with customer group change events to reduce the number of active calls to this interface.
    /// </summary>
    [NotNull]
    [JsonProperty("member_version")]
    [JsonPropertyName("member_version")]
    public string MemberVersion { get; set; } = default!;
}
