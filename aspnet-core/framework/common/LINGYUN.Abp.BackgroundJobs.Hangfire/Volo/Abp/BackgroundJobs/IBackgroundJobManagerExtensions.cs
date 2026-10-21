using Hangfire;
using JetBrains.Annotations;
using LINGYUN.Abp.BackgroundJobs.Hangfire;
using System.Threading.Tasks;

namespace Volo.Abp.BackgroundJobs;

public static class IBackgroundJobManagerExtensions
{
    /// <summary>
    /// Background jobs enter the periodic queue
    /// </summary>
    /// <typeparam name="TArgs">Job parameter type</typeparam>
    /// <param name="backgroundJobManager">Background job manager</param>
    /// <param name="cron">Cron expression</param>
    /// <param name="args">Job parameters</param>
    /// <returns></returns>
    public static Task EnqueueAsync<TArgs>(
        this IBackgroundJobManager backgroundJobManager,
        [NotNull] string cron,
        TArgs args
    )
    {
        Check.NotNullOrWhiteSpace(cron, nameof(cron));
        Check.NotNull(args, nameof(args));

        var jobName = BackgroundJobNameAttribute.GetName<TArgs>();

        RecurringJob.AddOrUpdate<HangfireJobExecutionAdapter<TArgs>>(jobName, adapter => adapter.ExecuteAsync(args), cron);

        return Task.CompletedTask;
    }
}
