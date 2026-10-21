using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Customers.Request;
/// <summary>
/// Get the rule group list request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/94883#%E8%8E%B7%E5%8F%96%E8%A7%84%E5%88%99%E7%BB%84%E5%88%97%E8%A1%A8" />
/// </remarks>
public class WeChatWorkGetCustomerStrategyListRequest : WeChatWorkRequest
{
    /// <summary>
    /// Paging query cursor, you don’t need to fill it in for the first time.
    /// </summary>
    [CanBeNull]
    [JsonProperty("cursor")]
    [JsonPropertyName("cursor")]
    public string? Cursor { get; }
    /// <summary>
    /// Paging size, the default is 1000, the maximum is not more than 1000
    /// </summary>
    [CanBeNull]
    [JsonProperty("limit")]
    [JsonPropertyName("limit")]
    public int? Limit { get; }
    public WeChatWorkGetCustomerStrategyListRequest(string? cursor = null, int? limit = 1000)
    {
        if (limit.HasValue)
        {
            Check.Range(limit.Value, nameof(limit), 1, 1000);
        }

        Cursor = cursor;
        Limit = limit;
    }
}
