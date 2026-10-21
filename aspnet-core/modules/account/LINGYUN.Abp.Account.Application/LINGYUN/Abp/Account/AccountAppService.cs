using LINGYUN.Abp.Account.Security;
using LINGYUN.Abp.Identity;
using LINGYUN.Abp.Identity.Settings;
using LINGYUN.Abp.WeChat;
using LINGYUN.Abp.WeChat.MiniProgram;
using LINGYUN.Abp.WeChat.OpenId;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;
using Volo.Abp.Clients;
using Volo.Abp.Identity;
using Volo.Abp.Settings;
using Volo.Abp.Validation;
using IIdentityUserRepository = LINGYUN.Abp.Identity.IIdentityUserRepository;

namespace LINGYUN.Abp.Account;

public class AccountAppService : AccountApplicationServiceBase, IAccountAppService
{
    protected IIdentityUserRepository UserRepository { get; }
    protected IAccountSmsSecurityCodeSender SecurityCodeSender { get; }
    protected IWeChatOpenIdFinder WeChatOpenIdFinder { get; }
    protected IdentitySecurityLogManager IdentitySecurityLogManager { get; }
    protected AbpWeChatMiniProgramOptionsFactory MiniProgramOptionsFactory { get; }
    protected IDistributedCache<SecurityTokenCacheItem> SecurityTokenCache { get; }
    protected IAccountEmailSecurityCodeSender EmailSecurityCodeSender { get; }

    public AccountAppService(
        IWeChatOpenIdFinder weChatOpenIdFinder,
        IIdentityUserRepository userRepository,
        IAccountSmsSecurityCodeSender securityCodeSender,
        IDistributedCache<SecurityTokenCacheItem> securityTokenCache,
        AbpWeChatMiniProgramOptionsFactory miniProgramOptionsFactory,
        IdentitySecurityLogManager identitySecurityLogManager,
        IAccountEmailSecurityCodeSender emailSecurityCodeSender)
    {
        UserRepository = userRepository;
        WeChatOpenIdFinder = weChatOpenIdFinder;
        SecurityCodeSender = securityCodeSender;
        SecurityTokenCache = securityTokenCache;
        MiniProgramOptionsFactory = miniProgramOptionsFactory;
        IdentitySecurityLogManager = identitySecurityLogManager;
        EmailSecurityCodeSender = emailSecurityCodeSender;
    }

    public async virtual Task RegisterAsync(WeChatRegisterDto input)
    {
        ThowIfInvalidEmailAddress(input.EmailAddress);

        await CheckSelfRegistrationAsync();
        await IdentityOptions.SetAsync();

        var options = await MiniProgramOptionsFactory.CreateAsync();

        var wehchatOpenId = await WeChatOpenIdFinder.FindAsync(input.Code, options.AppId, options.AppSecret)
            ?? throw new UserFriendlyException(L["InvalidWeChatCode"]);

        var user = await UserManager.FindByLoginAsync(AbpWeChatMiniProgramConsts.ProviderName, wehchatOpenId.OpenId);
        if (user != null)
        {
            // WeChat ID registered exception should be thrown instead of returning registered user data directly, otherwise user information will be leaked
            throw new UserFriendlyException(L["DuplicateWeChat"]);
        }
        var userName = input.UserName;
        if (userName.IsNullOrWhiteSpace())
        {
            userName = "wxid-" + wehchatOpenId.OpenId.ToMd5().ToLower();
        }

        var userEmail = input.EmailAddress;//If the email address is not verified, feel free to write one
        if (userEmail.IsNullOrWhiteSpace())
        {
            userEmail = $"{userName}@{CurrentTenant.Name ?? "default"}.io";
        }

        user = new IdentityUser(GuidGenerator.Create(), userName, userEmail, CurrentTenant.Id);
        (await UserManager.CreateAsync(user)).CheckErrors();
        if (!input.Password.IsNullOrWhiteSpace())
        {
            (await UserManager.AddPasswordAsync(user, input.Password)).CheckErrors();
        }

        (await UserManager.AddDefaultRolesAsync(user)).CheckErrors();

        var userLogin = new UserLoginInfo(AbpWeChatMiniProgramConsts.ProviderName, wehchatOpenId.OpenId, AbpWeChatGlobalConsts.DisplayName);
        (await UserManager.AddLoginAsync(user, userLogin)).CheckErrors();

        await IdentitySecurityLogManager.SaveAsync(
            new IdentitySecurityLogContext
            {
                Action = "WeChatRegister",
                ClientId = await FindClientIdAsync(),
                Identity = "Account",
                UserName = user.UserName
            });
    }

