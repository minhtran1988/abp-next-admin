using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.ExternalContact.Transfers.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Transfers.Response;
/// <summary>
/// Get the response result of the list of resigned members to be assigned
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/92124" />
/// </remarks>
public class WeChatWorkGetUnassignedListResponse : WeChatWorkResponse
{
    /// <summary>
    /// Customers to whom resigned members are to be assigned
    /// </summary>
    [NotNull]
    [JsonProperty("info")]
    [JsonPropertyName("info")]
    public UnassignedCustomerInfo[] Info { get; set; } = default!;
    /// <summary>
    /// Is it the last record?
    /// </summary>
    [NotNull]
    [JsonProperty("is_last")]
    [JsonPropertyName("is_last")]
    public bool IsLast { get; set; }
    /// <summary>
    /// Paging query cursor will return empty ("") if the query has been completed. It will not return when page_id is used as the query parameter.
    /// </summary>
    [CanBeNull]
    [JsonProperty("next_cursor")]
    [JsonPropertyName("next_cursor")]
    public string? NextCursor { get; set; }
}
