using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Picture style
/// </summary>
public class WebhookTemplateCardImage
{
    /// <summary>
    /// URL of the image
    /// </summary>
    [NotNull]
    [JsonProperty("url")]
    [JsonPropertyName("url")]
    public string Url { get; set; }
    /// <summary>
    /// The aspect ratio of the picture. The aspect ratio should be less than 2.25 and greater than 1.3. If this parameter is not filled in, the default is 1.3.
    /// </summary>
    [CanBeNull]
    [JsonProperty("aspect_ratio")]
    [JsonPropertyName("aspect_ratio")]
    public float? AspectRatio { get; set; }
    /// <summary>
    /// Create an image style
    /// </summary>
    /// <param name="url">URL of the image</param>
    /// <param name="aspectRatio">The aspect ratio of the picture, if this parameter is not filled in, the default is 1.3</param>
    public WebhookTemplateCardImage(string url, float? aspectRatio = 1.3f)
    {
        Url = url;
        AspectRatio = aspectRatio;
    }
}
