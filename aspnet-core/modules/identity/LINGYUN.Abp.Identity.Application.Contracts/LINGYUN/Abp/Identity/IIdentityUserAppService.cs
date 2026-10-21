using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace LINGYUN.Abp.Identity;

public interface IIdentityUserAppService : IApplicationService
{

    #region OrganizationUnit

    Task<ListResultDto<OrganizationUnitDto>> GetOrganizationUnitsAsync(Guid id);

    Task SetOrganizationUnitsAsync(Guid id, IdentityUserOrganizationUnitUpdateDto input);

    Task RemoveOrganizationUnitsAsync(Guid id, Guid ouId);

    #endregion

    #region ClaimType

    Task<ListResultDto<IdentityClaimDto>> GetClaimsAsync(Guid id);

    Task AddClaimAsync(Guid id, IdentityUserClaimCreateDto input);

    Task UpdateClaimAsync(Guid id, IdentityUserClaimUpdateDto input);

    Task DeleteClaimAsync(Guid id, IdentityUserClaimDeleteDto input);

    #endregion

    /// <summary>
    /// Change user two-factor authentication options
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task ChangeTwoFactorEnabledAsync(Guid id, TwoFactorEnabledDto input);
    /// <summary>
    /// Change user password
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task ChangePasswordAsync(Guid id, IdentityUserSetPasswordInput input);
    /// <summary>
    /// Lock
    /// </summary>
    /// <param name="id"></param>
    /// <param name="seconds">Lock duration</param>
    /// <returns></returns>
    Task LockAsync(Guid id, int seconds);
    /// <summary>
    /// Unlock
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task UnLockAsync(Guid id);
}
