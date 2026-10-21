using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Function;

/// <summary>
/// Create/update function request
/// </summary>
public class SaveFunctionRequest
{
    /// <summary>
    /// Function name
    /// </summary>
    /// <remarks>
    /// The update interface is subject to the path parameters.
    /// </remarks>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Function body
    /// </summary>
    [JsonPropertyName("function")]
    public string Function { get; set; } = default!;

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
    /// Execution order
    /// </summary>
    /// <remarks>
    /// The document example contains this field, but the current implementation does not accept it and is reserved for compatibility with the document calling method.
    /// </remarks>
    [JsonPropertyName("order")]
    public int? Order { get; set; }
}
