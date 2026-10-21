using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Enterprise WeChat text message
/// </summary>
public class WeChatWorkTextMessage : WeChatWorkMessage
{
    public WeChatWorkTextMessage(
        string agentId,
        TextMessage text) : base(agentId, "text")
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
    /// <summary>
    /// Indicates whether to enable id translation, 0 means no, 1 means yes, and the default is 0.
    /// Only needed for third-party applications
    /// Enterprise self-built applications can be ignored.
    /// </summary>
    [JsonProperty("enable_id_trans")]
    [JsonPropertyName("enable_id_trans")]
    public byte EnableIdTrans { get; set; }
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
