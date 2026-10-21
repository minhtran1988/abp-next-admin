using System;
using System.Text.Json.Serialization;
using Volo.Abp.Data;

namespace LINGYUN.Abp.AuditLogging.OpenObserve;

/// <summary>
/// Flattened entity change records (written to the OpenObserve entity change data stream)
/// </summary>
/// <remarks>
/// Pure data object, does not contain serialization logic (see <see cref="OpenObserveEntityChangeConverter"/>);
/// Because OpenObserve will save nested arrays as JSON string columns,
/// <see cref="EntityChange.PropertyChanges"/> Save as JSON string
/// </remarks>
public class OpenObserveEntityChange
{
    /// <summary>
    /// OpenObserve document time (microseconds)
    /// </summary>
    /// <remarks>
    /// Overridden by <see cref="EntityChange.ChangeTime"/>; when null, use the server-side write time
    /// </remarks>
    [JsonPropertyName("_timestamp")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? Timestamp { get; set; }

    public Guid Id { get; set; }

    public Guid AuditLogId { get; set; }

    public Guid? TenantId { get; set; }

    public Guid? EntityTenantId { get; set; }

    public DateTime ChangeTime { get; set; }

    /// <summary>
    /// Change type (name of <see cref="EntityChangeType"/>)
    /// </summary>
    public string ChangeType { get; set; }

    public string? EntityId { get; set; }

    public string? EntityTypeFullName { get; set; }

    /// <summary>
    /// The user to whom the audit log belongs
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// The username to which the audit log belongs
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// Property change details (JSON string, processed by <see cref="OpenObserveEntityChangeConverter"/>)
    /// </summary>
    [JsonPropertyName("propertychanges")]
    public string? PropertyChangesJson { get; set; }

    public ExtraPropertyDictionary ExtraProperties { get; set; }

    public OpenObserveEntityChange()
    {
        ChangeType = string.Empty;
        ExtraProperties = new ExtraPropertyDictionary();
    }
}
