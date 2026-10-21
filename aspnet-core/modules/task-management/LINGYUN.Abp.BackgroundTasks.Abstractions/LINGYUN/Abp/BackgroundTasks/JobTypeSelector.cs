using System;
using Volo.Abp;

namespace LINGYUN.Abp.BackgroundTasks;
public class JobTypeSelector : NamedTypeSelector
{
    public JobTypeSelector(
        string name,
        Func<Type, bool> predicate,
        int? lockTimeOut = null, 
        string? nodeName = null, 
        string? cron = null,
        JobPriority? priority = null, 
        int? interval = null,
        int? maxCount = null, 
        int? maxTryCount = null)
        : base(name, predicate)
    {
        LockTimeOut = lockTimeOut;
        NodeName = nodeName;
        Cron = cron;
        Priority = priority;
        Interval = interval;
        MaxCount = maxCount;
        MaxTryCount = maxTryCount;
    }

    /// <summary>
    /// Task exclusive timeout (seconds)
    /// 0 or less does not take effect
    /// </summary>
    public int? LockTimeOut { get; set; }
    /// <summary>
    /// Specify the running node
    /// </summary>
    public string? NodeName { get; set; }
    /// <summary>
    /// Task priority
    /// </summary>
    public JobPriority? Priority { get; set; }
    /// <summary>
    /// Cron expression, if it is a periodic task, it needs to be specified
    /// </summary>
    public string? Cron { get; set; }
    /// <summary>
    /// Interval time, unit seconds, conflicts with Cron expression
    /// </summary>
    public int? Interval { get; set; }
    /// <summary>
    /// Maximum number of triggers
    /// </summary>
    public int? MaxCount { get; set; }
    /// <summary>
    /// Failed retry upper limit
    /// Default: 50
    /// </summary>
    public int? MaxTryCount { get; set; }
}
