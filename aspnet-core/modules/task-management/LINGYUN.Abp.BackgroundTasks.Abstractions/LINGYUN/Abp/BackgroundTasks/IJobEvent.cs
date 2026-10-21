using System.Threading.Tasks;

namespace LINGYUN.Abp.BackgroundTasks;

/// <summary>
/// Mount task event interface
/// </summary>
public interface IJobEvent
{
    /// <summary>
    /// Event before task start
    /// </summary>
    /// <param name="jobEventData"></param>
    /// <returns></returns>
    Task OnJobBeforeExecuted(JobEventContext context);
    /// <summary>
    /// Event after task completion
    /// </summary>
    /// <param name="jobEventData"></param>
    /// <returns></returns>
    Task OnJobAfterExecuted(JobEventContext context);
}
