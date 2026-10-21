using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Stream;

/// <summary>
/// Delete data response by time range
/// </summary>
public class StreamDeleteJobResponse
{
    /// <summary>
    /// Delete task ID
    /// </summary>
    /// <remarks>
    /// Used to query deletion progress
    /// </remarks>
    [JsonPropertyName("id")]
    public string Id { get; set; } = default!;
}
