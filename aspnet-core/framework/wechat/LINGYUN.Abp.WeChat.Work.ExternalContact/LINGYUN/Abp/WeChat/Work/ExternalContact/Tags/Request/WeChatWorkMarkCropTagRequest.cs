using JetBrains.Annotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Tags.Request;
/// <summary>
/// Edit customer enterprise label request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/92118" />
/// </remarks>
public class WeChatWorkMarkCropTagRequest : WeChatWorkRequest
{
    /// <summary>
    /// Add the userid of the external contact
    /// </summary>
    [NotNull]
    [JsonProperty("userid")]
    [JsonPropertyName("userid")]
    public string UserId { get; }
    /// <summary>
    /// External contact userid
    /// </summary>
    [NotNull]
    [JsonProperty("external_userid")]
    [JsonPropertyName("external_userid")]
    public string ExternalUserId { get; }
    /// <summary>
    /// List of tags to mark
    /// </summary>
    [NotNull]
    [JsonProperty("add_tag")]
    [JsonPropertyName("add_tag")]
    public List<string> CreateTag { get; }
    /// <summary>
    /// List of tags to remove
    /// </summary>
    [NotNull]
    [JsonProperty("remove_tag")]
    [JsonPropertyName("remove_tag")]
    public List<string> RemoveTag { get; }
    public WeChatWorkMarkCropTagRequest(string userId, string externalUserId)
    {
        Check.NotNullOrWhiteSpace(userId, nameof(userId));
        Check.NotNullOrWhiteSpace(externalUserId, nameof(externalUserId));

        UserId = userId;
        ExternalUserId = externalUserId;

        CreateTag = new List<string>();
        RemoveTag = new List<string>();
    }

    protected override void Validate()
    {
        if (CreateTag.IsNullOrEmpty() &&
            RemoveTag.IsNullOrEmpty())
        {
            throw new ArgumentException("CreateTag and RemoveTag cannot be empty simultaneously!");
        }
    }
}
