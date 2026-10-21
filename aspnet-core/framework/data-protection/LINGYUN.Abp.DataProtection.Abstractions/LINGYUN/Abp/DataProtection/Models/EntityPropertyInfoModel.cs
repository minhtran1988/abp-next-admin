namespace LINGYUN.Abp.DataProtection.Models;

public class EntityPropertyInfoModel
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
    /// JavaScript type
    /// </summary>
    public string JavaScriptType { get; set; } = default!;
    /// <summary>
    /// JavaScript name
    /// </summary>
    public string JavaScriptName { get; set; } = default!;
    /// <summary>
    /// enumeration list
    /// </summary>
    public EntityEnumInfoModel[]? Enums { get; set; }
    /// <summary>
    /// List of allowed filter operations
    /// </summary>
    public DataAccessFilterOperate[]? Operates { get; set; }
}
