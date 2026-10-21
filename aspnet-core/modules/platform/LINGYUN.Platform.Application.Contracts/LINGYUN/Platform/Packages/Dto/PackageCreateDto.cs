using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace LINGYUN.Platform.Packages;
public class PackageCreateDto : PackageCreateOrUpdateDto
{
    /// <summary>
    /// Name
    /// </summary>
    [Required]
    [DynamicMaxLength(typeof(PackageConsts), nameof(PackageConsts.MaxNameLength))]
    public string Name { get; set; } = default!;
    /// <summary>
    /// version
    /// </summary>
    [Required]
    [DynamicMaxLength(typeof(PackageConsts), nameof(PackageConsts.MaxVersionLength))] 
    public string Version { get; set; } = default!;
}
