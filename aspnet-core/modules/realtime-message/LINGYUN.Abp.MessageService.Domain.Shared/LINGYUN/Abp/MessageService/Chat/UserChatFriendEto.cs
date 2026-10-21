using LINGYUN.Abp.IM.Contract;
using System;
using Volo.Abp.MultiTenancy;

namespace LINGYUN.Abp.MessageService.Chat;

public class UserChatFriendEto : IMultiTenant
{
    public Guid? TenantId { get; set; }
    /// <summary>
    /// User ID
    /// </summary>
    public Guid UserId { get; set; }
    /// <summary>
    /// Friend ID
    /// </summary>
    public Guid FrientId { get; set; }
    /// <summary>
    /// Status
    /// </summary>
    public UserFriendStatus Status { get; set; }
}
