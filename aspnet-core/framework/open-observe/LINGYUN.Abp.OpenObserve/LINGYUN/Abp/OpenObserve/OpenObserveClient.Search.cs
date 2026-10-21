using LINGYUN.Abp.OpenObserve.Models;
using LINGYUN.Abp.OpenObserve.Models.Search;
using LINGYUN.Abp.OpenObserve.Utils;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.OpenObserve;

public partial class OpenObserveClient
{
    /// <summary>
    /// Search
    /// </summary>
    /// <remarks>
    /// POST /api/{organization}/_search
    /// Reference: https://openobserve.ai/docs/reference/api/search/
    /// </remarks>
    /// <typeparam name="THint">Hit data structure</typeparam>
    /// <param name="organization">Used when not specified AbpOpenObserveOptions.Organization</param>
    /// <param name="query">Query conditions, time range is microseconds, required</param>
    /// <param name="searchType">Search source (ui/dashboards/reports/alerts, etc.)</param>
    /// <param name="timeout">Timeout (seconds), default 30</param>
    /// <param name="agentOptions">agent/MCP client options</param>
    /// <param name="useCache">Whether to use result caching, it is decided by the server when omitted (default true)</param>
    /// <param name="clearCache">Whether to clear the result cache, when true, force the cache not to be used</param>
    public async virtual Task<SearchResponse<THint>> SearchAsync<THint>(
        string? organization,
        SearchQuery query,
        SearchType? searchType = null,
        int? timeout = OpenObserveConstants.ZO_QUERY_TIMEOUT,
        SearchAgentOptions? agentOptions = null,
        bool? useCache = null,
        bool? clearCache = null,
        CancellationToken cancellationToken = default)
    {
        var queryString = OpenObserveQueryString.Build(
            ("use_cache", useCache),
            ("clear_cache", clearCache));

        var requestUri = $"/api/{ResolveOrganization(organization)}/_search{queryString}";

        var response = await PostJsonAsync<SearchRequest, SearchResponse<THint>>(
            requestUri,
            new SearchRequest(query, searchType, timeout, agentOptions),
            ThrowIfSearchException,
            cancellationToken);

        return response!;
    }

    /// <summary>
    /// Search
    /// </summary>
    /// <remarks>
    /// POST /api/{organization}/_search, using AbpOpenObserveOptions.Organization
    /// </remarks>
    public async virtual Task<SearchResponse<THint>> SearchAsync<THint>(
        SearchQuery query,
        SearchType? searchType = null,
        int? timeout = OpenObserveConstants.ZO_QUERY_TIMEOUT,
        SearchAgentOptions? agentOptions = null,
        bool? useCache = null,
        bool? clearCache = null,
        CancellationToken cancellationToken = default)
    {
        return await SearchAsync<THint>(
            null,
            query,
            searchType,
            timeout,
            agentOptions,
            useCache,
            clearCache,
            cancellationToken);
    }

    protected virtual Func<Models.OpenObserveErrorResponse, HttpResponseMessage, Exception> ThrowIfSearchException
    {
        get
        {
            return (errorResponse, httpResponse) => new OpenObserveSearchException(
                errorResponse,
                (int)httpResponse.StatusCode);
        }
    }
}
