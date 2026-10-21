using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace LINGYUN.Platform.Packages;

public class PackageDto : ExtensibleAuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public string ConcurrencyStamp { get; set; } = default!;
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; } = default!;
    /// <summary>
    /// Release Notes
    /// </summary>
    public string Note { get; set; } = default!;
    /// <summary>
    /// version
    /// </summary>
    public string Version { get; set; } = default!;
    /// <summary>
    /// Description
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// Force update
    /// </summary>
    public bool ForceUpdate { get; set; }

    public string? Authors { get; set; }

    public PackageLevel Level { get; set; }

    public List<PackageBlobDto> Blobs { get; set; } = new List<PackageBlobDto>();

    public static PackageDto None()
    {
        return new PackageDto
        {
            Level = PackageLevel.None,
            Blobs = new List<PackageBlobDto>()
        };
    }
}
