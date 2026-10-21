using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Cluster;

/// <summary>
/// Cluster node information
/// </summary>
public class ClusterNodeInfo
{
    /// <summary>
    /// Number of compression tasks to be processed
    /// </summary>
    [JsonPropertyName("pending_jobs")]
    public int PendingJobs { get; set; }

    /// <summary>
    /// Fields not declared in the model
    /// </summary>
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtraProperties { get; set; }
}
