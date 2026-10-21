using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Enterprise WeChat group chat text message
/// </summary>
public class WeChatWorkAppChatTextMessage : WeChatWorkAppChatMessage
{
    public WeChatWorkAppChatTextMessage(
        string agentId,
        string chatId,
        TextMessage text) : base(agentId, chatId, "text")
    {
        Text = text;
    }

    /// <summary>
    /// Message content, up to 2048 bytes, will be truncated if exceeded (supports id translation)
    /// </summary>
    [NotNull]
    [JsonProperty("text")]
    [JsonPropertyName("text")]
    public TextMessage Text { get; set; }
    /// <summary>
    /// Indicates whether it is confidential information,
    /// 0 means it can be shared externally.
    /// 1 means it cannot be shared and the content displays a watermark.
    /// Default is 0
    /// </summary>
    [JsonProperty("safe")]
    [JsonPropertyName("safe")]
    public int Safe { get; set; }
}
