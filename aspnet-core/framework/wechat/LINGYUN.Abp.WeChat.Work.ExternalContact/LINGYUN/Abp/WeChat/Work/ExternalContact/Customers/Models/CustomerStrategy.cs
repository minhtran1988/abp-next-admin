using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Customers.Models;
/// <summary>
/// Rule group
/// </summary>
public class CustomerStrategy
{
    /// <summary>
    /// Rule group id
    /// </summary>
    [NotNull]
    [JsonProperty("strategy_id")]
    [JsonPropertyName("strategy_id")]
    public int StrategyId { get; set; }
}
