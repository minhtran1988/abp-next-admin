using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Common.Members.Models;
/// <summary>
/// Member external attributes
/// </summary>
public abstract class ExternalAttribute
{
    /// <summary>
    /// Attribute name: You need to ensure that this attribute is created on the management side first, otherwise it will be ignored
    /// </summary>
    [NotNull]
    [JsonProperty("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
    /// <summary>
    /// Property type
    /// </summary>
    [NotNull]
    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public ExternalAttributeType Type { get; set; }
}
