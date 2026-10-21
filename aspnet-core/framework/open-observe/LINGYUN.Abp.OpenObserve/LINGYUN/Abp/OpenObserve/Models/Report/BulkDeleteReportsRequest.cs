using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Report;

/// <summary>
/// Batch delete report requests
/// </summary>
public class BulkDeleteReportsRequest
{
    [JsonPropertyName("ids")]
    public string[] Ids { get; set; } = [];
}
