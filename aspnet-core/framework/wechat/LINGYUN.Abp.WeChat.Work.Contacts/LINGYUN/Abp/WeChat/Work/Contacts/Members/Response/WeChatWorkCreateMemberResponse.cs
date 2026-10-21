using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.Contacts.Departments.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Members.Response;
/// <summary>
/// Create member response parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/90195" />
/// </remarks>
public class WeChatWorkCreateMemberResponse : WeChatWorkResponse
{
    /// <summary>
    /// Added department list due to filling in non-existing departments
    /// </summary>
    [CanBeNull]
    [JsonProperty("created_department_list")]
    [JsonPropertyName("created_department_list")]
    public Department[]? CreatedDepartmentList { get; set; }
}
