using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Chat.Models;
public class WeChatWorkAppChatInfo
{
    /// <summary>
    /// Group chat name
    /// </summary>
    [JsonProperty("name")]
    [JsonPropertyName("name")]
    public virtual string Name { get; set; } = default!;
    /// <summary>
    /// Group owner id
    /// </summary>
    [JsonProperty("owner")]
    [JsonPropertyName("owner")]
    public virtual string Owner { get; set; } = default!;
    /// <summary>
    /// Group member id list
    /// </summary>
    [JsonProperty("userlist")]
    [JsonPropertyName("userlist")]
    public virtual List<string> Users { get; set; } = default!;
    /// <summary>
    /// The only flag for group chat
    /// </summary>
    [JsonProperty("chatid")]
    [JsonPropertyName("chatid")]
    public virtual string ChatId { get; set; } = default!;
}
