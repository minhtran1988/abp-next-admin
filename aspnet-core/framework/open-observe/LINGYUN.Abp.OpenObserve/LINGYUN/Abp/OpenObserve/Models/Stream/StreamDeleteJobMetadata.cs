using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Stream;

/// <summary>
/// Delete task execution details
/// </summary>
public class StreamDeleteJobMetadata
{
    [JsonPropertyName("cluster")]
    public string? Cluster { get; set; }

    [JsonPropertyName("region")]
    public string? Region { get; set; }

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("key")]
    public string? Key { get; set; }

    /// <summary>
    /// Creation time (microseconds)
    /// </summary>
    [JsonPropertyName("created_at")]
    public long CreatedAt { get; set; }

    /// <summary>
    /// End time (microseconds), 0 means still in progress
    /// </summary>
    [JsonPropertyName("ended_at")]
    public long EndedAt { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }
}
