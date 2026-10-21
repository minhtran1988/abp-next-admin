using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Customers.Models;
/// <summary>
/// External contact list
/// </summary>
public class ExternalContactList
{
    /// <summary>
    /// Basic information of the customer
    /// </summary>
    [NotNull]
    [JsonProperty("external_contact")]
    [JsonPropertyName("external_contact")]
    public ExternalContactInfo ExternalContact { get; set; } = default!;
    /// <summary>
    /// Corporate member customer follow-up information
    /// </summary>
    [NotNull]
    [JsonProperty("follow_info")]
    [JsonPropertyName("follow_info")]
    public FollowUser FollowUser { get; set; } = default!;
}
