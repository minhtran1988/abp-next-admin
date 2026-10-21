using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Follows.Response;
/// <summary>
/// Get the response parameters of the member list configured with the customer contact function
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/91853" />
/// </remarks>
public class WeChatWorkGetFollowUserListResponse : WeChatWorkResponse
{
    /// <summary>
    /// List of member userids configured with customer contact function
    /// </summary>
    [NotNull]
    [JsonProperty("follow_user")]
    [JsonPropertyName("follow_user")]
    public List<string> FollowUser {  get; set; } = new List<string>();
}
