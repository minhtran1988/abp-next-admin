using DotNetCore.CAP.Messages;
using System;

namespace LINGYUN.Abp.EventBus.CAP;

/// <summary>
/// CAP message extension
/// </summary>
public static class AbpCAPMessageExtensions
{
    /// <summary>
    /// Try to get the tenant ID in the message header
    /// </summary>
    /// <param name="message"></param>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    public static bool TryGetTenantId(
        this Message message,
        out Guid? tenantId)
    {
        if (message.Headers.TryGetValue(AbpCAPHeaders.TenantId, out var tenantStr))
        {
            if (Guid.TryParse(tenantStr, out var id))
            {
                tenantId = id;
                return true;
            }
        }
        tenantId = null;
        return false;
    }
    /// <summary>
    /// Get the tenant ID in the message header
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public static Guid? GetTenantIdOrNull(
        this Message message)
    {
        if (message.TryGetTenantId(out var tenantId))
        {
            return tenantId;
        }
        return null;
    }
    /// <summary>
    /// Try to get the link ID in the message header
    /// </summary>
    /// <param name="message"></param>
    /// <param name="correlationId"></param>
    /// <returns></returns>
    public static bool TryGetCorrelationId(
        this Message message,
        out string? correlationId)
    {
        return message.Headers.TryGetValue(AbpCAPHeaders.CorrelationId, out correlationId);
    }
    /// <summary>
    /// Get the link ID in the message header
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public static string? GetCorrelationIdOrNull(this Message message)
    {
        if (message.TryGetCorrelationId(out var correlationId))
        {
            return correlationId;
        }
        return null;
    }
}
