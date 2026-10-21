using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Guids;
using Volo.Abp.Json;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Timing;

namespace LINGYUN.Abp.BackgroundTasks;

public class BackgroundJobManager : IBackgroundJobManager
{
    protected IClock Clock { get; }
    protected IJobStore JobStore { get; }
    protected IJobPublisher JobPublisher { get; }
    protected ICurrentTenant CurrentTenant { get; }
    protected IGuidGenerator GuidGenerator { get; }
    protected IJsonSerializer JsonSerializer { get; }
    protected AbpBackgroundTasksOptions TasksOptions { get; }
    protected AbpBackgroundJobOptions Options { get; }
    public BackgroundJobManager(
        IClock clock,
        IJobStore jobStore,
        IJobPublisher jobPublisher,
        ICurrentTenant currentTenant,
        IGuidGenerator guidGenerator,
        IJsonSerializer jsonSerializer,
        IOptions<AbpBackgroundJobOptions> options,
        IOptions<AbpBackgroundTasksOptions> taskOptions)
    {
        Clock = clock;
        JobStore = jobStore;
        JobPublisher = jobPublisher;
        CurrentTenant = currentTenant;
        GuidGenerator = guidGenerator;
        JsonSerializer = jsonSerializer;
        Options = options.Value;
        TasksOptions = taskOptions.Value;
    }

    public async virtual Task<string> EnqueueAsync<TArgs>(
        TArgs args,
        BackgroundJobPriority priority = BackgroundJobPriority.Normal,
        TimeSpan? delay = null)
    {
        var jobConfiguration = Options.GetJob<TArgs>();
         // Use BeginTime to express delayed start to avoid confusion with JobInfo.Interval (repeat interval) semantics.
         // The original implementation uses delay.Value.Seconds (only the second component 0-59 is taken),
         // Will cause whole minute/hour delays such as TimeSpan.FromMinutes(5) to degrade to 0, causing the task to be executed immediately.
         var now = Clock.Now;
         var beginTime = now;
         if (delay.HasValue && delay.Value > TimeSpan.Zero)
         {
             beginTime = now.Add(delay.Value);
         }
         var jobId = GuidGenerator.Create();
         var jobArgs = new Dictionary<string, object>
         {
             { nameof(TArgs), JsonSerializer.Serialize(args!) },
             { "ArgsType", jobConfiguration.ArgsType.AssemblyQualifiedName! },
             { "JobType", jobConfiguration.JobType.AssemblyQualifiedName! },
             { "JobName", jobConfiguration.JobName },
         };
         var jobInfo = new JobInfo
         {
             Id = jobId.ToString(),
             TenantId = CurrentTenant.Id,
             Name = jobId.ToString(),
             Group = "BackgroundJobs",
             Priority = ConverForm(priority),
             Source = JobSource.System,
             BeginTime = beginTime,
             Args = jobArgs,
             Description = "From the framework background jobs",
             JobType = JobType.Once,
             CreationTime = now,
            // Make sure not to be polled into the queue
            Status = JobStatus.None,
            NodeName = TasksOptions.NodeName,
            Type = typeof(BackgroundJobAdapter<TArgs>).AssemblyQualifiedName!,
        };

        if (TasksOptions.JobDispatcherSelectors.IsMatch(jobConfiguration.JobType))
        {
            var selector = TasksOptions.JobDispatcherSelectors.GetJobTypeSelector(jobConfiguration.JobType);

            jobInfo.Interval = selector.Interval ?? jobInfo.Interval;
            jobInfo.LockTimeOut = selector.LockTimeOut ?? jobInfo.LockTimeOut;
            jobInfo.Priority = selector.Priority ?? jobInfo.Priority;
            jobInfo.MaxCount = selector.MaxCount ?? jobInfo.MaxCount;
            jobInfo.MaxTryCount = selector.MaxTryCount ?? jobInfo.MaxTryCount;

            if (!selector.NodeName.IsNullOrWhiteSpace())
            {
                jobInfo.NodeName = selector.NodeName;
            }

            if (!selector.Cron.IsNullOrWhiteSpace())
            {
                jobInfo.Cron = selector.Cron;
            }
        }

        // store state
        await JobStore.StoreAsync(jobInfo);

        // Publish job
        await JobPublisher.PublishAsync(jobInfo);

        return jobInfo.Id;
    }

    private JobPriority ConverForm(BackgroundJobPriority priority)
    {
        return priority switch
        {
            BackgroundJobPriority.Low => JobPriority.Low,
            BackgroundJobPriority.High => JobPriority.High,
            BackgroundJobPriority.BelowNormal => JobPriority.BelowNormal,
            BackgroundJobPriority.AboveNormal => JobPriority.AboveNormal,
            _ => JobPriority.Normal,
        };
    }
}
