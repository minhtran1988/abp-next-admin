using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Enterprise WeChat Webhook template card message
/// </summary>
public class WeChatWorkWebhookTemplateCardMessage : WeChatWorkWebhookMessage
{
    /// <summary>
    /// Template card message body
    /// </summary>
    [NotNull]
    [JsonProperty("template_card")]
    [JsonPropertyName("template_card")]
    public WebhookTemplateCardMessage TemplateCard { get; set; }
    /// <summary>
    /// Create an enterprise WeChat Webhook template card message
    /// </summary>
    /// <param name="templateCard">Template card message body</param>
    public WeChatWorkWebhookTemplateCardMessage(WebhookTemplateCardMessage templateCard)
        : base("template_card")
    {
        TemplateCard = templateCard;
    }
}
