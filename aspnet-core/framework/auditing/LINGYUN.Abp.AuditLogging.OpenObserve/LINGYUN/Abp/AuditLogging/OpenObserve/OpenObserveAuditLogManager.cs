using LINGYUN.Abp.OpenObserve;
using LINGYUN.Abp.OpenObserve.Utils;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Specifications;
using Volo.Abp.Timing;

namespace LINGYUN.Abp.AuditLogging.OpenObserve;

/// <summary>
/// Audit log OpenObserve manager
/// </summary>
/// <remarks>
/// For the limitations and processing methods of OpenObserve, please refer to <see cref="OpenObserveAuditLoggingManagerBase"/>;
/// Entity changes and operations are saved in separate data streams calculated by AuditLogStream:
/// * <c>Actions.*</c> If the condition is reached first, the operation data stream matches <c>auditlogid</c>, and then returns to the main data stream to filter by id (two-stage query);
/// * Assemble details from a subdataflow by pressing <c>auditlogid in (...)</c> when <c>includeDetails</c> is pressed.
/// </remarks>
[Dependency(ReplaceServices = true)]
public class OpenObserveAuditLogManager : OpenObserveAuditLoggingManagerBase, IAuditLogManager, ITransientDependency
{
    /// <summary>
    /// Expression member name -> Data flow column name (data flow column names must be lowercase)
    /// </summary>
    protected readonly static Dictionary<string, string> MapedFields = new(StringComparer.OrdinalIgnoreCase)
    {
        { nameof(AuditLog.Id), "id" },
        { nameof(AuditLog.ApplicationName), "applicationname" },
        { nameof(AuditLog.UserId), "userid" },
        { nameof(AuditLog.UserName), "username" },
        { nameof(AuditLog.TenantId), "tenantid" },
        { nameof(AuditLog.TenantName), "tenantname" },
        { nameof(AuditLog.ImpersonatorUserId), "impersonatoruserid" },
        { nameof(AuditLog.ImpersonatorUserName), "impersonatorusername" },
        { nameof(AuditLog.ImpersonatorTenantId), "impersonatortenantid" },
        { nameof(AuditLog.ImpersonatorTenantName), "impersonatortenantname" },
        { nameof(AuditLog.ExecutionTime), "executiontime" },
        { nameof(AuditLog.ExecutionDuration), "executionduration" },
        { nameof(AuditLog.ClientIpAddress), "clientipaddress" },
        { nameof(AuditLog.ClientName), "clientname" },
        { nameof(AuditLog.ClientId), "clientid" },
        { nameof(AuditLog.CorrelationId), "correlationid" },
        { nameof(AuditLog.BrowserInfo), "browserinfo" },
        { nameof(AuditLog.HttpMethod), "httpmethod" },
        { nameof(AuditLog.Url), "url" },
        { nameof(AuditLog.HttpStatusCode), "httpstatuscode" },
        { nameof(AuditLog.Exceptions), "exceptions" },
        { nameof(AuditLog.Comments), "comments" },
    };

    /// <summary>
    /// Field mapping of operation data flow (Actions.* conditions are executed in sub-data flow)
    /// </summary>
    protected readonly static Dictionary<string, string> ActionMapedFields = new(StringComparer.OrdinalIgnoreCase)
    {
        { nameof(AuditLogAction.Id), "id" },
        { nameof(AuditLogAction.AuditLogId), "auditlogid" },
        { nameof(AuditLogAction.TenantId), "tenantid" },
        { nameof(AuditLogAction.ServiceName), "servicename" },
        { nameof(AuditLogAction.MethodName), "methodname" },
        { nameof(AuditLogAction.Parameters), "parameters" },
        { nameof(AuditLogAction.ExecutionTime), "executiontime" },
        { nameof(AuditLogAction.ExecutionDuration), "executionduration" },
        { "Actions.Parameters", "parameters" },
        { "Actions.ServiceName", "servicename" },
        { "Actions.MethodName", "methodname" },
        { "Actions.ExecutionTime", "executiontime" },
        { "Actions.ExecutionDuration", "executionduration" },
    };

    /// <summary>
    /// Operation condition prefix
    /// </summary>
    protected const string ActionFieldPrefix = "Actions.";

    protected OpenObserveSqlBuilder ActionSqlBuilder { get; }
    protected OpenObserveEntityChangeConverter EntityChangeConverter { get; }

