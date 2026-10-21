using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// The main content of the template card
/// </summary>
public class WebhookTemplateCardMainTitle
{
    /// <summary>
    /// The first-level title is recommended to be no more than 26 words. One of the first-level title main_title.title and the second-level ordinary text sub_title_text of the main content of the template card must be filled in
    /// </summary>
    [CanBeNull]
    [JsonProperty("title")]
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    /// <summary>
    /// Title auxiliary information, it is recommended not to exceed 30 words
    /// </summary>
    [CanBeNull]
    [JsonProperty("desc")]
    [JsonPropertyName("desc")]
    public string? Description { get; set; }
    /// <summary>
    /// Create the main content of a template card
    /// </summary>
    /// <param name="title">First-level title</param>
    /// <param name="description">Title auxiliary information</param>
    public WebhookTemplateCardMainTitle(string title, string? description = null)
    {
        Title = title;
        Description = description;
    }
}
