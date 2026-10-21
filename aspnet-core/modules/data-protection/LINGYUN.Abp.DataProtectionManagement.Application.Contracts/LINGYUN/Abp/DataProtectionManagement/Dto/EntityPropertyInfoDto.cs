using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace LINGYUN.Abp.DataProtectionManagement;
public class EntityPropertyInfoDto : EntityDto<Guid>
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
    /// Js type
    /// </summary>
    public string JavaScriptType { get; set; } = default!;
    /// <summary>
    /// enumeration list
    /// </summary>
    public virtual List<EntityEnumInfoDto> Enums { get; set; } = new List<EntityEnumInfoDto>();
}
