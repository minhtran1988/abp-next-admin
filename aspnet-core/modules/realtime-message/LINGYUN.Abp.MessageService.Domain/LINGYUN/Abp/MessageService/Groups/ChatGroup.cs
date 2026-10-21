using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace LINGYUN.Abp.MessageService.Groups;

/// <summary>
/// Chat group
/// </summary>
public class ChatGroup : AuditedEntity<long>, IMultiTenant
{
    /// <summary>
    /// Tenant
    /// </summary>
    public virtual Guid? TenantId { get; protected set; }
    /// <summary>
    /// Group owner
    /// </summary>
    public virtual Guid AdminUserId { get; protected set; }
    /// <summary>
    /// Group ID
    /// </summary>
    public virtual long GroupId { get; protected set; }
    /// <summary>
    /// Group name
    /// </summary>
    public virtual string Name { get; protected set; } = default!;
    /// <summary>
    /// Group tag
    /// </summary>
    public virtual string? Tag { get; protected set; }
    /// <summary>
    /// Group address
    /// </summary>
    public virtual string? Address { get; set; }
    /// <summary>
    /// Group announcement
    /// </summary>
    public virtual string? Notice { get; set; }
    /// <summary>
    /// Maximum number of users
    /// </summary>
    public virtual int MaxUserCount { get; protected set; }
    /// <summary>
    /// Allow anonymous chat
    /// </summary>
    public virtual bool AllowAnonymous { get; set; }
    /// <summary>
    /// Allow sending messages
    /// </summary>
    public virtual bool AllowSendMessage { get; set; }
    /// <summary>
    /// Group description
    /// </summary>
    public virtual string? Description { get; set; }
    /// <summary>
    /// Group avatar address
    /// </summary>
    public virtual string? AvatarUrl { get; set; }
    protected ChatGroup()
    {
    }
    public ChatGroup(
        long id,
        Guid adminUserId,
        string name,
        string? tag = null,
        string? address = null,
        int maxUserCount = 200)
    {
        GroupId = id;
        AdminUserId = adminUserId;
        Name = name;
        Tag = tag;
        Address = address;
        MaxUserCount = maxUserCount;
    }
}
