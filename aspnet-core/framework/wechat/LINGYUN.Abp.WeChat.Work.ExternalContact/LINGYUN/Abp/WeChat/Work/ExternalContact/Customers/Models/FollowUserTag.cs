using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Customers.Models;
/// <summary>
/// Tags tagged by external contacts
/// </summary>
public class FollowUserTag
{
    /// <summary>
    /// The member adds the group name tagged by this external contact
    /// </summary>
    [CanBeNull]
    [JsonProperty("group_name")]
    [JsonPropertyName("group_name")]
    public string? GroupName { get; set; }
    /// <summary>
    /// The member adds the tag name of this external contact
    /// </summary>
    [NotNull]
    [JsonProperty("tag_name")]
    [JsonPropertyName("tag_name")]
    public string TagName { get; set; } = default!;
    /// <summary>
    /// The member adds the tag type of this external contact
    /// </summary>
    [NotNull]
    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public FollowUserTagType Type { get; set; }
    /// <summary>
    /// The member adds the id of the enterprise label marked by this external contact. User-defined type labels (type=2) are not returned.
    /// </summary>
    [CanBeNull]
    [JsonProperty("tag_id")]
    [JsonPropertyName("tag_id")]
    public string? TagId { get; set; }
}
