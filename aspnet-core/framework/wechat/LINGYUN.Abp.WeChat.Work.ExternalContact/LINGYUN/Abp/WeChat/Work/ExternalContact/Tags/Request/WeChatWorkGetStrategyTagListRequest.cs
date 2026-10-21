using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Tags.Request;
public class WeChatWorkGetStrategyTagListRequest : WeChatWorkRequest
{
    /// <summary>
    /// Rule group id
    /// </summary>
    [NotNull]
    [JsonProperty("strategy_id")]
    [JsonPropertyName("strategy_id")]
    public int StrategyId { get; }
    /// <summary>
    /// The tag id to be queried
    /// </summary>
    [CanBeNull]
    [JsonProperty("tag_id")]
    [JsonPropertyName("tag_id")]
    public string[]? TagId { get; }
    /// <summary>
    /// The tag group id to be queried returns the tag group and all tag information under it.
    /// </summary>
    [CanBeNull]
    [JsonProperty("group_id")]
    [JsonPropertyName("group_id")]
    public string[]? GroupId { get; }
    public WeChatWorkGetStrategyTagListRequest(
        int strategyId,
        string[]? tagId = null,
        string[]? groupId = null)
    {
        Check.NotDefaultOrNull<int>(strategyId, nameof(strategyId));

        StrategyId = strategyId;
        TagId = tagId;
        GroupId = groupId;
    }
}
