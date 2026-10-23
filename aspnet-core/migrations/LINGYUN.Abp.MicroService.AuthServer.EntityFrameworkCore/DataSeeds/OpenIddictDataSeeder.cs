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

namespace LINGYUN.Abp.MicroService.AuthServer.EntityFrameworkCore.DataSeeds;

public class OpenIddictDataSeeder : OpenIddictDataSeedContributorBase, ITransientDependency
{
    public ILogger<OpenIddictDataSeeder> Logger { protected get; set; }
    protected IPermissionDataSeeder PermissionDataSeeder { get; }
    public OpenIddictDataSeeder(
        IConfiguration configuration,
        IOpenIddictApplicationRepository openIddictApplicationRepository,
        IAbpApplicationManager applicationManager,
        IOpenIddictScopeRepository openIddictScopeRepository,
        IOpenIddictScopeManager scopeManager,
        IPermissionDataSeeder permissionDataSeeder)
        : base(configuration, openIddictApplicationRepository, applicationManager, openIddictScopeRepository, scopeManager)
    {
        PermissionDataSeeder = permissionDataSeeder;

        Logger = NullLogger<OpenIddictDataSeeder>.Instance;
    }

    public async virtual Task SeedAsync(DataSeedContext context)
    {
        if (context.TenantId.HasValue)
        {
            return;
        }
        var applicationScope = "lingyun-abp-application";

        Logger.LogInformation("Seeding the default scope...");
        await CreateDefaultScopeAsync();
        await CreateApiScopesAsync(applicationScope);

        Logger.LogInformation("Seeding the default applications...");
        await CreateApplicationAsync(applicationScope);

        Logger.LogInformation("Seeding default applications completed.");
    }

    private async Task CreateDefaultScopeAsync()
    {
        // OpenId Connect
        await CreateServiceScopeAsync(
            OpenIddictConstants.Scopes.OpenId,
            "OpenId Connect",
            "\u8eab\u4efd\u8ba4\u8bc1",
            "OpenId Connect",
            "OpenId Connect protocol for authentication",
            "\u5141\u8bb8\u5e94\u7528\u7a0b\u5e8f\u4f7f\u7528\u60a8\u7684\u8eab\u4efd\u4fe1\u606f\u8fdb\u884c\u767b\u5f55",
            "Allow the application to authenticate you using OpenID Connect");

        // Profile
        await CreateServiceScopeAsync(
            OpenIddictConstants.Scopes.Profile,
            "Personal Information",
            "\u4e2a\u4eba\u8d44\u6599",
            "Profile",
            "Access your basic profile information",
            "\u5141\u8bb8\u5e94\u7528\u7a0b\u5e8f\u8bbf\u95ee\u60a8\u7684\u59d3\u540d\u3001\u5934\u50cf\u7b49\u57fa\u672c\u4fe1\u606f",
            "Allow the application to access your basic profile information like name and picture");

        // Email
        await CreateServiceScopeAsync(
            OpenIddictConstants.Scopes.Email,
            "email",
            "\u7535\u5b50\u90ae\u4ef6",
            "Email",
            "Access your email address",
            "\u5141\u8bb8\u5e94\u7528\u7a0b\u5e8f\u8bbf\u95ee\u60a8\u7684\u7535\u5b50\u90ae\u4ef6\u5730\u5740",
            "Allow the application to access your email address");

        // Phone
        await CreateServiceScopeAsync(
            OpenIddictConstants.Scopes.Phone,
            "phone number",
            "\u7535\u8bdd\u53f7\u7801",
            "Phone",
            "Access your phone number",
            "\u5141\u8bb8\u5e94\u7528\u7a0b\u5e8f\u8bbf\u95ee\u60a8\u7684\u7535\u8bdd\u53f7\u7801",
            "Allow the application to access your phone number");

        // Address
        await CreateServiceScopeAsync(
            OpenIddictConstants.Scopes.Address,
            "address information",
            "\u5730\u5740\u4fe1\u606f",
            "Address",
            "Access your address information",
            "\u5141\u8bb8\u5e94\u7528\u7a0b\u5e8f\u8bbf\u95ee\u60a8\u7684\u5730\u5740\u4fe1\u606f",
            "Allow the application to access your address information");

        // Roles
        await CreateServiceScopeAsync(
            OpenIddictConstants.Scopes.Roles,
            "role information",
            "\u89d2\u8272\u4fe1\u606f",
            "Roles",
            "Access your role information",
            "\u5141\u8bb8\u5e94\u7528\u7a0b\u5e8f\u8bbf\u95ee\u60a8\u7684\u89d2\u8272\u548c\u6743\u9650\u4fe1\u606f",
            "Allow the application to access your roles and permissions");

        // OfflineAccess
        await CreateServiceScopeAsync(
            OpenIddictConstants.Scopes.OfflineAccess,
            "offline access",
            "\u79bb\u7ebf\u8bbf\u95ee",
            "Offline access",
            "Accessing your information when you are not logged in",
            "\u5141\u8bb8\u5e94\u7528\u7a0b\u5e8f\u5728\u60a8\u672a\u767b\u5f55\u65f6\u8bbf\u95ee\u60a8\u7684\u4fe1\u606f",
            "Allow the application to access your information while you are offline");
    }

