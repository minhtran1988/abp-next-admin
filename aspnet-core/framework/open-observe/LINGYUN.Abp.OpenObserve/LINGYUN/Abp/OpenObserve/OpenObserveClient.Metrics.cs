using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.OpenObserve;

public partial class OpenObserveClient
{
    /// <summary>
    /// Internal indicator (Prometheus text format)
    /// </summary>
    /// <remarks>
    /// GET /metrics, note that this interface is not under the /api prefix
    /// The server needs to be turned on ZO_PROMETHEUS_ENABLE=true, and empty content will be returned if it is not turned on.
    /// Reference: https://openobserve.ai/docs/reference/api/metrics/
    /// </remarks>
    /// <returns>Prometheus text format content (text/plain; version=0.0.4)</returns>
    public async virtual Task<string> GetMetricsAsync(
        CancellationToken cancellationToken = default)
    {
        var httpResponse = await HttpClient.GetAsync("/metrics", cancellationToken);

        await EnsureSuccessStatusCode(httpResponse, null, cancellationToken);

        return await httpResponse.Content.ReadAsStringAsync(cancellationToken);
    }
}
