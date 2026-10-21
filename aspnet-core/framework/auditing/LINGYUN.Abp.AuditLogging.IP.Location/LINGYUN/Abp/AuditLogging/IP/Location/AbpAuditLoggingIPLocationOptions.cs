namespace LINGYUN.Abp.AuditLogging.IP.Location;

/// <summary>
/// Audit log IP targeting options
/// </summary>
/// <remarks>
/// Configuration section: <c>AuditLogging:IPLocation</c>
/// (Environment variable: <c>AuditLogging__IPLocation__IsEnabled</c>)
/// </remarks>
public class AbpAuditLoggingIPLocationOptions
{
    /// <summary>
    /// Whether to enable IP positioning
    /// </summary>
    /// <remarks>
    /// Default <c>false</c>, needs to be explicitly enabled in the host configuration (such as docker-compose);
    /// When enabled, the parsing results will be written to the <c>Location</c> attribute of the audit log and security log.
    /// </remarks>
    public bool IsEnabled { get; set; } = false;
}
