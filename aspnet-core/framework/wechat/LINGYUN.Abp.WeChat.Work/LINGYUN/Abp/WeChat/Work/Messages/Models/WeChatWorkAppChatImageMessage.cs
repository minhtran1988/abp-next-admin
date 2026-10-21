using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Corporate WeChat group chat picture message
/// </summary>
public class WeChatWorkAppChatImageMessage : WeChatWorkAppChatMessage
{
    public WeChatWorkAppChatImageMessage(
        string agentId,
        string chatId,
        MediaMessage image) : base(agentId, chatId, "image")
    {
        Image = image;
    }
    /// <summary>
    /// Image media files
    /// </summary>
    [NotNull]
    [JsonProperty("image")]
    [JsonPropertyName("image")]
    public MediaMessage Image { get; set; }
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
