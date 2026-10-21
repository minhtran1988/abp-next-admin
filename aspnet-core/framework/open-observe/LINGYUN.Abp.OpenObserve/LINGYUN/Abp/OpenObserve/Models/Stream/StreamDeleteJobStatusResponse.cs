using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Stream;

/// <summary>
/// Delete data task status by time range
/// </summary>
public class StreamDeleteJobStatusResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = default!;

    /// <summary>
    /// Task status
    /// </summary>
    /// <remarks>
    /// Completed / Pending
    /// </remarks>
    [JsonPropertyName("status")]
    public string Status { get; set; } = default!;

    /// <summary>
    /// Execution details of each cluster/region
    /// </summary>
    [JsonPropertyName("metadata")]
    public StreamDeleteJobMetadata[]? Metadata { get; set; }

    /// <summary>
    /// Execution error
    /// </summary>
    [JsonPropertyName("errors")]
    public StreamDeleteJobError[]? Errors { get; set; }
}
