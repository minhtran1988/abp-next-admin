using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Tags.Models;
public abstract class Tag
{
    /// <summary>
    /// tag id
    /// </summary>
    [NotNull]
    [JsonProperty("id")]
    [JsonPropertyName("id")]
    public string Id { get; set; } = default!;
    /// <summary>
    /// Tag name
    /// </summary>
    [NotNull]
    [JsonProperty("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
    /// <summary>
    /// Tag creation time
    /// </summary>
    [NotNull]
    [JsonProperty("create_time")]
    [JsonPropertyName("create_time")]
    public long CreateTime { get; set; }
    /// <summary>
    /// The order value of label sorting, the one with a larger order value is sorted first. Valid value range is [0, 2^32)
    /// </summary>
    [NotNull]
    [JsonProperty("order")]
    [JsonPropertyName("order")]
    public int Order { get; set; }
}
