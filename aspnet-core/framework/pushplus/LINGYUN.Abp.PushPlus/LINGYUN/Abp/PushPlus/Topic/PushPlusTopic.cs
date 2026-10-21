using Newtonsoft.Json;
using System;

namespace LINGYUN.Abp.PushPlus.Topic;
/// <summary>
/// Group
/// </summary>
public class PushPlusTopic
{
    /// <summary>
    /// Group number
    /// </summary>
    [JsonProperty("topicId")]
    public int TopicId { get; set; }
    /// <summary>
    /// Group encoding
    /// </summary>
    [JsonProperty("topicCode")]
    public string TopicCode { get; set; } = default!;
    /// <summary>
    /// Group name
    /// </summary>
    [JsonProperty("topicName")]
    public string TopicName { get; set; } = default!;
    /// <summary>
    /// Creation time
    /// </summary>
    [JsonProperty("createTime")]
    public DateTime CreateTime { get; set; }
}
