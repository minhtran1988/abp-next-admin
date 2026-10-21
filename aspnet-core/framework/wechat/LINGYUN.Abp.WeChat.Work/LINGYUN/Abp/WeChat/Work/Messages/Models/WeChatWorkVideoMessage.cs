using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Enterprise WeChat language message
/// </summary>
public class WeChatWorkVideoMessage : WeChatWorkMessage
{
    public WeChatWorkVideoMessage(
        string agentId,
        VideoMessage video) : base(agentId, "video")
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
    public int Safe { get; set; }
    /// <summary>
    /// Indicates whether to enable duplicate message checking, 0 means no, 1 means yes, the default is 0
    /// </summary>
    [JsonProperty("enable_duplicate_check")]
    [JsonPropertyName("enable_duplicate_check")]
    public byte EnableDuplicateCheck { get; set; }
    /// <summary>
    /// Indicates whether to repeat the message check time interval, the default is 1800s, the maximum is no more than 4 hours
    /// </summary>
    [JsonProperty("duplicate_check_interval")]
    [JsonPropertyName("duplicate_check_interval")]
    public int DuplicateCheckInterval { get; set; } = 1800;
}
