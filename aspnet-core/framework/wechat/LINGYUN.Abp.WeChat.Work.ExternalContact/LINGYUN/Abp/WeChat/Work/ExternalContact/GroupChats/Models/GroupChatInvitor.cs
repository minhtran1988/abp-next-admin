using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.GroupChats.Models;
/// <summary>
/// Inviter
/// </summary>
public class GroupChatInvitor
{
    /// <summary>
    /// The userid of the inviter
    /// </summary>
    [NotNull]
    [JsonProperty("userid")]
    [JsonPropertyName("userid")]
    public string UserId { get; set; } = default!;
}
