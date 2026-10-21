using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace LINGYUN.Abp.Account;

public interface IMyClaimAppService : IApplicationService
{
    /// <summary>
    /// Query binding status
    /// </summary>
    /// <param name="claimType"></param>
    /// <returns></returns>
    Task<GetUserClaimStateDto> GetStateAsync(string claimType);
    /// <summary>
    /// Reset binding status
    /// </summary>
    /// <param name="claimType"></param>
    /// <returns></returns>
    Task ResetAsync(string claimType);
}
