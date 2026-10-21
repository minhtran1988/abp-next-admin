using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Tags.Request;
public class WeChatWorkCreateStrategyTagRequest : WeChatWorkRequest
{
    /// <summary>
    /// Rule group id
    /// </summary>
    [NotNull]
    [JsonProperty("strategy_id")]
    [JsonPropertyName("strategy_id")]
    public int StrategyId { get; }
    /// <summary>
    /// Tag group id
    /// </summary>
    [CanBeNull]
    [JsonProperty("group_id")]
    [JsonPropertyName("group_id")]
    public string? GroupId { get; set; }

    private string? _groupName;
    /// <summary>
    /// Tag group name, up to 30 characters
    /// </summary>
    [CanBeNull]
    [JsonProperty("group_name")]
    [JsonPropertyName("group_name")]
    public string? GroupName {
        get => _groupName;
        set {

            Check.Length(value, nameof(GroupName), 30);
            _groupName = value;
        }
    }
    /// <summary>
    /// The order value of the label group sorting, the order value with the larger order value is sorted first. Valid value range is [0, 2^32)
    /// </summary>
    [CanBeNull]
    [JsonProperty("order")]
    [JsonPropertyName("order")]
    public int? Order { get; set; }
    /// <summary>
    /// Added tag group
    /// </summary>
    [CanBeNull]
    [JsonProperty("tag")]
    [JsonPropertyName("tag")]
    public List<NewStrategyTag> Tag { get; }

    public WeChatWorkCreateStrategyTagRequest(int strategyId)
    {
        Check.NotDefaultOrNull<int>(strategyId, nameof(strategyId));

        StrategyId = strategyId;

        Tag = new List<NewStrategyTag>();
    }
}

public class NewStrategyTag
{
    /// <summary>
    /// Added label name, up to 30 characters
    /// </summary>
    [NotNull]
    [JsonProperty("name")]
    [JsonPropertyName("name")]
    public string Name { get; }
    /// <summary>
    /// Tag order value. The ones with larger order values ​​are sorted first. Valid value range is [0, 2^32)
    /// </summary>
    [CanBeNull]
    [JsonProperty("order")]
    [JsonPropertyName("order")]
    public int? Order { get; set; }
    public NewStrategyTag(string name, int? order = null)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name), 30);

        Name = name;
        Order = order;
    }
}
