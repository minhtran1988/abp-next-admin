using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Tags.Models;
public abstract class TagGroup
{
    /// <summary>
    /// Tag group id
    /// </summary>
    [NotNull]
    [JsonProperty("group_id")]
    [JsonPropertyName("group_id")]
    public string GroupId { get; set; } = default!;
    /// <summary>
    /// Tag group name
    /// </summary>
    [NotNull]
    [JsonProperty("group_name")]
    [JsonPropertyName("group_name")]
    public string GroupName { get; set; } = default!;
    /// <summary>
    /// Tag group creation time
    /// </summary>
    [NotNull]
    [JsonProperty("create_time")]
    [JsonPropertyName("create_time")]
    public long CreateTime { get; set; }
    /// <summary>
    /// The order value of the label group sorting, the order value with the larger order value is sorted first. Valid value range is [0, 2^32)
    /// </summary>
    [NotNull]
    [JsonProperty("order")]
    [JsonPropertyName("order")]
    public int Order { get; set; }
}
