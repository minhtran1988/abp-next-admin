using JetBrains.Annotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Tags.Request;
/// <summary>
/// Add enterprise customer tag request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/92117#%E6%B7%BB%E5%8A%A0%E4%BC%81%E4%B8%9A%E5%AE%A2%E6%88%B7%E6%A0%87%E7%AD%BE" />
/// </remarks>
public class WeChatWorkCreateCropTagRequest : WeChatWorkRequest
{
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
    /// The agentid of the application installed by the authorized party. Only old third-party multi-application suites need to fill in this parameter
    /// </summary>
    [CanBeNull]
    [JsonProperty("agentid")]
    [JsonPropertyName("agentid")]
    public string? AgentId { get; set; }
    /// <summary>
    /// Added tag group
    /// </summary>
    [CanBeNull]
    [JsonProperty("tag")]
    [JsonPropertyName("tag")]
    public List<NewCropTag> Tag { get; }

    public WeChatWorkCreateCropTagRequest()
    {
        Tag = new List<NewCropTag>();
    }

    protected override void Validate()
    {
        if (GroupName.IsNullOrWhiteSpace() &&
            Tag.IsNullOrEmpty())
        {
            throw new ArgumentException("The name of the tag group or the tag list cannot be empty at the same time!");
        }
    }
}

public class NewCropTag
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
    public NewCropTag(string name, int? order = null)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name), 30);

        Name = name;
        Order = order;
    }
}
