using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Chat.Request;
public class WeChatWorkAppChatUpdateRequest : WeChatWorkAppChatRequest
{
    public WeChatWorkAppChatUpdateRequest(
        string agentId,
        string chatId,
        string? name = null,
        string? owner = null,
        List<string>? addUsers = null,
        List<string>? delUsers = null)
        : base(agentId)
    {
        Name = name;
        Owner = owner;
        AddUsers = addUsers;
        DelUsers = delUsers;
        ChatId = chatId;
    }

    /// <summary>
    /// New group chat name. If no update is required, please ignore this parameter. Maximum 50 utf8 characters, exceeding will be truncated
    /// </summary>
    [CanBeNull]
    [JsonProperty("name")]
    [JsonPropertyName("name")]
    public virtual string? Name { get; set; }
    /// <summary>
    /// The id of the new group owner.
    /// If no update is required, please ignore this parameter. The course group chat group owner must be in the set group owner list
    /// </summary>
    [CanBeNull]
    [JsonProperty("owner")]
    [JsonPropertyName("owner")]
    public virtual string? Owner { get; set; }
    /// <summary>
    /// Add member id list
    /// </summary>
    [CanBeNull]
    [JsonProperty("add_user_list")]
    [JsonPropertyName("add_user_list")]
    public virtual List<string>? AddUsers { get; set; }
    /// <summary>
    /// ID list of kicked members
    /// </summary>
    [CanBeNull]
    [JsonProperty("del_user_list")]
    [JsonPropertyName("del_user_list")]
    public virtual List<string>? DelUsers { get; set; }
    /// <summary>
    /// Group chat id
    /// </summary>
    [NotNull]
    [JsonProperty("chatid")]
    [JsonPropertyName("chatid")]
    public virtual string ChatId { get; set; }
}
