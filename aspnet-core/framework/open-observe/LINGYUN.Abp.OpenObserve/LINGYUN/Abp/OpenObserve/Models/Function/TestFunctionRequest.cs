using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Function;

/// <summary>
/// Test function request
/// </summary>
/// <remarks>
/// POST /api/{org_id}/functions/test
/// Note: This interface does not appear in the API document and comes from the implementation source code
/// </remarks>
public class TestFunctionRequest
{
    /// <summary>
    /// Function body
    /// </summary>
    [JsonPropertyName("function")]
    public string Function { get; set; } = default!;

    /// <summary>
    /// Sample data for testing
    /// </summary>
    [JsonPropertyName("events")]
    public JsonNode[] Events { get; set; } = [];

    /// <summary>
    /// Functional language
    /// </summary>
    /// <remarks>
    /// If not transmitted, it will be automatically recognized by the server.
    /// </remarks>
    [JsonPropertyName("trans_type")]
    public FunctionTransType? TransType { get; set; }
}
