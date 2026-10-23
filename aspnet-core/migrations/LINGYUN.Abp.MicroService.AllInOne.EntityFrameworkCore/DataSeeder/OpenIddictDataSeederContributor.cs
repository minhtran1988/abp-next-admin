using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using OpenIddict.Abstractions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.OpenIddict;
using Volo.Abp.OpenIddict.Applications;
using Volo.Abp.OpenIddict.Scopes;
using Volo.Abp.PermissionManagement;

namespace LINGYUN.Abp.MicroService.AllInOne.EntityFrameworkCore.DataSeeder;
public class OpenIddictDataSeederContributor : OpenIddictDataSeedContributorBase, IDataSeedContributor, ITransientDependency
{
    public ILogger<OpenIddictDataSeederContributor> Logger { protected get; set; }
    protected IPermissionDataSeeder PermissionDataSeeder { get; }
    public OpenIddictDataSeederContributor(
        IConfiguration configuration, 
        IOpenIddictApplicationRepository openIddictApplicationRepository, 
        IAbpApplicationManager applicationManager, 
        IOpenIddictScopeRepository openIddictScopeRepository, 
        IOpenIddictScopeManager scopeManager,
        IPermissionDataSeeder permissionDataSeeder) 
        : base(configuration, openIddictApplicationRepository, applicationManager, openIddictScopeRepository, scopeManager)
    {
        PermissionDataSeeder = permissionDataSeeder;

        Logger = NullLogger<OpenIddictDataSeederContributor>.Instance;
    }

    public async virtual Task SeedAsync(DataSeedContext context)
    {
        if (context.TenantId.HasValue)
        {
            return;
        }
        var scope = "lingyun-abp-application";

        Logger.LogInformation("Seeding the default scope...");
        await CreateDefaultScopeAsync();
        await CreateApiScopeAsync(scope, "all_in_one");

        Logger.LogInformation("Seeding the default applications...");
        await CreateApplicationAsync(scope);

        Logger.LogInformation("Seeding default applications completed.");
    }

