using LINGYUN.Abp.Account.Dto;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace LINGYUN.Abp.Account;
/// <summary>
/// Associated user application service interface
/// </summary>
public interface IIdentityLinkUserAppService : IApplicationService
{
    /// <summary>
    /// Associated users
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task LinkAsync(LinkUserInput input);
    /// <summary>
    /// Unassociate user
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task UnlinkAsync(UnLinkUserInput input);
    /// <summary>
    /// Generate associated user Token
    /// </summary>
    /// <returns></returns>
    Task<string> GenerateLinkTokenAsync();
    /// <summary>
    /// Generate associated user login Token
    /// </summary>
    /// <returns></returns>
    Task<string> GenerateLinkLoginTokenAsync();
    /// <summary>
    /// Get the list of associated users
    /// </summary>
    /// <returns></returns>
    Task<ListResultDto<LinkUserDto>> GetListAsync();
    /// <summary>
    /// Verify associated users
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<VerifyLinkUserDto> VerifyLinkUserAsync(VerifyLinkUserInput input);
    /// <summary>
    /// Verify associated user Token
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<bool> VerifyLinkTokenAsync(VerifyLinkTokenInput input);
    /// <summary>
    /// Verify associated user login token
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<bool> VerifyLinkLoginTokenAsync(VerifyLinkTokenInput input);
}
