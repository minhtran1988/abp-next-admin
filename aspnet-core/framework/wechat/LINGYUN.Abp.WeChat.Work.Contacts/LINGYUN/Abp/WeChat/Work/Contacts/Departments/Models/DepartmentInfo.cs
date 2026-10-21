using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Departments.Models;
/// <summary>
/// Department details
/// </summary>
public class DepartmentInfo : Department
{
    /// <summary>
    /// Department English name
    /// </summary>
    [CanBeNull]
    [JsonProperty("name_en")]
    [JsonPropertyName("name_en")]
    public string? NameEn { get; set; }
    /// <summary>
    /// The UserID of the department head, returns the list of department heads within the visible range of the application
    /// </summary>
    [NotNull]
    [JsonProperty("department_leader")]
    [JsonPropertyName("department_leader")]
    public string[] DepartmentLeader { get; set; } = default!;
    /// <summary>
    /// Parent department id. The root department is 1
    /// </summary>
    [NotNull]
    [JsonProperty("parentid")]
    [JsonPropertyName("parentid")]
    public int ParentId { get; set; }
    /// <summary>
    /// The order value in the parent department. The ones with larger order values ​​are sorted first. The value range is [0, 2^32)
    /// </summary>
    [NotNull]
    [JsonProperty("order")]
    [JsonPropertyName("order")]
    public int Order { get; set; }
}