    private async Task CreateDefaultScopeAsync()
    {
        // OpenID Connect core scope - used to identify this is an OIDC request
        await CreateScopesAsync(new OpenIddictScopeDescriptor
        {
            Name = OpenIddictConstants.Scopes.OpenId,
            DisplayName = "OpenID Connect", // Friendly display name
            DisplayNames =
            {
                [CultureInfo.GetCultureInfo("zh-Hans")] = "\u8eab\u4efd\u8ba4\u8bc1",
                [CultureInfo.GetCultureInfo("en")] = "OpenID Connect"
            },
            Description = "Using OpenID Connect protocol for authentication",
            Descriptions =
            {
                [CultureInfo.GetCultureInfo("zh-Hans")] = "\u5141\u8bb8\u5e94\u7528\u7a0b\u5e8f\u4f7f\u7528\u60a8\u7684\u8eab\u4efd\u4fe1\u606f\u8fdb\u884c\u767b\u5f55",
                [CultureInfo.GetCultureInfo("en")] = "Allow the application to authenticate you using OpenID Connect"
            }
        });

        // Profile scope - basic user information
        await CreateScopesAsync(new OpenIddictScopeDescriptor
        {
            Name = OpenIddictConstants.Scopes.Profile,
            DisplayName = "Profile",
            DisplayNames =
            {
                [CultureInfo.GetCultureInfo("zh-Hans")] = "\u4e2a\u4eba\u8d44\u6599",
                [CultureInfo.GetCultureInfo("en")] = "Profile"
            },
            Description = "Access your basic profile information",
            Descriptions =
            {
                [CultureInfo.GetCultureInfo("zh-Hans")] = "\u5141\u8bb8\u5e94\u7528\u7a0b\u5e8f\u8bbf\u95ee\u60a8\u7684\u59d3\u540d\u3001\u5934\u50cf\u7b49\u57fa\u672c\u4fe1\u606f",
                [CultureInfo.GetCultureInfo("en")] = "Allow the application to access your basic profile information like name and picture"
            }
        });

        // Email scope
        await CreateScopesAsync(new OpenIddictScopeDescriptor
        {
            Name = OpenIddictConstants.Scopes.Email,
            DisplayName = "email",
            DisplayNames =
            {
                [CultureInfo.GetCultureInfo("zh-Hans")] = "\u7535\u5b50\u90ae\u4ef6",
                [CultureInfo.GetCultureInfo("en")] = "Email"
            },
            Description = "Access your email address",
            Descriptions =
            {
                [CultureInfo.GetCultureInfo("zh-Hans")] = "\u5141\u8bb8\u5e94\u7528\u7a0b\u5e8f\u8bbf\u95ee\u60a8\u7684\u7535\u5b50\u90ae\u4ef6\u5730\u5740",
                [CultureInfo.GetCultureInfo("en")] = "Allow the application to access your email address"
            }
        });

        // Phone scope
        await CreateScopesAsync(new OpenIddictScopeDescriptor
        {
            Name = OpenIddictConstants.Scopes.Phone,
            DisplayName = "Phone number",
            DisplayNames =
            {
                [CultureInfo.GetCultureInfo("zh-Hans")] = "\u7535\u8bdd\u53f7\u7801",
                [CultureInfo.GetCultureInfo("en")] = "Phone"
            },
            Description = "Access your phone number",
            Descriptions =
            {
                [CultureInfo.GetCultureInfo("zh-Hans")] = "\u5141\u8bb8\u5e94\u7528\u7a0b\u5e8f\u8bbf\u95ee\u60a8\u7684\u7535\u8bdd\u53f7\u7801",
                [CultureInfo.GetCultureInfo("en")] = "Allow the application to access your phone number"
            }
        });

        // Address scope
        await CreateScopesAsync(new OpenIddictScopeDescriptor
        {
            Name = OpenIddictConstants.Scopes.Address,
            DisplayName = "Address information",
            DisplayNames =
            {
                [CultureInfo.GetCultureInfo("zh-Hans")] = "\u5730\u5740\u4fe1\u606f",
                [CultureInfo.GetCultureInfo("en")] = "Address"
            },
            Description = "Access your address information",
            Descriptions =
            {
                [CultureInfo.GetCultureInfo("zh-Hans")] = "\u5141\u8bb8\u5e94\u7528\u7a0b\u5e8f\u8bbf\u95ee\u60a8\u7684\u5730\u5740\u4fe1\u606f",
                [CultureInfo.GetCultureInfo("en")] = "Allow the application to access your address information"
            }
        });

        // Roles scope
        await CreateScopesAsync(new OpenIddictScopeDescriptor
        {
            Name = OpenIddictConstants.Scopes.Roles,
            DisplayName = "Role Information",
            DisplayNames =
            {
                [CultureInfo.GetCultureInfo("zh-Hans")] = "\u89d2\u8272\u4fe1\u606f",
                [CultureInfo.GetCultureInfo("en")] = "Roles"
            },
            Description = "Access your role information",
            Descriptions =
            {
                [CultureInfo.GetCultureInfo("zh-Hans")] = "\u5141\u8bb8\u5e94\u7528\u7a0b\u5e8f\u8bbf\u95ee\u60a8\u7684\u89d2\u8272\u548c\u6743\u9650\u4fe1\u606f",
                [CultureInfo.GetCultureInfo("en")] = "Allow the application to access your roles and permissions"
            }
        });

        // Offline Access scope - used to obtain refresh token
        await CreateScopesAsync(new OpenIddictScopeDescriptor
        {
            Name = OpenIddictConstants.Scopes.OfflineAccess,
            DisplayName = "Offline access",
            DisplayNames =
            {
                [CultureInfo.GetCultureInfo("zh-Hans")] = "\u79bb\u7ebf\u8bbf\u95ee",
                [CultureInfo.GetCultureInfo("en")] = "Offline Access"
            },
            Description = "Access your information when you are not logged in",
            Descriptions =
            {
                [CultureInfo.GetCultureInfo("zh-Hans")] = "\u5141\u8bb8\u5e94\u7528\u7a0b\u5e8f\u5728\u60a8\u672a\u767b\u5f55\u65f6\u8bbf\u95ee\u60a8\u7684\u4fe1\u606f",
                [CultureInfo.GetCultureInfo("en")] = "Allow the application to access your information while you are offline"
            }
        });
    }

    private async Task CreateApiScopeAsync(string scope, string apiResource)
    {
        await CreateScopesAsync(new OpenIddictScopeDescriptor
        {
            Name = scope,
            DisplayName = "Single Applications",
            DisplayNames =
            {
                [CultureInfo.GetCultureInfo("zh-Hans")] = "\u5355\u4f53\u5e94\u7528\u7a0b\u5e8f",
                [CultureInfo.GetCultureInfo("en")] = "Single Applications"
            },
            Description = "Interface authorization for single applications",
            Descriptions =
            {
                [CultureInfo.GetCultureInfo("zh-Hans")] = "\u5141\u8bb8\u5e94\u7528\u7a0b\u5e8f\u4f7f\u7528\u5355\u4f53\u5e94\u7528\u7a0b\u5e8f\u7684\u63a5\u53e3",
                [CultureInfo.GetCultureInfo("en")] = "Allow applications to use the interface of monolithic applications"
            },
            Resources =
            {
                apiResource,
                "http://localhost:30000",
            }
        });
    }

