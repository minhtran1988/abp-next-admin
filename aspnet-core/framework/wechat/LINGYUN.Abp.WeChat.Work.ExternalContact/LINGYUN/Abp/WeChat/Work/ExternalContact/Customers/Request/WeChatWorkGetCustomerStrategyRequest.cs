using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Customers.Request;
/// <summary>
/// Get rule group request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/94883#%E8%8E%B7%E5%8F%96%E8%A7%84%E5%88%99%E7%BB%84%E8%AF%A6%E6%83%85" />
/// </remarks>
public class WeChatWorkGetCustomerStrategyRequest : WeChatWorkRequest
{
    /// <summary>
    /// Rule group id
    /// </summary>
    [NotNull]
    [JsonProperty("strategy_id")]
    [JsonPropertyName("strategy_id")]
    public int StrategyId { get; }
    public WeChatWorkGetCustomerStrategyRequest(int strategyId)
    {
        StrategyId = strategyId;
    }
}
