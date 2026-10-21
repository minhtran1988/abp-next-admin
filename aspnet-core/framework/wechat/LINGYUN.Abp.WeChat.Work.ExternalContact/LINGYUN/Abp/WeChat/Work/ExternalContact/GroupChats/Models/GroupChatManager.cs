using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.GroupChats.Models;
/// <summary>
/// Group administrator
/// </summary>
public class GroupChatManager
{
    /// <summary>
    /// Group administrator userid
    /// </summary>
    [NotNull]
    [JsonProperty("userid")]
    [JsonPropertyName("userid")]
    public string UserId { get; set; } = default!;
}
