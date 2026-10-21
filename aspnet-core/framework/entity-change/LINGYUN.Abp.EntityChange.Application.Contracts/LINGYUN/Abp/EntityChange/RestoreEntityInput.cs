using System;
using System.ComponentModel.DataAnnotations;

namespace LINGYUN.Abp.EntityChange;

public class RestoreEntityInput
{
    /// <summary>
    /// Entity ID
    /// </summary>
    [Required]
    public string EntityId { get; set; } = default!;
    /// <summary>
    /// Restore to a certain version ID
    /// </summary>
    /// <remarks>
    /// Note: When this value is passed, it will be restored to the specified version of NewValue
    /// Otherwise, restore to the latest version of OriginalValue
    /// </remarks>
    public Guid? EntityChangeId { get; set; }
}
