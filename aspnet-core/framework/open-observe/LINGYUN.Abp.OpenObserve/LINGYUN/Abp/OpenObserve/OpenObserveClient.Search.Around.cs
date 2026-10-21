using LINGYUN.Abp.OpenObserve.Models.Search;
using LINGYUN.Abp.OpenObserve.Models.Stream;
using LINGYUN.Abp.OpenObserve.Utils;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.OpenObserve;

public partial class OpenObserveClient
{
    /// <summary>
    /// Query the data before and after a record
    /// </summary>
    /// <remarks>
    /// GET /api/{organization}/{stream}/_around
    /// Center the record corresponding to key(_timestamp) and query within 5 minutes before and after
    /// Reference: https://openobserve.ai/docs/reference/api/search/
    /// </remarks>
    /// <param name="key">_timestamp (microseconds) recorded by the center</param>
    /// <param name="size">Number of items returned</param>
    public async virtual Task<SearchResponse<THint>> AroundAsync<THint>(
        string stream,
        long key,
        long? size = null,
        StreamType? type = null,
        int? timeout = null,
        string? organization = null,
        CancellationToken cancellationToken = default)
    {
        var queryString = OpenObserveQueryString.Build(
            ("key", key),
            ("size", size),
            ("type", type?.AsString()),
            ("timeout", timeout));

        var requestUri = $"/api/{ResolveOrganization(organization)}/{Uri.EscapeDataString(stream)}/_around{queryString}";

        return (await GetAsync<SearchResponse<THint>>(requestUri, cancellationToken: cancellationToken))!;
    }
}
