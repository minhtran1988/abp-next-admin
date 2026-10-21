using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.Contacts.Members.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Members.Response;
/// <summary>
/// Get department member details response parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/90201" />
/// </remarks>
public class WeChatWorkGetMemberListResponse : WeChatWorkResponse
{
    /// <summary>
    /// Member list
    /// </summary>
    [NotNull]
    [JsonProperty("userlist")]
    [JsonPropertyName("userlist")]
    public MemberInfo[] UserList { get; set; } = default!;
}
