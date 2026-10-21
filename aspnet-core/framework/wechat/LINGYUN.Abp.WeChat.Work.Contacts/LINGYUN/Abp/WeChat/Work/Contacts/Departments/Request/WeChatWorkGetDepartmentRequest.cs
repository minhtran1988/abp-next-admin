using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Departments.Request;
/// <summary>
/// Get single department details request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/90207" />
/// </remarks>
public class WeChatWorkGetDepartmentRequest : WeChatWorkRequest
{
    /// <summary>
    /// Department id
    /// </summary>
    [NotNull]
    [JsonProperty("id")]
    [JsonPropertyName("id")]
    public int Id { get; }
    public WeChatWorkGetDepartmentRequest(int id)
    {
        Id = id;
    }
}
