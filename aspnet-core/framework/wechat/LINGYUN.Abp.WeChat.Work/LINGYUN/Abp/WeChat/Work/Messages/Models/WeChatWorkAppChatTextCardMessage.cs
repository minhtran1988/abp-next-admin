using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Enterprise WeChat group chat text card message
/// </summary>
public class WeChatWorkAppChatTextCardMessage : WeChatWorkAppChatMessage
{
    public WeChatWorkAppChatTextCardMessage(
        string agentId,
        string chatId,
        TextCardMessage textcard) : base(agentId, chatId, "textcard")
    {
        TextCard = textcard;
    }
    /// <summary>
    /// Card message
    /// </summary>
    [NotNull]
    [JsonProperty("textcard")]
    [JsonPropertyName("textcard")]
    public TextCardMessage TextCard { get; set; }
    /// <summary>
    /// Indicates whether it is confidential information,
    /// 0 means it can be shared externally.
    /// 1 means it cannot be shared and the content displays a watermark.
    /// Default is 0
    /// </summary>
    [JsonProperty("safe")]
    [JsonPropertyName("safe")]
    public byte Safe { get; set; }
}
