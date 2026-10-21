using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Members.Models;
/// <summary>
/// Member properties of web page type
/// </summary>
public class MemberWebAttribute : MemberAttribute
{
    /// <summary>
    /// Web page
    /// </summary>
    [NotNull]
    [JsonProperty("web")]
    [JsonPropertyName("web")]
    public MemberWebModel Web { get; set; } = default!;
}

public class MemberWebModel
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
