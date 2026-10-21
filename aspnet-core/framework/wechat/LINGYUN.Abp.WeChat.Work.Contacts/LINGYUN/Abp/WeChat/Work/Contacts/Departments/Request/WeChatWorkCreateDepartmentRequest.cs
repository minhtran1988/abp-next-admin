using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Departments.Request;
/// <summary>
/// Create department request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/90205" />
/// </remarks>
public class WeChatWorkCreateDepartmentRequest : WeChatWorkRequest
{
    /// <summary>
    /// Department name. Department names at the same level cannot be repeated. Length limit is 1~64 UTF-8 characters
    /// </summary>
    [NotNull]
    [JsonProperty("name")]
    [JsonPropertyName("name")]
    public string Name { get; }
    /// <summary>
    /// English name. Department names at the same level cannot be repeated. Multi-language support needs to be enabled in the management background to take effect. Length limit is 1~64 characters
    /// </summary>
    [CanBeNull]
    [JsonProperty("name_en")]
    [JsonPropertyName("name_en")]
    public string? NameEn { get; set; }
    /// <summary>
    /// Parent department id, 32-bit integer
    /// </summary>
    [NotNull]
    [JsonProperty("parentid")]
    [JsonPropertyName("parentid")]
    public int ParentId { get; }
    /// <summary>
    /// The order value in the parent department. The ones with larger order values ​​are sorted first. Valid value range is [0, 2^32)
    /// </summary>
    [CanBeNull]
    [JsonProperty("order")]
    [JsonPropertyName("order")]
    public int? Order { get; set; }
    /// <summary>
    /// Department id, 32-bit integer, must be greater than 1 when specified. If this parameter is not filled in, the ID will be automatically generated.
    /// </summary>
    [CanBeNull]
    [JsonProperty("id")]
    [JsonPropertyName("id")]
    public int? Id { get; set; }
    public WeChatWorkCreateDepartmentRequest(string name, int parentId = 0, string? nameEn = null, int? order = null, int? id = null)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name), 64, 1);
        Check.Length(nameEn, nameof(nameEn), 64, 1);

        Name = name;
        NameEn = nameEn;
        ParentId = parentId;
        Order = order;
        Id = id;
    }

    protected override void Validate()
    {
        Check.NotNullOrWhiteSpace(Name, nameof(Name), 64, 1);
        Check.Length(NameEn, nameof(NameEn), 64, 1);
    }
}
