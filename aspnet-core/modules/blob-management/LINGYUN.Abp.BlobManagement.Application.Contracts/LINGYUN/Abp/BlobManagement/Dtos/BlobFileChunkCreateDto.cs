using System;
using System.ComponentModel.DataAnnotations;

namespace LINGYUN.Abp.BlobManagement.Dtos;

public class BlobFileChunkCreateDto : BlobFileChunkCreateBaseDto
{
    /// <summary>
    /// ContainerId
    /// </summary>
    [Required]
    public Guid ContainerId { get; set; }
}
