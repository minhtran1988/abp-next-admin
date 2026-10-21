using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace LINGYUN.Abp.DataProtectionManagement;
public class EntityTypeInfoDto : AuditedEntityDto<Guid>
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
    /// <summary>
    /// Entity attribute list
    /// </summary>
    public virtual List<EntityPropertyInfoDto> Properties { get; set; } = new List<EntityPropertyInfoDto>();
}
