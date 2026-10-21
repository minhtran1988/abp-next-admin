using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace LINGYUN.Abp.BackgroundTasks.Internal;

public class JobExecutedEvent : JobEventBase<JobExecutedEvent>, ITransientDependency
{
    protected override Task<bool> CanAfterExecuted(JobEventContext context)
    {
        if (context.EventData.Type.IsDefined(typeof(DisableJobStatusAttribute), true))
        {
            Logger.LogDebug("The job change event could not be processed because the job marked the DisableJobStatus attribute!");
            return Task.FromResult(false);
        }
        return base.CanAfterExecuted(context);
    }

    protected override async Task OnJobAfterExecutedAsync(JobEventContext context)
    {
        var store = context.ServiceProvider.GetRequiredService<IJobStore>();
        var job = await store.FindAsync(context.EventData.Key, context.EventData.CancellationToken);
        if (job == null)
        {
            Logger.LogWarning("Cannot process job change event because job with key {Id} could not be found!", context.EventData.Key);
            return;
        }

        job.TriggerCount += 1;
        job.TenantId = context.EventData.TenantId;
        job.LastRunTime = context.EventData.RunTime;
        job.NextRunTime = context.EventData.NextRunTime;
        job.Result = context.EventData.Result ?? "OK";
        job.Status = JobStatus.Running;

        // You can retry after the task is abnormal
        if (context.EventData.Exception != null)
        {
            job.TryCount += 1;
            job.IsAbandoned = false;
            job.Result = GetExceptionMessage(context.EventData.Exception);

            // There is no need to change the retry strategy for periodic tasks
            if (job.JobType != JobType.Period)
            {
                // Mark the task as running and will be polled to re-enter the queue
                job.Status = JobStatus.FailedRetry;
                // Priority needs to be recalculated after multiple exceptions
                if (job.TryCount <= (job.MaxTryCount / 2) &&
                    job.TryCount > (job.MaxTryCount / 3))
                {
                    job.Priority = JobPriority.BelowNormal;
                }
                else if (job.TryCount > (job.MaxTryCount / 1.5))
                {
                    job.Priority = JobPriority.Low;
                }

                // Waiting time adjustment
                if (job.Interval <= 0)
                {
                    job.Interval = 50;
                }

                var retryInterval = job.Interval * 1.5;
                job.Interval = Convert.ToInt32(retryInterval);
            }

            // Will not mark stop when maximum retries are not set
            if (job.MaxTryCount > 0 && job.TryCount >= job.MaxTryCount)
            {
                job.Status = JobStatus.Stopped;
                job.IsAbandoned = true;
                job.NextRunTime = null;
                await RemoveJobQueueAsync(context, job, context.EventData.CancellationToken);
            }
            else
            {
                // The periodic job is already in the queue and needs to be ignored
                if (job.JobType != JobType.Period)
                {
                    // Failed jobs need to be scheduled by the current node
                    await ScheduleJobAsync(context, job, context.EventData.CancellationToken);
                }
            }
        }
        else
        {
            // Reset the number of retries after one success
            job.TryCount = 0;
            var jobCompleted = false;

            // Mark completed when trying to reach the upper limit
            if (job.Status == JobStatus.FailedRetry &&
                job.TryCount >= job.MaxTryCount)
            {
                jobCompleted = true;
            }

            // When all tasks reach the upper limit, mark them as completed
            if (job.MaxCount > 0 && job.TriggerCount >= job.MaxCount)
            {
                jobCompleted = true;
            }

            // Mark the one-time task as completed after executing it once
            if (job.JobType == JobType.Once)
            {
                jobCompleted = true;
            }

            if (jobCompleted)
            {
                job.Status = JobStatus.Completed;
                job.NextRunTime = null;

                await RemoveJobQueueAsync(context, job, context.EventData.CancellationToken);
            }
        }

        await store.StoreAsync(job, context.EventData.CancellationToken);
    }

    private async Task RemoveJobQueueAsync(JobEventContext context, JobInfo jobInfo, CancellationToken cancellationToken = default)
    {
        var jobScheduler = context.ServiceProvider.GetRequiredService<IJobScheduler>();
        await jobScheduler.RemoveAsync(jobInfo, cancellationToken);
    }

    private async Task ScheduleJobAsync(JobEventContext context, JobInfo jobInfo, CancellationToken cancellationToken = default)
    {
        var jobScheduler = context.ServiceProvider.GetRequiredService<IJobScheduler>();
        await jobScheduler.QueueAsync(jobInfo, cancellationToken);
    }

    private string GetExceptionMessage(Exception exception)
    {
        if (exception.InnerException != null)
        {
            return GetExceptionMessage(exception.InnerException);
        }

        return exception.Message;
    }
}
