using System;
using System.Threading.Tasks;

namespace LINGYUN.Abp.Account.Security;
/// <summary>
/// Email security code sending interface
/// </summary>
public interface IAccountEmailSecurityCodeSender
{
    /// <summary>
    /// Send email login verification code
    /// </summary>
    /// <param name="code">Verification code</param>
    /// <param name="userName">Username</param>
    /// <param name="emailAddress">Email address</param>
    /// <returns></returns>
    Task SendLoginCodeAsync(
        string code,
        string userName,
        string emailAddress);
    /// <summary>
    /// Send email confirmation link
    /// </summary>
    /// <param name="userId">UserId</param>
    /// <param name="userEmail">User Email</param>
    /// <param name="confirmToken">Confirm Token</param>
    /// <param name="appName">Application name</param>
    /// <param name="returnUrl">Callback path</param>
    /// <param name="returnUrlHash">Callback path Hash</param>
    /// <param name="userTenantId">User tenant Id</param>
    /// <returns></returns>
    Task SendConfirmLinkAsync(
        Guid userId,
        string userEmail,
        string confirmToken,
        string appName,
        string? returnUrl = null,
        string? returnUrlHash = null,
        Guid? userTenantId = null
    );
}
