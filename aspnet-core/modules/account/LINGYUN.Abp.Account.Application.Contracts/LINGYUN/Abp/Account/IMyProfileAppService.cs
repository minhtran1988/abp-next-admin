using LINGYUN.Abp.Identity;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace LINGYUN.Abp.Account;

public interface IMyProfileAppService : IApplicationService
{
    /// <summary>
    /// Get validator information
    /// </summary>
    /// <returns></returns>
    Task<AuthenticatorDto> GetAuthenticatorAsync();
    /// <summary>
    /// Verify validator code
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<AuthenticatorRecoveryCodeDto> VerifyAuthenticatorCodeAsync(VerifyAuthenticatorCodeInput input);
    /// <summary>
    /// Get the session list
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedResultDto<IdentitySessionDto>> GetSessionsAsync(GetMySessionsInput input);
    /// <summary>
    /// Cancel session
    /// </summary>
    /// <param name="sessionId">session id</param>
    /// <returns></returns>
    Task RevokeSessionAsync(string sessionId);
    /// <summary>
    /// Reset validator
    /// </summary>
    /// <returns></returns>
    Task ResetAuthenticatorAsync();
    /// <summary>
    /// Get the secondary authentication status
    /// </summary>
    /// <returns></returns>
    Task<TwoFactorEnabledDto> GetTwoFactorEnabledAsync();
    /// <summary>
    /// Change secondary authentication
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task ChangeTwoFactorEnabledAsync(TwoFactorEnabledDto input);
    /// <summary>
    /// Send verification code to change mobile phone number
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task SendChangePhoneNumberCodeAsync(SendChangePhoneNumberCodeInput input);
    /// <summary>
    /// Change mobile phone binding
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <remarks>
    /// Secondary authentication is required, mainly because the redirect page cannot be used to modify relevant information (named WeChat applet)
    /// </remarks>
    Task ChangePhoneNumberAsync(ChangePhoneNumberInput input);
    /// <summary>
    /// Send confirmation email link
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task SendEmailConfirmLinkAsync(SendEmailConfirmCodeDto input);
    /// <summary>
    /// Confirm email address
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task ConfirmEmailAsync(ConfirmEmailInput input);
    /// <summary>
    /// Change user avatar
    /// </summary>
    /// <returns></returns>
    Task ChangePictureAsync(ChangePictureInput input);
    /// <summary>
    /// Get user avatar
    /// </summary>
    /// <returns></returns>
    Task<IRemoteStreamContent> GetPictureAsync();
}
