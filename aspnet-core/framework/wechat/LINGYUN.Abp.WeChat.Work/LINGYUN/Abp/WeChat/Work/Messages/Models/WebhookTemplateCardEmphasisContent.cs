using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Key data style
/// </summary>
public class WebhookTemplateCardEmphasisContent
{
    /// <summary>
    /// The data content of the key data style is recommended to be no more than 10 words.
    /// </summary>
    [CanBeNull]
    [JsonProperty("title")]
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    /// <summary>
    /// The data description content of the key data style is recommended to be no more than 15 words.
    /// </summary>
    [CanBeNull]
    [JsonProperty("desc")]
    [JsonPropertyName("desc")]
    public string? Description { get; set; }
    /// <summary>
    /// Create a key data style
    /// </summary>
    /// <param name="title">Data content of key data style</param>
    /// <param name="description">Data description content of key data style</param>
    public WebhookTemplateCardEmphasisContent(string title, string? description = null)
    {
        Title = title;
        Description = description;
    }
}
