using System;
using System.ComponentModel.DataAnnotations;

namespace LINGYUN.Abp.BlobManagement.Dtos;

public class BlobFolderCreateDto : BlobFolderCreateBaseDto
{
    /// <summary>
    /// ContainerId
    /// </summary>
    [Required]
    public Guid ContainerId { get; set; }
}
