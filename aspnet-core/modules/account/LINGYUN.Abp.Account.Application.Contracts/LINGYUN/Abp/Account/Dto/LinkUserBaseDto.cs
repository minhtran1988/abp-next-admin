using System;

namespace LINGYUN.Abp.Account.Dto;

public abstract class LinkUserBaseDto
{
    /// <summary>
    /// Associated user ID
    /// </summary>
    public Guid UserId { get; set; }
    /// <summary>
    /// Associated tenant ID
    /// </summary>
    public Guid? TenantId { get; set; }
}
