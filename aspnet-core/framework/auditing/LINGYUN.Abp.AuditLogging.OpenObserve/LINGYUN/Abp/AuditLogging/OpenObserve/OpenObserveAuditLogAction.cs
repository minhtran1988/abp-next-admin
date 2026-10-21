using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Volo.Abp.Data;

namespace LINGYUN.Abp.AuditLogging.OpenObserve;

/// <summary>
/// Flattened operation records (written to the OpenObserve operation data stream)
/// </summary>
/// <remarks>
/// <see cref="AuditLogAction.Parameters"/> may be large under large data volume operations, so the operation is flattened and written
/// <c>{AuditLogStream}_actions</c> independent data stream; when querying, first match <c>auditlogid</c> in this data stream,
/// Return to the main data stream to filter by id
/// </remarks>
public class OpenObserveAuditLogAction
{
    /// <summary>
    /// OpenObserve document time (microseconds)
    /// </summary>
    /// <remarks>
    /// Overridden by <see cref="AuditLogAction.ExecutionTime"/>; when null, use the server-side write time
    /// </remarks>
    [JsonPropertyName("_timestamp")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? Timestamp { get; set; }

    public Guid Id { get; set; }

    public Guid AuditLogId { get; set; }

    public Guid? TenantId { get; set; }

    public string ServiceName { get; set; }

    public string MethodName { get; set; }

    public string Parameters { get; set; }

    public DateTime ExecutionTime { get; set; }

    public int ExecutionDuration { get; set; }

    /// <summary>
    /// The user whose audit log belongs (redundant, easy for independent retrieval)
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Username of the audit log to which it belongs (redundant, easy for independent retrieval)
    /// </summary>
    public string? UserName { get; set; }

    public ExtraPropertyDictionary ExtraProperties { get; set; }

    public OpenObserveAuditLogAction()
    {
        ServiceName = string.Empty;
        MethodName = string.Empty;
        Parameters = string.Empty;
        ExtraProperties = new ExtraPropertyDictionary();
    }

    /// <summary>
    /// Create flattened records from audit logs and operations
    /// </summary>
    public static OpenObserveAuditLogAction From(AuditLog auditLog, AuditLogAction action)
    {
        return new OpenObserveAuditLogAction
        {
            Id = action.Id,
            AuditLogId = action.AuditLogId,
            TenantId = action.TenantId ?? auditLog.TenantId,
            ServiceName = action.ServiceName,
            MethodName = action.MethodName,
            Parameters = action.Parameters,
            ExecutionTime = action.ExecutionTime,
            ExecutionDuration = action.ExecutionDuration,
            UserId = auditLog.UserId,
            UserName = auditLog.UserName,
            ExtraProperties = action.ExtraProperties ?? new ExtraPropertyDictionary(),
        };
    }

    /// <summary>
    /// Revert to operation
    /// </summary>
    public virtual AuditLogAction ToAuditLogAction()
    {
        return new AuditLogAction
        {
            Id = Id,
            AuditLogId = AuditLogId,
            TenantId = TenantId,
            ServiceName = ServiceName,
            MethodName = MethodName,
            Parameters = Parameters,
            ExecutionTime = ExecutionTime,
            ExecutionDuration = ExecutionDuration,
            ExtraProperties = ExtraProperties ?? new ExtraPropertyDictionary(),
        };
    }

    /// <summary>
    /// Create flattened records in batches
    /// </summary>
    public static List<OpenObserveAuditLogAction> CreateMany(AuditLog auditLog)
    {
        return auditLog.Actions?
            .Select(action => From(auditLog, action))
            .ToList()
            ?? new List<OpenObserveAuditLogAction>();
    }
}
