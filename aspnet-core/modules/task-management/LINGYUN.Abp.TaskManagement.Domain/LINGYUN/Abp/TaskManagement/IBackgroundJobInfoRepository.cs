using LINGYUN.Abp.BackgroundTasks;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Specifications;

namespace LINGYUN.Abp.TaskManagement;

public interface IBackgroundJobInfoRepository : IRepository<BackgroundJobInfo, string>
{
    Task<bool> CheckNameAsync(
        string group,
        string name,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get the job
    /// </summary>
    /// <param name="id"></param>
    /// <param name="includeDetails"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<JobInfo?> FindJobAsync(
        string id,
        bool includeDetails = true,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get the list of expired tasks
    /// </summary>
    /// <param name="maxResultCount"></param>
    /// <param name="jobExpiratime"></param>
    /// <param name="nodeName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<BackgroundJobInfo>> GetExpiredJobsAsync(
        int maxResultCount,
        TimeSpan jobExpiratime,
        string? nodeName = null,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get all periodic tasks
    /// Tasks with Cron expressions specified need to be handed over to the task engine as persistent tasks.
    /// </summary>
    /// <returns></returns>
    Task<List<BackgroundJobInfo>> GetAllPeriodTasksAsync(
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get the list of tasks waiting to be queued
    /// </summary>
    /// <param name="maxResultCount"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<BackgroundJobInfo>> GetWaitingListAsync(
        int maxResultCount,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get the number of filtered tasks
    /// </summary>
    /// <param name="specification">Query specification</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> GetCountAsync(
        ISpecification<BackgroundJobInfo> specification,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get the filtered task list
    /// </summary>
    /// <param name="specification">Query specification</param>
    /// <param name="sorting"></param>
    /// <param name="maxResultCount"></param>
    /// <param name="skipCount"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<BackgroundJobInfo>> GetListAsync(
        ISpecification<BackgroundJobInfo> specification,
        string? sorting = $"{nameof(BackgroundJobInfo.CreationTime)} DESC",
        int maxResultCount = 10,
        int skipCount = 0,
        CancellationToken cancellationToken = default);
}
