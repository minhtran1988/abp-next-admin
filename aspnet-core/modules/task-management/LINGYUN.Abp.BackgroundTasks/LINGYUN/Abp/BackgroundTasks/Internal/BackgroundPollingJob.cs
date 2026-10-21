using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Auditing;
using Volo.Abp.MultiTenancy;

namespace LINGYUN.Abp.BackgroundTasks.Internal;

[DisableAuditing]
[DisableJobAction]
public class BackgroundPollingJob : IJobRunnable
{
    public async virtual Task ExecuteAsync(JobRunnableContext context)
    {
        var options = context.ServiceProvider.GetRequiredService<IOptions<AbpBackgroundTasksOptions>>().Value;
        var store = context.ServiceProvider.GetRequiredService<IJobStore>();
        var currentTenant = context.ServiceProvider.GetRequiredService<ICurrentTenant>();

        context.TryGetMultiTenantId(out var tenantId);

        using (currentTenant.Change(tenantId))
        {
            var waitingJobs = await store.GetWaitingListAsync(
                options.MaxJobFetchCount, context.CancellationToken);

            if (!waitingJobs.Any())
            {
                return;
            }

            /* changes: 2023-04-06
             * The job polling interface will poll all node jobs
             * The previous design was to publish to the job scheduler through IJobPublisher
             * If the job is not running on the current node, it will be ignored, causing the job to never actually be executed.
             * 
             * The improvement method is to create a new interface IJobDispatcher, through which the polled non-current node jobs are scheduled.
             * The implementer is the distributed event job scheduler, only the target node will receive the job and start it
             * 
             */

            // Current node job release
            var scheduleJobs = waitingJobs.Where(job => string.Equals(job.NodeName, options.NodeName) || job.NodeName.IsNullOrWhiteSpace());
            var jobPublisher = context.ServiceProvider.GetRequiredService<IJobPublisher>();
            foreach (var job in scheduleJobs)
            {
                await jobPublisher.PublishAsync(job, context.CancellationToken);
            }

            // Non-current node job scheduling
            var dispatchJobs = waitingJobs.Where(job => !job.NodeName.IsNullOrWhiteSpace() && !string.Equals(job.NodeName, options.NodeName));
            if (dispatchJobs.Any())
            {
                var jobDispatcher = context.ServiceProvider.GetRequiredService<IJobDispatcher>();
                foreach (var jobByGroup in dispatchJobs.GroupBy(job => job.NodeName))
                {
                    await jobDispatcher.DispatchAsync(
                        jobByGroup,
                        jobByGroup.Key,
                        tenantId);
                }
            }
        }
    }
}
