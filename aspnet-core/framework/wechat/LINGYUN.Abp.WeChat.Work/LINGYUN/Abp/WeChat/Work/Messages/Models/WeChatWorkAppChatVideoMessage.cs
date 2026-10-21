using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Enterprise WeChat group chat language message
/// </summary>
public class WeChatWorkAppChatVideoMessage : WeChatWorkAppChatMessage
{
    public WeChatWorkAppChatVideoMessage(
        string agentId,
        string chatId,
        VideoMessage video) : base(agentId, chatId, "video")
    {
        Video = video;
    }
    /// <summary>
    /// Video media file
    /// </summary>
    [NotNull]
    [JsonProperty("video")]
    [JsonPropertyName("video")]
    public VideoMessage Video { get; set; }
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
