using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Departments.Request;
/// <summary>
/// Update department request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/90205" />
/// </remarks>
public class WeChatWorkUpdateDepartmentRequest : WeChatWorkRequest
{
    /// <summary>
    /// Department id
    /// </summary>
    [NotNull]
    [JsonProperty("id")]
    [JsonPropertyName("id")]
    public int Id { get; }
    /// <summary>
    /// Department name. Length limit is 1~64 UTF-8 characters
    /// </summary>
    [CanBeNull]
    [JsonProperty("name")]
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    /// <summary>
    /// English name, multi-language support needs to be enabled in the management background to take effect. Length limit is 1~64 characters
    /// </summary>
    [CanBeNull]
    [JsonProperty("name_en")]
    [JsonPropertyName("name_en")]
    public string? NameEn { get; set; }
    /// <summary>
    /// Parent department id
    /// </summary>
    [CanBeNull]
    [JsonProperty("parentid")]
    [JsonPropertyName("parentid")]
    public int? ParentId { get; set; }
    /// <summary>
    /// The order value in the parent department. The ones with larger order values ​​are sorted first. Valid value range is [0, 2^32)
    /// </summary>
    [CanBeNull]
    [JsonProperty("order")]
    [JsonPropertyName("order")]
    public int? Order { get; set; }
    public WeChatWorkUpdateDepartmentRequest(int id)
    {
        Id = id;
    }

    protected override void Validate()
    {
        Check.NotNullOrWhiteSpace(Name, nameof(Name), 64, 1);
        Check.Length(NameEn, nameof(NameEn), 64, 1);
    }
}
