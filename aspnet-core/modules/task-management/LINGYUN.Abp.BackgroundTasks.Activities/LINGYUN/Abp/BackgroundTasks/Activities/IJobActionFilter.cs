using System.Threading.Tasks;

namespace LINGYUN.Abp.BackgroundTasks.Activities;
/// <summary>
/// Job behavior filter
/// </summary>
public interface IJobActionFilter
{
    /// <summary>
    /// Whether to allow post-trigger operations
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    Task<bool> CanAfterExecuted(JobEventContext context);
    /// <summary>
    /// Whether to allow pre-trigger operations
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    Task<bool> CanBeforeExecuted(JobEventContext context);
}
