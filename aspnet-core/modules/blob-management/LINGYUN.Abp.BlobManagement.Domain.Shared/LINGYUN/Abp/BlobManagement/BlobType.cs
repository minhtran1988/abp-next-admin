using System.ComponentModel;

namespace LINGYUN.Abp.BlobManagement;

/// <summary>
/// Object type
/// </summary>
[Description("Object Type")]
public enum BlobType
{
    /// <summary>
    /// Directory
    /// </summary>
    [Description("Directory")]
    Folder = 0,
    /// <summary>
    /// File
    /// </summary>
    [Description("File")]
    File = 1
}
