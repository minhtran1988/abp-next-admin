using LINGYUN.Abp.OpenObserve.Models.Dashboard;
using LINGYUN.Abp.OpenObserve.Utils;
using System;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.OpenObserve;

public partial class OpenObserveClient
{
    /// <summary>
    /// Add a new dashboard panel
    /// </summary>
    /// <remarks>
    /// POST /api/{org_id}/dashboards/{dashboard_id}/panels
    /// Only supports v8 dashboard; hash is a required parameter, and 409 will be returned if it does not match
    /// Reference: https://openobserve.ai/docs/reference/api/dashboard/
    /// </remarks>
    /// <param name="panel">Panel definition (v8 panel object), you can omit the layout when adding it and the layout will be automatically calculated by the server</param>
    /// <param name="hash">Current dashboard hash</param>
    /// <param name="tabId">Tab ID, default first tab</param>
    /// <param name="folder">The folder where the dashboard is located, the default folder</param>
    public async virtual Task<DashboardPanelResponse> AddDashboardPanelAsync(
        string dashboardId,
        string hash,
        JsonNode panel,
        string? tabId = null,
        string? folder = null,
        string? organization = null,
        CancellationToken cancellationToken = default)
    {
        var queryString = OpenObserveQueryString.Build(
            ("hash", hash),
            ("folder", folder));

        var requestUri = $"/api/{ResolveOrganization(organization)}/dashboards/{Uri.EscapeDataString(dashboardId)}/panels{queryString}";

        var request = new SaveDashboardPanelRequest
        {
            Panel = panel,
            TabId = tabId,
        };

        return (await PostJsonAsync<SaveDashboardPanelRequest, DashboardPanelResponse>(
            requestUri,
            request,
            cancellationToken: cancellationToken))!;
    }

    /// <summary>
    /// Update dashboard panel
    /// </summary>
    /// <remarks>
    /// PUT /api/{org_id}/dashboards/{dashboard_id}/panels/{panel_id}
    /// The panel_id in the path shall prevail; when panel omits layout, the original layout will be retained
    /// Reference: https://openobserve.ai/docs/reference/api/dashboard/
    /// </remarks>
    /// <param name="panel">Panel definition (v8 panel object)</param>
    /// <param name="hash">Current dashboard hash</param>
    /// <param name="tabId">Tab ID, default first tab</param>
    /// <param name="folder">The folder where the dashboard is located, the default folder</param>
    public async virtual Task<DashboardPanelResponse> UpdateDashboardPanelAsync(
        string dashboardId,
        string panelId,
        string hash,
        JsonNode panel,
        string? tabId = null,
        string? folder = null,
        string? organization = null,
        CancellationToken cancellationToken = default)
    {
        var queryString = OpenObserveQueryString.Build(
            ("hash", hash),
            ("folder", folder));

        var requestUri = $"/api/{ResolveOrganization(organization)}/dashboards/{Uri.EscapeDataString(dashboardId)}/panels/{Uri.EscapeDataString(panelId)}{queryString}";

        var request = new SaveDashboardPanelRequest
        {
            Panel = panel,
            TabId = tabId,
        };

        return (await PutJsonAsync<SaveDashboardPanelRequest, DashboardPanelResponse>(
            requestUri,
            request,
            cancellationToken: cancellationToken))!;
    }

    /// <summary>
    /// Delete dashboard panel
    /// </summary>
    /// <remarks>
    /// DELETE /api/{org_id}/dashboards/{dashboard_id}/panels/{panel_id}
    /// Reference: https://openobserve.ai/docs/reference/api/dashboard/
    /// </remarks>
    /// <param name="hash">Current dashboard hash</param>
    /// <param name="tabId">Specify the tab, if omitted, search in all tabs</param>
    /// <param name="folder">The folder where the dashboard is located, the default folder</param>
    public async virtual Task<DeleteDashboardPanelResponse> DeleteDashboardPanelAsync(
        string dashboardId,
        string panelId,
        string hash,
        string? tabId = null,
        string? folder = null,
        string? organization = null,
        CancellationToken cancellationToken = default)
    {
        var queryString = OpenObserveQueryString.Build(
            ("hash", hash),
            ("tabId", tabId),
            ("folder", folder));

        var requestUri = $"/api/{ResolveOrganization(organization)}/dashboards/{Uri.EscapeDataString(dashboardId)}/panels/{Uri.EscapeDataString(panelId)}{queryString}";

        return (await DeleteAsync<DeleteDashboardPanelResponse>(requestUri, cancellationToken: cancellationToken))!;
    }
}
