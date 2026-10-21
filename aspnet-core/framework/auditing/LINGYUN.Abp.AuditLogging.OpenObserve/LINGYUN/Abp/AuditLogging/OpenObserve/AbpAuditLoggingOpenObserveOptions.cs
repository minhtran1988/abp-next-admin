namespace LINGYUN.Abp.AuditLogging.OpenObserve;

/// <summary>
/// Audit log OpenObserve options
/// </summary>
/// <remarks>
/// Configuration section: <c>AuditLogging:OpenObserve</c>
/// </remarks>
public class AbpAuditLoggingOpenObserveOptions
{
    /// <summary>
    /// Default audit log data stream
    /// </summary>
    public const string DefaultAuditLogStream = "audit_log";
    /// <summary>
    /// Default security log data stream
    /// </summary>
    public const string DefaultSecurityLogStream = "security_log";
    /// <summary>
    /// Entity change data stream suffix
    /// </summary>
    public const string EntityChangeStreamSuffix = "_entity_changes";
    /// <summary>
    /// Operation data stream suffix
    /// </summary>
    public const string ActionStreamSuffix = "_actions";

    /// <summary>
    /// Organization name
    /// </summary>
    /// <remarks>
    /// Use <c>AbpOpenObserveOptions.Organization</c> when not specified
    /// </remarks>
    public string? Organization { get; set; }
    /// <summary>
    /// Audit log data stream name
    /// </summary>
    public string AuditLogStream { get; set; }
    /// <summary>
    /// Security log data stream name
    /// </summary>
    public string SecurityLogStream { get; set; }
    /// <summary>
    /// Whether to flatten written entity changes
    /// </summary>
    /// <remarks>
    /// Default is true; <c>IEntityChangeStore</c> will not be able to query data after closing
    /// </remarks>
    public bool WriteEntityChanges { get; set; }
    /// <summary>
    /// Whether to flatten write operations
    /// </summary>
    /// <remarks>
    /// Default is true; after closing, queries based on <c>Actions.*</c> conditions will not be matched.
    /// </remarks>
    public bool WriteActions { get; set; }
    /// <summary>
    /// Default query span (days) when no time range is specified
    /// </summary>
    /// <remarks>
    /// OpenObserve query must specify a time range, the default is 365 days
    /// </remarks>
    public int DefaultQueryDays { get; set; }
    /// <summary>
    /// Whether to overwrite OpenObserve's <c>_timestamp</c> with the log's own business time
    /// </summary>
    /// <remarks>
    /// Default true: Audit log uses <c>AuditLog.ExecutionTime</c>, security log uses <c>SecurityLog.CreationTime</c>,
    /// Entity changes use <c>EntityChange.ChangeTime</c>, operations use <c>AuditLogAction.ExecutionTime</c>
    /// </remarks>
    public bool UseLogTimeAsTimestamp { get; set; }
    /// <summary>
    /// Maximum number of lookback hours allowed to override <c>_timestamp</c>
    /// </summary>
    /// <remarks>
    /// By default, OpenObserve only accepts data within <c>ZO_INGEST_ALLOWED_UPTO</c> (default 5) hours. If the window is exceeded, it will be discarded by the server.
    /// Therefore, logs beyond this window will no longer cover <c>_timestamp</c> (server writing time is used instead); it must be consistent with the server environment variables
    /// </remarks>
    public int TimestampAllowedUptoHours { get; set; }

    public AbpAuditLoggingOpenObserveOptions()
    {
        AuditLogStream = DefaultAuditLogStream;
        SecurityLogStream = DefaultSecurityLogStream;

        WriteEntityChanges = true;
        WriteActions = true;
        DefaultQueryDays = 365;
        UseLogTimeAsTimestamp = true;
        TimestampAllowedUptoHours = 5;
    }

    /// <summary>
    /// Get the entity change data stream name
    /// </summary>
    /// <remarks>
    /// Calculated by <see cref="AuditLogStream"/>: <c>{AuditLogStream}_entity_changes</c>, no separate configuration required
    /// </remarks>
    public virtual string GetEntityChangeStream()
    {
        return $"{AuditLogStream}{EntityChangeStreamSuffix}";
    }

    /// <summary>
    /// Get the operation data stream name
    /// </summary>
    /// <remarks>
    /// Calculated by <see cref="AuditLogStream"/>: <c>{AuditLogStream}_actions</c>, no separate configuration is required;
    /// Large data volume operations (parameters may be very large) are written to an independent data stream. When querying, first match the data stream and then return to the main data stream to filter by id.
    /// </remarks>
    public virtual string GetActionStream()
    {
        return $"{AuditLogStream}{ActionStreamSuffix}";
    }
}
