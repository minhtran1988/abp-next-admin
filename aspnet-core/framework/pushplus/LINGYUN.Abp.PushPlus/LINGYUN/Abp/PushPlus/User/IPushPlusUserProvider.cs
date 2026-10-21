using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.PushPlus.User;
/// <summary>
/// User interface
/// </summary>
public interface IPushPlusUserProvider
{
    /// <summary>
    /// Get token
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<string> GetMyTokenAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Profile details
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PushPlusUserProfile> GetMyProfileAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Get the remaining time to unblock
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PushPlusUserLimitTime> GetMyLimitTimeAsync(CancellationToken cancellationToken = default);
}
