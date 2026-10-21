using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.ExternalContact.Transfers.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Transfers.Response;
/// <summary>
/// Assign the customer response results of resigned members
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/94081" />
/// </remarks>
public class WeChatWorkResignedTransferCustomerResponse : WeChatWorkResponse
{
    /// <summary>
    /// Assign the resigned member customer results
    /// </summary>
    [NotNull]
    [JsonProperty("customer")]
    [JsonPropertyName("customer")]
    public UnassignedTransferCustomer[] Customer { get; set; } = default!;
}
