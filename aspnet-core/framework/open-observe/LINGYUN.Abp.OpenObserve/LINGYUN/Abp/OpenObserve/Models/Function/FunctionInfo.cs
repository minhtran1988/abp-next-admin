using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Function;

/// <summary>
/// Function information
/// </summary>
/// <remarks>
/// The document field table is named with underscores (stream_name/num_args/trans_type), and the implementation returns camelCase(numArgs/transType/streams)
/// Undeclared fields are reserved through extended attributes
/// </remarks>
public class FunctionInfo
{
    /// <summary>
    /// Function name
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Function body
    /// </summary>
    [JsonPropertyName("function")]
    public string? Function { get; set; }

    /// <summary>
    /// Function parameters
    /// </summary>
    [JsonPropertyName("params")]
    public string? Params { get; set; }

    /// <summary>
    /// Number of parameters
    /// </summary>
    [JsonPropertyName("numArgs")]
    public int? NumArgs { get; set; }

    /// <summary>
    /// Functional language
    /// </summary>
    [JsonPropertyName("transType")]
    public FunctionTransType? TransType { get; set; }

    /// <summary>
    /// Data flow where the function takes effect
    /// </summary>
    [JsonPropertyName("streams")]
    public FunctionStreamInfo[]? Streams { get; set; }

    /// <summary>
    /// Fields not declared in the model
    /// </summary>
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtraProperties { get; set; }
}
