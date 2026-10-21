using Newtonsoft.Json;

namespace LINGYUN.Abp.PushPlus.Topic;
/// <summary>
/// Details of the group I joined
/// </summary>
public class PushPlusTopicForMe : PushPlusTopic
{
    /// <summary>
    /// Contact information
    /// </summary>
    [JsonProperty("contact")]
    public string? Contact { get; set; }
    /// <summary>
    /// Group introduction
    /// </summary>
    [JsonProperty("introduction")]
    public string? Introduction { get; set; }
}
