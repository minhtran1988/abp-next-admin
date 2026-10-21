using JetBrains.Annotations;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Webhook Markdown message body
/// </summary>
public class WebhookMarkdownMessage
{
    /// <summary>
    /// markdown content, up to 4096 bytes, must be utf8 encoded
    /// </summary>
    [NotNull]
    [StringLength(4096)]
    [JsonProperty("content")]
    [JsonPropertyName("content")]
    public string Content { get; set; }
    /// <summary>
    /// Create a Webhook Markdown message body
    /// </summary>
    /// <param name="content">markdown content, up to 4096 bytes, must be utf8 encoding</param>
    public WebhookMarkdownMessage(string content)
    {
        Content = Check.NotNullOrWhiteSpace(content, nameof(content), 4096);
    }
}
