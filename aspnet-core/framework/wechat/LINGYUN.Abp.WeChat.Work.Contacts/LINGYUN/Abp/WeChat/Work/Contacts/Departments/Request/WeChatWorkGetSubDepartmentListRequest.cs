using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Departments.Request;
/// <summary>
/// Get sub-department ID list request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/95350" />
/// </remarks>
public class WeChatWorkGetSubDepartmentListRequest : WeChatWorkRequest
{
    /// <summary>
    /// Department id. Gets the specified department and its subdepartments (and subdepartments of subdepartments, and so on, recursively). If left blank, the full organizational structure will be obtained by default.
    /// </summary>
    [CanBeNull]
    [JsonProperty("id")]
    [JsonPropertyName("id")]
    public int? Id { get; }
    public WeChatWorkGetSubDepartmentListRequest(int? id = null)
    {
        Id = id;
    }
}
