using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Function;

/// <summary>
/// Test function results
/// </summary>
public class TestFunctionResult
{
    /// <summary>
    /// Process the message
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    /// <summary>
    /// Processed data
    /// </summary>
    [JsonPropertyName("event")]
    public JsonNode? Event { get; set; }
}
