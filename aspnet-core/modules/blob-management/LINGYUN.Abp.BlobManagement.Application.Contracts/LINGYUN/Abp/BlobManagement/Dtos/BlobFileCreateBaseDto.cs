using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Auditing;
using Volo.Abp.Content;
using Volo.Abp.Validation;

namespace LINGYUN.Abp.BlobManagement.Dtos;

public abstract class BlobFileCreateBaseDto
{
    /// <summary>
    /// Used for server-side verification of uploaded files
    /// </summary>
    public string? CompareMd5 { get; set; }
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
    /// <summary>
    /// File content
    /// </summary>
    [DisableAuditing]
    [DisableValidation]
    public IRemoteStreamContent File { get; set; } = default!;
}