    private async Task CreateApplicationAsync(string scope)
    {
        var configurationSection = Configuration.GetSection("OpenIddict:Applications");
        var vueClientId = configurationSection["VueAdmin:ClientId"];
        if (!vueClientId.IsNullOrWhiteSpace())
        {
            Logger.LogInformation("Seeding application {vueClientId}...", vueClientId);

            var vueClientRootUrls = configurationSection.GetSection("VueAdmin:RootUrls").Get<List<string>>() ?? [];

            var vueClientRedirectUrls = new List<string>();
            var vueClientPostLogoutRedirectUrls = new List<string>();
            vueClientRootUrls.ForEach(url =>
            {
                vueClientRedirectUrls.Add(url.EnsureEndsWith('/'));
                vueClientRedirectUrls.Add(url.EnsureEndsWith('/') + "signin-callback");

                vueClientPostLogoutRedirectUrls.Add(url.EnsureEndsWith('/'));
                vueClientPostLogoutRedirectUrls.Add(url.EnsureEndsWith('/') + "signout-callback");
            });

            await CreateOrUpdateApplicationAsync(
                OpenIddictConstants.ApplicationTypes.Web,
                vueClientId,
                OpenIddictConstants.ClientTypes.Confidential,
                OpenIddictConstants.ConsentTypes.Explicit,
                "Abp Vue Admin Client",
                configurationSection["VueAdmin:ClientSecret"] ?? "1q2w3e*",
                [OpenIddictConstants.GrantTypes.AuthorizationCode,
                OpenIddictConstants.GrantTypes.Implicit,
                OpenIddictConstants.GrantTypes.Password,
                OpenIddictConstants.GrantTypes.RefreshToken,
                // TODO: Reference project?
                "link_user"],
                [OpenIddictConstants.Scopes.OpenId,
                OpenIddictConstants.Scopes.Email,
                OpenIddictConstants.Scopes.Roles,
                OpenIddictConstants.Scopes.Address,
                OpenIddictConstants.Scopes.Phone,
                OpenIddictConstants.Scopes.Profile,
                OpenIddictConstants.Scopes.OfflineAccess,
                 scope],
                vueClientRedirectUrls,
                vueClientPostLogoutRedirectUrls);

            var vueClientPermissions = new string[1]
            {
                "AbpIdentity.UserLookup"
            };
            await PermissionDataSeeder.SeedAsync(ClientPermissionValueProvider.ProviderName, vueClientId, vueClientPermissions);
        }

        var internalServiceClientId = configurationSection["InternalService:ClientId"];
        if (!internalServiceClientId.IsNullOrWhiteSpace())
        {
            Logger.LogInformation("Seeding application {internalServiceClientId}...", internalServiceClientId);

            await CreateOrUpdateApplicationAsync(
                OpenIddictConstants.ApplicationTypes.Native,
                internalServiceClientId,
                OpenIddictConstants.ClientTypes.Confidential,
                OpenIddictConstants.ConsentTypes.Explicit,
                "Abp Internal Service Client",
                configurationSection["InternalService:ClientSecret"] ?? "1q2w3e*",
                [OpenIddictConstants.GrantTypes.ClientCredentials],
                [OpenIddictConstants.ResponseTypes.Token, scope]);
        }

        var oauthClientId = configurationSection["VueOAuthClient:ClientId"];
        if (!oauthClientId.IsNullOrWhiteSpace())
        {
            Logger.LogInformation("Seeding application {oauthClientId}...", oauthClientId);

            var oauthClientRootUrls = configurationSection.GetSection("VueOAuthClient:RootUrls").Get<List<string>>() ?? [];

            var oauthClientRedirectUrls = new List<string>();
            var oauthClientPostLogoutRedirectUrls = new List<string>();
            oauthClientRootUrls.ForEach(url =>
            {
                oauthClientRedirectUrls.Add(url.EnsureEndsWith('/'));
                oauthClientRedirectUrls.Add(url.EnsureEndsWith('/') + "signin-callback");
                oauthClientRedirectUrls.Add(url.EnsureEndsWith('/') + "swagger/oauth2-redirect.html");

                oauthClientPostLogoutRedirectUrls.Add(url.EnsureEndsWith('/'));
                oauthClientPostLogoutRedirectUrls.Add(url.EnsureEndsWith('/') + "signout-callback");
            });

            await CreateOrUpdateApplicationAsync(
                OpenIddictConstants.ApplicationTypes.Web,
                oauthClientId,
                OpenIddictConstants.ClientTypes.Public,
                OpenIddictConstants.ConsentTypes.Implicit,
                "Abp OAuth Client",
                null,
                [OpenIddictConstants.GrantTypes.AuthorizationCode,
                 OpenIddictConstants.GrantTypes.RefreshToken,
                // TODO: Reference project?
                "link_user"],
                [OpenIddictConstants.Scopes.OpenId,
                OpenIddictConstants.Scopes.Email,
                OpenIddictConstants.Scopes.Roles,
                OpenIddictConstants.Scopes.Address,
                OpenIddictConstants.Scopes.Phone,
                OpenIddictConstants.Scopes.Profile,
                OpenIddictConstants.Scopes.OfflineAccess,
                 scope],
                oauthClientRedirectUrls,
                oauthClientPostLogoutRedirectUrls);

            var oauthClientPermissions = new string[1]
            {
                "AbpIdentity.UserLookup"
            };
            await PermissionDataSeeder.SeedAsync(ClientPermissionValueProvider.ProviderName, oauthClientId, oauthClientPermissions);
        }
    }
}
