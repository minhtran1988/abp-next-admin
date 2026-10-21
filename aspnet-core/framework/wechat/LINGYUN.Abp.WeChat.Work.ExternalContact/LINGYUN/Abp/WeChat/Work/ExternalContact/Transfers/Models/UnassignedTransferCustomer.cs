using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Transfers.Models;
/// <summary>
/// Assign departing member customer details
/// </summary>
public class UnassignedTransferCustomer
{
    /// <summary>
    /// The customer's external contact userid
    /// </summary>
    [NotNull]
    [JsonProperty("external_userid")]
    [JsonPropertyName("external_userid")]
    public string ExternalUserid { get; set; } = default!;
    /// <summary>
    /// The result of the allocation for this customer, 0 means the allocation process starts, and will be automatically taken over after 24 hours. It does not mean that the final allocation is successful.
    /// </summary>
    [NotNull]
    [JsonProperty("errcode")]
    [JsonPropertyName("errcode")]
    public int ErrCode { get; set; }
}
