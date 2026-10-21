using LINGYUN.Abp.OpenObserve;
using LINGYUN.Abp.OpenObserve.Models;
using LINGYUN.Abp.OpenObserve.Models.Search;
using LINGYUN.Abp.OpenObserve.Utils;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Timing;

namespace LINGYUN.Abp.AuditLogging.OpenObserve;

/// <summary>
/// Audit log OpenObserve manager base class
/// </summary>
/// <remarks>
/// Several constraints of OpenObserve are handled uniformly here:
/// * The query must specify a time range, and the range acts on the writing time (_timestamp), so a wide window is used uniformly.
/// Business time filtering is completed by SQL conditions (column names are lowercase, time is an ISO 8601 string);
/// * Deletion by record is not supported, and the deletion interface directly throws <see cref="NotSupportedException"/>;
/// * Column names must be lowercase (OpenObserve will lowercase field names).
/// </remarks>
public abstract class OpenObserveAuditLoggingManagerBase
{
    /// <summary>
    /// The maximum number of records returned in a single query
    /// </summary>
    public const int MaxSearchResultCount = 5000;

    protected IClock Clock { get; }
    protected OpenObserveClient Client { get; }
    protected AbpAuditLoggingOpenObserveOptions Options { get; }
    protected OpenObserveSqlBuilder SqlBuilder { get; }
    protected IOpenObserveStreamNameNormalizer StreamNameNormalizer { get; }
    protected ILogger Logger { get; }

    protected OpenObserveAuditLoggingManagerBase(
        IClock clock,
        OpenObserveClient client,
        AbpAuditLoggingOpenObserveOptions options,
        OpenObserveSqlBuilder sqlBuilder,
        IOpenObserveStreamNameNormalizer streamNameNormalizer,
        ILogger logger)
    {
        Clock = clock;
        Client = client;
        Options = options;
        SqlBuilder = sqlBuilder;
        StreamNameNormalizer = streamNameNormalizer;
        Logger = logger;
    }

    /// <summary>
    /// Get the audit log data stream name (by tenant)
    /// </summary>
    protected virtual string GetAuditLogStream(Guid? tenantId = null)
    {
        return StreamNameNormalizer.NormalizeStream(Options.AuditLogStream, tenantId);
    }

    /// <summary>
    /// Get the security log data stream name (by tenant)
    /// </summary>
    protected virtual string GetSecurityLogStream(Guid? tenantId = null)
    {
        return StreamNameNormalizer.NormalizeStream(Options.SecurityLogStream, tenantId);
    }

    /// <summary>
    /// Get the entity change data flow name (by tenant)
    /// </summary>
    protected virtual string GetEntityChangeStream(Guid? tenantId = null)
    {
        return StreamNameNormalizer.NormalizeStream(Options.GetEntityChangeStream(), tenantId);
    }

    /// <summary>
    /// Get the operation data flow name (by tenant)
    /// </summary>
    protected virtual string GetActionStream(Guid? tenantId = null)
    {
        return StreamNameNormalizer.NormalizeStream(Options.GetActionStream(), tenantId);
    }

    /// <summary>
    /// Query window start time
    /// </summary>
    /// <remarks>
    /// The scope is the writing time of the record (_timestamp). Please use SQL conditions to filter the business time.
    /// </remarks>
    protected virtual DateTime GetQueryStartTime()
    {
        return Clock.Now.AddDays(-Math.Abs(Options.DefaultQueryDays));
    }

    /// <summary>
    /// Query window end time
    /// </summary>
    protected virtual DateTime GetQueryEndTime()
    {
        return Clock.Now.AddDays(1);
    }

