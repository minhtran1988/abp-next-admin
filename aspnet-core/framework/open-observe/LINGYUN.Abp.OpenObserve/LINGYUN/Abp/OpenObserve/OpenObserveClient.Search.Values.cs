using LINGYUN.Abp.OpenObserve.Models.Search;
using LINGYUN.Abp.OpenObserve.Utils;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.OpenObserve;

public partial class OpenObserveClient
{
    /// <summary>
    /// Query field value
    /// </summary>
    /// <remarks>
    /// GET /api/{organization}/{stream}/_values
    /// Reference: https://openobserve.ai/docs/reference/api/search/
    /// </remarks>
    /// <param name="fields">Field name collection, comma splicing</param>
    /// <param name="size">The number of returned values, sorted by the number of occurrences</param>
    /// <param name="keyword">Value keyword filtering</param>
    /// <param name="noCount">When true, the count will not be returned, and the count will be sorted directly by value</param>
    public async virtual Task<ValuesResponse> GetValuesAsync(
        string stream,
        IEnumerable<string> fields,
        DateTime startTime,
        DateTime endTime,
        long? size = null,
        string? keyword = null,
        bool? noCount = null,
        string? organization = null,
        CancellationToken cancellationToken = default)
    {
        var queryString = OpenObserveQueryString.Build(
            ("fields", string.Join(",", fields)),
            ("start_time", startTime.ToMicroseconds()),
            ("end_time", endTime.ToMicroseconds()),
            ("size", size),
            ("keyword", keyword),
            ("no_count", noCount));

        var requestUri = $"/api/{ResolveOrganization(organization)}/{Uri.EscapeDataString(stream)}/_values{queryString}";

        return (await GetAsync<ValuesResponse>(requestUri, cancellationToken: cancellationToken))!;
    }
}
