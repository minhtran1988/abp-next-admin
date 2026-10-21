using System.Collections.Generic;

namespace LINGYUN.Abp.AuditLogging;
public class AbpAuditLoggingOptions
{
    /// <summary>
    /// Whether to enable audit logging
    /// </summary>
    public bool IsAuditLogEnabled { get; set; }
    /// <summary>
    /// Use the audit log queue
    /// </summary>
    /// <remarks>
    /// If the queue is not enabled, write directly to the persistence facility
    /// </remarks>
    public bool UseAuditLogQueue { get; set; }
    /// <summary>
    /// Audit log maximum queue size
    /// </summary>
    public int MaxAuditLogQueueSize { get; set; }
    /// <summary>
    /// Process the audit log queue size at one time
    /// </summary>
    public int BatchAuditLogSize { get; set; }
    /// <summary>
    /// List of audit log contributors, used to modify custom attributes before writing
    /// </summary>
    public List<IAuditLogInfoContributor> AuditLogInfoContributors { get; }
    /// <summary>
    /// Whether to enable security logging
    /// </summary>
    public bool IsSecurityLogEnabled { get; set; }
    /// <summary>
    /// Use security log queue
    /// </summary>
    /// <remarks>
    /// If the queue is not enabled, write directly to the persistence facility
    /// </remarks>
    public bool UseSecurityLogQueue { get; set; }
    /// <summary>
    /// Security log maximum queue size
    /// </summary>
    public int MaxSecurityLogQueueSize { get; set; }
    /// <summary>
    /// Process the security log queue size at once
    /// </summary>
    public int BatchSecurityLogSize { get; set; }
    /// <summary>
    /// List of security log contributors, used to modify custom attributes before writing
    /// </summary>
    public List<ISecurityLogInfoContributor> SecurityLogInfoContributors { get; }
    public AbpAuditLoggingOptions()
    {
        IsAuditLogEnabled = true;
        UseAuditLogQueue = true;
        BatchAuditLogSize = 100;
        MaxAuditLogQueueSize = 10000;
        AuditLogInfoContributors = new List<IAuditLogInfoContributor>();

        IsSecurityLogEnabled = true;
        UseSecurityLogQueue = true;
        BatchSecurityLogSize = 100;
        MaxSecurityLogQueueSize = 10000;
        SecurityLogInfoContributors = new List<ISecurityLogInfoContributor>();
    }
}
