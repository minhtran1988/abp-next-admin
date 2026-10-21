using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Enterprise WeChat Webhook MarkdownV2 message
/// </summary>
public class WeChatWorkWebhookMarkdownV2Message : WeChatWorkWebhookMessage
{
    /// <summary>
    /// markdown_v2 message body
    /// </summary>
    [NotNull]
    [JsonProperty("markdown_v2")]
    [JsonPropertyName("markdown_v2")]
    public WebhookMarkdownV2Message Markdown { get; set; }
    /// <summary>
    /// Create an enterprise WeChat Webhook MarkdownV2 message
    /// </summary>
    /// <param name="markdown">markdown_v2 message body</param>
    public WeChatWorkWebhookMarkdownV2Message(WebhookMarkdownV2Message markdown)
    : base("markdown_v2")
    {
        Markdown = markdown;
    }
}
