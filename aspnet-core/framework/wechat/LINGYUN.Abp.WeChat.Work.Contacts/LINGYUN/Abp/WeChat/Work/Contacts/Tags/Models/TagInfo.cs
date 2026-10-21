using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Tags.Models;
/// <summary>
/// tag
/// </summary>
public class TagInfo
{
    /// <summary>
    /// tag id
    /// </summary>
    [NotNull]
    [JsonProperty("tagid")]
    [JsonPropertyName("tagid")]
    public int TagId { get; set; }
    /// <summary>
    /// Tag name
    /// </summary>
    [NotNull]
    [JsonProperty("tagname")]
    [JsonPropertyName("tagname")]
    public string TagName { get; set; } = default!;
}
