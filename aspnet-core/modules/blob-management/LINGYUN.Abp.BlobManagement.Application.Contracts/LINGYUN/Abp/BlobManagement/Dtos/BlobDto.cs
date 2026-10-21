using System;
using Volo.Abp.Application.Dtos;

namespace LINGYUN.Abp.BlobManagement.Dtos;

public class BlobDto : ExtensibleAuditedEntityDto<Guid>
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; } = default!;
    /// <summary>
    /// Full name
    /// </summary>
    public string FullName { get; set; } = default!;
    /// <summary>
    /// Type
    /// </summary>
    public BlobType Type { get; set; }
    /// <summary>
    /// Content type
    /// </summary>
    public string? ContentType { get; set; }
    /// <summary>
    /// File size
    /// </summary>
    public long Size { get; set; }
    /// <summary>
    /// Expiration time
    /// </summary>
    public DateTime? ExpirationTime { get; set; }
    /// <summary>
    /// Number of downloads
    /// </summary>
    public long DownloadCount { get; set; }
}
