using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.ExternalContact.Transfers.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Transfers.Response;
/// <summary>
/// Assign customer response results to active members
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/92125" />
/// </remarks>
public class WeChatWorkTransferCustomerResponse : WeChatWorkResponse
{
    /// <summary>
    /// Assign customers
    /// </summary>
    [NotNull]
    [JsonProperty("customer")]
    [JsonPropertyName("customer")]
    public TransferCustomer[] Customer { get; set; } = default!;
}
