using System.Collections.Generic;

namespace LINGYUN.Abp.DataProtection.Models;

public class EntityTypeInfoModel
{
    /// <summary>
    /// Entity name
    /// </summary>
    public string Name { get; set; } = default!;
    /// <summary>
    /// Display name
    /// </summary>
    public string DisplayName { get; set; } = default!;
    /// <summary>
    /// List of accessible properties
    /// </summary>
    public List<EntityPropertyInfoModel> Properties { get; set; } = new List<EntityPropertyInfoModel>();
}
