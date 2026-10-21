using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Report;

/// <summary>
/// Scheduling frequency
/// </summary>
public class ReportFrequency
{
    /// <summary>
    /// Frequency type
    /// </summary>
    /// <remarks>
    /// once / hours / days / weeks / months / cron
    /// </remarks>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// interval
    /// </summary>
    [JsonPropertyName("interval")]
    public int? Interval { get; set; }

    /// <summary>
    /// cron expression
    /// </summary>
    [JsonPropertyName("cron")]
    public string? Cron { get; set; }

    /// <summary>
    /// Whether to align time
    /// </summary>
    [JsonPropertyName("align_time")]
    public bool? AlignTime { get; set; }
}
