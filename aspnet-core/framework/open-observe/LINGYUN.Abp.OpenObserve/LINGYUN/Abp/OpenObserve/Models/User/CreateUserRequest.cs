using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.User;

/// <summary>
/// Create user request
/// </summary>
public class CreateUserRequest
{
    /// <summary>
    /// Email
    /// </summary>
    [JsonPropertyName("email")]
    public string Email { get; set; } = default!;

    [JsonPropertyName("first_name")]
    public string? FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string? LastName { get; set; }

    /// <summary>
    /// Password
    /// </summary>
    [JsonPropertyName("password")]
    public string Password { get; set; } = default!;

    /// <summary>
    /// role
    /// </summary>
    /// <remarks>
    /// admin / user
    /// </remarks>
    [JsonPropertyName("role")]
    public string? Role { get; set; }
}
