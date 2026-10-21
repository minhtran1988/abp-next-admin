using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.BackgroundTasks;
/// <summary>
/// Job release interface
/// </summary>
/// <remarks>
/// Usage scenarios: Publish jobs to the job scheduler (publish jobs to the current running node)
/// </remarks>
public interface IJobPublisher
{
    Task<bool> PublishAsync(JobInfo job, CancellationToken cancellationToken = default);
}
