using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Volo.Abp.Auditing;
using Volo.Abp.DependencyInjection;

namespace LINGYUN.Abp.BackgroundTasks.Internal;

/// <summary>
/// Storage task log
/// </summary>
/// <remarks>
/// If the task type is marked with the <see cref="DisableAuditingAttribute"/> attribute, the log will not be recorded.
/// </remarks>
public class JobLogEvent : JobEventBase<JobLogEvent>, ITransientDependency
{
    protected override Task<bool> CanAfterExecuted(JobEventContext context)
    {
        if (context.EventData.Type.IsDefined(typeof(DisableAuditingAttribute), true))
        {
            Logger.LogDebug("The job change event could not be processed because the job marked the DisableAuditing attribute!");
            return Task.FromResult(false);
        }
        return base.CanAfterExecuted(context);
    }

    protected async override Task OnJobAfterExecutedAsync(JobEventContext context)
    {
        var store = context.ServiceProvider.GetRequiredService<IJobStore>();
        await store.StoreLogAsync(context.EventData);
    }
}
