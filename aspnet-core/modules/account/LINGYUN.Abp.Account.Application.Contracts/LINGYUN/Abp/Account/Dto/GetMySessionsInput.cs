using Volo.Abp.Application.Dtos;

namespace LINGYUN.Abp.Identity;
public class GetMySessionsInput : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// Device
    /// </summary>
    public string? Device { get; set; }
    /// <summary>
    /// client id
    /// </summary>
    public string? ClientId { get; set; }
}
