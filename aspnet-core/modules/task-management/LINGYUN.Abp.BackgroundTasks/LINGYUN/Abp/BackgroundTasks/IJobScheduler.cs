using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace LINGYUN.Abp.BackgroundTasks;
/// <summary>
/// Job scheduler interface
/// </summary>
public interface IJobScheduler
{
    /// <summary>
    /// Task enqueue
    /// </summary>
    /// <param name="job"></param>
    /// <returns></returns>
    Task<bool> QueueAsync(JobInfo job, CancellationToken cancellationToken = default);
    /// <summary>
    /// Task enqueue
    /// </summary>
    /// <param name="jobs"></param>
    /// <returns></returns>
    Task QueuesAsync(IEnumerable<JobInfo> jobs, CancellationToken cancellationToken = default);
    /// <summary>
    /// Whether the task exists
    /// </summary>
    /// <param name="group"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<bool> ExistsAsync(JobInfo job, CancellationToken cancellationToken = default);
    /// <summary>
    /// Trigger task
    /// </summary>
    /// <param name="group"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    Task TriggerAsync(JobInfo job, CancellationToken cancellationToken = default);
    /// <summary>
    /// Pause the task
    /// </summary>
    /// <param name="group"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    Task PauseAsync(JobInfo job, CancellationToken cancellationToken = default);
    /// <summary>
    /// Resume the suspended task
    /// </summary>
    /// <param name="group"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    Task ResumeAsync(JobInfo job, CancellationToken cancellationToken = default);
    /// <summary>
    /// Remove task
    /// </summary>
    /// <param name="group"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<bool> RemoveAsync(JobInfo job, CancellationToken cancellationToken = default);
    /// <summary>
    /// Start the task coordinator
    /// </summary>
    /// <returns></returns>
    Task<bool> StartAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Stop the task coordinator
    /// </summary>
    /// <returns></returns>
    Task<bool> StopAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Release the task coordinator
    /// </summary>
    /// <returns></returns>
    Task<bool> ShutdownAsync(CancellationToken cancellationToken = default);
}
