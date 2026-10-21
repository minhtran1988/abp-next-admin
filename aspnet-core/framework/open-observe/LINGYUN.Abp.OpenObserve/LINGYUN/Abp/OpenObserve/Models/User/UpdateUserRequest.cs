using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.User;

/// <summary>
/// Update user request
/// </summary>
public class UpdateUserRequest
{
    [JsonPropertyName("first_name")]
    public string? FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string? LastName { get; set; }

    /// <summary>
    /// Old password
    /// </summary>
    /// <remarks>
    /// Do not pass the value when the password is not changed.
    /// </remarks>
    [JsonPropertyName("old_password")]
    public string? OldPassword { get; set; }

    /// <summary>
    /// New password
    /// </summary>
    /// <remarks>
    /// Do not pass the value when the password is not changed.
    /// </remarks>
    [JsonPropertyName("new_password")]
    public string? NewPassword { get; set; }

    /// <summary>
    /// role
    /// </summary>
    [JsonPropertyName("role")]
    public string? Role { get; set; }
}
