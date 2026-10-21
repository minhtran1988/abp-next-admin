using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Chat.Response;

public class WeChatWorkAppChatCreateResponse : WeChatWorkResponse
{
    /// <summary>
    /// The only sign of group chat
    /// </summary>
    [JsonProperty("chatid")]
    [JsonPropertyName("chatid")]
    public virtual string ChatId { get; set; } = default!;
}
