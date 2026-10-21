using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Ingestion;

/// <summary>
/// _bulk Single record processing result
/// </summary>
public class JsonBulkItem
{
    [JsonPropertyName("_index")]
    public string? Index { get; set; }

    [JsonPropertyName("_id")]
    public string? Id { get; set; }

    /// <summary>
    /// version
    /// </summary>
    /// <remarks>
    /// Placeholder value compatible with Elasticsearch, does not represent the real version
    /// </remarks>
    [JsonPropertyName("_version")]
    public int Version { get; set; }

    /// <summary>
    /// Processing results
    /// </summary>
    /// <remarks>
    /// as created
    /// </remarks>
    [JsonPropertyName("result")]
    public string? Result { get; set; }

    /// <summary>
    /// Status code
    /// </summary>
    /// <remarks>
    /// 200 means success, >=400 means failure
    /// </remarks>
    [JsonPropertyName("status")]
    public int Status { get; set; }

    /// <summary>
    /// Error message
    /// </summary>
    [JsonPropertyName("error")]
    public JsonBulkItemError? Error { get; set; }

    /// <summary>
    /// Original record
    /// </summary>
    /// <remarks>
    /// Return only on failure
    /// </remarks>
    [JsonPropertyName("originalRecord")]
    public JsonElement? OriginalRecord { get; set; }

    /// <summary>
    /// Is it successful?
    /// </summary>
    [JsonIgnore]
    public bool IsSuccess => Status is >= 200 and < 300;
}
