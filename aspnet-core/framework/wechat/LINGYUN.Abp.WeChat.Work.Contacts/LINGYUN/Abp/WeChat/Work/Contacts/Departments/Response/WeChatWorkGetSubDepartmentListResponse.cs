using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.Contacts.Departments.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Departments.Response;
/// <summary>
/// Get the sub-department ID list response parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/95350" />
/// </remarks>
public class WeChatWorkGetSubDepartmentListResponse : WeChatWorkResponse
{
    /// <summary>
    /// Department list data.
    /// </summary>
    [NotNull]
    [JsonProperty("department_id")]
    [JsonPropertyName("department_id")]
    public SubDepartment[] Department { get; set; } = default!;
}