    public async virtual Task SendPhoneRegisterCodeAsync(SendPhoneRegisterCodeDto input)
    {
        await CheckSelfRegistrationAsync();
        await CheckNewUserPhoneNumberNotBeUsedAsync(input.PhoneNumber);

        var securityTokenCacheKey = SecurityTokenCacheItem.CalculateSmsCacheKey(
            input.PhoneNumber,
            UserTwoFactorTokenProviderConsts.PhoneNumberRegisterPurpose);
        var securityTokenCacheItem = await SecurityTokenCache.GetAsync(securityTokenCacheKey);
        var interval = await SettingProvider.GetAsync(IdentitySettingNames.User.SmsRepetInterval, 1);

        if (securityTokenCacheItem != null)
        {
            throw new UserFriendlyException(L["SendRepeatSmsVerifyCode", interval]);
        }

        var template = await SettingProvider.GetOrNullAsync(IdentitySettingNames.User.SmsNewUserRegister);

        var tempNewUser = new IdentityUser(
            GuidGenerator.Create(),
            "TempUser",
            "TempUser@abp.io",
            CurrentTenant.Id);
        tempNewUser.SetPhoneNumberRegisterUser();

        await UserStore.SetSecurityStampAsync(tempNewUser, Guid.NewGuid().ToString("n"));

        var code = await UserManager.GenerateUserTokenAsync(
            tempNewUser,
            UserTwoFactorTokenProviderConsts.PhoneNumberRegisterTokenProvider,
            UserTwoFactorTokenProviderConsts.PhoneNumberRegisterPurpose);

        securityTokenCacheItem = new SecurityTokenCacheItem(code, tempNewUser.Id, await UserManager.GetSecurityStampAsync(tempNewUser));

        await SecurityCodeSender.SendAsync(
            input.PhoneNumber, securityTokenCacheItem.Token, template!);

        await SecurityTokenCache
            .SetAsync(securityTokenCacheKey, securityTokenCacheItem,
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(interval)
                });
    }

    public async virtual Task RegisterAsync(PhoneRegisterDto input)
    {
        await CheckSelfRegistrationAsync();
        await IdentityOptions.SetAsync();
        ThowIfInvalidEmailAddress(input.EmailAddress);
        await CheckNewUserPhoneNumberNotBeUsedAsync(input.PhoneNumber);

        var securityTokenCacheKey = SecurityTokenCacheItem.CalculateSmsCacheKey(
            input.PhoneNumber,
            UserTwoFactorTokenProviderConsts.PhoneNumberRegisterPurpose);
        var securityTokenCacheItem = await SecurityTokenCache.GetAsync(securityTokenCacheKey);
        if (securityTokenCacheItem == null)
        {
            // Verification code expires
            throw new UserFriendlyException(L["InvalidVerifyCode"]);
        }

        // Is the verification code valid?
        if (input.Code.Equals(securityTokenCacheItem.Token))
        {
            var tempNewUser = new IdentityUser(
                securityTokenCacheItem.UserId,
                "TempUser",
                "TempUser@abp.io",
                CurrentTenant.Id);
            tempNewUser.SetPhoneNumberRegisterUser();
            await UserStore.SetSecurityStampAsync(tempNewUser, securityTokenCacheItem.SecurityToken);

            if (await UserManager.VerifyUserTokenAsync(
                tempNewUser,
                UserTwoFactorTokenProviderConsts.PhoneNumberRegisterTokenProvider,
                UserTwoFactorTokenProviderConsts.PhoneNumberRegisterPurpose,
                input.Code))
            {
                var userEmail = input.EmailAddress ?? $"{input.PhoneNumber}@{CurrentTenant.Name ?? "default"}.io";//If the email address is not verified, feel free to write one
                var userName = input.UserName ?? input.PhoneNumber;
                var user = new IdentityUser(GuidGenerator.Create(), userName, userEmail, CurrentTenant.Id)
                {
                    Name = input.Name ?? input.PhoneNumber
                };
                (await UserManager.CreateAsync(user)).CheckErrors();

                (await UserManager.SetPhoneNumberAsync(user, input.PhoneNumber)).CheckErrors();
                if (!input.Password.IsNullOrWhiteSpace())
                {
                    (await UserManager.AddPasswordAsync(user, input.Password)).CheckErrors();
                }

                await UserStore.SetPhoneNumberConfirmedAsync(user, true);

                (await UserManager.AddDefaultRolesAsync(user)).CheckErrors();

                await SecurityTokenCache.RemoveAsync(securityTokenCacheKey);

                await IdentitySecurityLogManager.SaveAsync(
                    new IdentitySecurityLogContext
                    {
                        Action = "PhoneNumberRegister",
                        ClientId = await FindClientIdAsync(),
                        Identity = "Account",
                        UserName = user.UserName
                    });

                return;
            }
        }
        // Verification code is invalid
        throw new UserFriendlyException(L["InvalidVerifyCode"]);
    }

    public async virtual Task SendPhoneResetPasswordCodeAsync(SendPhoneResetPasswordCodeDto input)
    {
        // Pass isConfirmed The user must have confirmed the mobile phone number
        var user = await GetUserByPhoneNumberAsync(input.PhoneNumber, isConfirmed: true);
        // External authentication users are not allowed to change passwords
        if (user.IsExternal)
        {
            throw new BusinessException(code: Volo.Abp.Identity.IdentityErrorCodes.ExternalUserPasswordChange);
        }

        var securityTokenCacheKey = SecurityTokenCacheItem.CalculateSmsCacheKey(
            input.PhoneNumber,
            UserTwoFactorTokenProviderConsts.PhoneResetPasswordPurpose);
        var securityTokenCacheItem = await SecurityTokenCache.GetAsync(securityTokenCacheKey);
        var interval = await SettingProvider.GetAsync(IdentitySettingNames.User.SmsRepetInterval, 1);
        // If the cache can be queried, it is sent repeatedly.
        if (securityTokenCacheItem != null)
        {
            throw new UserFriendlyException(L["SendRepeatSmsVerifyCode", interval]);
        }

        var template = await SettingProvider.GetOrNullAsync(IdentitySettingNames.User.SmsResetPassword);
        // Generate secondary authentication code
        var code = await UserManager.GenerateUserTokenAsync(
            user,
            TokenOptions.DefaultPhoneProvider,
            UserTwoFactorTokenProviderConsts.PhoneResetPasswordPurpose);
        // Send SMS verification code
        await SecurityCodeSender.SendAsync(input.PhoneNumber, code, template!);
        // Cache the record of this mobile phone number to prevent duplication
        securityTokenCacheItem = new SecurityTokenCacheItem(code, user.Id, user.SecurityStamp);
        await SecurityTokenCache
            .SetAsync(securityTokenCacheKey, securityTokenCacheItem,
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(interval)
                });
    }

    public async virtual Task ResetPasswordAsync(PhoneResetPasswordDto input)
    {
        var securityTokenCacheKey = SecurityTokenCacheItem.CalculateSmsCacheKey(
            input.PhoneNumber,
            UserTwoFactorTokenProviderConsts.PhoneResetPasswordPurpose);
        var securityTokenCacheItem = await SecurityTokenCache.GetAsync(securityTokenCacheKey);
        if (securityTokenCacheItem == null)
        {
            throw new UserFriendlyException(L["InvalidVerifyCode"]);
        }
        await IdentityOptions.SetAsync();
        // Pass isConfirmed The user must have confirmed the mobile phone number
        var user = await GetUserByPhoneNumberAsync(input.PhoneNumber, isConfirmed: true);
        // External authentication users are not allowed to change passwords
        if (user.IsExternal)
        {
            throw new BusinessException(code: Volo.Abp.Identity.IdentityErrorCodes.ExternalUserPasswordChange);
        }
        // Verify the secondary authentication code
        if (!await UserManager.VerifyUserTokenAsync(
            user,
            TokenOptions.DefaultPhoneProvider,
            UserTwoFactorTokenProviderConsts.PhoneResetPasswordPurpose,
            input.Code))
        {
            // Verification code is invalid
            throw new UserFriendlyException(L["InvalidVerifyCode"]);
        }
        // Generate the real reset password Token
        var resetPwdToken = await UserManager.GeneratePasswordResetTokenAsync(user);
        // reset password
        (await UserManager.ResetPasswordAsync(user, resetPwdToken, input.NewPassword)).CheckErrors();

        // Remove cache item
        await SecurityTokenCache.RemoveAsync(securityTokenCacheKey);

        await IdentitySecurityLogManager.SaveAsync(
            new IdentitySecurityLogContext
            {
                Action = "ResetPassword",
                ClientId = await FindClientIdAsync(),
                Identity = "Account",
                UserName = user.UserName
            });
    }

    public async virtual Task SendPhoneSigninCodeAsync(SendPhoneSigninCodeDto input)
    {
        var securityTokenCacheKey = SecurityTokenCacheItem.CalculateSmsCacheKey(input.PhoneNumber, "PhoneNumberSignin");
        var securityTokenCacheItem = await SecurityTokenCache.GetAsync(securityTokenCacheKey);
        var interval = await SettingProvider.GetAsync(IdentitySettingNames.User.SmsRepetInterval, 1);
        if (securityTokenCacheItem != null)
        {
            throw new UserFriendlyException(L["SendRepeatSmsVerifyCode", interval]);
        }
        // Only verified users passing isConfirmed are allowed to log in via mobile phone
        var user = await GetUserByPhoneNumberAsync(input.PhoneNumber, isConfirmed: true);
        // To log in and verify your mobile phone number, you need to use the secondary authentication that comes with it.
        var code = await UserManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultPhoneProvider);
        var template = await SettingProvider.GetOrNullAsync(IdentitySettingNames.User.SmsUserSignin);

        // Send login verification code SMS
        await SecurityCodeSender.SendAsync(input.PhoneNumber, code, template!);
        // Cache login verification code status to prevent repeated sending of the same mobile phone number
        securityTokenCacheItem = new SecurityTokenCacheItem(code, user.Id, user.SecurityStamp);
        await SecurityTokenCache
            .SetAsync(securityTokenCacheKey, securityTokenCacheItem,
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(interval)
                });
    }

    public async virtual Task SendEmailRegisterCodeAsync(SendEmailRegisterCodeDto input)
    {
        await CheckSelfRegistrationAsync();
        await CheckNewUserEmailNotBeUsedAsync(input.EmailAddress);

        var interval = await SettingProvider.GetAsync(IdentitySettingNames.User.EmailRegisterRepetInterval, 1);
        var securityTokenCacheKey = SecurityTokenCacheItem.CalculateEmailCacheKey(
            input.EmailAddress,
            UserTwoFactorTokenProviderConsts.EmailAddressRegisterPurpose);
        var securityTokenCacheItem = await SecurityTokenCache.GetAsync(securityTokenCacheKey);
        if (securityTokenCacheItem != null)
        {
            throw new UserFriendlyException(L["SendRepeatEmailVerifyCode", interval]);
        }

        var tempNewUser = new IdentityUser(
            GuidGenerator.Create(),
            input.EmailAddress,
            input.EmailAddress,
            CurrentTenant.Id);
        tempNewUser.SetEmailAddressRegisterUser();

        await UserStore.SetSecurityStampAsync(tempNewUser, Guid.NewGuid().ToString("n"));

        var code = await UserManager.GenerateUserTokenAsync(
            tempNewUser,
            UserTwoFactorTokenProviderConsts.EmailAddressRegisterTokenProvider,
            UserTwoFactorTokenProviderConsts.EmailAddressRegisterPurpose);

        securityTokenCacheItem = new SecurityTokenCacheItem(code, tempNewUser.Id, await UserManager.GetSecurityStampAsync(tempNewUser));

        var sender = LazyServiceProvider.LazyGetRequiredService<IAccountEmailSecurityCodeSender>();

        await sender.SendLoginCodeAsync(code, input.EmailAddress, input.EmailAddress);

        await SecurityTokenCache
            .SetAsync(securityTokenCacheKey, securityTokenCacheItem,
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(interval)
                });
    }

    public async virtual Task<bool> VerifyEmailRegisterCodeAsync(VerifyEmailRegisterCodeInput input)
    {
        var securityTokenCacheKey = SecurityTokenCacheItem.CalculateEmailCacheKey(
            input.EmailAddress,
            UserTwoFactorTokenProviderConsts.EmailAddressRegisterPurpose);
        var securityTokenCacheItem = await SecurityTokenCache.GetAsync(securityTokenCacheKey);
        if (securityTokenCacheItem == null)
        {
            return false;
        }

        // Is the verification code valid?
        if (input.VerifyCode.Equals(securityTokenCacheItem.Token))
        {
            var tempNewUser = new IdentityUser(
                securityTokenCacheItem.UserId,
                input.EmailAddress,
                input.EmailAddress,
                CurrentTenant.Id);
            tempNewUser.SetPhoneNumberRegisterUser();
            await UserStore.SetSecurityStampAsync(tempNewUser, securityTokenCacheItem.SecurityToken);

            if (await UserManager.VerifyUserTokenAsync(
                tempNewUser,
                UserTwoFactorTokenProviderConsts.EmailAddressRegisterTokenProvider,
                UserTwoFactorTokenProviderConsts.EmailAddressRegisterPurpose,
                input.VerifyCode))
            {
                await SecurityTokenCache.RemoveAsync(securityTokenCacheKey);

                return true;
            }
        }
        return false;
    }

    public async virtual Task SendEmailSigninCodeAsync(SendEmailSigninCodeDto input)
    {
        var sender = LazyServiceProvider.LazyGetRequiredService<IAccountEmailSecurityCodeSender>();

        var user = await UserManager.FindByEmailAsync(input.EmailAddress) 
            ?? throw new UserFriendlyException(L["UserNotRegisterd"]);

        if (!user.EmailConfirmed)
        {
            throw new UserFriendlyException(L["UserEmailNotConfirmed"]);
        }

        // The login verification email security code requires the use of the secondary authentication
        var code = await UserManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultEmailProvider);

        await sender.SendLoginCodeAsync(code, user.UserName, user.Email);
    }

    public async virtual Task SendEmailConfirmLinkAsync(SendUserEmailConfirmCodeDto input)
    {
        var user = await UserManager.GetByIdAsync(input.UserId);

        if (user.EmailConfirmed)
        {
            throw new BusinessException(Identity.IdentityErrorCodes.DuplicateConfirmEmailAddress);
        }

        var token = await UserManager.GenerateEmailConfirmationTokenAsync(user);
        var confirmToken = WebUtility.UrlEncode(token);

        await EmailSecurityCodeSender.SendConfirmLinkAsync(
            user.Id,
            user.Email,
            confirmToken,
            input.AppName,
            input.ReturnUrl,
            input.ReturnUrlHash,
            user.TenantId);
    }

    public async virtual Task ConfirmEmailAsync(ConfirmUserEmailInput input)
    {
        await IdentityOptions.SetAsync();

        var user = await UserManager.GetByIdAsync(input.UserId);

        var confirmToken = HttpUtility.UrlDecode(input.ConfirmToken); ;
        (await UserManager.ConfirmEmailAsync(user, confirmToken)).CheckErrors();

        await IdentitySecurityLogManager.SaveAsync(new IdentitySecurityLogContext
        {
            Identity = IdentitySecurityLogIdentityConsts.Identity,
            Action = IdentitySecurityLogActionConsts.ChangeEmail
        });
    }

    public async virtual Task<ListResultDto<NameValue>> GetTwoFactorProvidersAsync(GetTwoFactorProvidersInput input)
    {
        var user = await UserManager.GetByIdAsync(input.UserId);

        var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(user);
        return new ListResultDto<NameValue>(
            userFactors.Select(key => new NameValue(L[$"TwoFactor:{key}"].Value, key)).ToList());
    }

    protected async virtual Task<IdentityUser> GetUserByPhoneNumberAsync(string phoneNumber, bool isConfirmed = true)
    {
        var user = await UserRepository.FindByPhoneNumberAsync(phoneNumber, isConfirmed, true);
        if (user == null)
        {
            throw new UserFriendlyException(L["PhoneNumberNotRegisterd"]);
        }
        return user;
    }

    /// <summary>
    /// Check whether user registration is allowed
    /// </summary>
    /// <returns></returns>
    protected async virtual Task CheckSelfRegistrationAsync()
    {
        if (!await SettingProvider.IsTrueAsync(Volo.Abp.Account.Settings.AccountSettingNames.IsSelfRegistrationEnabled))
        {
            throw new UserFriendlyException(L["SelfRegistrationDisabledMessage"]);
        }
    }

    protected async virtual Task CheckNewUserPhoneNumberNotBeUsedAsync(string phoneNumber)
    {
        if (await UserRepository.IsPhoneNumberUedAsync(phoneNumber))
        {
            throw new UserFriendlyException(L["DuplicatePhoneNumber"]);
        }
    }

    protected async virtual Task CheckNewUserEmailNotBeUsedAsync(string emailAddress)
    {
        if (await UserRepository.FindByNormalizedEmailAsync(emailAddress, includeDetails: false) != null)
        {
            throw new UserFriendlyException(L["DuplicateEmailAddress"]);
        }
    }

    protected virtual Task<string?> FindClientIdAsync()
    {
        var client = LazyServiceProvider.LazyGetRequiredService<ICurrentClient>();

        return Task.FromResult(client.Id);
    }

    private void ThowIfInvalidEmailAddress(string? inputEmail)
    {
        if (!inputEmail.IsNullOrWhiteSpace() &&
            !ValidationHelper.IsValidEmailAddress(inputEmail))
        {
            throw new AbpValidationException(
                new ValidationResult[]
                {
                    new ValidationResult(L["The {0} field is not a valid e-mail address.", L["DisplayName:EmailAddress"]], new string[]{ "EmailAddress" })
                });
        }
    }
}
