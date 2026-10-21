using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models;

/// <summary>
/// OpenObserve generic response
/// </summary>
/// <remarks>
/// Most write operation interfaces return {"code":200,"message":"..."}
/// </remarks>
public class OpenObserveCodeMessageResponse
{
    /// <summary>
    /// Status code
    /// </summary>
    [JsonPropertyName("code")]
    public int Code { get; set; }

    /// <summary>
    /// Message
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    /// <summary>
    /// Whether the request was successful
    /// </summary>
    [JsonIgnore]
    public bool IsSuccess => Code is >= 200 and < 300;
}
