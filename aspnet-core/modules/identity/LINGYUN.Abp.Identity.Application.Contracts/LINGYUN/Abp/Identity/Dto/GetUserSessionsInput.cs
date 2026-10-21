using System;
using Volo.Abp.Application.Dtos;

namespace LINGYUN.Abp.Identity;
public class GetUserSessionsInput : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// user id
    /// </summary>
    public Guid? UserId { get; set; }
    /// <summary>
    /// Device
    /// </summary>
    public string? Device { get; set; }
    /// <summary>
    /// client id
    /// </summary>
    public string? ClientId { get; set; }
}