    private async Task CreateApiScopesAsync(string scope)
    {
        // Front-end summary authorization scope
        await CreateServiceScopeAsync(
            scope,
            "Microservice access authorization",
            "\u5fae\u670d\u52a1\u8bbf\u95ee\u6388\u6743",
            "MicroService applications access",
            "Interface authorization suitable for microservice systems",
            "\u5141\u8bb8\u5e94\u7528\u7a0b\u5e8f\u4f7f\u7528\u5404\u5fae\u670d\u52a1\u6a21\u5757\u7684\u63a5\u53e3",
            "Allow the application to use the interfaces of each microservice module",
            [
                "api-gateway",
                "auth-server",
                "admin-service",
                "ai-service",
                "identity-service",
                "localization-service",
                "message-service",
                "platform-service",
                "task-service",
                "webhook-service",
                "wechat-service",
                "workflow-service"
             ]);
        // ApiGateway Swagger
        await CreateServiceScopeAsync(
            "api-gateway",
            "Api Gateway",
            "\u5e94\u7528\u7a0b\u5e8f\u63a5\u53e3\u7f51\u5173",
            "Api Gateway",
            "Applies to API Gateway Swagger authorization",
            "\u9002\u7528\u4e8e\u5e94\u7528\u7a0b\u5e8f\u63a5\u53e3\u7f51\u5173Swagger\u6388\u6743",
            "Applicable to application Programming interface gateway Swagger authorization",
            ["api-gateway"]);
        // Admin Service
        await CreateServiceScopeAsync(
            "admin-service",
            "Admin Service",
            "\u540e\u53f0\u7ba1\u7406\u670d\u52a1",
            "Admin Service",
            "Applicable to backend management service Swagger authorization",
            "\u9002\u7528\u4e8e\u540e\u53f0\u7ba1\u7406\u670d\u52a1Swagger\u6388\u6743",
            "Applicable to the back-end management service Swagger authorization",
            ["admin-service"]);
        // Auth Server
        await CreateServiceScopeAsync(
            "auth-server",
            "Auth Server",
            "\u8eab\u4efd\u8ba4\u8bc1\u670d\u52a1\u5668",
            "Auth Server",
            "Applicable to identity authentication server Swagger authorization",
            "\u9002\u7528\u4e8e\u8eab\u4efd\u8ba4\u8bc1\u670d\u52a1\u5668Swagger\u6388\u6743",
            "Applicable to the auth server Swagger authorization",
            ["auth-server"]);
        // Identity Service
        await CreateServiceScopeAsync(
            "identity-service",
            "Identity Service",
            "\u8eab\u4efd\u8ba4\u8bc1\u670d\u52a1",
            "Identity Service",
            "Applicable to identity authentication service Swagger authorization",
            "\u9002\u7528\u4e8e\u8eab\u4efd\u8ba4\u8bc1\u670d\u52a1Swagger\u6388\u6743",
            "Applicable to the identity service Swagger authorization",
            ["identity-service"]);
        // Localization Service
        await CreateServiceScopeAsync(
            "localization-service",
            "Localization Service",
            "\u672c\u5730\u5316\u7ba1\u7406\u670d\u52a1",
            "Localization Service",
            "Applicable to localized management service Swagger authorization",
            "\u9002\u7528\u4e8e\u672c\u5730\u5316\u7ba1\u7406\u670d\u52a1Swagger\u6388\u6743",
            "Applicable to the Localization service Swagger authorization",
            ["localization-service"]);
        // Message Service
        await CreateServiceScopeAsync(
            "message-service",
            "Message Service",
            "\u6d88\u606f\u7ba1\u7406\u670d\u52a1",
            "Message Service",
            "Applicable to message management service Swagger authorization",
            "\u9002\u7528\u4e8e\u6d88\u606f\u7ba1\u7406\u670d\u52a1Swagger\u6388\u6743",
            "Applicable to the Message service Swagger authorization",
            ["message-service"]);
        // Platform Service
        await CreateServiceScopeAsync(
            "platform-service",
            "Platform Service",
            "\u5e73\u53f0\u7ba1\u7406\u670d\u52a1",
            "Platform Service",
            "Applicable to platform management service Swagger authorization",
            "\u9002\u7528\u4e8e\u5e73\u53f0\u7ba1\u7406\u670d\u52a1Swagger\u6388\u6743",
            "Applicable to the Platform service Swagger authorization",
            ["platform-service"]);
        // Task Service
        await CreateServiceScopeAsync(
            "task-service",
            "Task Service",
            "\u4efb\u52a1\u7ba1\u7406\u670d\u52a1",
            "Task Service",
            "Applicable to task management service Swagger authorization",
            "\u9002\u7528\u4e8e\u4efb\u52a1\u7ba1\u7406\u670d\u52a1Swagger\u6388\u6743",
            "Applicable to the Task service Swagger authorization",
            ["task-service"]);
        // Webhook Service
        await CreateServiceScopeAsync(
            "webhook-service",
            "Webhook Service",
            "Webhook\u7ba1\u7406\u670d\u52a1",
            "Webhook Service",
            "Applicable to Webhook management service Swagger authorization",
            "\u9002\u7528\u4e8eWebhook\u7ba1\u7406\u670d\u52a1Swagger\u6388\u6743",
            "Applicable to the Webhook service Swagger authorization",
            ["webhook-service"]);
        // Wechat Service
        await CreateServiceScopeAsync(
            "wechat-service",
            "Wechat Service",
            "\u5fae\u4fe1\u7ba1\u7406\u670d\u52a1",
            "Wechat Service",
            "Applicable to WeChat management service Swagger authorization",
            "\u9002\u7528\u4e8e\u5fae\u4fe1\u7ba1\u7406\u670d\u52a1Swagger\u6388\u6743",
            "Applicable to the Wechat service Swagger authorization",
            ["wechat-service"]);
        // Workflow Service
        await CreateServiceScopeAsync(
            "workflow-service",
            "Workflow Service",
            "\u5de5\u4f5c\u6d41\u7ba1\u7406\u670d\u52a1",
            "Workflow Service",
            "Applicable to workflow management service Swagger authorization",
            "\u9002\u7528\u4e8e\u5de5\u4f5c\u6d41\u7ba1\u7406\u670d\u52a1Swagger\u6388\u6743",
            "Applicable to the Workflow service Swagger authorization",
            ["workflow-service"]);
        // AI Service
        await CreateServiceScopeAsync(
            "ai-service",
            "Artificial Intelligence Service",
            "AI\u7ba1\u7406\u670d\u52a1",
            "Artificial Intelligence Service",
            "Applicable to AI management service Swagger authorization",
            "\u9002\u7528\u4e8eAI\u7ba1\u7406\u670d\u52a1Swagger\u6388\u6743",
            "Applicable to the Artificial Intelligence service Swagger authorization",
            ["ai-service"]);
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
                OpenIddictConstants.GrantTypes.Password,
                OpenIddictConstants.GrantTypes.RefreshToken,
                // TODO: Reference project?
                "link_user",
                "impersonation"],
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
                OpenIddictConstants.ApplicationTypes.Web,
                internalServiceClientId,
                OpenIddictConstants.ClientTypes.Confidential,
                OpenIddictConstants.ConsentTypes.Implicit,
                "Abp Internal Service Client",
                configurationSection["InternalService:ClientSecret"] ?? "1q2w3e*",
                [OpenIddictConstants.GrantTypes.ClientCredentials],
                [scope]);
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
                "link_user",
                "impersonation"],
                [OpenIddictConstants.Scopes.OpenId,
                OpenIddictConstants.Scopes.Email,
                OpenIddictConstants.Scopes.Roles,
                OpenIddictConstants.Scopes.Address,
                OpenIddictConstants.Scopes.Phone,
                OpenIddictConstants.Scopes.Profile,
                OpenIddictConstants.Scopes.OfflineAccess,
                 scope,
                 "api-gateway",
                "auth-server",
                "admin-service",
                "ai-service",
                "identity-service",
                "localization-service",
                "message-service",
                "platform-service",
                "task-service",
                "webhook-service",
                "wechat-service",
                "workflow-service"],
                oauthClientRedirectUrls,
                oauthClientPostLogoutRedirectUrls);

            var oauthClientPermissions = new string[1]
            {
                "AbpIdentity.UserLookup"
            };
            await PermissionDataSeeder.SeedAsync(ClientPermissionValueProvider.ProviderName, oauthClientId, oauthClientPermissions);
        }
    }

    private async Task CreateServiceScopeAsync(
        string name,
        string displayNameDefault,
        string displayNameZh,
        string displayNameEn,
        string descriptionDefault,
        string descriptionZh,
        string descriptionEn,
        List<string>? resources = null)
    {
        var descriptor = new OpenIddictScopeDescriptor
        {
            Name = name,
            DisplayName = displayNameDefault,
            DisplayNames =
            {
                [CultureInfo.GetCultureInfo("zh-Hans")] = displayNameZh,
                [CultureInfo.GetCultureInfo("en")] = displayNameEn
            },
            Description = descriptionDefault,
            Descriptions =
            {
                [CultureInfo.GetCultureInfo("zh-Hans")] = descriptionZh,
                [CultureInfo.GetCultureInfo("en")] = descriptionEn
            },
        };
        if (resources is { Count: > 0 })
        {
            descriptor.Resources.AddIfNotContains(resources);
        }

        await CreateScopesAsync(descriptor);
    }
}
