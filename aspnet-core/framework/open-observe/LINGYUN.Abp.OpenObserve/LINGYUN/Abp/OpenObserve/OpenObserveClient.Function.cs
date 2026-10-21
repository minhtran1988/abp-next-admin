using LINGYUN.Abp.OpenObserve.Models;
using LINGYUN.Abp.OpenObserve.Models.Function;
using System;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.OpenObserve;

public partial class OpenObserveClient
{
    /// <summary>
    /// Create function
    /// </summary>
    /// <remarks>
    /// POST /api/{organization}/functions
    /// Reference: https://openobserve.ai/docs/reference/api/function/
    /// </remarks>
    /// <param name="name">Function name</param>
    /// <param name="function">Function body</param>
    /// <param name="transType">Functional language: 0 = VRL, 1 = JavaScript</param>
    /// <param name="streams">Data streams where the function takes effect</param>
    /// <param name="order">Execution order (document example field, not used by the current implementation)</param>
    public async virtual Task<OpenObserveCodeMessageResponse> CreateFunctionAsync(
        string name,
        string function,
        string? functionParams = null,
        int? numArgs = null,
        FunctionTransType? transType = null,
        FunctionStreamInfo[]? streams = null,
        int? order = null,
        string? organization = null,
        CancellationToken cancellationToken = default)
    {
        var requestUri = $"/api/{ResolveOrganization(organization)}/functions";

        var request = new SaveFunctionRequest
        {
            Name = name,
            Function = function,
            Params = functionParams,
            NumArgs = numArgs,
            TransType = transType,
            Streams = streams,
            Order = order,
        };

        return (await PostJsonAsync<SaveFunctionRequest, OpenObserveCodeMessageResponse>(
            requestUri,
            request,
            cancellationToken: cancellationToken))!;
    }

    /// <summary>
    /// Update function
    /// </summary>
    /// <remarks>
    /// PUT /api/{organization}/functions/{name}
    /// Reference: https://openobserve.ai/docs/reference/api/function/
    /// </remarks>
    public async virtual Task<OpenObserveCodeMessageResponse> UpdateFunctionAsync(
        string name,
        string function,
        string? functionParams = null,
        int? numArgs = null,
        FunctionTransType? transType = null,
        FunctionStreamInfo[]? streams = null,
        int? order = null,
        string? organization = null,
        CancellationToken cancellationToken = default)
    {
        var requestUri = $"/api/{ResolveOrganization(organization)}/functions/{Uri.EscapeDataString(name)}";

        var request = new SaveFunctionRequest
        {
            Name = name,
            Function = function,
            Params = functionParams,
            NumArgs = numArgs,
            TransType = transType,
            Streams = streams,
            Order = order,
        };

        return (await PutJsonAsync<SaveFunctionRequest, OpenObserveCodeMessageResponse>(
            requestUri,
            request,
            cancellationToken: cancellationToken))!;
    }

    /// <summary>
    /// Delete function
    /// </summary>
    /// <remarks>
    /// DELETE /api/{organization}/functions/{name}
    /// Return 409 when the pipeline depends on this function
    /// Reference: https://openobserve.ai/docs/reference/api/function/
    /// </remarks>
    /// <param name="force">The implementation declares that this parameter is used for forced deletion, and the current version of the server does not use it</param>
    public async virtual Task<OpenObserveCodeMessageResponse> DeleteFunctionAsync(
        string name,
        bool? force = null,
        string? organization = null,
        CancellationToken cancellationToken = default)
    {
        var queryString = Utils.OpenObserveQueryString.Build(("force", force));

        var requestUri = $"/api/{ResolveOrganization(organization)}/functions/{Uri.EscapeDataString(name)}{queryString}";

        return (await DeleteAsync<OpenObserveCodeMessageResponse>(requestUri, cancellationToken: cancellationToken))!;
    }

    /// <summary>
    /// List functions
    /// </summary>
    /// <remarks>
    /// GET /api/{organization}/functions
    /// Reference: https://openobserve.ai/docs/reference/api/function/
    /// </remarks>
    public async virtual Task<FunctionListResponse> GetFunctionsAsync(
        string? organization = null,
        CancellationToken cancellationToken = default)
    {
        var requestUri = $"/api/{ResolveOrganization(organization)}/functions";

        return (await GetAsync<FunctionListResponse>(requestUri, cancellationToken: cancellationToken))!;
    }

    /// <summary>
    /// Test function
    /// </summary>
    /// <remarks>
    /// POST /api/{org_id}/functions/test
    /// Note: This interface does not appear in the API document and comes from the implementation source code
    /// </remarks>
    /// <param name="function">Function body</param>
    /// <param name="events">Sample data for testing</param>
    /// <param name="transType">Function language, automatically recognized by the server when not passed</param>
    public async virtual Task<TestFunctionResponse> TestFunctionAsync(
        string function,
        JsonNode[] events,
        FunctionTransType? transType = null,
        string? organization = null,
        CancellationToken cancellationToken = default)
    {
        var requestUri = $"/api/{ResolveOrganization(organization)}/functions/test";

        var request = new TestFunctionRequest
        {
            Function = function,
            Events = events,
            TransType = transType,
        };

        return (await PostJsonAsync<TestFunctionRequest, TestFunctionResponse>(
            requestUri,
            request,
            cancellationToken: cancellationToken))!;
    }
}
