using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.BackgroundTasks;
/// <summary>
/// Job scheduling interface
/// </summary>
/// <remarks>
/// Usage scenario: Scheduling jobs to the job scheduler (scheduling jobs to the specified running node)
/// </remarks>
public interface IJobDispatcher
{
    /// <summary>
    /// Schedule a single job
    /// </summary>
    /// <param name="job">Job details</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> DispatchAsync(JobInfo job, CancellationToken cancellationToken = default);
    /// <summary>
    /// Schedule multiple jobs
    /// </summary>
    /// <param name="jobs">Job list</param>
    /// <param name="nodeName">Run node</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> DispatchAsync(
        IEnumerable<JobInfo> jobs,
        string? nodeName = null,
        Guid? tenantId = null,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Notify to clean up expired jobs
    /// </summary>
    /// <remarks>
    /// Notify each node to clean up expired jobs in the current node
    /// </remarks>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task CleanExpiredJobAsync(
        Guid? tenantId = null,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Check running jobs
    /// </summary>
    /// <remarks>
    /// Notify each node to check the running jobs on the current node
    /// </remarks>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task CheckRuningJobAsync(
        Guid? tenantId = null,
        CancellationToken cancellationToken = default);
}
