using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.User;

/// <summary>
/// User information
/// </summary>
public class UserInfo
{
    [JsonPropertyName("email")]
    public string Email { get; set; } = default!;

    /// <summary>
    /// name
    /// </summary>
    /// <remarks>
    /// The response example of the document is mistakenly written as fist_name. Here, first_name is used according to the field table, and the extended attribute is used to find out.
    /// </remarks>
    [JsonPropertyName("first_name")]
    public string? FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string? LastName { get; set; }

    /// <summary>
    /// role
    /// </summary>
    /// <remarks>
    /// admin / user / root
    /// </remarks>
    [JsonPropertyName("role")]
    public string? Role { get; set; }

    /// <summary>
    /// Whether it is an external user
    /// </summary>
    [JsonPropertyName("is_external")]
    public bool? IsExternal { get; set; }

    /// <summary>
    /// Creation time (microseconds)
    /// </summary>
    [JsonPropertyName("created_at")]
    public long? CreatedAt { get; set; }

    /// <summary>
    /// Fields not declared in the model
    /// </summary>
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtraProperties { get; set; }
}