    /// <summary>
    /// Execute SQL query
    /// </summary>
    /// <remarks>
    /// OpenObserve returns an error when the data stream does not exist (no data has been written yet), and is treated as an empty result here;
    /// Other errors (credentials, syntax, service exceptions) continue to be thrown upward
    /// </remarks>
    protected async virtual Task<SearchResponse<THit>> SearchAsync<THit>(
        string sql,
        int skipCount = 0,
        int maxResultCount = 50,
        CancellationToken cancellationToken = default)
    {
        var query = new SearchQuery(
            sql,
            GetQueryStartTime(),
            GetQueryEndTime(),
            skipCount,
            maxResultCount);

        try
        {
            return await Client.SearchAsync<THit>(
                Options.Organization,
                query,
                cancellationToken: cancellationToken);
        }
        catch (OpenObserveException exception) when (IsStreamNotFound(exception))
        {
            Logger.LogWarning(
                "The OpenObserve stream does not exist yet (no data has been ingested), an empty result is returned. {Message}",
                exception.Message);

            return new SearchResponse<THit>();
        }
    }

    /// <summary>
    /// Is it a "data stream does not exist" error?
    /// </summary>
    /// <remarks>
    /// When OpenObserve has not written data, the data stream has not yet been created, and returns code 20002 and
    /// <c>Search stream not found: {stream}</c> message
    /// </remarks>
    protected virtual bool IsStreamNotFound(OpenObserveException exception)
    {
        return exception.Message.Contains("stream not found", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Execute SQL query and return all hits
    /// </summary>
    protected async virtual Task<List<THit>> SearchAllAsync<THit>(
        string sql,
        int maxResultCount = MaxSearchResultCount,
        CancellationToken cancellationToken = default)
    {
        var response = await SearchAsync<THit>(sql, 0, maxResultCount, cancellationToken);

        return response.Hits.ToList();
    }

    /// <summary>
    /// Count the number of records that meet the conditions
    /// </summary>
    protected async virtual Task<long> GetTotalAsync(
        string stream,
        string condition,
        CancellationToken cancellationToken = default)
    {
        var sql = $"select count(*) as total from {stream} where {condition}";

        var response = await SearchAsync<OpenObserveCount>(sql, 0, 1, cancellationToken);

        return response.Hits.Length == 0 ? 0 : response.Hits[0].Total;
    }

    /// <summary>
    /// Build conditions
    /// </summary>
    protected virtual string BuildCondition(IEnumerable<Condition> conditions)
    {
        return SqlBuilder.Build(conditions);
    }

    /// <summary>
    /// Build time range conditions
    /// </summary>
    /// <remarks>
    /// The time column is stored in ISO 8601 string and compared in the same format
    /// </remarks>
    protected virtual string BuildTimeCondition(string field, DateTime? startTime, DateTime? endTime)
    {
        var parts = new List<string>();

        if (startTime.HasValue)
        {
            parts.Add($"{field} >= {SqlBuilder.FormatDateTime(startTime.Value)}");
        }
        if (endTime.HasValue)
        {
            parts.Add($"{field} <= {SqlBuilder.FormatDateTime(endTime.Value)}");
        }

        return string.Join(" AND ", parts);
    }

    /// <summary>
    /// Combined conditions
    /// </summary>
    protected virtual string Combine(params string[] conditions)
    {
        var parts = conditions
            .Where(condition => !condition.IsNullOrWhiteSpace())
            .ToList();

        return parts.Count == 0 ? "1 = 1" : string.Join(" AND ", parts);
    }

    /// <summary>
    /// Normalized sorting field (expression member name -> data flow column name)
    /// </summary>
    protected virtual string NormalizeSorting(string? sorting, string defaultSorting)
    {
        var value = sorting.IsNullOrWhiteSpace() ? defaultSorting : sorting!;

        var parts = value.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length > 0)
        {
            parts[0] = SqlBuilder.GetMappedField(parts[0]);
        }

        return string.Join(' ', parts);
    }

    /// <summary>
    /// Escape string
    /// </summary>
    protected virtual string Escape(string? value)
    {
        return SqlBuilder.Escape(value ?? string.Empty);
    }
}
