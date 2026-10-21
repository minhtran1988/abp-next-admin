using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Stream;

/// <summary>
/// Data flow information
/// </summary>
public class StreamInfo
{
    /// <summary>
    /// Data stream name
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    /// <summary>
    /// Storage type
    /// </summary>
    /// <remarks>
    /// s3 / disk
    /// </remarks>
    [JsonPropertyName("storage_type")]
    public string? StorageType { get; set; }

    /// <summary>
    /// Data stream type
    /// </summary>
    /// <remarks>
    /// logs / metrics / traces
    /// </remarks>
    [JsonPropertyName("stream_type")]
    public string? StreamType { get; set; }

    /// <summary>
    /// Statistics
    /// </summary>
    [JsonPropertyName("stats")]
    public StreamStats? Stats { get; set; }

    /// <summary>
    /// Field definition
    /// </summary>
    /// <remarks>
    /// Does not return when the query parameter fetchSchema is false
    /// </remarks>
    [JsonPropertyName("schema")]
    public StreamSchemaField[]? Schema { get; set; }

    /// <summary>
    /// Data flow settings
    /// </summary>
    [JsonPropertyName("settings")]
    public StreamSettingsInfo? Settings { get; set; }
}
