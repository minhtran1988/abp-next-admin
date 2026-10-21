using System.Threading.Tasks;

namespace LINGYUN.Abp.BackgroundTasks;

/// <summary>
/// Job lock provider
/// </summary>
public interface IJobLockProvider
{
    Task<bool> TryLockAsync(
        string jobKey,
        int lockSeconds);

    Task<bool> TryReleaseAsync(
        string jobKey);
}
