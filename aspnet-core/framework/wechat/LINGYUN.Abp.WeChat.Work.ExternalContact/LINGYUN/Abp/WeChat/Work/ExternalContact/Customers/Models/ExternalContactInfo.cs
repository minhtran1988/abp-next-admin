using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.Common.Members.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Customers.Models;
/// <summary>
/// External contact information
/// </summary>
public class ExternalContactInfo
{
    /// <summary>
    /// The userid of the external contact
    /// </summary>
    [NotNull]
    [JsonProperty("external_userid")]
    [JsonPropertyName("external_userid")]
    public string ExternalUserId { get; set; } = default!;
    /// <summary>
    /// Name of external contact
    /// </summary>
    /// <remarks>
    /// If it is a WeChat user, return its WeChat nickname. <br />
    /// If it is an enterprise WeChat contact, return the alias or real name set for external display.
    /// </remarks>
    [NotNull]
    [JsonProperty("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
    /// <summary>
    /// External contact avatar
    /// </summary>
    [CanBeNull]
    [JsonProperty("avatar")]
    [JsonPropertyName("avatar")]
    public string? Avatar { get; set; }
    /// <summary>
    /// Type of external contact
    /// </summary>
    [NotNull]
    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public ExternalContactType Type { get; set; }
    /// <summary>
    /// External contact gender
    /// </summary>
    [NotNull]
    [JsonProperty("gender")]
    [JsonPropertyName("gender")]
    public ExternalContactGender Gender { get; set; }
    /// <summary>
    /// The unique identity of the external contact on the WeChat open platform (WeChat unionid). Through this field, the enterprise can associate the external contact with the official account/mini program user. <br />
    /// This field only exists when the contact type is a WeChat user and the enterprise is bound to a WeChat developer ID
    /// </summary>
    [CanBeNull]
    [JsonProperty("unionid")]
    [JsonPropertyName("unionid")]
    public string? UnionId { get; set; }
    /// <summary>
    /// The position of the external contact. If the external company or user chooses to hide the position, it will not be returned. This field is only available when the contact type is an enterprise WeChat user.
    /// </summary>
    [CanBeNull]
    [JsonProperty("position")]
    [JsonPropertyName("position")]
    public string? Position { get; set; }
    /// <summary>
    /// The abbreviation of the company where the external contact is located, this field is only available when the contact type is an enterprise WeChat user
    /// </summary>
    [CanBeNull]
    [JsonProperty("corp_name")]
    [JsonPropertyName("corp_name")]
    public string? CorpName { get; set; }
    /// <summary>
    /// The subject name of the company where the external contact is located. This field is only available when the contact type is an enterprise WeChat user<br />
    /// Only available for enterprise self-built applications
    /// </summary>
    [CanBeNull]
    [JsonProperty("corp_full_name")]
    [JsonPropertyName("corp_full_name")]
    public string? CorpFullName { get; set; }
    /// <summary>
    /// Customized display information for external contacts
    /// </summary>
    [CanBeNull]
    [JsonProperty("external_profile")]
    [JsonPropertyName("external_profile")]
    public ExternalProfile? ExternalProfile { get; set; }
}
