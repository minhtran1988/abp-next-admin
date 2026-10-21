using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.Contacts.Members.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Members.Response;
/// <summary>
/// Get member ID list response parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/96067" />
/// </remarks>
public class WeChatWorkGetUserIdListResponse : WeChatWorkResponse
{
    /// <summary>
    /// User-department relationship list
    /// </summary>
    [CanBeNull]
    [JsonProperty("next_cursor")]
    [JsonPropertyName("next_cursor")]
    public string? NextCursor { get; set; }
    /// <summary>
    /// Paging cursor, fill in the next request to obtain subsequent paging records. If this field returns empty it means there is no more data
    /// </summary>
    [NotNull]
    [JsonProperty("dept_user")]
    [JsonPropertyName("dept_user")]
    public DepartmentUser[] DepartmentUser { get; set; } = default!;
}
