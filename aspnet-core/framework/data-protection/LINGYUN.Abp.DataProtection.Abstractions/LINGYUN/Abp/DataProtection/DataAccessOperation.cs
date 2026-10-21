using System.ComponentModel;

namespace LINGYUN.Abp.DataProtection;
/// <summary>
/// Data operations
/// </summary>
public enum DataAccessOperation
{
    /// <summary>
    /// Query
    /// </summary>
    [Description("query")]
    Read, 
    /// <summary>
    /// Update
    /// </summary>
    [Description("Update")]
    Write, 
    /// <summary>
    /// Delete
    /// </summary>
    [Description("Delete")]
    Delete
}
