using System;
using System.Collections.Generic;
using System.Linq;

namespace LINGYUN.Abp.Telemetry.OpenTelemetry;
public class AbpTelemetryOpenTelemetryOptions
{
    /// <summary>
    /// Whether to ignore CAP dashboard requests, default: true
    /// </summary>
    public bool IgnoreCapDashboardUrls { get; set; }
    /// <summary>
    /// Whether to ignore ES requests, default: true
    /// </summary>
    public bool IgnoreElasticsearchUrls { get; set; }
    /// <summary>
    /// Ignore local request paths
    /// </summary>
    public List<string> IgnoreLocalRequestUrls { get; set; }
    /// <summary>
    /// Ignore the remote request path
    /// </summary>
    public List<string> IgnoreRemoteRequestUrls { get; set; }

    public AbpTelemetryOpenTelemetryOptions()
    {
        IgnoreLocalRequestUrls = new List<string>
        {
             "/healthz", // Service health status request is not recorded
        };
        IgnoreRemoteRequestUrls = new List<string>();
        IgnoreCapDashboardUrls = true;
        IgnoreElasticsearchUrls = true;
    }

    public bool IsIgnureLocalRequestUrl(string url)
    {
        // Ignore CAP dashboard requests
        if (IgnoreCapDashboardUrls && url.StartsWith("/cap"))
        {
            return true;
        }

        return IgnoreLocalRequestUrls.Any(localUrl => url.Equals(localUrl, StringComparison.InvariantCultureIgnoreCase));
    }

    public bool IsIgnureRemoteRequestUrl(string url)
    {
        // Ignore requests to push data to es
        if (IgnoreElasticsearchUrls && url.EndsWith("/_bulk"))
        {
            return true;
        }

        return IgnoreRemoteRequestUrls.Any(remoteUrl => url.Equals(remoteUrl, StringComparison.InvariantCultureIgnoreCase));
    }
}

