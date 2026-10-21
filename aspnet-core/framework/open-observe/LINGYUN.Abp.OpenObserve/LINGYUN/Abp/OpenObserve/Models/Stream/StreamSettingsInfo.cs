using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Stream;

/// <summary>
/// Data flow settings (response)
/// </summary>
public class StreamSettingsInfo
{
    /// <summary>
    /// Partition field
    /// </summary>
    [JsonPropertyName("partition_keys")]
    public Dictionary<string, object?>? PartitionKeys { get; set; }

    /// <summary>
    /// Full text search field
    /// </summary>
    [JsonPropertyName("full_text_search_keys")]
    public string[]? FullTextSearchKeys { get; set; }
}
