using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Departments.Request;
/// <summary>
/// Get department list request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/90208" />
/// </remarks>
public class WeChatWorkGetDepartmentListRequest : WeChatWorkRequest
{
    /// <summary>
    /// Department id. Gets the specified department and its subdepartments (and subdepartments of subdepartments, and so on, recursively). If left blank, the full organizational structure will be obtained by default.
    /// </summary>
    [CanBeNull]
    [JsonProperty("id")]
    [JsonPropertyName("id")]
    public int? Id { get; }
    public WeChatWorkGetDepartmentListRequest(int? id = null)
    {
        Id = id;
    }
}
