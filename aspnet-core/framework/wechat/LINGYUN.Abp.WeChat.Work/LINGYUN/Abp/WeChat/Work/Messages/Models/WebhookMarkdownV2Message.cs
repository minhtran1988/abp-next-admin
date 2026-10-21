using JetBrains.Annotations;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Webhook MarkdownV2 message body
/// </summary>
public class WebhookMarkdownV2Message
{
    /// <summary>
    /// markdown_v2 content, the longest is no more than 4096 bytes, and must be utf8 encoded.
    /// </summary>
    /// <remarks>
    /// 1. markdown_v2 does not support font color and @group member syntax. For specific supported syntax, please refer to the instructions below<br />
    /// 2. The message content is in the client version below 4.1.36 (Android version is below 4.1.38). The message appears as plain text. It is recommended to use the latest client version for experience.
    /// </remarks>
    [NotNull]
    [StringLength(4096)]
    [JsonProperty("content")]
    [JsonPropertyName("content")]
    public string Content { get; set; }
    /// <summary>
    /// Create a Webhook MarkdownV2 message body
    /// </summary>
    /// <param name="content">markdown_v2 content, no longer than 4096 bytes, must be utf8 encoding</param>
    public WebhookMarkdownV2Message(string content)
    {
        Content = Check.NotNullOrWhiteSpace(content, nameof(content), 4096);
    }
}
