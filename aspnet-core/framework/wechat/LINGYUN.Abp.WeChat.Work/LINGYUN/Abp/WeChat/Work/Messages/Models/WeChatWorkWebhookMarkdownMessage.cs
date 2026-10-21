using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Enterprise WeChat Webhook Markdown message
/// </summary>
public class WeChatWorkWebhookMarkdownMessage : WeChatWorkWebhookMessage
{
    /// <summary>
    /// Markdown message body
    /// </summary>
    [NotNull]
    [JsonProperty("markdown")]
    [JsonPropertyName("markdown")]
    public WebhookMarkdownMessage Markdown { get; set; }
    /// <summary>
    /// Create an enterprise WeChat Webhook Markdown message
    /// </summary>
    /// <param name="markdown">Markdown message body</param>
    public WeChatWorkWebhookMarkdownMessage(WebhookMarkdownMessage markdown)
    : base("markdown")
    {
        Markdown = markdown;
    }
}
