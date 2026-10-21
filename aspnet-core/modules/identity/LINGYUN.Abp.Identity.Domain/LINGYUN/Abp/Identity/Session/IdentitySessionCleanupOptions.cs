using System;

namespace LINGYUN.Abp.Identity.Session;
public class IdentitySessionCleanupOptions
{
    /// <summary>
    /// Whether to enable session cleaning
    /// Default: false
    /// </summary>
    public bool IsCleanupEnabled { get; set; }
    /// <summary>
    /// Session cleanup interval (ms)
    /// Default: 1 hour
    /// </summary>
    public int CleanupPeriod { get; set; }
    /// <summary>
    /// How long to keep the inactive session
    /// Default: 30 days
    /// </summary>
    public TimeSpan InactiveTimeSpan { get; set; }
    public IdentitySessionCleanupOptions()
    {
        CleanupPeriod = 3_600_000;
        InactiveTimeSpan = TimeSpan.FromDays(30);
    }
}
