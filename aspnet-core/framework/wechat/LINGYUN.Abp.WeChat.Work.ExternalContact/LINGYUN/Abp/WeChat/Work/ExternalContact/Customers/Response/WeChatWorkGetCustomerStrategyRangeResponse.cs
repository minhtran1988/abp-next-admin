using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.ExternalContact.Customers.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Customers.Response;
/// <summary>
/// Get the rule group management scope response parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/94883" />
/// </remarks>
public class WeChatWorkGetCustomerStrategyRangeResponse : WeChatWorkResponse
{
    /// <summary>
    /// Rule group management scope
    /// </summary>
    [NotNull]
    [JsonProperty("range")]
    [JsonPropertyName("range")]
    public CustomerStrategyRange[] Range { get; set; } = default!;
    /// <summary>
    /// Paging cursor, used to query the data of the next page, and does not return when there is no more data.
    /// </summary>
    [CanBeNull]
    [JsonProperty("next_cursor")]
    [JsonPropertyName("next_cursor")]
    public string? NextCursor { get; set; }
}
