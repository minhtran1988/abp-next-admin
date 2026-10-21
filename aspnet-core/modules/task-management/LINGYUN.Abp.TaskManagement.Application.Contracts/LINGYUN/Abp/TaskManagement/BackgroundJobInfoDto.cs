using LINGYUN.Abp.BackgroundTasks;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

namespace LINGYUN.Abp.TaskManagement;

public class BackgroundJobInfoDto : ExtensibleAuditedEntityDto<string>, IHasConcurrencyStamp
{
    public string ConcurrencyStamp { get; set; } = default!;
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
    /// Task parameters
    /// </summary>
    public ExtraPropertyDictionary Args { get; set; } = new ExtraPropertyDictionary();
    /// <summary>
    /// Task status
    /// </summary>
    public JobStatus Status { get; set; }
    /// <summary>
    /// Description
    /// </summary>
    public string? Description { get; set; }
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
    public JobType JobType { get; set; }
    /// <summary>
    /// Cron expression, if it is a continuous task, it needs to be specified
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
    public int MaxTryCount { get; set; }
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
    /// Whether to enable
    /// </summary>
    public bool IsEnabled { get; set; }
    /// <summary>
    /// Interval time, unit seconds, conflicts with Cron expression
    /// Default: 300
    /// </summary>
    public int Interval { get; set; } = 300;
    /// <summary>
    /// Task priority
    /// </summary>
    public JobPriority Priority { get; set; }
    /// <summary>
    /// Job source
    /// </summary>
    public JobSource Source { get; set; }
    /// <summary>
    /// Task exclusive timeout (seconds)
    /// 0 or less does not take effect
    /// </summary>
    public int LockTimeOut { get; set; }
    /// <summary>
    /// Specify the job running node
    /// </summary>
    public string? NodeName { get; set; }
}
