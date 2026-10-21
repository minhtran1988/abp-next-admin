using System;

namespace LINGYUN.Abp.Identity.Session;
/// <summary>
/// Configuration for session management
/// </summary>
public class IdentitySessionCheckOptions
{
    /// <summary>
    /// Keep access duration
    /// Default: 1 minute
    /// </summary>
    /// <remarks>
    /// Refresh cache session interval length
    /// </remarks>
    public TimeSpan KeepAccessTimeSpan { get; set; }
    /// <summary>
    /// Session synchronization interval (ms)
    /// Default: 10 minutes
    /// </summary>
    /// <remarks>
    /// The interval between cache synchronization and persistence
    /// </remarks>
    public TimeSpan SessionSyncTimeSpan { get; set; }
    public IdentitySessionCheckOptions()
    {
        KeepAccessTimeSpan = TimeSpan.FromMinutes(1);
        SessionSyncTimeSpan = TimeSpan.FromMinutes(10);
    }
}
