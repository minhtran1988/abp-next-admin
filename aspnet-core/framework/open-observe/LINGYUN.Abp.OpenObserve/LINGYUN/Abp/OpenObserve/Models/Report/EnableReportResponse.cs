using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Report;

/// <summary>
/// Enable/disable report response
/// </summary>
public class EnableReportResponse
{
    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; }
}
