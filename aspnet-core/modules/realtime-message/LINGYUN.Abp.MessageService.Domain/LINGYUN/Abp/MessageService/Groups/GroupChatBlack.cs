using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace LINGYUN.Abp.MessageService.Groups;

/// <summary>
/// User blacklist
/// </summary>
public class GroupChatBlack : CreationAuditedEntity<long>, IMultiTenant
{
    /// <summary>
    /// Tenant
    /// </summary>
    public virtual Guid? TenantId { get; protected set; }
    /// <summary>
    /// Group ID
    /// </summary>
    public virtual long GroupId { get; protected set; }
    /// <summary>
    /// Blocked users
    /// </summary>
    public virtual Guid ShieldUserId { get; protected set; }
    protected GroupChatBlack() { }
    public GroupChatBlack(long groupId, Guid shieldUserId, Guid? tenantId)
    {
        GroupId = groupId;
        ShieldUserId = shieldUserId;
        TenantId = tenantId;
    }
}
