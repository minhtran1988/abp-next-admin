using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.Identity;
using Volo.Abp.Identity.AspNetCore;
using Volo.Abp.Uow;
using Volo.Abp.Users;

namespace LINGYUN.Abp.Account.Web.Pages.Account;

[IgnoreAntiforgeryToken]
public class ExternalLoginBindModel : AbpPageModel
{
    protected AbpSignInManager SignInManager => LazyServiceProvider.LazyGetRequiredService<AbpSignInManager>();

    protected IdentityUserManager UserManager => LazyServiceProvider.LazyGetRequiredService<IdentityUserManager>();

    protected IOptions<IdentityOptions> IdentityOptions => LazyServiceProvider.LazyGetRequiredService<IOptions<IdentityOptions>>();

    public virtual async Task<IActionResult> OnGetAsync()
    {
        return await Task.FromResult(NotFound());
    }

    /// <summary>
    /// SPA binding third-party binding
    /// </summary>
    /// <param name="provider">Third-party provider</param>
    /// <param name="returnUrl">Binding success callback address</param>
    /// <returns>IActionResult</returns>
    public virtual async Task<IActionResult> OnPostAsync(string provider, string returnUrl)
    {
        if (string.IsNullOrWhiteSpace(provider) || string.IsNullOrWhiteSpace(returnUrl))
        {
            Logger.LogWarning("The parameter is incorrect");
            return Redirect(QueryHelpers.AddQueryString(returnUrl, new Dictionary<string, string?>()
            {
                ["error"] = "The parameter is incorrect"
            }));
        }

        var tenantId = CurrentTenant.Id;
        Logger.LogInformation("CurrentTenant:{TenantId}", tenantId);
        var userId = CurrentUser.GetId();
        Logger.LogInformation("CurrentUser:{UserId}", userId);

        var redirectUrl = Url.Page("ExternalLoginBind", pageHandler: "BindCallback", values: new { returnUrl, userId, tenantId });

        var properties = SignInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, userId.ToString());
        properties.Items["scheme"] = provider;

        return await Task.FromResult(Challenge(properties, provider));
    }

    /// <summary>
    /// SPA binds third-party login callback
    /// </summary>
    /// <param name="returnUrl">Binding success callback address</param>
    /// <param name="userId">UserId</param>
    /// <param name="tenantId">TenantId</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [UnitOfWork]
    public virtual async Task<IActionResult> OnGetBindCallbackAsync(string returnUrl, string userId, Guid? tenantId)
    {
        if (string.IsNullOrWhiteSpace(returnUrl))
        {
            Logger.LogWarning("The returnUrl cannot be empty");
            return Redirect(QueryHelpers.AddQueryString(returnUrl, new Dictionary<string, string?>()
            {
                ["error"] = "The returnUrl cannot be empty"
            }));
        }

        using (CurrentTenant.Change(tenantId))
        {
            Logger.LogInformation("CurrentTenant:{TenantId}", tenantId);

            await IdentityOptions.SetAsync();

            var loginInfo = await SignInManager.GetExternalLoginInfoAsync(userId);
            if (loginInfo == null)
            {
                Logger.LogWarning("External login info is not available");
                return Redirect(QueryHelpers.AddQueryString(returnUrl, new Dictionary<string, string?>()
                {
                    ["error"] = "External login info is not available."
                }));
            }

            await SignInManager.SignOutAsync();

            if (await UserManager.FindByLoginAsync(loginInfo.LoginProvider, loginInfo.ProviderKey) == null)
            {
                var externalUser = await UserManager.FindByIdAsync(userId);
                CheckIdentityErrors(await UserManager.AddLoginAsync(externalUser!, loginInfo));
            }

            return Redirect(returnUrl);
        }
    }

    protected virtual void CheckIdentityErrors(IdentityResult identityResult)
    {
        if (!identityResult.Succeeded)
        {
            throw new UserFriendlyException("Operation failed: " + identityResult.Errors.Select(e => $"[{e.Code}] {e.Description}").JoinAsString(", "));
        }
    }
}
