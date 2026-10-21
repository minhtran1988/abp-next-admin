using System;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.DataProtectionManagement;

[Serializable]
[EventName("abp.data_protection.entity_type_info")]
public class EntityTypeInfoEto : EntityEto<Guid>
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; } = default!;
    /// <summary>
    /// Display name
    /// </summary>
    public string DisplayName { get; set; } = default!;
    /// <summary>
    /// Type full name
    /// </summary>
    public string TypeFullName { get; set; } = default!;
    /// <summary>
    /// Whether to enable data auditing
    /// </summary>
    public bool IsAuditEnabled { get; set; }
}
