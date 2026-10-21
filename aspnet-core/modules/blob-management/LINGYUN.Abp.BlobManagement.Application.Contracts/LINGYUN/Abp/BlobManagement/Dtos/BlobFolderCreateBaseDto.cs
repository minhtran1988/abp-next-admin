using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace LINGYUN.Abp.BlobManagement.Dtos;

public abstract class BlobFolderCreateBaseDto
{
    /// <summary>
    /// Name
    /// </summary>
    [Required]
    [DynamicStringLength(typeof(BlobConsts), nameof(BlobConsts.MaxNameLength))]
    public string Name { get; set; } = default!;
    /// <summary>
    /// Owning BlobId
    /// </summary>
    public Guid? ParentId { get; set; }
}
