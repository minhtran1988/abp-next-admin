using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.User;

/// <summary>
/// User list response
/// </summary>
/// <remarks>
/// The measured response is {"data":[...]}, the document example is {"list":[...]}, both are compatible.
/// </remarks>
public class UserListResponse
{
    /// <summary>
    /// User list
    /// </summary>
    [JsonPropertyName("data")]
    public UserInfo[]? Data { get; set; }

    /// <summary>
    /// User list
    /// </summary>
    [JsonPropertyName("list")]
    public UserInfo[]? List { get; set; }

    /// <summary>
    /// User list
    /// </summary>
    [JsonIgnore]
    public UserInfo[] Users => Data ?? List ?? [];
}
