using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Customers.Response;
/// <summary>
/// Create new rule group response parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/94883" />
/// </remarks>
public class WeChatWorkCreateCustomerStrategyResponse : WeChatWorkResponse
{
    /// <summary>
    /// Rule group id
    /// </summary>
    [NotNull]
    [JsonProperty("strategy_id")]
    [JsonPropertyName("strategy_id")]
    public int StrategyId { get; set; }
}
