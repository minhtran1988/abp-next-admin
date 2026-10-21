using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Report;

/// <summary>
/// Report information
/// </summary>
public class ReportInfo
{
    /// <summary>
    /// Report identification (KSUID)
    /// </summary>
    [JsonPropertyName("report_id")]
    public string? ReportId { get; set; }

    /// <summary>
    /// Report name
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Display title
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    /// <summary>
    /// The folder to which it belongs
    /// </summary>
    [JsonPropertyName("folder_id")]
    public string? FolderId { get; set; }

    /// <summary>
    /// Associated dashboard
    /// </summary>
    /// <remarks>
    /// The list interface returns a string array, and the create/update interface is an object array, so use free JSON
    /// </remarks>
    [JsonPropertyName("dashboards")]
    public JsonNode? Dashboards { get; set; }

    /// <summary>
    /// Scheduling frequency
    /// </summary>
    [JsonPropertyName("frequency")]
    public ReportFrequency? Frequency { get; set; }

    /// <summary>
    /// Whether to enable
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; }

    /// <summary>
    /// Owner
    /// </summary>
    [JsonPropertyName("owner")]
    public string? Owner { get; set; }

    /// <summary>
    /// Last trigger time (microseconds)
    /// </summary>
    [JsonPropertyName("last_triggered_at")]
    public long? LastTriggeredAt { get; set; }

    /// <summary>
    /// Fields not declared in the model
    /// </summary>
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtraProperties { get; set; }
}
