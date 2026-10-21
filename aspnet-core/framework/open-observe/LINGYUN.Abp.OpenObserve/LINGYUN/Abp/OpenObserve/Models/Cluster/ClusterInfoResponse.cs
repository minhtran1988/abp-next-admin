using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Cluster;

/// <summary>
/// Cluster information response
/// </summary>
/// <remarks>
/// Response structure: {"regions":{"openobserve":{"zo1":{"pending_jobs":0}}}}
/// </remarks>
public class ClusterInfoResponse
{
    /// <summary>
    /// Cluster information organized by region and node
    /// </summary>
    [JsonPropertyName("regions")]
    public Dictionary<string, Dictionary<string, ClusterNodeInfo>> Regions { get; set; } = [];

    /// <summary>
    /// Fields not declared in the model
    /// </summary>
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtraProperties { get; set; }
}
