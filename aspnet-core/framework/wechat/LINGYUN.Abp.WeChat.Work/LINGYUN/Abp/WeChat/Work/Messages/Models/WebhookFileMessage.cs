using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Webhook file message body
/// </summary>
public class WebhookFileMessage
{
    /// <summary>
    /// File id, obtained through the file upload interface below
    /// </summary>
    [NotNull]
    [JsonProperty("media_id")]
    [JsonPropertyName("media_id")]
    public string MediaId { get; set; }
    /// <summary>
    /// Create a Webhook file message body
    /// </summary>
    /// <param name="mediaId">File id, obtained through the file upload interface below</param>
    public WebhookFileMessage(string mediaId)
    {
        MediaId = mediaId;
    }
}
