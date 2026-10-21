using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace LINGYUN.Abp.MessageService.Chat;

public class UserChatSetting : Entity<long>, IMultiTenant
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
    /// Allow anonymous chat
    /// </summary>
    public virtual bool AllowAnonymous { get; set; }
    /// <summary>
    /// Allow adding friends
    /// </summary>
    public virtual bool AllowAddFriend { get; set; }
    /// <summary>
    /// Adding friends requires verification
    /// </summary>
    public virtual bool RequireAddFriendValition { get; set; }
    /// <summary>
    /// Allow receiving messages
    /// </summary>
    public virtual bool AllowReceiveMessage { get; set; }
    /// <summary>
    /// Allow sending messages
    /// </summary>
    public virtual bool AllowSendMessage { get; set; }
    protected UserChatSetting() 
    {
    }
    public UserChatSetting(Guid userId, Guid? tenantId)
        : this()
    {
        UserId = userId;
        TenantId = tenantId;
        AllowAnonymous = false;
        AllowAddFriend = true;
        AllowReceiveMessage = true;
        AllowSendMessage = true;
        RequireAddFriendValition = true;
    }
}
