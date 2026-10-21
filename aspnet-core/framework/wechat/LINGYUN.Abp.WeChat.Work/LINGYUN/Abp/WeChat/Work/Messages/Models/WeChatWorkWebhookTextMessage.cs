using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Enterprise WeChat Webhook text message
/// </summary>
public class WeChatWorkWebhookTextMessage : WeChatWorkWebhookMessage
{
    /// <summary>
    /// Text message body
    /// </summary>
    [NotNull]
    [JsonProperty("text")]
    [JsonPropertyName("text")]
    public WebhookTextMessage Text { get; set; }
    /// <summary>
    /// Create an enterprise WeChat Webhook text message
    /// </summary>
    /// <param name="text">Text message body</param>
    public WeChatWorkWebhookTextMessage(WebhookTextMessage text) 
        : base("text")
    {
        Text = text;
    }
}
