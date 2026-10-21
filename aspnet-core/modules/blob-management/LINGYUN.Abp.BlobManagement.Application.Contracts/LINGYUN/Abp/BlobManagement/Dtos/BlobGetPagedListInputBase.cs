using System;
using Volo.Abp.Application.Dtos;

namespace LINGYUN.Abp.BlobManagement.Dtos;

public abstract class BlobGetPagedListInputBase : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// Superior BlobId
    /// </summary>
    public Guid? ParentId { get; set; }
    /// <summary>
    /// Type
    /// </summary>
    public BlobType? Type { get; set; }
    /// <summary>
    /// Content type
    /// </summary>
    public string? ContentType { get; set; }
}
