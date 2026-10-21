using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Members.Models;
/// <summary>
/// Member extended attributes
/// </summary>
public abstract class MemberAttribute
{
    /// <summary>
    /// Attribute name: When adding or updating operations, you need to first ensure that the attribute is created on the management side, otherwise it will be ignored.
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
    public AttributeType Type { get; set; }
}
