using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Tags.Models;
public class StrategyTagGroup : TagGroup
{
    /// <summary>
    /// The rule group id to which the label group belongs
    /// </summary>
    [NotNull]
    [JsonProperty("strategy_id")]
    [JsonPropertyName("strategy_id")]
    public int StrategyId { get; set; }
    /// <summary>
    /// Tag list
    /// </summary>
    [NotNull]
    [JsonProperty("tag")]
    [JsonPropertyName("tag")]
    public StrategyTag[] Tag { get; set; } = default!;
}
