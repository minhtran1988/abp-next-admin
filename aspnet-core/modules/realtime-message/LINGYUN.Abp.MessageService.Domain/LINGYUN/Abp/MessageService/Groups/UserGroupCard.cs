using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Timing;

namespace LINGYUN.Abp.MessageService.Groups;

/// <summary>
/// User group card
/// </summary>
public class UserGroupCard : AuditedAggregateRoot<long>, IMultiTenant
{
    /// <summary>
    /// Tenant
    /// </summary>
    public virtual Guid? TenantId { get; protected set; }
    /// <summary>
    /// User ID
    /// </summary>
    public virtual Guid UserId { get; protected set; }
    /// <summary>
    /// Nickname
    /// </summary>
    public virtual string? NickName { get; set; }
    /// <summary>
    /// Is it an administrator?
    /// </summary>
    public virtual bool IsAdmin { get; protected set; }
    /// <summary>
    /// The ban period expires
    /// </summary>
    public virtual DateTime? SilenceEnd { get; protected set; }
    protected UserGroupCard()
    {
    }
    public UserGroupCard(
        Guid userId,
        string nickName = "",
        bool isAdmin = false,
        DateTime? silenceEnd = null,
        Guid? tenantId = null)
    {
        UserId = userId;
        NickName = nickName;
        IsAdmin = isAdmin;
        SilenceEnd = silenceEnd;
        TenantId = tenantId;
    }
    /// <summary>
    /// Set administrator
    /// </summary>
    /// <param name="admin"></param>
    public void SetAdmin(bool admin)
    {
        IsAdmin = admin;
    }
    /// <summary>
    /// Ban
    /// </summary>
    /// <param name="clock"></param>
    /// <param name="seconds"></param>
    public void Silence(IClock clock, double seconds)
    {
        SilenceEnd = clock.Now.AddSeconds(seconds);
    }
    /// <summary>
    /// Unban
    /// </summary>
    public void UnSilence()
    {
        SilenceEnd = null;
    }
}
