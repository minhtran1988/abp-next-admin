using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.Identity.Session;
/// <summary>
/// Manage user sessions
/// </summary>
public interface IIdentitySessionManager
{
    /// <summary>
    /// Save session
    /// </summary>
    /// <param name="claimsPrincipal">User identity principal</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SaveSessionAsync(
        ClaimsPrincipal claimsPrincipal,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Save session
    /// </summary>
    /// <param name="clientId">ClientId</param>
    /// <param name="claimsPrincipal">User identity principal</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SaveSessionAsync(
        string? clientId,
        ClaimsPrincipal claimsPrincipal,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Cancel user session
    /// </summary>
    /// <param name="sessionId">session id</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task RevokeSessionAsync(
        string sessionId,
        CancellationToken cancellationToken = default);
}
