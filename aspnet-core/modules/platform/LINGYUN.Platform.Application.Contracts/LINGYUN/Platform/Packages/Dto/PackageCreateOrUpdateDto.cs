using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace LINGYUN.Platform.Packages;

public abstract class PackageCreateOrUpdateDto
{
    /// <summary>
    /// Release Notes
    /// </summary>
    [Required]
    [DynamicMaxLength(typeof(PackageConsts), nameof(PackageConsts.MaxNoteLength))]
    public string Note { get; set; } = default!;
    /// <summary>
    /// Description
    /// </summary>
    [DynamicMaxLength(typeof(PackageConsts), nameof(PackageConsts.MaxDescriptionLength))] 
    public string? Description { get; set; }
    /// <summary>
    /// Force update
    /// </summary>
    public bool ForceUpdate { get; set; }

    [DynamicMaxLength(typeof(PackageConsts), nameof(PackageConsts.MaxAuthorsLength))]
    public string? Authors { get; set; }

    public PackageLevel Level { get; set; }
}
