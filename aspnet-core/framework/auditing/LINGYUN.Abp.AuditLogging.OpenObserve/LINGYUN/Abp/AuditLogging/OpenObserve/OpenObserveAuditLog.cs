using System.Collections.Generic;
using System.Text.Json.Serialization;
using Volo.Abp.Data;

namespace LINGYUN.Abp.AuditLogging.OpenObserve;

/// <summary>
/// Write the audit log document of OpenObserve
/// </summary>
/// <remarks>
/// Inherit <see cref="AuditLog"/> and append <c>_timestamp</c> column (overridden by <see cref="AuditLog.ExecutionTime"/>);
/// Entity changes and operations are written to independent data streams calculated by AuditLogStream
/// </remarks>
public class OpenObserveAuditLog : AuditLog
{
    /// <summary>
    /// OpenObserve document time (microseconds)
    /// </summary>
    /// <remarks>
    /// Overridden by <see cref="AuditLog.ExecutionTime"/>; when null, use the server-side write time
    /// </remarks>
    [JsonPropertyName("_timestamp")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? Timestamp { get; set; }

    public static OpenObserveAuditLog From(AuditLog auditLog)
    {
        return new OpenObserveAuditLog
        {
            Id = auditLog.Id,
            ApplicationName = auditLog.ApplicationName,
            UserId = auditLog.UserId,
            UserName = auditLog.UserName,
            TenantId = auditLog.TenantId,
            TenantName = auditLog.TenantName,
            ImpersonatorUserId = auditLog.ImpersonatorUserId,
            ImpersonatorUserName = auditLog.ImpersonatorUserName,
            ImpersonatorTenantId = auditLog.ImpersonatorTenantId,
            ImpersonatorTenantName = auditLog.ImpersonatorTenantName,
            ExecutionTime = auditLog.ExecutionTime,
            ExecutionDuration = auditLog.ExecutionDuration,
            ClientIpAddress = auditLog.ClientIpAddress,
            ClientName = auditLog.ClientName,
            ClientId = auditLog.ClientId,
            CorrelationId = auditLog.CorrelationId,
            BrowserInfo = auditLog.BrowserInfo,
            HttpMethod = auditLog.HttpMethod,
            Url = auditLog.Url,
            Exceptions = auditLog.Exceptions,
            Comments = auditLog.Comments,
            HttpStatusCode = auditLog.HttpStatusCode,
            // Entity changes and operations are written to independent data streams (see GetEntityChangeStream / GetActionStream). The main document is no longer saved redundantly. Details are obtained from substreams on demand.
            EntityChanges = new List<EntityChange>(),
            Actions = new List<AuditLogAction>(),
            ExtraProperties = auditLog.ExtraProperties ?? new ExtraPropertyDictionary(),
            // _timestamp is overridden by the writer by AuditLog.ExecutionTime (see OpenObserveTimestampHelper)
            Timestamp = null,
        };
    }
}
