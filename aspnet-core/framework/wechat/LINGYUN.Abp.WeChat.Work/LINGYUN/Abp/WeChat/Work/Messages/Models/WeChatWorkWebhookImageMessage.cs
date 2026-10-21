using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Enterprise WeChat Webhook picture message
/// </summary>
public class WeChatWorkWebhookImageMessage : WeChatWorkWebhookMessage
{
    /// <summary>
    /// Image message body
    /// </summary>
    [NotNull]
    [JsonProperty("image")]
    [JsonPropertyName("image")]
    public WebhookImageMessage Image { get; set; }
    /// <summary>
    /// Create an enterprise WeChat Webhook picture message
    /// </summary>
    /// <param name="image">Image message body</param>
    public WeChatWorkWebhookImageMessage(WebhookImageMessage image)
        : base("image")
    {
        Image = image;
    }
}
