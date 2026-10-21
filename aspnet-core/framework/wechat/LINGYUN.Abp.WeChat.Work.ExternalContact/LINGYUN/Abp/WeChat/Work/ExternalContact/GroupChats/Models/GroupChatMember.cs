using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.GroupChats.Models;
/// <summary>
/// Group members
/// </summary>
public class GroupChatMember
{
    /// <summary>
    /// Group member id
    /// </summary>
    [NotNull]
    [JsonProperty("userid")]
    [JsonPropertyName("userid")]
    public string UserId { get; set; } = default!;
    /// <summary>
    /// Member type
    /// </summary>
    [NotNull]
    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public GroupChatMemberType Type { get; set; }
    /// <summary>
    /// The unique identity of the external contact on the WeChat open platform (WeChat unionid). Through this field, the enterprise can associate the external contact with the official account/mini program user. <br />
    /// This field only exists when the group member type is a WeChat user (including corporate members who have not added friends), and the company is bound to a WeChat developer ID
    /// </summary>
    [CanBeNull]
    [JsonProperty("unionid")]
    [JsonPropertyName("unionid")]
    public string? UnionId { get; set; }
    /// <summary>
    /// Group entry time
    /// </summary>
    [NotNull]
    [JsonProperty("join_time")]
    [JsonPropertyName("join_time")]
    public long JoinTime { get; set; }
    /// <summary>
    /// How to join the group
    /// </summary>
    [NotNull]
    [JsonProperty("join_scene")]
    [JsonPropertyName("join_scene")]
    public GroupChatMemberJoinScene JoinScene { get; set; }
    /// <summary>
    /// Inviter. Currently, this value will be returned only when invited to the group by an internal member of the company.
    /// </summary>
    [CanBeNull]
    [JsonProperty("invitor")]
    [JsonPropertyName("invitor")]
    public GroupChatInvitor? Invitor { get; set; }
    /// <summary>
    /// Nickname in the group
    /// </summary>
    [CanBeNull]
    [JsonProperty("group_nickname")]
    [JsonPropertyName("group_nickname")]
    public string? GroupNickname { get; set; }
    /// <summary>
    /// Name. Only returned if need_name = 1<br />
    /// If it is a WeChat user, return the name set in WeChat<br />
    /// If it is an enterprise WeChat contact, return the alias or real name set for external display.
    /// </summary>
    [CanBeNull]
    [JsonProperty("name")]
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
