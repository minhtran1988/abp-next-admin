using System;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Search;

/// <summary>
/// Search query conditions
/// </summary>
public class SearchQuery
{
    /// <summary>
    /// SQL query statement
    /// </summary>
    /// <remarks>
    /// Use SQL to filter data, sorted by _timestamp by default, and can be overridden by order by.
    /// Use from/size for paging
    /// </remarks>
    [JsonPropertyName("sql")]
    public string Sql { get; set; }

    /// <summary>
    /// Start time (microseconds)
    /// </summary>
    /// <remarks>
    /// Must be provided, otherwise it will be scanned in full
    /// </remarks>
    [JsonPropertyName("start_time")]
    public long StartTime { get; set; }

    /// <summary>
    /// End time (microseconds)
    /// </summary>
    [JsonPropertyName("end_time")]
    public long EndTime { get; set; }

    /// <summary>
    /// Offset
    /// </summary>
    [JsonPropertyName("from")]
    public long From { get; set; }

    /// <summary>
    /// Return the number of items
    /// </summary>
    [JsonPropertyName("size")]
    public long Size { get; set; }

    /// <summary>
    /// Quick mode
    /// </summary>
    /// <remarks>
    /// Only scan Interesting Fields, the server needs to turn on ZO_QUICK_MODE_ENABLED
    /// </remarks>
    [JsonPropertyName("quick_mode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool QuickMode { get; set; }

    /// <summary>
    /// Query type label
    /// </summary>
    [JsonPropertyName("query_type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? QueryType { get; set; }

    /// <summary>
    /// Whether to count the total number of hits
    /// </summary>
    [JsonPropertyName("track_total_hits")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool TrackTotalHits { get; set; }

    /// <summary>
    /// Whether to use the function
    /// </summary>
    [JsonPropertyName("uses_zo_fn")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool UsesZoFn { get; set; }

    /// <summary>
    /// Function name to apply to the result (VRL/JS)
    /// </summary>
    [JsonPropertyName("query_fn")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? QueryFn { get; set; }

    /// <summary>
    /// Whether to skip WAL
    /// </summary>
    [JsonPropertyName("skip_wal")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool SkipWal { get; set; }

    /// <summary>
    /// Whether to stream output
    /// </summary>
    [JsonPropertyName("streaming_output")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool StreamingOutput { get; set; }

    /// <summary>
    /// Streaming identifier
    /// </summary>
    [JsonPropertyName("streaming_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? StreamingId { get; set; }

    /// <summary>
    /// Histogram interval (seconds)
    /// </summary>
    [JsonPropertyName("histogram_interval")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public long HistogramInterval { get; set; }

    /// <summary>
    /// Time zone
    /// </summary>
    /// <remarks>
    /// Fixed offset, such as +08:00, applied to histogram() buckets without specifying the third parameter
    /// </remarks>
    [JsonPropertyName("timezone")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Timezone { get; set; }

    public SearchQuery(
        string sql,
        DateTime startTime,
        DateTime endTime,
        long from = 0,
        long size = 10)
    {
        Sql = sql;
        StartTime = startTime.ToMicroseconds();
        EndTime = endTime.ToMicroseconds();
        From = from;
        Size = size;
    }

    /// <summary>
    /// Set time range
    /// </summary>
    public virtual SearchQuery WithTimeRange(DateTime startTime, DateTime endTime)
    {
        StartTime = startTime.ToMicroseconds();
        EndTime = endTime.ToMicroseconds();

        return this;
    }

    /// <summary>
    /// Set paging
    /// </summary>
    public virtual SearchQuery WithPaging(long from, long size)
    {
        From = from;
        Size = size;

        return this;
    }
}
