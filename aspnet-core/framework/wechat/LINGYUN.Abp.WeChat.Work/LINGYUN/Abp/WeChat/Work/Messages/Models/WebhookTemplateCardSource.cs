using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Card source style information
/// </summary>
public class WebhookTemplateCardSource
{
    /// <summary>
    /// URL of the source image
    /// </summary>
    [CanBeNull]
    [JsonProperty("icon_url")]
    [JsonPropertyName("icon_url")]
    public string? IconUrl { get; set; }
    /// <summary>
    /// The description of the source image is recommended to be no more than 13 words.
    /// </summary>
    [CanBeNull]
    [JsonProperty("desc")]
    [JsonPropertyName("desc")]
    public string? Description { get; set; }
    /// <summary>
    /// The color of the source text, currently supported: 0 (default) gray, 1 black, 2 red, 3 green
    /// </summary>
    [CanBeNull]
    [JsonProperty("desc_color")]
    [JsonPropertyName("desc_color")]
    public int? DescriptionColor { get; set; }
    [System.Text.Json.Serialization.JsonConstructor]
    public WebhookTemplateCardSource(
        string? iconUrl = null,
        string? description = null,
        int? descriptionColor = 0)
    {
        IconUrl = iconUrl;
        Description = description;
        DescriptionColor = descriptionColor;
    }
    /// <summary>
    /// Create a gray card source style
    /// </summary>
    /// <param name="iconUrl">URL of the source image</param>
    /// <param name="description">Description of the source image</param>
    /// <returns></returns>
    public static WebhookTemplateCardSource Grey(string iconUrl, string? description = null)
    {
        return new WebhookTemplateCardSource(iconUrl, description, 0);
    }
    /// <summary>
    /// Create a black card source style
    /// </summary>
    /// <param name="iconUrl">URL of the source image</param>
    /// <param name="description">Description of the source image</param>
    /// <returns></returns>
    public static WebhookTemplateCardSource Black(string iconUrl, string? description = null)
    {
        return new WebhookTemplateCardSource(iconUrl, description, 1);
    }
    /// <summary>
    /// Create a red card source style
    /// </summary>
    /// <param name="iconUrl">URL of the source image</param>
    /// <param name="description">Description of the source image</param>
    /// <returns></returns>
    public static WebhookTemplateCardSource Red(string iconUrl, string? description = null)
    {
        return new WebhookTemplateCardSource(iconUrl, description, 2);
    }
    /// <summary>
    /// Create a green card source style
    /// </summary>
    /// <param name="iconUrl">URL of the source image</param>
    /// <param name="description">Description of the source image</param>
    /// <returns></returns>
    public static WebhookTemplateCardSource Green(string iconUrl, string? description = null)
    {
        return new WebhookTemplateCardSource(iconUrl, description, 3);
    }
}
