using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Tags.Request;
/// <summary>
/// Get the enterprise tag library request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/92117#%E8%8E%B7%E5%8F%96%E4%BC%81%E4%B8%9A%E6%A0%87%E7%AD%BE%E5%BA%93" />
/// </remarks>
public class WeChatWorkGetCropTagListRequest : WeChatWorkRequest
{
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
    public WeChatWorkGetCropTagListRequest(string[]? tagId = null, string[]? groupId = null)
    {
        TagId = tagId;
        GroupId = groupId;
    }
}
