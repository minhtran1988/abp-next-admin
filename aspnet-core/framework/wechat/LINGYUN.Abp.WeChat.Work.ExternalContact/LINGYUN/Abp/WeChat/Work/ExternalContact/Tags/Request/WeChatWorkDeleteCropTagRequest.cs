using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Tags.Request;
/// <summary>
/// Delete enterprise customer label request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/92117#%E5%88%A0%E9%99%A4%E4%BC%81%E4%B8%9A%E5%AE%A2%E6%88%B7%E6%A0%87%E7%AD%BE" />
/// </remarks>
public class WeChatWorkDeleteCropTagRequest : WeChatWorkRequest
{
    /// <summary>
    /// ID list of tag group
    /// </summary>
    [CanBeNull]
    [JsonProperty("group_id")]
    [JsonPropertyName("group_id")]
    public string[]? GroupId { get; }
    /// <summary>
    /// ID list of tag group
    /// </summary>
    [CanBeNull]
    [JsonProperty("tag_id")]
    [JsonPropertyName("tag_id")]
    public string[]? TagId { get; }
    /// <summary>
    /// The agentid of the application installed by the authorized party. Only old third-party multi-application suites need to fill in this parameter
    /// </summary>
    [CanBeNull]
    [JsonProperty("agentid")]
    [JsonPropertyName("agentid")]
    public string? AgentId { get; set; }

    private WeChatWorkDeleteCropTagRequest(
        string[]? groupId = null,
        string[]? tagId = null)
    {
        GroupId = groupId;
        TagId = tagId;
    }

    public static WeChatWorkDeleteCropTagRequest Tag(string[] tagId)
    {
        return new WeChatWorkDeleteCropTagRequest(tagId: tagId);
    }

    public static WeChatWorkDeleteCropTagRequest Group(string[] groupId)
    {
        return new WeChatWorkDeleteCropTagRequest(groupId);
    }
}
