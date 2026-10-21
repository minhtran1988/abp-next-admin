using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Stream;

/// <summary>
/// Supports add/set/remove setting items
/// </summary>
public class UpdateStreamSettingValue<T>
{
    /// <summary>
    /// Cover
    /// </summary>
    [JsonPropertyName("set")]
    public T[]? Set { get; set; }

    /// <summary>
    /// Append
    /// </summary>
    [JsonPropertyName("add")]
    public T[]? Add { get; set; }

    /// <summary>
    /// Remove
    /// </summary>
    [JsonPropertyName("remove")]
    public T[]? Remove { get; set; }
}
