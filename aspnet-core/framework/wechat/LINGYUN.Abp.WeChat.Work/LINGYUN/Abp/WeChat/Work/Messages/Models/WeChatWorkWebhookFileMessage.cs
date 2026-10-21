using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Enterprise WeChat Webhook file message
/// </summary>
public class WeChatWorkWebhookFileMessage : WeChatWorkWebhookMessage
{
    /// <summary>
    /// File message body
    /// </summary>
    [NotNull]
    [JsonProperty("file")]
    [JsonPropertyName("file")]
    public WebhookFileMessage File { get; set; }
    /// <summary>
    /// Create an enterprise WeChat Webhook file message
    /// </summary>
    /// <param name="file">File message body</param>
    public WeChatWorkWebhookFileMessage(WebhookFileMessage file)
        : base("file")
    {
        File = file;
    }
}
