using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.User;

/// <summary>
/// Add user to organization request
/// </summary>
public class AddUserToOrganizationRequest
{
    /// <summary>
    /// role
    /// </summary>
    /// <remarks>
    /// admin / user
    /// </remarks>
    [JsonPropertyName("role")]
    public string? Role { get; set; }
}