    public OpenObserveAuditLogManager(
        IClock clock,
        OpenObserveClient client,
        OpenObserveEntityChangeConverter entityChangeConverter,
        IOpenObserveStreamNameNormalizer streamNameNormalizer,
        IOptions<AbpAuditLoggingOpenObserveOptions> options,
        ILogger<OpenObserveAuditLogManager> logger)
        : base(clock, client, options.Value, CreateSqlBuilder(), streamNameNormalizer, logger)
    {
        EntityChangeConverter = entityChangeConverter;
        ActionSqlBuilder = CreateActionSqlBuilder();
    }

    public async virtual Task<AuditLog?> GetAsync(
        Guid id,
        bool includeDetails = false,
        CancellationToken cancellationToken = default)
    {
        var condition = $"id = '{id:D}'";

        var auditLog = (await FindAsync(condition, cancellationToken: cancellationToken))
            .FirstOrDefault();

        if (auditLog != null && includeDetails)
        {
            await FillDetailsAsync([auditLog], cancellationToken);
        }

        return auditLog;
    }

    public virtual Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw CreateDeleteNotSupportedException();
    }

    public virtual Task DeleteManyAsync(List<Guid> ids, CancellationToken cancellationToken = default)
    {
        throw CreateDeleteNotSupportedException();
    }

    /// <summary>
    /// OpenObserve does not support deleting records
    /// </summary>
    protected virtual BusinessException CreateDeleteNotSupportedException()
    {
        return new BusinessException(AuditLoggingOpenObserveErrorCodes.DeleteNotSupported);
    }

    public async virtual Task<long> GetCountAsync(
        DateTime? startTime = null,
        DateTime? endTime = null,
        string? httpMethod = null,
        string? url = null,
        Guid? userId = null,
        string? userName = null,
        string? applicationName = null,
        string? correlationId = null,
        string? clientId = null,
        string? clientIpAddress = null,
        int? maxExecutionDuration = null,
        int? minExecutionDuration = null,
        bool? hasException = null,
        HttpStatusCode? httpStatusCode = null,
        CancellationToken cancellationToken = default)
    {
        var conditions = BuildConditions(
            httpMethod,
            url,
            userId,
            userName,
            applicationName,
            correlationId,
            clientId,
            clientIpAddress,
            maxExecutionDuration,
            minExecutionDuration,
            hasException,
            httpStatusCode);

        var condition = Combine(
            BuildCondition(conditions),
            BuildTimeCondition("executiontime", startTime, endTime));

        return await GetTotalAsync(GetAuditLogStream(), condition, cancellationToken);
    }

    public async virtual Task<List<AuditLog>> GetListAsync(
        string? sorting = null,
        int maxResultCount = 50,
        int skipCount = 0,
        DateTime? startTime = null,
        DateTime? endTime = null,
        string? httpMethod = null,
        string? url = null,
        Guid? userId = null,
        string? userName = null,
        string? applicationName = null,
        string? correlationId = null,
        string? clientId = null,
        string? clientIpAddress = null,
        int? maxExecutionDuration = null,
        int? minExecutionDuration = null,
        bool? hasException = null,
        HttpStatusCode? httpStatusCode = null,
        bool includeDetails = false,
        CancellationToken cancellationToken = default)
    {
        var conditions = BuildConditions(
            httpMethod,
            url,
            userId,
            userName,
            applicationName,
            correlationId,
            clientId,
            clientIpAddress,
            maxExecutionDuration,
            minExecutionDuration,
            hasException,
            httpStatusCode);

        var condition = Combine(
            BuildCondition(conditions),
            BuildTimeCondition("executiontime", startTime, endTime));

        var auditLogs = await FindAsync(
            condition,
            sorting,
            skipCount,
            maxResultCount,
            cancellationToken);

        if (includeDetails && auditLogs.Count > 0)
        {
            await FillDetailsAsync(auditLogs, cancellationToken);
        }

        return auditLogs;
    }

    public async virtual Task<long> GetCountAsync(
        ISpecification<AuditLog> specification,
        CancellationToken cancellationToken = default)
    {
        var conditions = ExpressionParser.ExtractFields(specification.ToExpression());

        var (actionConditions, otherConditions) = SplitConditions(conditions);

        var condition = BuildCondition(otherConditions);

        if (actionConditions.Count > 0)
        {
            var auditLogIds = await ResolveAuditLogIdsByActionsAsync(actionConditions, cancellationToken);
            if (auditLogIds.Count == 0)
            {
                return 0;
            }

            condition = Combine(condition, BuildIdCondition("id", auditLogIds));
        }

        return await GetTotalAsync(GetAuditLogStream(), condition, cancellationToken);
    }

    public async virtual Task<List<AuditLog>> GetListAsync(
        ISpecification<AuditLog> specification,
        string? sorting = null,
        int maxResultCount = 50,
        int skipCount = 0,
        bool includeDetails = false,
        CancellationToken cancellationToken = default)
    {
        var conditions = ExpressionParser.ExtractFields(specification.ToExpression());

        var (actionConditions, otherConditions) = SplitConditions(conditions);

        var condition = BuildCondition(otherConditions);

        if (actionConditions.Count > 0)
        {
            var auditLogIds = await ResolveAuditLogIdsByActionsAsync(actionConditions, cancellationToken);
            if (auditLogIds.Count == 0)
            {
                return [];
            }

            condition = Combine(condition, BuildIdCondition("id", auditLogIds));
        }

        var auditLogs = await FindAsync(
            condition,
            sorting,
            skipCount,
            maxResultCount,
            cancellationToken);

        if (includeDetails && auditLogs.Count > 0)
        {
            await FillDetailsAsync(auditLogs, cancellationToken);
        }

        return auditLogs;
    }

    /// <summary>
    /// Query audit log
    /// </summary>
    protected async virtual Task<List<AuditLog>> FindAsync(
        string condition,
        string? sorting = null,
        int skipCount = 0,
        int maxResultCount = 50,
        CancellationToken cancellationToken = default)
    {
        var sql = $"select * from {GetAuditLogStream()} where {condition} " +
                  $"order by {NormalizeSorting(sorting, $"{nameof(AuditLog.ExecutionTime)} DESC")}";

        var response = await SearchAsync<OpenObserveAuditLog>(sql, skipCount, maxResultCount, cancellationToken);

        return response.Hits
            .Cast<AuditLog>()
            .ToList();
    }

    /// <summary>
    /// Assembly entity changes and operation details
    /// </summary>
    protected async virtual Task FillDetailsAsync(
        List<AuditLog> auditLogs,
        CancellationToken cancellationToken = default)
    {
        var auditLogIds = auditLogs
            .Select(auditLog => $"'{auditLog.Id:D}'")
            .Distinct()
            .ToList();

        if (auditLogIds.Count == 0)
        {
            return;
        }

        var idCondition = $"auditlogid in ({string.Join(",", auditLogIds)})";

        if (Options.WriteEntityChanges)
        {
            var entityChanges = await SearchAllAsync<OpenObserveEntityChange>(
                $"select * from {GetEntityChangeStream()} where {idCondition}",
                MaxSearchResultCount,
                cancellationToken);

            var entityChangeGroups = entityChanges
                .GroupBy(change => change.AuditLogId)
                .ToDictionary(group => group.Key, group => group.ToList());

            foreach (var auditLog in auditLogs)
            {
                if (entityChangeGroups.TryGetValue(auditLog.Id, out var changes))
                {
                    auditLog.EntityChanges = changes
                        .Select(EntityChangeConverter.ToEntityChange)
                        .ToList();
                }
            }
        }

        if (Options.WriteActions)
        {
            var actions = await SearchAllAsync<OpenObserveAuditLogAction>(
                $"select * from {GetActionStream()} where {idCondition}",
                MaxSearchResultCount,
                cancellationToken);

            var actionGroups = actions
                .GroupBy(action => action.AuditLogId)
                .ToDictionary(group => group.Key, group => group.ToList());

            foreach (var auditLog in auditLogs)
            {
                if (actionGroups.TryGetValue(auditLog.Id, out var logActions))
                {
                    auditLog.Actions = logActions
                        .Select(action => action.ToAuditLogAction())
                        .ToList();
                }
            }
        }
    }

    /// <summary>
    /// First match the operation data flow, and then return the associated audit log ID (two-stage query)
    /// </summary>
    protected async virtual Task<List<string>> ResolveAuditLogIdsByActionsAsync(
        List<Condition> actionConditions,
        CancellationToken cancellationToken = default)
    {
        var condition = ActionSqlBuilder.Build(actionConditions);

        var sql = $"select distinct auditlogid from {GetActionStream()} where {condition}";

        var actions = await SearchAllAsync<OpenObserveAuditLogAction>(
            sql,
            MaxSearchResultCount,
            cancellationToken);

        var auditLogIds = actions
            .Select(action => action.AuditLogId.ToString("D"))
            .Distinct()
            .ToList();

        if (auditLogIds.Count >= MaxSearchResultCount)
        {
            Logger.LogWarning(
                "The action query matched more than {Max} audit logs, the result may be incomplete. " +
                "Please narrow the time range or the conditions.",
                MaxSearchResultCount);
        }

        return auditLogIds;
    }

    /// <summary>
    /// Split operation conditions and main data flow conditions
    /// </summary>
    protected virtual (List<Condition> ActionConditions, List<Condition> OtherConditions) SplitConditions(
        List<Condition> conditions)
    {
        var actionConditions = new List<Condition>();
        var otherConditions = new List<Condition>();

        foreach (var condition in conditions)
        {
            if (condition.Field.StartsWith(ActionFieldPrefix, StringComparison.OrdinalIgnoreCase))
            {
                actionConditions.Add(condition);
            }
            else
            {
                otherConditions.Add(condition);
            }
        }

        return (actionConditions, otherConditions);
    }

    /// <summary>
    /// Construct Id collection conditions
    /// </summary>
    protected virtual string BuildIdCondition(string field, IEnumerable<string> ids)
    {
        return $"{field} in ({string.Join(",", ids.Select(id => $"'{id}'"))})";
    }

    /// <summary>
    /// Construct conditions from query parameters
    /// </summary>
    protected virtual List<Condition> BuildConditions(
        string? httpMethod = null,
        string? url = null,
        Guid? userId = null,
        string? userName = null,
        string? applicationName = null,
        string? correlationId = null,
        string? clientId = null,
        string? clientIpAddress = null,
        int? maxExecutionDuration = null,
        int? minExecutionDuration = null,
        bool? hasException = null,
        HttpStatusCode? httpStatusCode = null)
    {
        var conditions = new List<Condition>();

        if (!httpMethod.IsNullOrWhiteSpace())
        {
            conditions.Add(new Condition(nameof(AuditLog.HttpMethod), ComparisonOp.Equal, httpMethod));
        }
        if (!url.IsNullOrWhiteSpace())
        {
            conditions.Add(new Condition(nameof(AuditLog.Url), ComparisonOp.Contains, url));
        }
        if (userId.HasValue)
        {
            conditions.Add(new Condition(nameof(AuditLog.UserId), ComparisonOp.Equal, userId.Value));
        }
        if (!userName.IsNullOrWhiteSpace())
        {
            conditions.Add(new Condition(nameof(AuditLog.UserName), ComparisonOp.Contains, userName));
        }
        if (!applicationName.IsNullOrWhiteSpace())
        {
            conditions.Add(new Condition(nameof(AuditLog.ApplicationName), ComparisonOp.Contains, applicationName));
        }
        if (!correlationId.IsNullOrWhiteSpace())
        {
            conditions.Add(new Condition(nameof(AuditLog.CorrelationId), ComparisonOp.Equal, correlationId));
        }
        if (!clientId.IsNullOrWhiteSpace())
        {
            conditions.Add(new Condition(nameof(AuditLog.ClientId), ComparisonOp.Equal, clientId));
        }
        if (!clientIpAddress.IsNullOrWhiteSpace())
        {
            conditions.Add(new Condition(nameof(AuditLog.ClientIpAddress), ComparisonOp.Equal, clientIpAddress));
        }
        if (minExecutionDuration.HasValue)
        {
            conditions.Add(new Condition(nameof(AuditLog.ExecutionDuration), ComparisonOp.GreaterThanOrEqual, minExecutionDuration.Value));
        }
        if (maxExecutionDuration.HasValue)
        {
            conditions.Add(new Condition(nameof(AuditLog.ExecutionDuration), ComparisonOp.LessThanOrEqual, maxExecutionDuration.Value));
        }
        if (hasException.HasValue)
        {
            conditions.Add(new Condition(
                nameof(AuditLog.Exceptions),
                hasException.Value ? ComparisonOp.IsNotNull : ComparisonOp.IsNull,
                null));
        }
        if (httpStatusCode.HasValue)
        {
            conditions.Add(new Condition(nameof(AuditLog.HttpStatusCode), ComparisonOp.Equal, (int)httpStatusCode.Value));
        }

        return conditions;
    }

    protected static OpenObserveSqlBuilder CreateSqlBuilder()
    {
        var sqlBuilder = new OpenObserveSqlBuilder();

        foreach (var field in MapedFields)
        {
            sqlBuilder.MapField(field.Key, field.Value);
        }

        return sqlBuilder;
    }

    protected static OpenObserveSqlBuilder CreateActionSqlBuilder()
    {
        var sqlBuilder = new OpenObserveSqlBuilder();

        foreach (var field in ActionMapedFields)
        {
            sqlBuilder.MapField(field.Key, field.Value);
        }

        return sqlBuilder;
    }
}
