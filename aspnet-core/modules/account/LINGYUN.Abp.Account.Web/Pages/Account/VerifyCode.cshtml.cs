using LINGYUN.Abp.Account.Dto;
using LINGYUN.Abp.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Volo.Abp.Account.Web.Pages.Account;
using Volo.Abp.Identity;
using Volo.Abp.Identity.AspNetCore;
using Volo.Abp.Security.Claims;

namespace LINGYUN.Abp.Account.Web.Pages.Account
{
    public class VerifyCodeModel : AccountPageModel
    {
        [BindProperty]
        public VerifyCodeInputModel Input { get; set; } = default!;
        /// <summary>
        /// Two-factor authentication provider
        /// </summary>
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public string Provider { get; set; } = default!;
        /// <summary>
        /// Redirect Url
        /// </summary>
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public string? ReturnUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public string? ReturnUrlHash { get; set; }
        /// <summary>
        /// Whether to remember login status
        /// </summary>
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public bool RememberMe { get; set; }

        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid? LinkUserId { get; set; }

        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid? LinkTenantId { get; set; }

        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public string? LinkToken { get; set; }

        protected ICurrentPrincipalAccessor CurrentPrincipalAccessor { get; }
        protected IIdentityLinkUserAppService IdentityLinkUserAppService { get; }
        protected IdentityDynamicClaimsPrincipalContributorCache IdentityDynamicClaimsPrincipalContributorCache { get; }

        public VerifyCodeModel(
            ICurrentPrincipalAccessor currentPrincipalAccessor,
            IIdentityLinkUserAppService identityLinkUserAppService,
            IdentityDynamicClaimsPrincipalContributorCache identityDynamicClaimsPrincipalContributorCache)
        {
            CurrentPrincipalAccessor = currentPrincipalAccessor;
            IdentityLinkUserAppService = identityLinkUserAppService;
            IdentityDynamicClaimsPrincipalContributorCache = identityDynamicClaimsPrincipalContributorCache;
        }

        public virtual IActionResult OnGet()
        {
            Input = new VerifyCodeInputModel();

            return Page();
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            // Verify user login status
            var user = await SignInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                Alerts.Warning(L["TwoFactorAuthenticationInvaidUser"]);
                return Page();
            }
            // Two-factor login
            var result = await SignInManager.TwoFactorSignInAsync(Provider, Input.VerifyCode, RememberMe, Input.RememberBrowser);
            if (result.Succeeded)
            {
                if (await VerifyLinkTokenAsync())
                {
                    await HandleLinkUserLogin(user);
                }
                // Clear the dynamic claims cache.
                await IdentityDynamicClaimsPrincipalContributorCache.ClearAsync(user.Id, user.TenantId);

                await IdentitySecurityLogManager.SaveAsync(new IdentitySecurityLogContext()
                {
                    Identity = IdentitySecurityLogIdentityConsts.Identity,
                    Action = IdentitySecurityLogExtendActionConsts.LoginTwoFactorSucceeded,
                    UserName = user.UserName
                });

                return await RedirectSafelyAsync(ReturnUrl!, ReturnUrlHash);
            }

            await IdentitySecurityLogManager.SaveAsync(new IdentitySecurityLogContext()
            {
                Identity = IdentitySecurityLogIdentityConsts.Identity,
                Action = result.ToIdentitySecurityLogAction(),
                UserName = user.UserName
            });
            if (result.IsLockedOut)
            {
                Logger.LogWarning(7, "User account locked out.");
                Alerts.Warning(L["UserLockedOutMessage"]);
                return Page();
            }
            else
            {
                Alerts.Danger(L["TwoFactorAuthenticationInvaidUser"]);//TODO: More status code interpretations
                return Page();
            }
        }

        #region LinkUser
        protected async virtual Task HandleLinkUserLogin(IdentityUser user)
        {
            using (CurrentPrincipalAccessor.Change(await SignInManager.CreateUserPrincipalAsync(user)))
            {
                await IdentityLinkUserAppService.LinkAsync(new LinkUserInput
                {
                    UserId = LinkUserId!.Value,
                    TenantId = LinkTenantId,
                    Token = LinkToken!
                });

                await IdentitySecurityLogManager.SaveAsync(new IdentitySecurityLogContext()
                {
                    Identity = IdentitySecurityLogIdentityConsts.Identity,
                    UserName = user.UserName,
                    Action = "LinkUser",
                    ExtraProperties =
                    {
                        { "LinkTenantId",  LinkTenantId },
                        { "LinkUserId", LinkUserId }
                    }
                });

                using (CurrentTenant.Change(LinkTenantId))
                {
                    var targetUser = await UserManager.GetByIdAsync(LinkUserId.Value);
                    using (CurrentPrincipalAccessor.Change(await SignInManager.CreateUserPrincipalAsync(targetUser)))
                    {
                        await IdentitySecurityLogManager.SaveAsync(new IdentitySecurityLogContext()
                        {
                            Identity = IdentitySecurityLogIdentityConsts.Identity,
                            UserName = targetUser.UserName,
                            Action = "LinkUser",
                            ExtraProperties =
                            {
                                { "LinkTenantId",  LinkTenantId },
                                { "LinkUserId", LinkUserId }
                            }
                        });
                    }
                }
            }
        }

        protected virtual async Task<bool> VerifyLinkTokenAsync()
        {
            return !LinkToken.IsNullOrWhiteSpace() && LinkUserId != null
                && await IdentityLinkUserAppService.VerifyLinkTokenAsync(new VerifyLinkTokenInput
                {
                    UserId = LinkUserId.Value,
                    TenantId = LinkTenantId,
                    Token = LinkToken
                });
        }
        #endregion
    }

    public class VerifyCodeInputModel
    {
        /// <summary>
        /// Whether to remember the login status in the browser
        /// </summary>
        public bool RememberBrowser { get; set; }
        /// <summary>
        /// Verification code sent
        /// </summary>
        [Required]
        public string VerifyCode { get; set; } = default!;
    }
}
