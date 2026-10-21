using System;
using System.Collections.Generic;

namespace LINGYUN.Abp.BackgroundTasks;

public class JobInfo
{
    /// <summary>
    /// Task ID
    /// </summary>
    public string Id { get; set; } = default!;
    /// <summary>
    /// Tenant ID
    /// </summary>
    public Guid? TenantId { get; set; }
    /// <summary>
    /// Task name
    /// </summary>
    public string Name { get; set; } = default!;
    /// <summary>
    /// Task grouping
    /// </summary>
    public string Group { get; set; } = default!;
    /// <summary>
    /// Task type
    /// </summary>
    public string Type { get; set; } = default!;
    /// <summary>
    /// Return parameters
    /// </summary>
    public string? Result { get; set; }
    /// <summary>
    /// Job source
    /// </summary>
    public JobSource Source { get; set; } = JobSource.None;
    /// <summary>
    /// Task parameters
    /// </summary>
    public IDictionary<string, object> Args { get; set; } = new Dictionary<string, object>();
    /// <summary>
    /// Task status
    /// </summary>
    public JobStatus Status { get; set; } = JobStatus.None;
    /// <summary>
    /// Description
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// Creation time
    /// </summary>
    public DateTime CreationTime { get; set; }
    /// <summary>
    /// Start time
    /// </summary>
    public DateTime BeginTime { get; set; }
    /// <summary>
    /// End time
    /// </summary>
    public DateTime? EndTime { get; set; }
    /// <summary>
    /// Last run time
    /// </summary>
    public DateTime? LastRunTime { get; set; }
    /// <summary>
    /// Next execution time
    /// </summary>
    public DateTime? NextRunTime { get; set; }
    /// <summary>
    /// Task category
    /// </summary>
    public JobType JobType { get; set; } = JobType.Once;
    /// <summary>
    /// Cron expression, if it is a periodic task, it needs to be specified
    /// </summary>
    public string? Cron { get; set; }
    /// <summary>
    /// Number of triggers
    /// </summary>
    public int TriggerCount { get; set; }
    /// <summary>
    /// Number of failed retries
    /// </summary>
    public int TryCount { get; set; }
    /// <summary>
    /// Failed retry upper limit
    /// Default: 50
    /// </summary>
    public int MaxTryCount { get; set; } = 50;
    /// <summary>
    /// Maximum number of executions
    /// Default: 0, no limit
    /// </summary>
    public int MaxCount { get; set; }
    /// <summary>
    /// Continuous failure and will not be executed again
    /// </summary>
    public bool IsAbandoned { get; set; }
    /// <summary>
    /// Interval time, unit seconds, conflicts with Cron expression
    /// Default: 300
    /// </summary>
    public int Interval { get; set; } = 300;
    /// <summary>
    /// Task priority
    /// </summary>
    public JobPriority Priority { get; set; } = JobPriority.Normal;
    /// <summary>
    /// Task exclusive timeout (seconds)
    /// 0 or less does not take effect
    /// </summary>
    public int LockTimeOut { get; set; }
    /// <summary>
    /// Specify the running node
    /// </summary>
    public string? NodeName { get; set; }
    /// <summary>
    /// Calculate the number of times a job can be triggered
    /// </summary>
    /// <returns></returns>
    public int CalcCanBeTriggered()
    {
        // unlimited times
        var maxCount = -1;

        // Remaining times
        if (MaxCount > 0)
        {
            maxCount = MaxCount - TriggerCount;
            if (maxCount < 0)
            {
                maxCount = 0;
            }
        }

        // once
        if (JobType == JobType.Once)
        {
            maxCount = 1;
        }

        // Retry
        // TODO: Quartz's built-in retry mechanism cannot be used
        // if (Status == JobStatus.FailedRetry && MaxTryCount > 0)
        //{
        //    maxCount = MaxTryCount - TryCount;

        //    if (maxCount < 0)
        //    {
        //        maxCount = 0;
        //    }

        //    if (maxCount > 0)
        //    {
        // // When retry is triggered, the failure interval is adjusted
        //        Interval = 60;
        //    }
        //}

        return maxCount;
    }
}
