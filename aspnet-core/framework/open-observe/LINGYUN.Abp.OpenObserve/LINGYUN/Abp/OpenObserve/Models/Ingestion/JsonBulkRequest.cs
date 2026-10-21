using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Ingestion;

/// <summary>
/// _bulk action line
/// </summary>
/// <remarks>
/// {"index":{"_index":"stream1"}}, the action name (index/create/update) is determined by the key of the outer dictionary
/// </remarks>
public class JsonBulkRequest
{
    /// <summary>
    /// Data stream name
    /// </summary>
    [JsonPropertyName("_index")]
    public string Index { get; set; } = default!;
}
