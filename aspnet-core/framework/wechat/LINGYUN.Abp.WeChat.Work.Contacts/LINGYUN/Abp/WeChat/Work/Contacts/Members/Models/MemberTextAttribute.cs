using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Members.Models;
/// <summary>
/// Member properties of text type
/// </summary>
public class MemberTextAttribute : MemberAttribute
{
    /// <summary>
    /// text
    /// </summary>
    [NotNull]
    [JsonProperty("text")]
    [JsonPropertyName("text")]
    public MemberTextModel Text { get; set; } = default!;
}

public class MemberTextModel
{
    /// <summary>
    /// text
    /// </summary>
    [NotNull]
    [JsonProperty("value")]
    [JsonPropertyName("value")]
    public string Value { get; set; } = default!;
}
