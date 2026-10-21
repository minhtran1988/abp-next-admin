using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Dashboard;

/// <summary>
/// Add/update dashboard panel request
/// </summary>
/// <remarks>
/// panel is a v8 panel object, the document does not give a complete schema, so free JSON is used
/// Layout can be omitted (automatically calculated) when adding new items, and omitting layout when updating means retaining the original layout.
/// </remarks>
public class SaveDashboardPanelRequest
{
    /// <summary>
    /// Panel definition
    /// </summary>
    [JsonPropertyName("panel")]
    public JsonNode? Panel { get; set; }

    /// <summary>
    /// Page label identification
    /// </summary>
    /// <remarks>
    /// Default first tab
    /// </remarks>
    [JsonPropertyName("tabId")]
    public string? TabId { get; set; }
}
