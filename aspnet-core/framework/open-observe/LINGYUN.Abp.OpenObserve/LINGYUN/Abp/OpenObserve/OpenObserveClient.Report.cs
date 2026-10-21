using LINGYUN.Abp.OpenObserve.Models;
using LINGYUN.Abp.OpenObserve.Models.Report;
using LINGYUN.Abp.OpenObserve.Utils;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.OpenObserve;

/// <summary>
/// Report interface
/// </summary>
/// <remarks>
/// The report interface uses the /api/v2 prefix
/// Reference: https://openobserve.ai/docs/reference/api/report/
/// </remarks>
public partial class OpenObserveClient
{
    /// <summary>
    /// List reports
    /// </summary>
    /// <remarks>
    /// GET /api/v2/{org_id}/reports
    /// The response is a naked array, no paging packaging
    /// </remarks>
    /// <param name="folder">Filter by folder, return reports of multiple folders when omitted</param>
    /// <param name="dashboardId">Only return reports pointing to this dashboard</param>
    /// <param name="cache">When true, only reports without delivery targets (cache only) will be returned</param>
    public async virtual Task<ReportInfo[]> GetReportsAsync(
        string? organization = null,
        string? folder = null,
        string? dashboardId = null,
        bool? cache = null,
        CancellationToken cancellationToken = default)
    {
        var queryString = OpenObserveQueryString.Build(
            ("folder", folder),
            ("dashboard_id", dashboardId),
            ("cache", cache));

        var requestUri = $"/api/v2/{ResolveOrganization(organization)}/reports{queryString}";

        return (await GetAsync<ReportInfo[]>(requestUri, cancellationToken: cancellationToken))!;
    }

    /// <summary>
    /// Create report
    /// </summary>
    /// <remarks>
    /// POST /api/v2/{org_id}/reports
    /// </remarks>
    /// <param name="name">Report name (unique within the organization)</param>
    /// <param name="frequency">Scheduling frequency</param>
    /// <param name="dashboards">Associated dashboards (required)</param>
    /// <param name="destinations">Delivery destination (required)</param>
    /// <param name="startTime">Generate start time, converted to microsecond submission</param>
    /// <param name="folder">Destination folder, required for Enterprise version</param>
    public async virtual Task<OpenObserveCodeMessageResponse> CreateReportAsync(
        string name,
        ReportFrequency frequency,
        ReportDashboardReference[] dashboards,
        ReportDestination[] destinations,
        string? title = null,
        string? description = null,
        string? message = null,
        bool enabled = true,
        DateTime? startTime = null,
        string? timezone = null,
        int? timezoneOffset = null,
        string? folder = null,
        string? organization = null,
        CancellationToken cancellationToken = default)
    {
        var queryString = OpenObserveQueryString.Build(("folder", folder));

        var requestUri = $"/api/v2/{ResolveOrganization(organization)}/reports{queryString}";

        return (await PostJsonAsync<SaveReportRequest, OpenObserveCodeMessageResponse>(
            requestUri,
            CreateReportRequest(name, frequency, dashboards, destinations, title, description,
                message, enabled, startTime, timezone, timezoneOffset, organization),
            cancellationToken: cancellationToken))!;
    }

    /// <summary>
    /// Get the report
    /// </summary>
    /// <remarks>
    /// GET /api/v2/{org_id}/reports/{report_id}
    /// </remarks>
    public async virtual Task<ReportInfo> GetReportAsync(
        string reportId,
        string? organization = null,
        string? folder = null,
        CancellationToken cancellationToken = default)
    {
        var queryString = OpenObserveQueryString.Build(("folder", folder));

        var requestUri = $"/api/v2/{ResolveOrganization(organization)}/reports/{Uri.EscapeDataString(reportId)}{queryString}";

        return (await GetAsync<ReportInfo>(requestUri, cancellationToken: cancellationToken))!;
    }

    /// <summary>
    /// Update report
    /// </summary>
    /// <remarks>
    /// PUT /api/v2/{org_id}/reports/{report_id}
    /// Need to provide complete report content
    /// </remarks>
    /// <param name="folder">Move the report to this folder when passing in</param>
    public async virtual Task<OpenObserveCodeMessageResponse> UpdateReportAsync(
        string reportId,
        string name,
        ReportFrequency frequency,
        ReportDashboardReference[] dashboards,
        ReportDestination[] destinations,
        string? title = null,
        string? description = null,
        string? message = null,
        bool enabled = true,
        DateTime? startTime = null,
        string? timezone = null,
        int? timezoneOffset = null,
        string? folder = null,
        string? organization = null,
        CancellationToken cancellationToken = default)
    {
        var queryString = OpenObserveQueryString.Build(("folder", folder));

        var requestUri = $"/api/v2/{ResolveOrganization(organization)}/reports/{Uri.EscapeDataString(reportId)}{queryString}";

        return (await PutJsonAsync<SaveReportRequest, OpenObserveCodeMessageResponse>(
            requestUri,
            CreateReportRequest(name, frequency, dashboards, destinations, title, description,
                message, enabled, startTime, timezone, timezoneOffset, organization),
            cancellationToken: cancellationToken))!;
    }

