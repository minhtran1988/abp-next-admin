using System;
using Volo.Abp.EventBus;
using Volo.Abp.MultiTenancy;

namespace LINGYUN.Platform.Packages;

[EventName("platform.packages")]
public class PackageEto
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; } = default!;
    /// <summary>
    /// version
    /// </summary>
    public string Version { get; set; } = default!;
    /// <summary>
    /// Force update
    /// </summary>
    public bool ForceUpdate { get; set; }
    public PackageLevel Level { get; set; }
}
