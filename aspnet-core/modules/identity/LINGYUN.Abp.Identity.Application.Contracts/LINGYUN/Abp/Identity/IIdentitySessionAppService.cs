using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace LINGYUN.Abp.Identity;
public interface IIdentitySessionAppService
{
    /// <summary>
    /// Get user session list
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedResultDto<IdentitySessionDto>> GetSessionsAsync(GetUserSessionsInput input);
    /// <summary>
    /// Cancel user session
    /// </summary>
    /// <param name="sessionId">session id</param>
    /// <returns></returns>
    Task RevokeSessionAsync(string sessionId);
}
