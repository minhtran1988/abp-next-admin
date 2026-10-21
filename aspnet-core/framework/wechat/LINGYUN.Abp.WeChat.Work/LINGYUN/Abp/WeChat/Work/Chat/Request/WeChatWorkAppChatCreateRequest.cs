using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Chat.Request;
public class WeChatWorkAppChatCreateRequest : WeChatWorkAppChatRequest
{
    public WeChatWorkAppChatCreateRequest(
        string agentId,
        string name,
        List<string> users,
        string? owner = null,
        string? chatId = null)
        : base(agentId)
    {
        Name = name;
        Owner = owner;
        Users = users;
        ChatId = chatId;
    }

    /// <summary>
    /// Group chat name, up to 50 utf8 characters, will be truncated if it exceeds
    /// </summary>
    [NotNull]
    [JsonProperty("name")]
    [JsonPropertyName("name")]
    public virtual string Name { get; set; }
    /// <summary>
    /// Specify the id of the group owner.
    /// If not specified, the system will randomly select one person from the userlist as the group leader.
    /// </summary>
    [CanBeNull]
    [JsonProperty("owner")]
    [JsonPropertyName("owner")]
    public virtual string? Owner { get; set; }
    /// <summary>
    /// List of group member ids.
    /// At least 2 people, at most 2000 people
    /// </summary>
    [NotNull]
    [JsonProperty("userlist")]
    [JsonPropertyName("userlist")]
    public virtual List<string> Users { get; set; }
    /// <summary>
    /// The unique identifier of the group chat, which cannot be repeated with an existing group;
    /// String type, up to 32 characters.
    /// Only characters 0-9 and letters a-zA-Z are allowed.
    /// If not filled in, the system will randomly generate a group ID.
    /// </summary>
    [CanBeNull]
    [JsonProperty("chatid")]
    [JsonPropertyName("chatid")]
    public virtual string? ChatId { get; set; }
}
