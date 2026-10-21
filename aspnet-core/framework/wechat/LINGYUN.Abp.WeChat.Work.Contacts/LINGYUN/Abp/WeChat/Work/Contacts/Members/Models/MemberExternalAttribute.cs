using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Members.Models;
/// <summary>
/// Member extended attributes
/// </summary>
public class MemberExternalAttribute
{
    /// <summary>
    /// Extended attribute list
    /// </summary>
    [NotNull]
    [JsonProperty("attrs")]
    [JsonPropertyName("attrs")]
    public MemberAttribute[] Attributes { get; set; } = default!;
}
