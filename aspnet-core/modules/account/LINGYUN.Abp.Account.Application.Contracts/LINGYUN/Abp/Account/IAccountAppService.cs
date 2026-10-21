using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace LINGYUN.Abp.Account;

public interface IAccountAppService : IApplicationService
{
    /// <summary>
    /// Register a user account via mobile phone number
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task RegisterAsync(PhoneRegisterDto input);
    /// <summary>
    /// Register a user account through the WeChat applet
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task RegisterAsync(WeChatRegisterDto input);
    /// <summary>
    /// Reset user password via mobile phone number
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task ResetPasswordAsync(PhoneResetPasswordDto input);
    /// <summary>
    /// Send mobile phone registration verification code SMS
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task SendPhoneRegisterCodeAsync(SendPhoneRegisterCodeDto input);
    /// <summary>
    /// Send mobile phone login verification code SMS
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task SendPhoneSigninCodeAsync(SendPhoneSigninCodeDto input);
    /// <summary>
    /// Send mobile phone reset password verification code SMS
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task SendPhoneResetPasswordCodeAsync(SendPhoneResetPasswordCodeDto input);
    /// <summary>
    /// Send email registration verification code
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task SendEmailRegisterCodeAsync(SendEmailRegisterCodeDto input);
    /// <summary>
    /// Verification email registration verification code
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<bool> VerifyEmailRegisterCodeAsync(VerifyEmailRegisterCodeInput input);
    /// <summary>
    /// Send email login verification code
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task SendEmailSigninCodeAsync(SendEmailSigninCodeDto input);
    /// <summary>
    /// Send confirmation email link
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task SendEmailConfirmLinkAsync(SendUserEmailConfirmCodeDto input);
    /// <summary>
    /// Confirm email address
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task ConfirmEmailAsync(ConfirmUserEmailInput input);
    /// <summary>
    /// Get the list of user secondary authentication providers
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<ListResultDto<NameValue>> GetTwoFactorProvidersAsync(GetTwoFactorProvidersInput input);
}
