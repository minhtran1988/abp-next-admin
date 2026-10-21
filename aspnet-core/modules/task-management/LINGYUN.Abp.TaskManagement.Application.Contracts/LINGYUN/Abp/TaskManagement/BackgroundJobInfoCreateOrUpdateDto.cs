using LINGYUN.Abp.BackgroundTasks;
using Volo.Abp.Data;
using Volo.Abp.Validation;

namespace LINGYUN.Abp.TaskManagement;

public abstract class BackgroundJobInfoCreateOrUpdateDto
{
    /// <summary>
    /// Whether to enable
    /// </summary>
    public bool IsEnabled { get; set; } = true;
    /// <summary>
    /// Task parameters
    /// </summary>
    public ExtraPropertyDictionary Args { get; set; } = new ExtraPropertyDictionary();
    /// <summary>
    /// Description
    /// </summary>
    [DynamicStringLength(typeof(BackgroundJobInfoConsts), nameof(BackgroundJobInfoConsts.MaxDescriptionLength))]
    public string? Description { get; set; }
    /// <summary>
    /// Task category
    /// </summary>
    public JobType JobType { get; set; }
    /// <summary>
    /// Cron expression, if it is a continuous task, it needs to be specified
    /// </summary>
    [DynamicStringLength(typeof(BackgroundJobInfoConsts), nameof(BackgroundJobInfoConsts.MaxCronLength))]
    public string? Cron { get; set; }
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
    /// Interval time, unit seconds, conflicts with Cron expression
    /// Default: 300
    /// </summary>
    public int Interval { get; set; } = 300;
    /// <summary>
    /// Task priority
    /// </summary>
    public JobPriority Priority { get; set; }
    /// <summary>
    /// Task exclusive timeout (seconds)
    /// 0 or less does not take effect
    /// </summary>
    public int LockTimeOut { get; set; }
}
