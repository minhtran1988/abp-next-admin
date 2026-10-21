using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Stream;

/// <summary>
/// Data flow field definition
/// </summary>
public class StreamSchemaField
{
    /// <summary>
    /// Field name
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    /// <summary>
    /// Field type
    /// </summary>
    /// <remarks>
    /// Utf8 / Int64 / Float64 / Timestamp / Boolean
    /// </remarks>
    [JsonPropertyName("type")]
    public string Type { get; set; } = default!;
}
