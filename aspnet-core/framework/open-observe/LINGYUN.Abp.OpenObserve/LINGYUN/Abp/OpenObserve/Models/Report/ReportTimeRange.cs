using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Report;

/// <summary>
/// Report time range
/// </summary>
public class ReportTimeRange
{
    /// <summary>
    /// Type
    /// </summary>
    /// <remarks>
    /// as relative
    /// </remarks>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Period
    /// </summary>
    /// <remarks>
    /// Such as 1w
    /// </remarks>
    [JsonPropertyName("period")]
    public string? Period { get; set; }

    [JsonPropertyName("from")]
    public long From { get; set; }

    [JsonPropertyName("to")]
    public long To { get; set; }
}
