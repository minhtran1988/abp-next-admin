using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.ExternalContact.Transfers.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Transfers.Response;
/// <summary>
/// Query the response result of customer replacement status
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/94088" />
/// </remarks>
public class WeChatWorkGetTransferResultResponse : WeChatWorkResponse
{
    /// <summary>
    /// Assign customers
    /// </summary>
    [NotNull]
    [JsonProperty("customer")]
    [JsonPropertyName("customer")]
    public TransferCustomerResult[] Customer { get; set; } = default!;
    /// <summary>
    /// The starting cursor of the next page
    /// </summary>
    [CanBeNull]
    [JsonProperty("next_cursor")]
    [JsonPropertyName("next_cursor")]
    public string? NextCursor { get; set; }
}
