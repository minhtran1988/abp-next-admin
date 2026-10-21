using Newtonsoft.Json;
using System;

namespace LINGYUN.Abp.PushPlus.Channel.Webhook;
/// <summary>
/// webhook
/// </summary>
public class PushPlusWebhook
{
    /// <summary>
    /// webhook number
    /// </summary>
    [JsonProperty("id")]
    public int Id { get; set; }
    /// <summary>
    /// webhook encoding
    /// </summary>
    [JsonProperty("webhookCode")]
    public string WebhookCode { get; set; } = default!;
    /// <summary>
    /// webhook name
    /// </summary>
    [JsonProperty("webhookName")]
    public string WebhookName { get; set; } = default!;
    /// <summary>
    /// webhook type;
    /// 1-Enterprise WeChat,
    /// 2-DingTalk,
    /// 3-Feishu,
    /// 4-server sauce
    /// </summary>
    [JsonProperty("webhookType")]
    public PushPlusWebhookType WebhookType { get; set; }
    /// <summary>
    /// The calling url address
    /// </summary>
    [JsonProperty("webhookUrl")]
    public string WebhookUrl { get; set; } = default!;
    /// <summary>
    /// Creation date
    /// </summary>
    [JsonProperty("createTime")]
    public DateTime CreateTime { get; set; }
}
