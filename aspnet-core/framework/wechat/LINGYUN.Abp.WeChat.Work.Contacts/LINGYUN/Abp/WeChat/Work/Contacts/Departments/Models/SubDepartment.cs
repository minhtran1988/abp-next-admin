using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Departments.Models;
/// <summary>
/// Subdepartment
/// </summary>
public class SubDepartment
{
    /// <summary>
    /// Department id
    /// </summary>
    [NotNull]
    [JsonProperty("id")]
    [JsonPropertyName("id")]
    public string Id { get; set; } = default!;
    /// <summary>
    /// Parent department id. The root part is 1.
    /// </summary>
    [NotNull]
    [JsonProperty("parentid")]
    [JsonPropertyName("parentid")]
    public int ParentId { get; set; }
    /// <summary>
    /// The order value in the parent department. The ones with larger order values ​​are sorted first. The value range is [0, 2^32).
    /// </summary>
    [NotNull]
    [JsonProperty("order")]
    [JsonPropertyName("order")]
    public int Order { get; set; }
}
