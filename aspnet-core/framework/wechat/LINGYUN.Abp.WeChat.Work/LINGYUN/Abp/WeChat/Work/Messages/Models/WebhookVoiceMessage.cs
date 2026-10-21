using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Webhook voice message body
/// </summary>
public class WebhookVoiceMessage
{
    /// <summary>
    /// Voice file id, obtained through the file upload interface below
    /// </summary>
    [NotNull]
    [JsonProperty("media_id")]
    [JsonPropertyName("media_id")]
    public string MediaId { get; set; }
    /// <summary>
    /// Create a Webhook voice message body
    /// </summary>
    /// <param name="mediaId">Voice file id</param>
    public WebhookVoiceMessage(string mediaId)
    {
        MediaId = mediaId;
    }
}
