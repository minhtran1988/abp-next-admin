using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Enterprise WeChat group chat file message
/// </summary>
public class WeChatWorkAppChatFileMessage : WeChatWorkAppChatMessage
{
    public WeChatWorkAppChatFileMessage(
        string agentId,
        string chatId,
        MediaMessage file) : base(agentId, chatId, "file")
    {
        File = file;
    }
    /// <summary>
    /// Media files
    /// </summary>
    [NotNull]
    [JsonProperty("file")]
    [JsonPropertyName("file")]
    public MediaMessage File { get; set; }
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
