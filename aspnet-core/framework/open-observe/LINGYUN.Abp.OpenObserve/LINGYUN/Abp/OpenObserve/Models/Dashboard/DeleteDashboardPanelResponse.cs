using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Dashboard;

/// <summary>
/// Delete dashboard panel response
/// </summary>
public class DeleteDashboardPanelResponse
{
    /// <summary>
    /// New dashboard hash
    /// </summary>
    [JsonPropertyName("hash")]
    public string? Hash { get; set; }

    /// <summary>
    /// Deleted panel identifier
    /// </summary>
    [JsonPropertyName("panelId")]
    public string? PanelId { get; set; }
}