    /// <summary>
    /// Delete report
    /// </summary>
    /// <remarks>
    /// DELETE /api/v2/{org_id}/reports/{report_id}
    /// </remarks>
    public async virtual Task<OpenObserveCodeMessageResponse> DeleteReportAsync(
        string reportId,
        string? organization = null,
        string? folder = null,
        CancellationToken cancellationToken = default)
    {
        var queryString = OpenObserveQueryString.Build(("folder", folder));

        var requestUri = $"/api/v2/{ResolveOrganization(organization)}/reports/{Uri.EscapeDataString(reportId)}{queryString}";

        return (await DeleteAsync<OpenObserveCodeMessageResponse>(requestUri, cancellationToken: cancellationToken))!;
    }

    /// <summary>
    /// Delete reports in batches
    /// </summary>
    /// <remarks>
    /// DELETE /api/v2/{org_id}/reports/bulk
    /// Deletion is idempotent, and non-existing identifiers will also be reported as successful.
    /// </remarks>
    public async virtual Task<BulkDeleteReportsResponse> BulkDeleteReportsAsync(
        IEnumerable<string> ids,
        string? organization = null,
        CancellationToken cancellationToken = default)
    {
        var requestUri = $"/api/v2/{ResolveOrganization(organization)}/reports/bulk";

        return (await SendJsonAsync<BulkDeleteReportsRequest, BulkDeleteReportsResponse>(
            HttpMethod.Delete,
            requestUri,
            new BulkDeleteReportsRequest { Ids = [.. ids] },
            cancellationToken: cancellationToken))!;
    }

    /// <summary>
    /// Mobile report
    /// </summary>
    /// <remarks>
    /// PATCH /api/v2/{org_id}/reports/move
    /// </remarks>
    public async virtual Task<OpenObserveCodeMessageResponse> MoveReportsAsync(
        IEnumerable<string> reportIds,
        string destinationFolderId,
        string? organization = null,
        CancellationToken cancellationToken = default)
    {
        var requestUri = $"/api/v2/{ResolveOrganization(organization)}/reports/move";

        return (await SendJsonAsync<MoveReportsRequest, OpenObserveCodeMessageResponse>(
            HttpMethod.Patch,
            requestUri,
            new MoveReportsRequest
            {
                ReportIds = [.. reportIds],
                DestinationFolderId = destinationFolderId,
            },
            cancellationToken: cancellationToken))!;
    }

    /// <summary>
    /// Enable/disable reports
    /// </summary>
    /// <remarks>
    /// PATCH /api/v2/{org_id}/reports/{report_id}/enable?value=true
    /// </remarks>
    public async virtual Task<EnableReportResponse> EnableReportAsync(
        string reportId,
        bool value = true,
        string? organization = null,
        string? folder = null,
        CancellationToken cancellationToken = default)
    {
        var queryString = OpenObserveQueryString.Build(
            ("folder", folder),
            ("value", value));

        var requestUri = $"/api/v2/{ResolveOrganization(organization)}/reports/{Uri.EscapeDataString(reportId)}/enable{queryString}";

        return (await SendJsonAsync<object, EnableReportResponse>(
            HttpMethod.Patch,
            requestUri,
            null,
            cancellationToken: cancellationToken))!;
    }

    /// <summary>
    /// Manually trigger the report
    /// </summary>
    /// <remarks>
    /// PUT /api/v2/{org_id}/reports/{report_id}/trigger
    /// </remarks>
    public async virtual Task<OpenObserveCodeMessageResponse> TriggerReportAsync(
        string reportId,
        string? organization = null,
        string? folder = null,
        CancellationToken cancellationToken = default)
    {
        var queryString = OpenObserveQueryString.Build(("folder", folder));

        var requestUri = $"/api/v2/{ResolveOrganization(organization)}/reports/{Uri.EscapeDataString(reportId)}/trigger{queryString}";

        return (await SendJsonAsync<object, OpenObserveCodeMessageResponse>(
            HttpMethod.Put,
            requestUri,
            null,
            cancellationToken: cancellationToken))!;
    }

    /// <summary>
    /// Assemble report request
    /// </summary>
    protected virtual SaveReportRequest CreateReportRequest(
        string name,
        ReportFrequency frequency,
        ReportDashboardReference[] dashboards,
        ReportDestination[] destinations,
        string? title,
        string? description,
        string? message,
        bool enabled,
        DateTime? startTime,
        string? timezone,
        int? timezoneOffset,
        string? organization)
    {
        return new SaveReportRequest
        {
            Name = name,
            Title = title,
            Organization = ResolveOrganization(organization),
            Description = description,
            Message = message,
            Enabled = enabled,
            Frequency = frequency,
            Start = startTime?.ToMicroseconds(),
            Timezone = timezone,
            TimezoneOffset = timezoneOffset,
            Dashboards = dashboards,
            Destinations = destinations,
        };
    }
}
