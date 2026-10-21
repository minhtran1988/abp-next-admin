using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.Messages;
/// <summary>
/// Enterprise WeChat Webhook message structure
/// </summary>
public abstract class WeChatWorkWebhookMessage : WeChatWorkRequest
{
    /// <summary>
    /// Message type
    /// </summary>
    [NotNull]
    [JsonProperty("msgtype")]
    [JsonPropertyName("msgtype")]
    public string MsgType { get; set; }
    protected WeChatWorkWebhookMessage(string msgType)
    {
        MsgType = Check.NotNullOrWhiteSpace(msgType, nameof(msgType));
    }
}
