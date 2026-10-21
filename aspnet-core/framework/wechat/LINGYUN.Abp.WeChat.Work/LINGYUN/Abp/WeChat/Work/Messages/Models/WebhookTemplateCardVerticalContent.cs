using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Card secondary vertical content
/// </summary>
public class WebhookTemplateCardVerticalContent
{
    /// <summary>
    /// The secondary title of the card is recommended to be no more than 26 words.
    /// </summary>
    [CanBeNull]
    [JsonProperty("title")]
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    /// <summary>
    /// Second level ordinary text, it is recommended not to exceed 112 words
    /// </summary>
    [CanBeNull]
    [JsonProperty("desc")]
    [JsonPropertyName("desc")]
    public string? Description { get; set; }
    /// <summary>
    /// Create a card secondary vertical content
    /// </summary>
    /// <param name="title">Card secondary title</param>
    /// <param name="description">Level two ordinary text</param>
    public WebhookTemplateCardVerticalContent(string title, string? description = null)
    {
        Title = title;
        Description = description;
    }
}
