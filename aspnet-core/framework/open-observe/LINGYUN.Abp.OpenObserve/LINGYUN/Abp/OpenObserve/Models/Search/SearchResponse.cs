using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Search;

/// <summary>
/// Search response
/// </summary>
/// <remarks>
/// Reference: https://openobserve.ai/docs/reference/api/search/
/// </remarks>
public class SearchResponse<THit>
{
    /// <summary>
    /// Hit data
    /// </summary>
    /// <remarks>
    /// Each record is the original row when ingested, with _timestamp (microseconds) and _p appended
    /// </remarks>
    [JsonPropertyName("hits")]
    public THit[] Hits { get; set; } = [];

    /// <summary>
    /// Total number of hits
    /// </summary>
    [JsonPropertyName("total")]
    public long Total { get; set; }

    /// <summary>
    /// Offset
    /// </summary>
    /// <remarks>
    /// Taken from query.from
    /// </remarks>
    [JsonPropertyName("from")]
    public long From { get; set; }

    /// <summary>
    /// quantity
    /// </summary>
    /// <remarks>
    /// Taken from query.size
    /// </remarks>
    [JsonPropertyName("size")]
    public long Size { get; set; }

    /// <summary>
    /// Query time consumption
    /// </summary>
    /// <remarks>
    /// Unit: milliseconds
    /// </remarks>
    [JsonPropertyName("took")]
    public long Took { get; set; }

    /// <summary>
    /// Time-consuming details
    /// </summary>
    [JsonPropertyName("took_detail")]
    public SearchTookDetail? TookDetail { get; set; }

    /// <summary>
    /// Scan data amount
    /// </summary>
    /// <remarks>
    /// Unit: MB
    /// </remarks>
    [JsonPropertyName("scan_size")]
    public long ScanSize { get; set; }

    /// <summary>
    /// Number of scan records
    /// </summary>
    [JsonPropertyName("scan_records")]
    public long ScanRecords { get; set; }

    /// <summary>
    /// Cache hit ratio
    /// </summary>
    [JsonPropertyName("result_cache_ratio")]
    public int? ResultCacheRatio { get; set; }

    /// <summary>
    /// Returned columns
    /// </summary>
    [JsonPropertyName("columns")]
    public string[]? Columns { get; set; }

    /// <summary>
    /// Link ID
    /// </summary>
    [JsonPropertyName("trace_id")]
    public string? TraceId { get; set; }

    /// <summary>
    /// Function execution error
    /// </summary>
    [JsonPropertyName("function_error")]
    public string[]? FunctionError { get; set; }

    /// <summary>
    /// Is it partial data?
    /// </summary>
    /// <remarks>
    /// When true, it indicates that the response is based on partial data
    /// </remarks>
    [JsonPropertyName("is_partial")]
    public bool IsPartial { get; set; }

    /// <summary>
    /// Histogram interval (seconds)
    /// </summary>
    [JsonPropertyName("histogram_interval")]
    public long? HistogramInterval { get; set; }

    /// <summary>
    /// Server-side adjusted start time (microseconds)
    /// </summary>
    /// <remarks>
    /// The time range will be adjusted when limited by the max_query_range of the data stream
    /// </remarks>
    [JsonPropertyName("new_start_time")]
    public long? NewStartTime { get; set; }

    /// <summary>
    /// Server-side adjusted end time (microseconds)
    /// </summary>
    [JsonPropertyName("new_end_time")]
    public long? NewEndTime { get; set; }

    /// <summary>
    /// Sorting direction
    /// </summary>
    /// <remarks>
    /// asc / desc
    /// </remarks>
    [JsonPropertyName("order_by")]
    public string? OrderBy { get; set; }

    /// <summary>
    /// Sorting metadata
    /// </summary>
    [JsonPropertyName("order_by_metadata")]
    public string[][]? OrderByMetadata { get; set; }

    /// <summary>
    /// Response type
    /// </summary>
    [JsonPropertyName("response_type")]
    public string? ResponseType { get; set; }

    /// <summary>
    /// Result format
    /// </summary>
    /// <remarks>
    /// Returned when agent_options.output_format is csv/md_table, such as csv / md_table / ndjson
    /// </remarks>
    [JsonPropertyName("format")]
    public string? Format { get; set; }

    /// <summary>
    /// Formatted result
    /// </summary>
    /// <remarks>
    /// When agent_options.output_format is csv/md_table, hits is empty and the data is in this field
    /// </remarks>
    [JsonPropertyName("data")]
    public string? Data { get; set; }

    /// <summary>
    /// Prompt information
    /// </summary>
    [JsonPropertyName("advisory")]
    public string? Advisory { get; set; }

    /// <summary>
    /// Fields not declared in the model
    /// </summary>
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtraProperties { get; set; }
}
