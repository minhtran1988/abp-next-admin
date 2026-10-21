using Newtonsoft.Json;
using System;

namespace LINGYUN.Abp.WxPusher.User;

[Serializable]
public class UserProfile
{
    /// <summary>
    /// user uid
    /// </summary>
    [JsonProperty("uid")]
    public string Uid { get; set; } = default!;
    /// <summary>
    /// The application or topic id that the user follows is distinguished based on type.
    /// </summary>
    [JsonProperty("appOrTopicId")]
    public long? AppOrTopicId { get; set; }
    /// <summary>
    /// New user WeChat will no longer return, and is forced to return empty
    /// </summary>
    [JsonProperty("headImg")]
    public string? HeadImg { get; set; }
    /// <summary>
    /// Creation time
    /// </summary>
    [JsonProperty("createTime")]
    public long CreateTime { get; set; }
    /// <summary>
    /// New user WeChat will no longer return, and is forced to return empty
    /// </summary>
    [JsonProperty("nickName")]
    public string? NickName { get; set; }
    /// <summary>
    /// Whether to blacklist
    /// </summary>
    [JsonProperty("reject")]
    public bool Reject { get; set; }
    /// <summary>
    /// If you call the delete or blacklist interface, you need this id
    /// </summary>
    [JsonProperty("id")]
    public int Id { get; set; }
    /// <summary>
    /// Focus on type,
    /// 0: Follow the application,
    /// 1: Follow topic
    /// </summary>
    [JsonProperty("type")]
    public FlowType Type { get; set; }
    /// <summary>
    /// The name of the application or topic you are following
    /// </summary>
    [JsonProperty("target")]
    public string? Target { get; set; }
    /// <summary>
    /// 0 means the user is not a paying user, greater than 0 means the expiration time of the user's paid subscription, millisecond timestamp
    /// </summary>
    [JsonProperty("payEndTime")]
    public long PayEndTime { get; set; }
}
