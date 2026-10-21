using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Key data style
/// </summary>
public class TemplateCardEmphasisContent
{
    [Newtonsoft.Json.JsonConstructor]
    [System.Text.Json.Serialization.JsonConstructor]
    public TemplateCardEmphasisContent(
        string title = "", 
        string description = "")
    {
        Title = title;
        Description = description;
    }

    /// <summary>
    /// The data content of the key data style is recommended to be no more than 14 characters.
    /// </summary>
    [CanBeNull]
    [JsonProperty("title")]
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    /// <summary>
    /// The data description content of the key data style is recommended to be no more than 22 words.
    /// </summary>
    [CanBeNull]
    [JsonProperty("desc")]
    [JsonPropertyName("desc")]
    public string? Description { get; set; }
}
