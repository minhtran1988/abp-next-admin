using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Tags.Request;
/// <summary>
/// Edit enterprise customer tag request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/92117#%E7%BC%96%E8%BE%91%E4%BC%81%E4%B8%9A%E5%AE%A2%E6%88%B7%E6%A0%87%E7%AD%BE" />
/// </remarks>
public class WeChatWorkUpdateCropTagRequest : WeChatWorkRequest
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
    /// <summary>
    /// The agentid of the application installed by the authorized party. Only old third-party multi-application suites need to fill in this parameter
    /// </summary>
    [CanBeNull]
    [JsonProperty("agentid")]
    [JsonPropertyName("agentid")]
    public string? AgentId { get; set; }

    public WeChatWorkUpdateCropTagRequest(string id, string? name = null)
    {
        Check.NotNullOrWhiteSpace(id, nameof(id));
        Check.Length(name, nameof(name), 30);

        Id = id;
        Name = name;
    }
}
