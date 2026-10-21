using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Enterprise WeChat Webhook Voice Message
/// </summary>
public class WeChatWorkWebhookVoiceMessage : WeChatWorkWebhookMessage
{
    /// <summary>
    /// Voice message body
    /// </summary>
    [NotNull]
    [JsonProperty("voice")]
    [JsonPropertyName("voice")]
    public WebhookVoiceMessage Voice { get; set; }
    /// <summary>
    /// Create an enterprise WeChat Webhook voice message
    /// </summary>
    /// <param name="voice">Voice message body</param>
    public WeChatWorkWebhookVoiceMessage(WebhookVoiceMessage voice)
        : base("voice")
    {
        Voice = voice;
    }
}
