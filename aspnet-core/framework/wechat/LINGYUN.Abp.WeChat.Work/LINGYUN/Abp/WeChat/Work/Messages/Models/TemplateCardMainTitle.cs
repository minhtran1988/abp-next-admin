using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Card ticket title
/// </summary>
public class TemplateCardMainTitle
{
    [Newtonsoft.Json.JsonConstructor]
    [System.Text.Json.Serialization.JsonConstructor]
    public TemplateCardMainTitle(
        string title = "", 
        string description = "")
    {
        Title = title;
        Description = description;
    }

    /// <summary>
    /// First-level title, it is recommended not to exceed 36 words. This field of text notification card is not required, but it is not possible to fill both this field and sub_title_text (support id translation)
    /// </summary>
    [CanBeNull]
    [JsonProperty("title")]
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    /// <summary>
    /// Title auxiliary information, it is recommended not to exceed 44 words (supports id translation)
    /// </summary>
    [CanBeNull]
    [JsonProperty("desc")]
    [JsonPropertyName("desc")]
    public string? Description { get; set; }
}
