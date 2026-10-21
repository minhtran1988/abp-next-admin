using System;
using System.Text.Json.Serialization;
using Volo.Abp.Data;
using Volo.Abp.SecurityLog;

namespace LINGYUN.Abp.AuditLogging.OpenObserve;

/// <summary>
/// Write the security log document of OpenObserve
/// </summary>
/// <remarks>
/// Inherit <see cref="SecurityLog"/> and append <c>_timestamp</c> column (overridden by <see cref="SecurityLog.CreationTime"/>)
/// </remarks>
public class OpenObserveSecurityLog : SecurityLog
{
    /// <summary>
    /// OpenObserve document time (microseconds)
    /// </summary>
    /// <remarks>
    /// Overridden by <see cref="SecurityLog.CreationTime"/>; when null, use the server-side write time
    /// </remarks>
    [JsonPropertyName("_timestamp")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? Timestamp { get; set; }

    public static OpenObserveSecurityLog From(SecurityLog securityLog)
    {
        return new OpenObserveSecurityLog
        {
            Id = securityLog.Id,
            TenantId = securityLog.TenantId,
            TenantName = securityLog.TenantName,
            ApplicationName = securityLog.ApplicationName,
            Identity = securityLog.Identity,
            Action = securityLog.Action,
            UserId = securityLog.UserId,
            UserName = securityLog.UserName,
            ClientId = securityLog.ClientId,
            CorrelationId = securityLog.CorrelationId,
            ClientIpAddress = securityLog.ClientIpAddress,
            BrowserInfo = securityLog.BrowserInfo,
            CreationTime = securityLog.CreationTime,
            ExtraProperties = securityLog.ExtraProperties ?? new ExtraPropertyDictionary(),
            Timestamp = null,
        };
    }
}
