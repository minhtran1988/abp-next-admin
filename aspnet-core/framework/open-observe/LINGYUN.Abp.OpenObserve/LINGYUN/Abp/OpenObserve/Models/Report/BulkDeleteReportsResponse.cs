using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Report;

/// <summary>
/// Batch delete responses
/// </summary>
public class BulkDeleteReportsResponse
{
    [JsonPropertyName("successful")]
    public string[] Successful { get; set; } = [];

    [JsonPropertyName("unsuccessful")]
    public string[] Unsuccessful { get; set; } = [];

    /// <summary>
    /// Error message
    /// </summary>
    [JsonPropertyName("err")]
    public string? Error { get; set; }
}
