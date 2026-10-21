using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Webhook image message body
/// </summary>
public class WebhookImageMessage
{
    /// <summary>
    /// Base64 encoding of image content
    /// </summary>
    [NotNull]
    [JsonProperty("base64")]
    [JsonPropertyName("base64")]
    public string Base64 { get; set; }
    /// <summary>
    /// md5 of image
    /// </summary>
    [NotNull]
    [JsonProperty("md5")]
    [JsonPropertyName("md5")]
    public string Md5 { get; set; }
    /// <summary>
    /// Create a Webhook image message body
    /// </summary>
    /// <param name="base64">base64 encoding of image content</param>
    /// <param name="md5">md5 of the picture</param>
    public WebhookImageMessage(string base64, string md5)
    {
        Base64 = base64;
        Md5 = md5;
    }
}
