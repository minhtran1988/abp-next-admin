using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Stream;

/// <summary>
/// Create data flow setting request
/// </summary>
/// <remarks>
/// POST /api/{org_id}/streams/{stream_name}/settings, all fields optional
/// </remarks>
public class StreamSettingRequest
{
    /// <summary>
    /// Partition field
    /// </summary>
    [JsonPropertyName("partition_keys")]
    public string[]? PartitionKeys { get; set; }

    /// <summary>
    /// Secondary index field
    /// </summary>
    [JsonPropertyName("index_fields")]
    public string[]? IndexFields { get; set; }

    /// <summary>
    /// Full text search field
    /// </summary>
    [JsonPropertyName("full_text_search_keys")]
    public string[]? FullTextSearchKeys { get; set; }

    /// <summary>
    /// Bloom filter field
    /// </summary>
    [JsonPropertyName("bloom_filter_fields")]
    public string[]? BloomFilterFields { get; set; }

    /// <summary>
    /// Number of days to retain data
    /// </summary>
    /// <remarks>
    /// Minimum 3 days, compliance flow minimum 30 days
    /// </remarks>
    [JsonPropertyName("data_retention")]
    public int? DataRetention { get; set; }

    /// <summary>
    /// Flatten the hierarchy
    /// </summary>
    [JsonPropertyName("flatten_level")]
    public int? FlattenLevel { get; set; }

    /// <summary>
    /// Reserved custom schema fields
    /// </summary>
    [JsonPropertyName("defined_schema_fields")]
    public string[]? DefinedSchemaFields { get; set; }

    /// <summary>
    /// Maximum time range for a single query (hours)
    /// </summary>
    [JsonPropertyName("max_query_range")]
    public int? MaxQueryRange { get; set; }

    /// <summary>
    /// Whether to store original data
    /// </summary>
    [JsonPropertyName("store_original_data")]
    public bool? StoreOriginalData { get; set; }

    /// <summary>
    /// Whether to use equal time range partitioning
    /// </summary>
    [JsonPropertyName("approx_partition")]
    public bool? ApproxPartition { get; set; }

    /// <summary>
    /// Extend the time range of the retention period
    /// </summary>
    /// <remarks>
    /// The document does not describe the specific structure of the element
    /// </remarks>
    [JsonPropertyName("extended_retention_days")]
    public object[]? ExtendedRetentionDays { get; set; }

    /// <summary>
    /// Whether to establish a full-text index for the original log body
    /// </summary>
    [JsonPropertyName("index_original_data")]
    public bool? IndexOriginalData { get; set; }

    /// <summary>
    /// Whether to create exact matching indexes for all fields
    /// </summary>
    [JsonPropertyName("index_all_values")]
    public bool? IndexAllValues { get; set; }

    /// <summary>
    /// Storage type
    /// </summary>
    /// <remarks>
    /// normal (default) / compliance
    /// </remarks>
    [JsonPropertyName("storage_type")]
    public string? StorageType { get; set; }
}
