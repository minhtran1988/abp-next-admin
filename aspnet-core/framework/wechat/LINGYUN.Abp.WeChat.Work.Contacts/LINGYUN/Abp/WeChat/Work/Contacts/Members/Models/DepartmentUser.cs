using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Members.Models;
/// <summary>
/// User-department relationship
/// </summary>
public class DepartmentUser
{
    /// <summary>
    /// User userid, when the user is under multiple departments, there will be multiple records
    /// </summary>
    [NotNull]
    [JsonProperty("userid")]
    [JsonPropertyName("userid")]
    public string UserId { get; set; } = default!;
    /// <summary>
    /// The department to which the user belongs
    /// </summary>
    [NotNull]
    [JsonProperty("department")]
    [JsonPropertyName("department")]
    public int Department { get; set; } = default!;
}
