using LINGYUN.Abp.OpenObserve.Models;
using LINGYUN.Abp.OpenObserve.Models.Ingestion;
using System.Net.Http.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.OpenObserve;

public partial class OpenObserveClient
{
    /// <summary>
    /// Log ingestion - JSON
    /// </summary>
    /// <remarks>
    /// POST /api/{organization}/{stream}/_json
    /// The request body is a JSON array: [ {..}, {..} ]
    /// Response: {"code":200,"status":[{"name":"stream1","successful":2,"failed":0}]}
    /// Reference: https://openobserve.ai/docs/reference/api/ingestion/logs/json/
    /// </remarks>
    public async virtual Task<JsonResponse> JsonRequestAsync<TData>(
        string? organization,
        string stream,
        IEnumerable<TData> datas,
        CancellationToken cancellationToken = default)
    {
        var requestUri = $"/api/{ResolveOrganization(organization)}/{Uri.EscapeDataString(stream)}/_json";

        var httpResponse = await HttpClient.PostAsync(requestUri, CreateJsonContent(datas), cancellationToken);

        await EnsureSuccessStatusCode(httpResponse, ThrowIfIngestionException(stream), cancellationToken);

        var responseContent = await httpResponse.Content.ReadAsStringAsync(cancellationToken);

        return DeserializeOrDefault<JsonResponse>(responseContent)!;
    }

    /// <summary>
    /// Log ingestion - JSON (single record)
    /// </summary>
    /// <remarks>
    /// Use an independent method name for a single record to avoid overload ambiguity with <see cref="JsonRequestAsync{TData}(string?, string, IEnumerable{TData}, CancellationToken)"/>
    /// (When passing in an array, the generic single overload will be hit first, causing the request body to be wrapped with an extra layer of arrays)
    /// </remarks>
    public async virtual Task<JsonResponse> JsonRecordAsync<TData>(
        string? organization,
        string stream,
        TData data,
        CancellationToken cancellationToken = default)
    {
        return await JsonRequestAsync(organization, stream, [data], cancellationToken);
    }

    /// <summary>
    /// Log ingestion - Bulk
    /// </summary>
    /// <remarks>
    /// POST /api/{organization}/_bulk
    /// The request body is NDJSON, and each two lines is a group: the first line is the action line {"index":{"_index":"stream1"}}, and the second line is the record data
    /// The response is Elasticsearch _bulk compatible structure: {"took":0,"errors":false,"items":[{"index":{...}}]}
    /// Reference: https://openobserve.ai/docs/reference/api/ingestion/logs/bulk/
    /// </remarks>
    /// <param name="action">Action: index / create / update, default index</param>
    public async virtual Task<JsonBulkResponse> JsonBulkAsync<TData>(
        string? organization,
        string stream,
        IEnumerable<TData> datas,
        string action = OpenObserveConstants.BulkActions.Index,
        CancellationToken cancellationToken = default)
    {
        var requestUri = $"/api/{ResolveOrganization(organization)}/_bulk";

        var builder = new StringBuilder();
        foreach (var data in datas)
        {
            // Action line: {"index":{"_index":"stream1"}}
            builder.AppendLine(Serializer.Serialize(
                new Dictionary<string, JsonBulkRequest>
                {
                    [action] = new JsonBulkRequest { Index = stream },
                }));
            // Record data rows
            builder.AppendLine(Serializer.Serialize(data));
        }

        using var content = new StringContent(builder.ToString(), Encoding.UTF8, OpenObserveConstants.NdJsonContentType);

        var httpResponse = await HttpClient.PostAsync(requestUri, content, cancellationToken);

        await EnsureSuccessStatusCode(httpResponse, ThrowIfIngestionException(stream), cancellationToken);

        var responseContent = await httpResponse.Content.ReadAsStringAsync(cancellationToken);

        return DeserializeOrDefault<JsonBulkResponse>(responseContent)!;
    }

    /// <summary>
    /// Log ingestion - Multi
    /// </summary>
    /// <remarks>
    /// POST /api/{organization}/{stream}/_multi
    /// The request body is NDJSON, with one record object per line (no action lines)
    /// The response is the same as _json: {"code":200,"status":[{"name":"stream1","successful":2,"failed":0}]}
    /// Reference: https://openobserve.ai/docs/reference/api/ingestion/logs/multi/
    /// </remarks>
    public async virtual Task<JsonResponse> JsonMultiAsync<TData>(
        string? organization,
        string stream,
        IEnumerable<TData> datas,
        CancellationToken cancellationToken = default)
    {
        var requestUri = $"/api/{ResolveOrganization(organization)}/{Uri.EscapeDataString(stream)}/_multi";

        var builder = new StringBuilder();
        foreach (var data in datas)
        {
            builder.AppendLine(Serializer.Serialize(data));
        }

        using var content = new StringContent(builder.ToString(), Encoding.UTF8, OpenObserveConstants.NdJsonContentType);

        var httpResponse = await HttpClient.PostAsync(requestUri, content, cancellationToken);

        await EnsureSuccessStatusCode(httpResponse, ThrowIfIngestionException(stream), cancellationToken);

        var responseContent = await httpResponse.Content.ReadAsStringAsync(cancellationToken);

        return DeserializeOrDefault<JsonResponse>(responseContent)!;
    }

    /// <summary>
    /// Create exception when ingestion fails
    /// </summary>
    protected virtual Func<OpenObserveErrorResponse, HttpResponseMessage, Exception> ThrowIfIngestionException(string stream)
    {
        return (errorResponse, httpResponse) => new OpenObserveIngestionException(
            stream,
            errorResponse,
            (int)httpResponse.StatusCode);
    }
}
