using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Transfers.Models;
public class TransferCustomerResult
{
    /// <summary>
    /// The customer's external contact userid
    /// </summary>
    [NotNull]
    [JsonProperty("external_userid")]
    [JsonPropertyName("external_userid")]
    public string ExternalUserid { get; set; } = default!;
    /// <summary>
    /// Take over status
    /// </summary>
    [NotNull]
    [JsonProperty("status")]
    [JsonPropertyName("status")]
    public TransferStatus Status { get; set; }
    /// <summary>
    /// The time to take over the customer. If it is waiting for takeover, it is the future automatic takeover time.
    /// </summary>
    [NotNull]
    [JsonProperty("takeover_time")]
    [JsonPropertyName("takeover_time")]
    public long TakeoverTime { get; set; }
}
