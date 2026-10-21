using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.ExternalContact.Customers.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Customers.Response;
/// <summary>
/// Get customer details response parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/92114" />
/// </remarks>
public class WeChatWorkGetCustomerResponse : WeChatWorkResponse
{
    /// <summary>
    /// External contact information
    /// </summary>
    [NotNull]
    [JsonProperty("external_contact")]
    [JsonPropertyName("external_contact")]
    public ExternalContactInfo ExternalContact { get; set; } = default!;
    /// <summary>
    /// Enterprise members who added this external contact
    /// </summary>
    [NotNull]
    [JsonProperty("follow_user")]
    [JsonPropertyName("follow_user")]
    public List<FollowUser> FollowUser { get; set; } = default!;
    /// <summary>
    /// Paginated cursor, returned when there are more than 500 followers
    /// </summary>
    [CanBeNull]
    [JsonProperty("next_cursor")]
    [JsonPropertyName("next_cursor")]
    public string? NextCursor { get; set; }
}
