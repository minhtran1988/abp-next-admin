using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Function;

/// <summary>
/// Association between function and data flow
/// </summary>
public class FunctionStreamInfo
{
    /// <summary>
    /// Data stream name
    /// </summary>
    [JsonPropertyName("stream")]
    public string? Stream { get; set; }

    /// <summary>
    /// Execution order, the smaller the value, the earlier it is executed.
    /// </summary>
    [JsonPropertyName("order")]
    public int? Order { get; set; }

    /// <summary>
    /// Data stream type
    /// </summary>
    [JsonPropertyName("streamType")]
    public string? StreamType { get; set; }

    /// <summary>
    /// Whether to remove
    /// </summary>
    [JsonPropertyName("isRemoved")]
    public bool? IsRemoved { get; set; }

    /// <summary>
    /// Whether to execute before flattening
    /// </summary>
    [JsonPropertyName("applyBeforeFlattening")]
    public bool? ApplyBeforeFlattening { get; set; }
}
