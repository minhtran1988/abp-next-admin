using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Ingestion;

/// <summary>
/// _bulk response
/// </summary>
/// <remarks>
/// Elasticsearch _bulk compatible structure
/// {"took":0,"errors":false,"items":[{"index":{"_index":"stream1","_id":"..","status":200,"result":"created"}}]}
/// </remarks>
public class JsonBulkResponse
{
    /// <summary>
    /// Time consuming
    /// </summary>
    /// <remarks>
    /// OpenObserve returns 0 fixedly and does not fill in the actual time consumption
    /// </remarks>
    [JsonPropertyName("took")]
    public long Took { get; set; }

    /// <summary>
    /// Whether there is a failure item
    /// </summary>
    [JsonPropertyName("errors")]
    public bool Errors { get; set; }

    /// <summary>
    /// The processing result of each record
    /// </summary>
    /// <remarks>
    /// The key of each element is the action used when submitting (index/create/update)
    /// </remarks>
    [JsonPropertyName("items")]
    public Dictionary<string, JsonBulkItem>[] Items { get; set; } = [];
}
