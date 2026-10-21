using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Customers.Models;
/// <summary>
/// Rule group details
/// </summary>
public class CustomerStrategyInfo : CustomerStrategy
{
    /// <summary>
    /// Parent rule group id, if the current rule group has no parent rule group, it is 0
    /// </summary>
    [NotNull]
    [JsonProperty("parent_id")]
    [JsonPropertyName("parent_id")]
    public int ParentId { get; set; }
    /// <summary>
    /// Rule group name
    /// </summary>
    [NotNull]
    [JsonProperty("strategy_name")]
    [JsonPropertyName("strategy_name")]
    public string StrategyName { get; set; } = default!;
    /// <summary>
    /// Rule group creation timestamp
    /// </summary>
    [NotNull]
    [JsonProperty("create_time")]
    [JsonPropertyName("create_time")]
    public long CreateTime { get; set; }
    /// <summary>
    /// Rule group administrator userid list
    /// </summary>
    [NotNull]
    [JsonProperty("admin_list")]
    [JsonPropertyName("admin_list")]
    public string[] AdminList { get; set; } = default!;
    /// <summary>
    /// Rule group permissions
    /// </summary>
    [NotNull]
    [JsonProperty("privilege")]
    [JsonPropertyName("privilege")]
    public CustomerStrategyPrivilege Privilege { get; set; } = default!;
}
