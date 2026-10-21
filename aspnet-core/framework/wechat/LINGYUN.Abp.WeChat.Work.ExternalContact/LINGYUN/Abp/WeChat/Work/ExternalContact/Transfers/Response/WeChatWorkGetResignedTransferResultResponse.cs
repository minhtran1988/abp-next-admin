using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.ExternalContact.Transfers.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Transfers.Response;
/// <summary>
/// Query the response result of customer replacement status of resigned personnel
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/94082" />
/// </remarks>
public class WeChatWorkGetResignedTransferResultResponse : WeChatWorkResponse
{
    /// <summary>
    /// Transfer client
    /// </summary>
    [NotNull]
    [JsonProperty("customer")]
    [JsonPropertyName("customer")]
    public UnassignedTransferCustomerResult[] Customer { get; set; } = default!;
    /// <summary>
    /// The starting cursor of the next page
    /// </summary>
    [CanBeNull]
    [JsonProperty("next_cursor")]
    [JsonPropertyName("next_cursor")]
    public string? NextCursor { get; set; }
}
