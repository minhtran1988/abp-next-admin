using System;

namespace LINGYUN.Abp.Account.Dto;

public class VerifyLinkUserDto
{
    /// <summary>
    /// Associated user ID
    /// </summary>
    public Guid? LinkUserId { get; set; }
    /// <summary>
    /// Associated username
    /// </summary>
    public string? LinkUserName { get; set; }
    /// <summary>
    /// Associated tenant ID
    /// </summary>
    public Guid? LinkTenantId { get; set; }
    /// <summary>
    /// Associated tenant name
    /// </summary>
    public string? LinkTenantName { get; set; }
    /// <summary>
    /// Whether it is associated
    /// </summary>
    public bool IsLinked { get; set; }
}
