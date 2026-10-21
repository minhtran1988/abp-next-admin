using LINGYUN.Abp.IM.Contract;
using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace LINGYUN.Abp.MessageService.Chat;

public class UserChatFriend : CreationAuditedAggregateRoot<long>, IMultiTenant
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
    /// Friend ID
    /// </summary>
    public virtual Guid FrientId { get; protected set; }
    /// <summary>
    /// System Presets
    /// </summary>
    public virtual bool IsStatic { get; set; }
    /// <summary>
    /// Blacklist added
    /// </summary>
    public virtual bool Black { get; set; }
    /// <summary>
    /// Do not disturb messages
    /// </summary>
    public virtual bool DontDisturb { get; set; }
    /// <summary>
    /// Special attention
    /// </summary>
    public virtual bool SpecialFocus { get; set; }
    /// <summary>
    /// Remark name
    /// </summary>
    public virtual string? RemarkName { get; set; }
    /// <summary>
    /// Additional instructions
    /// </summary>
    public virtual string? Description { get; set; }

    public virtual UserFriendStatus Status { get; protected set; }

    protected UserChatFriend()
    {
    }

    public UserChatFriend(
        Guid userId,
        Guid friendId,
        string? remarkName = null,
        string? description = null,
        Guid? tenantId = null)
    {
        UserId = userId;
        FrientId = friendId;
        RemarkName = remarkName;
        TenantId = tenantId;
        Description = description;
        Status = UserFriendStatus.NeedValidation;
    }

    public void SetStatus(UserFriendStatus status = UserFriendStatus.NeedValidation)
    {
        if (Status == UserFriendStatus.NeedValidation && status == UserFriendStatus.Added)
        {
            // If subsequent verification passes, a separate event is required
            AddLocalEvent(new UserChatFriendEto
            {
                TenantId = TenantId,
                UserId = UserId,
                FrientId = FrientId,
                Status = UserFriendStatus.Added
            });
        }
        Status = status;
    }
}
