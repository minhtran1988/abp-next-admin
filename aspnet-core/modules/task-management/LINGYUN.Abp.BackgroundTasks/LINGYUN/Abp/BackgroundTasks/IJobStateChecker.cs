using System;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.BackgroundTasks;
/// <summary>
/// Job status check interface
/// </summary>
public interface IJobStateChecker
{
    /// <summary>
    /// Clean up expired jobs
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task CleanExpiredJobAsync(
        Guid? tenantId = null,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Check running jobs
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task CheckRuningJobAsync(
        Guid? tenantId = null,
        CancellationToken cancellationToken = default);
}
