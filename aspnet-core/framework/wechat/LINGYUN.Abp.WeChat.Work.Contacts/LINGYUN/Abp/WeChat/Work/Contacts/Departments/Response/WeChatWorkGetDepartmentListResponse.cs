using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.Contacts.Departments.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Departments.Response;
/// <summary>
/// Get department list response parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/90208" />
/// </remarks>
public class WeChatWorkGetDepartmentListResponse : WeChatWorkResponse
{
    /// <summary>
    /// Department list data
    /// </summary>
    [NotNull]
    [JsonProperty("department")]
    [JsonPropertyName("department")]
    public DepartmentInfo[] Department { get; set; } = default!;
}
