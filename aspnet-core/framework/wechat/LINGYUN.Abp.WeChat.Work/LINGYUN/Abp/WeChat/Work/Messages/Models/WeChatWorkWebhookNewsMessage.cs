using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Enterprise WeChat Webhook graphic message
/// </summary>
public class WeChatWorkWebhookNewsMessage : WeChatWorkWebhookMessage
{
    /// <summary>
    /// Graphic message body
    /// </summary>
    [NotNull]
    [JsonProperty("news")]
    [JsonPropertyName("news")]
    public WebhookNewsMessage News { get; set; }
    /// <summary>
    /// Create an enterprise WeChat Webhook graphic message
    /// </summary>
    /// <param name="news">Graphic and text message body</param>
    public WeChatWorkWebhookNewsMessage(WebhookNewsMessage news)
        : base("news")
    {
        News = news;
    }
}
