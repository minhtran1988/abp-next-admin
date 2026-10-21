using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Report;

/// <summary>
/// Dashboard associated with the report
/// </summary>
public class ReportDashboardReference
{
    /// <summary>
    /// Dashboard ID
    /// </summary>
    [JsonPropertyName("dashboard")]
    public string Dashboard { get; set; } = default!;

    /// <summary>
    /// The folder where the dashboard is located
    /// </summary>
    [JsonPropertyName("folder")]
    public string? Folder { get; set; }

    /// <summary>
    /// Tag
    /// </summary>
    [JsonPropertyName("tabs")]
    public string[]? Tabs { get; set; }

    /// <summary>
    /// Variables
    /// </summary>
    [JsonPropertyName("variables")]
    public JsonNode? Variables { get; set; }

    /// <summary>
    /// time range
    /// </summary>
    [JsonPropertyName("timerange")]
    public ReportTimeRange? TimeRange { get; set; }
}
