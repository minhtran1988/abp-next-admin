using LINGYUN.Abp.IM.Messages;
using System;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace LINGYUN.Abp.MessageService.Chat;

public abstract class Message : CreationAuditedAggregateRoot<long>, IMultiTenant
{
    /// <summary>
    /// Tenant
    /// </summary>
    public virtual Guid? TenantId { get; protected set; }
    /// <summary>
    /// Message ID
    /// </summary>
    public virtual long MessageId { get; protected set; }
    /// <summary>
    /// Send user name
    /// </summary>
    public virtual string SendUserName { get; protected set; } = default!;
    /// <summary>
    /// Content
    /// </summary>
    public virtual string Content { get; protected set; } = default!;
    /// <summary>
    /// Message type
    /// </summary>
    public virtual MessageType Type { get; protected set; }
    /// <summary>
    /// Source
    /// </summary>
    public virtual MessageSourceType Source { get; protected set; }
    /// <summary>
    /// Send status
    /// </summary>
    public virtual MessageState State { get; protected set; }
    protected Message() { }
    protected Message(
        long id,
        Guid sendUserId,
        string sendUserName,
        string content,
        MessageType type = MessageType.Text,
        MessageSourceType source = MessageSourceType.User,
        Guid? tenantId = null)
    {
        MessageId = id;
        CreatorId = sendUserId;
        SendUserName = sendUserName;
        Content = content;
        Type = type;
        Source = source;
        CreationTime = DateTime.Now;
        TenantId = tenantId;
        ChangeSendState();
    }

    public void ChangeSendState(MessageState state = MessageState.Send)
    {
        State = state;
    }
}
