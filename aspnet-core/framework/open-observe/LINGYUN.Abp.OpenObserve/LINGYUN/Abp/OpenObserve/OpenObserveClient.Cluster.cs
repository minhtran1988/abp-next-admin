using LINGYUN.Abp.OpenObserve.Models;
using LINGYUN.Abp.OpenObserve.Models.Cluster;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.OpenObserve;

public partial class OpenObserveClient
{
    /// <summary>
    /// Cluster information
    /// </summary>
    /// <remarks>
    /// Returns the number of pending compression tasks for each region and each node
    /// According to actual testing, this API is only open to the _meta organization, and using other organizations returns 403 "This API is only available for the _meta organization",
    /// Therefore organization defaults to _meta
    /// The path given in the document is GET /api/{org_id}/cluster_info, the actual measurement is 404, and the actual path is GET /api/{org_id}/cluster/info
    /// Reference: https://openobserve.ai/docs/reference/api/cluster/cluster-info/
    /// </remarks>
    public async virtual Task<ClusterInfoResponse> GetClusterInfoAsync(
        string? organization = OpenObserveConstants.MetaOrganization,
        CancellationToken cancellationToken = default)
    {
        var requestUri = CreateClusterInfoRequestUri(ResolveOrganization(organization));

        return (await GetAsync<ClusterInfoResponse>(requestUri, cancellationToken: cancellationToken))!;
    }

    /// <summary>
    /// Create cluster information request address
    /// </summary>
    /// <remarks>
    /// The path between the document and the implementation is inconsistent. Rewrite this method when compatibility is required.
    /// </remarks>
    protected virtual string CreateClusterInfoRequestUri(string organization)
    {
        return $"/api/{organization}/cluster/info";
    }
}
