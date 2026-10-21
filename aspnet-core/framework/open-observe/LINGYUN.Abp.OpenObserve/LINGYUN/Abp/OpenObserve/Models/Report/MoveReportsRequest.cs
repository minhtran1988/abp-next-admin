using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Report;

/// <summary>
/// Mobile report request
/// </summary>
public class MoveReportsRequest
{
    /// <summary>
    /// Report identification collection
    /// </summary>
    [JsonPropertyName("report_ids")]
    public string[] ReportIds { get; set; } = [];

    /// <summary>
    /// Target folder
    /// </summary>
    [JsonPropertyName("dst_folder_id")]
    public string DestinationFolderId { get; set; } = default!;
}
