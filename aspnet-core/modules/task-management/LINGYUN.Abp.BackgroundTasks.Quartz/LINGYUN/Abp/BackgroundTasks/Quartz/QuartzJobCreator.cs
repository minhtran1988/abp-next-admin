using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Quartz;
using System;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Timing;

namespace LINGYUN.Abp.BackgroundTasks.Quartz;

public class QuartzJobCreator : IQuartzJobCreator, ISingletonDependency
{
    public ILogger<QuartzJobCreator> Logger { protected get; set; }

    protected IClock Clock { get; }
    protected IQuartzKeyBuilder KeyBuilder { get; }
    protected IJobDefinitionManager JobDefinitionManager { get; }
    public QuartzJobCreator(
        IClock clock,
        IQuartzKeyBuilder keyBuilder,
        IJobDefinitionManager jobDefinitionManager)
    {
        Clock = clock;
        KeyBuilder = keyBuilder;
        JobDefinitionManager = jobDefinitionManager;

        Logger = NullLogger<QuartzJobCreator>.Instance;
    }

    public IJobDetail? CreateJob(JobInfo job)
    {
        var jobDefinition = JobDefinitionManager.GetOrNull(job.Type);
        var jobType = jobDefinition?.JobType ?? Type.GetType(job.Type);
        if (jobType == null)
        {
            // Logger.LogWarning($"The task: {job.Group} - {job.Name}: {job.Type} is not registered and cannot create an instance of the performer type.");
            // return null;

            // Search for local jobs at runtime
            jobType = typeof(QuartzJobSearchJobAdapter);
        }
        else if (!typeof(IJob).IsAssignableFrom(jobType))
        {
            var adapterType = typeof(QuartzJobSimpleAdapter<>);
            jobType = adapterType.MakeGenericType(jobType);
        }

        if (jobType == null)
        {
            return null;
        }

        // Change to JobId as name
        var jobBuilder = JobBuilder.Create(jobType)
                .WithIdentity(KeyBuilder.CreateJobKey(job))
                .WithDescription(job.Description);
        // Multi-node tasks require
        jobBuilder.UsingJobData(nameof(JobInfo.NodeName), job.NodeName);
        // Query task needs
        jobBuilder.UsingJobData(nameof(JobInfo.Id), job.Id);
        // Some scenarios require
        jobBuilder.UsingJobData(nameof(JobInfo.Name), job.Name);
        jobBuilder.UsingJobData(nameof(JobInfo.Type), job.Type);
        jobBuilder.UsingJobData(nameof(JobInfo.Group), job.Group);
        // Calculate the increment required
        jobBuilder.UsingJobData(nameof(JobInfo.TriggerCount), job.TriggerCount);
        // Exclusive tasks require
        jobBuilder.UsingJobData(nameof(JobInfo.LockTimeOut), job.LockTimeOut);
        // Passed job parameters
        jobBuilder.UsingJobData(new JobDataMap(job.Args));
        if (job.TenantId.HasValue)
        {
            // Used in multi-tenant scenarios
            jobBuilder.UsingJobData(nameof(JobInfo.TenantId), job.TenantId.ToString());
        }

        return jobBuilder.Build();
    }

    public ITrigger? CreateTrigger(JobInfo job)
    {
        var triggerBuilder = TriggerBuilder.Create();

        switch (job.JobType)
        {
            case JobType.Period:
                if (job.Cron.IsNullOrWhiteSpace() || !CronExpression.IsValidExpression(job.Cron))
                {
                    Logger.LogWarning($"The task: {job.Group} - {job.Name} periodic task Cron expression was invalid and the task trigger could not be created.");
                    return null;
                }
                if (job.CalcCanBeTriggered() == 0)
                {
                    Logger.LogWarning($"The task: {job.Group} - {job.Name} reached trigger peak and the task trigger could not be created.");
                    return null;
                }
                triggerBuilder
                    .WithIdentity(KeyBuilder.CreateTriggerKey(job))
                    .WithDescription(job.Description)
                    .EndAt(job.EndTime)
                    .ForJob(KeyBuilder.CreateJobKey(job))
                    .WithPriority((int)job.Priority)
                    .WithCronSchedule(job.Cron);
                if (job.BeginTime > Clock.Now)
                {
                    triggerBuilder = triggerBuilder.StartAt(job.BeginTime);
                }
                break;
            case JobType.Once:
            case JobType.Persistent:
            default:
                var maxCount = job.CalcCanBeTriggered();
                if (maxCount == 0)
                {
                    Logger.LogWarning($"The task: {job.Group} - {job.Name} reached trigger peak and the task trigger could not be created.");
                    return null;
                }

                // Quartz needs to be decremented by one digit
                maxCount -= 1;
                if (maxCount < -1)
                {
                    maxCount = -1;
                }

                triggerBuilder
                    .WithIdentity(KeyBuilder.CreateTriggerKey(job))
                    .WithDescription(job.Description)
                    .EndAt(job.EndTime)
                    .ForJob(KeyBuilder.CreateJobKey(job))
                    .WithPriority((int)job.Priority);
                    
                // The first startup time is determined by BeginTime, Interval is no longer used as "delayed startup".
                // Interval only represents "repeat interval" in Persistent periodic tasks.
                if (job.BeginTime > Clock.Now)
                {
                    triggerBuilder.StartAt(job.BeginTime);
                }
                else
                {
                    triggerBuilder.StartNow();
                }
                
                // Quartz convention. Repeat interval cannot be 0
                // fix throw Quartz.SchedulerException: Repeat Interval cannot be zero.
                var scheduleBuilder = SimpleScheduleBuilder.Create();
                // TODO: Quartz's built-in retry mechanism cannot be used
                // scheduleBuilder.WithRepeatCount(maxCount);
                if (job.JobType == JobType.Persistent)
                {
                    scheduleBuilder.WithRepeatCount(maxCount);
                }
                scheduleBuilder.WithIntervalInSeconds(job.Interval > 0 ? job.Interval : 300);


                triggerBuilder.WithSchedule(scheduleBuilder);

                break;
        }

        return triggerBuilder.Build();
    }
}
