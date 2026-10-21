using Elastic.Clients.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.Elasticsearch;

public interface IExpressionQueryService
{
    /// <summary>
    /// Query the number of documents that meet the conditions
    /// </summary>
    /// <typeparam name="TDocument">Document type</typeparam>
    /// <param name="indexName">Document Index</param>
    /// <param name="expression">Query expression tree</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<long> GetCountAsync<TDocument>(
        string indexName, 
        Expression<Func<TDocument, bool>> expression,
        CancellationToken cancellationToken = default) where TDocument : class;
    /// <summary>
    /// Query the list of documents that meet the conditions
    /// </summary>
    /// <typeparam name="TDocument">Document type</typeparam>
    /// <param name="indexName">Document Index</param>
    /// <param name="expression">Query expression tree</param>
    /// <param name="sorting">Sort field</param>
    /// <param name="maxResultCount">Maximum return data size</param>
    /// <param name="skipCount">Skip data size</param>
    /// <param name="sourceExcludes">Include fields</param>
    /// <param name="sourceIncludes">Ignore fields</param>
    /// <param name="beginMarker">Sort starting field</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<TDocument>> GetListAsync<TDocument>(
        string indexName,
        Expression<Func<TDocument, bool>> expression,
        string? sorting = null,
        int maxResultCount = 50,
        int skipCount = 0,
        Fields? sourceExcludes = null,
        Fields? sourceIncludes = null,
        object[]? beginMarker = null,
        CancellationToken cancellationToken = default) where TDocument : class;
}
