using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Contacts.Models;
/// <summary>
/// External contact
/// </summary>
public class ExternalContactInfo
{
    /// <summary>
    /// Whether the member is marked as a customer
    /// </summary>
    [NotNull]
    [JsonProperty("is_customer")]
    [JsonPropertyName("is_customer")]
    public bool IsCustomer { get; set; }
    /// <summary>
    /// External contact temporary ID
    /// </summary>
    /// <remarks>
    /// 
    /// The temporary ID of an external contact is the unique identifier of an external contact. The enterprise can perform deduplication statistics on external contacts based on this ID. <br />
    /// But the temporary ID of the external contact is only unique in one round of traversal query (from the beginning of the first paging query to the completion of the last paging query);<br />
    /// Each time the first data page is requested (cursor is empty), the returned external contact temporary id and next_cursor will change.
    /// </remarks>
    [NotNull]
    [JsonProperty("tmp_openid")]
    [JsonPropertyName("tmp_openid")]
    public string TmpOpenId { get; set; } = default!;
    /// <summary>
    /// External userid of the external contact (returned only if it is a customer)
    /// </summary>
    [CanBeNull]
    [JsonProperty("external_userid")]
    [JsonPropertyName("external_userid")]
    public string? ExternalUserId { get; set; }
    /// <summary>
    /// Desensitized external contact nickname (returned only if it is another external contact)
    /// </summary>
    [CanBeNull]
    [JsonProperty("name")]
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    /// <summary>
    /// Add the enterprise member of this external contact or the group owner userid of the group chat where the external contact belongs
    /// </summary>
    [CanBeNull]
    [JsonProperty("follow_userid")]
    [JsonPropertyName("follow_userid")]
    public string? FollowUserId { get; set; }
    /// <summary>
    /// The group chat ID where the external contact is located (returned only if the group chat is marked as a customer group by members)
    /// </summary>
    [CanBeNull]
    [JsonProperty("chat_id")]
    [JsonPropertyName("chat_id")]
    public string? ChatId { get; set; }
    /// <summary>
    /// The group name of the group chat where the external contact belongs (returned only if the group chat has not been marked as a customer group by members)
    /// </summary>
    [CanBeNull]
    [JsonProperty("chat_name")]
    [JsonPropertyName("chat_name")]
    public string? ChatName { get; set; }
    /// <summary>
    /// The time when the external contact was first added/joined the group
    /// </summary>
    [NotNull]
    [JsonProperty("add_time")]
    [JsonPropertyName("add_time")]
    public long AddTime { get; set; }
}
