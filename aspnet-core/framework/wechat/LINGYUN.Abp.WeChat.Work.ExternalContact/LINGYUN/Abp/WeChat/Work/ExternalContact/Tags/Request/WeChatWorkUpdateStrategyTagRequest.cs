using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Tags.Request;
public class WeChatWorkUpdateStrategyTagRequest : WeChatWorkRequest
{
    /// <summary>
    /// The id of the label or label group
    /// </summary>
    [NotNull]
    [JsonProperty("id")]
    [JsonPropertyName("id")]
    public string Id { get; }
    /// <summary>
    /// New label or label group name, up to 30 characters
    /// </summary>
    [CanBeNull]
    [JsonProperty("name")]
    [JsonPropertyName("name")]
    public string? Name { get; }
    /// <summary>
    /// The order value of the tag/tag group. The ones with larger order values ​​are sorted first. Valid value range is [0, 2^32)
    /// </summary>
    [CanBeNull]
    [JsonProperty("order")]
    [JsonPropertyName("order")]
    public int? Order { get; set; }

    public WeChatWorkUpdateStrategyTagRequest(string id, string? name = null)
    {
        Check.NotNullOrWhiteSpace(id, nameof(id));
        Check.Length(name, nameof(name), 30);

        Id = id;
        Name = name;
    }
}
