using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Transfers.Models;
public class TransferCustomer
{
    /// <summary>
    /// The customer's external contact userid
    /// </summary>
    [NotNull]
    [JsonProperty("external_userid")]
    [JsonPropertyName("external_userid")]
    public string ExternalUserid { get; set; } = default!;
    /// <summary>
    /// For the result of allocation to this customer, please refer to the global error code for details. 0 means that the replacement is successfully initiated. It will automatically take over after 24 hours. It does not mean that the final replacement is successful.
    /// </summary>
    [NotNull]
    [JsonProperty("errcode")]
    [JsonPropertyName("errcode")]
    public int ErrCode { get; set; }
}
