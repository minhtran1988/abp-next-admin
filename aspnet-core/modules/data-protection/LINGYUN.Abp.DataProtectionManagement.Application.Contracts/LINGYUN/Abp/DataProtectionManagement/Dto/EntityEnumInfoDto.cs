using System;
using Volo.Abp.Application.Dtos;

namespace LINGYUN.Abp.DataProtectionManagement;

public class EntityEnumInfoDto : EntityDto<Guid>
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
    /// enumeration value
    /// </summary>
    public string Value { get; set; } = default!;
}
