using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Source text color
/// </summary>
public enum DescriptionColor
{
    Gray = 0,
    Black = 1,
    Red = 2,
    Green = 3,
}
/// <summary>
/// Card source style information
/// </summary>
public class TemplateCardSource
{
    [Newtonsoft.Json.JsonConstructor]
    [System.Text.Json.Serialization.JsonConstructor]
    public TemplateCardSource(
        string iconUrl = "", 
        string description = "",
        DescriptionColor descriptionColor = DescriptionColor.Gray)
    {
        IconUrl = iconUrl;
        Description = description;
        DescriptionColor = descriptionColor;
    }

    /// <summary>
    /// The url of the source image. The recommended size of the source image is 72*72
    /// </summary>
    [CanBeNull]
    [JsonProperty("icon_url")]
    [JsonPropertyName("icon_url")]
    public string IconUrl { get; set; }
    /// <summary>
    /// The description of the source image is recommended to be no more than 20 words (supports ID translation)
    /// </summary>
    [CanBeNull]
    [JsonProperty("desc")]
    [JsonPropertyName("desc")]
    public string Description { get; set; }
    /// <summary>
    /// The color of the source text, currently supported: 0 (default) gray, 1 black, 2 red, 3 green
    /// </summary>
    [CanBeNull]
    [JsonProperty("desc_color")]
    [JsonPropertyName("desc_color")]
    public DescriptionColor DescriptionColor { get; set; }
}
