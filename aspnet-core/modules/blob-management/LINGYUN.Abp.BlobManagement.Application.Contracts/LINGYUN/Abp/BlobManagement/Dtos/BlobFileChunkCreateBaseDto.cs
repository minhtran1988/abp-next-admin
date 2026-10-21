using System.ComponentModel.DataAnnotations;

namespace LINGYUN.Abp.BlobManagement.Dtos;

public abstract class BlobFileChunkCreateBaseDto : BlobFileCreateBaseDto
{
    /// <summary>
    /// Regular block size
    /// </summary>
    [Required]
    public long ChunkSize { get; set; }
    /// <summary>
    /// Current block size
    /// </summary>
    [Required]
    public long CurrentChunkSize { get; set; }
    /// <summary>
    /// Index of the chunk in the current upload
    /// </summary>
    [Required]
    public int ChunkNumber { get; set; }
    /// <summary>
    /// Total number of blocks
    /// </summary>
    [Required]
    public int TotalChunks { get; set; }
}
