using LINGYUN.Abp.AuditLogging.OpenObserve.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace LINGYUN.Abp.AuditLogging.OpenObserve;

/// <summary>
/// Entity change document converter
/// </summary>
/// <remarks>
/// OpenObserve will save nested arrays as JSON string columns, so the attribute details of entity changes
/// (<c>propertychanges</c>) serialize before writing and deserialize after reading;
/// Transformation logic is concentrated here to keep the entity model as a pure data object
/// </remarks>
public class OpenObserveEntityChangeConverter : ITransientDependency
{
    protected IAuditLoggingOpenObserveSerializer Serializer { get; }

    public OpenObserveEntityChangeConverter(IAuditLoggingOpenObserveSerializer serializer)
    {
        Serializer = serializer;
    }

    /// <summary>
    /// Audit log -> Entity change document collection
    /// </summary>
    public virtual List<OpenObserveEntityChange> ToDocuments(AuditLog auditLog)
    {
        return auditLog.EntityChanges?
            .Select(entityChange => ToDocument(auditLog, entityChange))
            .ToList()
            ?? new List<OpenObserveEntityChange>();
    }

    /// <summary>
    /// Entity change -> Entity change document
    /// </summary>
    public virtual OpenObserveEntityChange ToDocument(AuditLog auditLog, EntityChange entityChange)
    {
        return new OpenObserveEntityChange
        {
            Id = entityChange.Id,
            AuditLogId = entityChange.AuditLogId,
            TenantId = entityChange.TenantId,
            EntityTenantId = entityChange.EntityTenantId,
            ChangeTime = entityChange.ChangeTime,
            ChangeType = entityChange.ChangeType.ToString(),
            EntityId = entityChange.EntityId,
            EntityTypeFullName = entityChange.EntityTypeFullName,
            UserId = auditLog.UserId,
            UserName = auditLog.UserName,
            PropertyChangesJson = SerializePropertyChanges(entityChange.PropertyChanges),
            ExtraProperties = entityChange.ExtraProperties ?? new ExtraPropertyDictionary(),
        };
    }

    /// <summary>
    /// Entity change document -> Entity change
    /// </summary>
    public virtual EntityChange ToEntityChange(OpenObserveEntityChange document)
    {
        return new EntityChange
        {
            Id = document.Id,
            AuditLogId = document.AuditLogId,
            TenantId = document.TenantId,
            EntityTenantId = document.EntityTenantId,
            ChangeTime = document.ChangeTime,
            ChangeType = ParseChangeType(document.ChangeType),
            EntityId = document.EntityId,
            EntityTypeFullName = document.EntityTypeFullName,
            PropertyChanges = DeserializePropertyChanges(document.PropertyChangesJson),
            ExtraProperties = document.ExtraProperties ?? new ExtraPropertyDictionary(),
        };
    }

    /// <summary>
    /// Entity change document -> Entity change with user name
    /// </summary>
    public virtual EntityChangeWithUsername ToEntityChangeWithUsername(OpenObserveEntityChange document)
    {
        return new EntityChangeWithUsername
        {
            UserName = document.UserName,
            EntityChange = ToEntityChange(document),
        };
    }

    /// <summary>
    /// Serialized attribute change details
    /// </summary>
    protected virtual string? SerializePropertyChanges(IEnumerable<EntityPropertyChange>? propertyChanges)
    {
        var changes = propertyChanges?.ToList();

        return changes == null || changes.Count == 0
            ? null
            : Serializer.Serialize(changes);
    }

    /// <summary>
    /// Deserialization attribute change details
    /// </summary>
    protected virtual List<EntityPropertyChange> DeserializePropertyChanges(string? propertyChangesJson)
    {
        return Serializer.Deserialize<List<EntityPropertyChange>>(propertyChangesJson ?? string.Empty)
            ?? new List<EntityPropertyChange>();
    }

    protected virtual EntityChangeType ParseChangeType(string changeType)
    {
        return Enum.TryParse<EntityChangeType>(changeType, ignoreCase: true, out var parsed)
            ? parsed
            : EntityChangeType.Updated;
    }
}
