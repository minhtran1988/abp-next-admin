using LINGYUN.Abp.BackgroundTasks;
using System;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace LINGYUN.Abp.TaskManagement;

public class BackgroundJobInfo : AuditedAggregateRoot<string>, IMultiTenant
{
    public virtual Guid? TenantId { get; protected set; }
    /// <summary>
    /// Task name
    /// </summary>
    public virtual string Name { get; protected set; } = default!;
    /// <summary>
    /// Task grouping
    /// </summary>
    public virtual string Group { get; protected set; } = default!;
    /// <summary>
    /// Task type
    /// </summary>
    public virtual string Type { get; protected set; } = default!;
    /// <summary>
    /// Last execution result
    /// </summary>
    public virtual string? Result { get; protected set; }
    /// <summary>
    /// Task parameters
    /// </summary>
    public virtual ExtraPropertyDictionary Args { get; set; } = default!;
    /// <summary>
    /// Task status
    /// </summary>
    public virtual JobStatus Status { get; protected set; }
    /// <summary>
    /// Whether to enable
    /// </summary>
    public virtual bool IsEnabled { get; set; }
    /// <summary>
    /// Description
    /// </summary>
    public virtual string? Description { get; set; }
    /// <summary>
    /// Task exclusive timeout (seconds)
    /// 0 or less does not take effect
    /// </summary>
    public virtual int LockTimeOut { get; set; }
    /// <summary>
    /// Start time
    /// </summary>
    public virtual DateTime BeginTime { get; protected set; }
    /// <summary>
    /// End time
    /// </summary>
    public virtual DateTime? EndTime { get; protected set; }
    /// <summary>
    /// Last execution time
    /// </summary>
    public virtual DateTime? LastRunTime { get; protected set; }
    /// <summary>
    /// Next execution time
    /// </summary>
    public virtual DateTime? NextRunTime { get; protected set; }
    /// <summary>
    /// Task category
    /// </summary>
    public virtual JobType JobType { get; protected set; }
    /// <summary>
    /// Cron expression, if it is a continuous task, it needs to be specified
    /// </summary>
    public virtual string? Cron { get; protected set; }
    /// <summary>
    /// Job source
    /// </summary>
    public virtual JobSource Source { get; protected set; }
    /// <summary>
    /// Task priority
    /// </summary>
    public virtual JobPriority Priority { get; protected set; }
    /// <summary>
    /// Number of triggers
    /// </summary>
    public virtual int TriggerCount { get; set; }
    /// <summary>
    /// Number of failed retries
    /// </summary>
    public virtual int TryCount { get; set; }
    /// <summary>
    /// Failed retry upper limit
    /// Default: 50
    /// </summary>
    public virtual int MaxTryCount { get; set; }
    /// <summary>
    /// Maximum number of executions
    /// Default: 0, no limit
    /// </summary>
    public virtual int MaxCount { get; set; }
    /// <summary>
    /// Interval time, unit seconds, conflicts with Cron expression
    /// Default: 300
    /// </summary>
    public virtual int Interval { get; protected set; }
    /// <summary>
    /// Continuous failure and will not be executed again
    /// </summary>
    public virtual bool IsAbandoned { get; set; }
    /// <summary>
    /// Specify the job running node
    /// </summary>
    public virtual string? NodeName { get; protected set; }
    protected BackgroundJobInfo() { }

    public BackgroundJobInfo(
        string id,
        string name,
        string group,
        string type,
        IDictionary<string, object?> args,
        DateTime beginTime,
        DateTime? endTime = null,
        JobPriority priority = JobPriority.Normal,
        JobSource source = JobSource.None,
        int maxCount = 0,
        int maxTryCount = 50,
        string? nodeName = null,
        Guid? tenantId = null) : base(id)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), BackgroundJobInfoConsts.MaxNameLength);
        Group = Check.NotNullOrWhiteSpace(group, nameof(group), BackgroundJobInfoConsts.MaxGroupLength);
        Type = Check.NotNullOrWhiteSpace(type, nameof(type), BackgroundJobInfoConsts.MaxTypeLength);
        Priority = priority;
        Source = source;
        BeginTime = beginTime;
        EndTime = endTime;

        MaxCount = maxCount;
        MaxTryCount = maxTryCount;
        NodeName = Check.Length(nodeName, nameof(nodeName), BackgroundJobInfoConsts.MaxNodeNameLength);
        TenantId = tenantId;

        Status = JobStatus.Queuing;

        // TODO: Do I need to move the parameters to another entity?
        // It is recommended to minimize the task parameters as much as possible and only store key information.
        Args = new ExtraPropertyDictionary();
        if (args != null)
        {
            foreach (var arg in args)
            {
                Args[arg.Key] = arg.Value;
            }
        }
    }

    public void SetPeriodJob(string cron)
    {
        Cron = Check.NotNullOrWhiteSpace(cron, nameof(cron), BackgroundJobInfoConsts.MaxCronLength);
        JobType = JobType.Period;
    }

    public void SetOnceJob(int interval)
    {
        Interval = interval;
        JobType = JobType.Once;
    }

    public void SetPersistentJob(int interval)
    {
        Interval = interval;
        JobType = JobType.Persistent;
    }

    public void SetLastRunTime(DateTime? lastRunTime)
    {
        LastRunTime = lastRunTime;
    }

    public void SetNextRunTime(DateTime? nextRunTime)
    {
        NextRunTime = nextRunTime;
    }

    public void SetResult(string? result)
    {
        if (result.IsNullOrWhiteSpace())
        {
            return;
        }
        if (result.Length > BackgroundJobInfoConsts.MaxResultLength)
        {
            result = result.Substring(0, BackgroundJobInfoConsts.MaxResultLength - 1);
        }

        Result = result;
    }

    public void SetStatus(JobStatus status)
    {
        Status = status;
    }

    public void SetPriority(JobPriority priority)
    {
        Priority = priority;
    }
}
