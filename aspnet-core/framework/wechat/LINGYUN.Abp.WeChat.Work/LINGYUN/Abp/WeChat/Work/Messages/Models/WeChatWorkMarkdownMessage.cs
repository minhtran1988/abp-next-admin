using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Enterprise WeChat markdown message
/// </summary>
public class WeChatWorkMarkdownMessage : WeChatWorkMessage
{
    public WeChatWorkMarkdownMessage(
        string agentId,
        MarkdownMessage markdown) : base(agentId, "markdown")
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
