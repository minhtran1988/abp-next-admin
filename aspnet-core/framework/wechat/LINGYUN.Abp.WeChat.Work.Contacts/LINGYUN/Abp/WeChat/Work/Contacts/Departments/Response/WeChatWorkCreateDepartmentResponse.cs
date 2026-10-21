using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Departments.Response;
/// <summary>
/// Create department response parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/90205" />
/// </remarks>
public class WeChatWorkCreateDepartmentResponse : WeChatWorkResponse
{
    /// <summary>
    /// Created department id
    /// </summary>
    [NotNull]
    [JsonProperty("id")]
    [JsonPropertyName("id")]
    public int Id { get; set; }
}
