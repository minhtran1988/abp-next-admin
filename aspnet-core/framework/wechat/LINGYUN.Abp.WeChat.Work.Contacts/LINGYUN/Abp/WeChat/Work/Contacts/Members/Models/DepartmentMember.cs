using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Members.Models;
/// <summary>
/// Department members
/// </summary>
public class DepartmentMember
{
    /// <summary>
    /// Member UserID. Account corresponding to the management side
    /// </summary>
    [NotNull]
    [JsonProperty("userid")]
    [JsonPropertyName("userid")]
    public string UserId { get; set; } = default!;
    /// <summary>
    /// Member name. Self-built applications developed on behalf of the user require administrator authorization before returning.
    /// </summary>
    [NotNull]
    [JsonProperty("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
    /// <summary>
    /// List of departments to which members belong. The list item is the department ID, a 32-bit integer
    /// </summary>
    [CanBeNull]
    [JsonProperty("department")]
    [JsonPropertyName("department")]
    public int[]? Department { get; set; }
    /// <summary>
    /// Globally unique
    /// </summary>
    [NotNull]
    [JsonProperty("open_userid")]
    [JsonPropertyName("open_userid")]
    public string OpenUserId { get; set; } = default!;
}
