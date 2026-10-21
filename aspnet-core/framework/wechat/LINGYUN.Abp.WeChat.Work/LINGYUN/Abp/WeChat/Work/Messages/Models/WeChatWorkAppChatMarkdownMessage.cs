using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Enterprise WeChat group chat markdown message
/// </summary>
public class WeChatWorkAppChatMarkdownMessage : WeChatWorkAppChatMessage
{
    public WeChatWorkAppChatMarkdownMessage(
        string agentId,
        string chatId,
        MarkdownMessage markdown) : base(agentId, chatId, "markdown")
    {
        Markdown = markdown;
    }
    /// <summary>
    /// markdown message
    /// </summary>
    [NotNull]
    [JsonProperty("markdown")]
    [JsonPropertyName("markdown")]
    public MarkdownMessage Markdown { get; set; }
    /// <summary>
    /// Indicates whether it is confidential information,
    /// 0 is not confidential
    /// 1Confidential
    /// Default is 0
    /// </summary>
    [JsonProperty("safe")]
    [JsonPropertyName("safe")]
    public byte Safe { get; set; }
}
