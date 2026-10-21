using System;

namespace LINGYUN.Abp.Account.Dto;
/// <summary>
/// Associated user Dto
/// </summary>
public class LinkUserDto
{
    /// <summary>
    /// Associated user ID
    /// </summary>
    public Guid LinkUserId { get; set; }
    /// <summary>
    /// Associated username
    /// </summary>
    public string LinkUserName { get; set; } = default!;
    /// <summary>
    /// Associated tenant ID
    /// </summary>
    public Guid? LinkTenantId { get; set; }
    /// <summary>
    /// Associated tenant name
    /// </summary>
    public string? LinkTenantName { get; set; }
    /// <summary>
    /// Directly related
    /// </summary>
    public bool DirectlyLinked { get; set; }
}
