using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.Contacts.Departments.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Departments.Response;
/// <summary>
/// Get the response parameters of a single department details
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/95351" />
/// </remarks>
public class WeChatWorkGetDepartmentResponse : WeChatWorkResponse
{
    /// <summary>
    /// Department details
    /// </summary>
    [NotNull]
    [JsonProperty("department")]
    [JsonPropertyName("department")]
    public DepartmentInfo Department { get; set; } = default!;
}
