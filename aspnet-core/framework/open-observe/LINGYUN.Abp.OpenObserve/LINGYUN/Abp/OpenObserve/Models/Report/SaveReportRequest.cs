using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Report;

/// <summary>
/// Create/update report request
/// </summary>
public class SaveReportRequest
{
    /// <summary>
    /// Report name (unique within the organization)
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    /// <summary>
    /// Display title
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    /// <summary>
    /// Organization name (required)
    /// </summary>
    [JsonPropertyName("org_id")]
    public string Organization { get; set; } = default!;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Report email text
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; }

    [JsonPropertyName("frequency")]
    public ReportFrequency? Frequency { get; set; }

    /// <summary>
    /// Generation start time (microseconds)
    /// </summary>
    [JsonPropertyName("start")]
    public long? Start { get; set; }

    /// <summary>
    /// Time zone name
    /// </summary>
    [JsonPropertyName("timezone")]
    public string? Timezone { get; set; }

    /// <summary>
    /// Fixed time zone offset (minutes)
    /// </summary>
    [JsonPropertyName("timezoneOffset")]
    public int? TimezoneOffset { get; set; }

    /// <summary>
    /// Associated dashboard (required)
    /// </summary>
    [JsonPropertyName("dashboards")]
    public ReportDashboardReference[] Dashboards { get; set; } = [];

    /// <summary>
    /// Delivery target (required)
    /// </summary>
    [JsonPropertyName("destinations")]
    public ReportDestination[] Destinations { get; set; } = [];
}
