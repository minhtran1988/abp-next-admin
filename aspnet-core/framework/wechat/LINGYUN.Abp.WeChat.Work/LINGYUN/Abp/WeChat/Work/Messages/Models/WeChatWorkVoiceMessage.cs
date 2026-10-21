using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Enterprise WeChat language message
/// </summary>
public class WeChatWorkVoiceMessage : WeChatWorkMessage
{
    public WeChatWorkVoiceMessage(
        string agentId,
        MediaMessage voice) : base(agentId, "voice")
    {
        Voice = voice;
    }
    /// <summary>
    /// Voice media file
    /// </summary>
    [NotNull]
    [JsonProperty("voice")]
    [JsonPropertyName("voice")]
    public MediaMessage Voice { get; set; }
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
