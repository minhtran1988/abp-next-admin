using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Common.Members.Models;
/// <summary>
/// Web page type attribute
/// </summary>
public class ExternalWebAttribute : ExternalAttribute
{
    /// <summary>
    /// Web page
    /// </summary>
    [NotNull]
    [JsonProperty("web")]
    [JsonPropertyName("web")]
    public ExternalWebModel Web { get; set; } = default!;
}

public class ExternalWebModel
{
    /// <summary>
    /// The URL of the web page must contain http or https headers
    /// </summary>
    [NotNull]
    [JsonProperty("url")]
    [JsonPropertyName("url")]
    public string Url { get; set; } = default!;
    /// <summary>
    /// The display title of the web page, the length is limited to 12 UTF8 characters
    /// </summary>
    [NotNull]
    [JsonProperty("title")]
    [JsonPropertyName("title")]
    public string Title { get; set; } = default!;
}
