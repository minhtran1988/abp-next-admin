using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.MsgAudits.Response;
/// <summary>
/// Get the session content archive to open the member list response parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/91614" />
/// </remarks>
public class WeChatWorkGetPermitUserListResponse : WeChatWorkResponse
{
    /// <summary>
    /// Set the userid list of members in the open range
    /// </summary>
    [NotNull]
    [JsonProperty("ids")]
    [JsonPropertyName("ids")]
    public string[] Ids { get; set; } = default!;
}
