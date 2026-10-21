using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Stream;

/// <summary>
/// Data flow statistics
/// </summary>
public class StreamStats
{
    /// <summary>
    /// Data stream creation time (microseconds)
    /// </summary>
    [JsonPropertyName("created_at")]
    public long CreatedAt { get; set; }

    /// <summary>
    /// Minimum timestamp recorded (microseconds)
    /// </summary>
    [JsonPropertyName("doc_time_min")]
    public long DocTimeMin { get; set; }

    /// <summary>
    /// Maximum timestamp recorded (microseconds)
    /// </summary>
    [JsonPropertyName("doc_time_max")]
    public long DocTimeMax { get; set; }

    /// <summary>
    /// Number of records
    /// </summary>
    [JsonPropertyName("doc_num")]
    public long DocNum { get; set; }

    /// <summary>
    /// Number of stored files
    /// </summary>
    [JsonPropertyName("file_num")]
    public long FileNum { get; set; }

    /// <summary>
    /// Original data size
    /// </summary>
    /// <remarks>
    /// The document field table is marked as int64, but the response example is decimal (3323.5), so double is used
    /// </remarks>
    [JsonPropertyName("storage_size")]
    public double StorageSize { get; set; }

    /// <summary>
    /// Size after compression
    /// </summary>
    [JsonPropertyName("compressed_size")]
    public double CompressedSize { get; set; }

    /// <summary>
    /// Index size
    /// </summary>
    [JsonPropertyName("index_size")]
    public double IndexSize { get; set; }
}
