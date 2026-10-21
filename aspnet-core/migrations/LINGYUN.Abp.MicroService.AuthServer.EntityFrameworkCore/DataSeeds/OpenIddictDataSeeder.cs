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
            "identity authentication",
            "OpenId Connect",
            "OpenId Connect protocol for authentication",
            "Allow applications to use your identity to sign in",
            "Allow the application to authenticate you using OpenID Connect");

        // Profile
        await CreateServiceScopeAsync(
            OpenIddictConstants.Scopes.Profile,
            "Personal Information",
            "Personal Information",
            "Profile",
            "Access your basic profile information",
            "Allow the application to access basic information such as your name, avatar, etc.",
            "Allow the application to access your basic profile information like name and picture");

        // Email
        await CreateServiceScopeAsync(
            OpenIddictConstants.Scopes.Email,
            "email",
            "email",
            "Email",
            "Access your email address",
            "Allow an application to access your email address",
            "Allow the application to access your email address");

        // Phone
        await CreateServiceScopeAsync(
            OpenIddictConstants.Scopes.Phone,
            "phone number",
            "phone number",
            "Phone",
            "Access your phone number",
            "Allow an application to access your phone number",
            "Allow the application to access your phone number");

        // Address
        await CreateServiceScopeAsync(
            OpenIddictConstants.Scopes.Address,
            "address information",
            "address information",
            "Address",
            "Access your address information",
            "Allow an application to access your address information",
            "Allow the application to access your address information");

        // Roles
        await CreateServiceScopeAsync(
            OpenIddictConstants.Scopes.Roles,
            "role information",
            "role information",
            "Roles",
            "Access your role information",
            "Allow applications to access your role and permission information",
            "Allow the application to access your roles and permissions");

        // OfflineAccess
        await CreateServiceScopeAsync(
            OpenIddictConstants.Scopes.OfflineAccess,
            "offline access",
            "offline access",
            "Offline access",
            "Accessing your information when you are not logged in",
            "Allow apps to access your information when you're not logged in",
            "Allow the application to access your information while you are offline");
    }

    private async Task CreateApiScopesAsync(string scope)
    {
        // Front-end summary authorization scope
        await CreateServiceScopeAsync(
            scope,
            "Microservice access authorization",
            "Microservice access authorization",
            "MicroService applications access",
            "Interface authorization suitable for microservice systems",
            "Allow applications to use the interfaces of each microservice module",
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
            "application interface gateway",
            "Api Gateway",
            "Applies to API Gateway Swagger authorization",
            "Applies to API Gateway Swagger authorization",
            "Applicable to application Programming interface gateway Swagger authorization",
            ["api-gateway"]);
        // Admin Service
        await CreateServiceScopeAsync(
            "admin-service",
            "Admin Service",
            "Backend management service",
            "Admin Service",
            "Applicable to backend management service Swagger authorization",
            "Applicable to backend management service Swagger authorization",
            "Applicable to the back-end management service Swagger authorization",
            ["admin-service"]);
        // Auth Server
        await CreateServiceScopeAsync(
            "auth-server",
            "Auth Server",
            "Identity Authentication Server",
            "Auth Server",
            "Applicable to identity authentication server Swagger authorization",
            "Applicable to identity authentication server Swagger authorization",
            "Applicable to the auth server Swagger authorization",
            ["auth-server"]);
        // Identity Service
        await CreateServiceScopeAsync(
            "identity-service",
            "Identity Service",
            "Identity Authentication Service",
            "Identity Service",
            "Applicable to identity authentication service Swagger authorization",
            "Applicable to identity authentication service Swagger authorization",
            "Applicable to the identity service Swagger authorization",
            ["identity-service"]);
        // Localization Service
        await CreateServiceScopeAsync(
            "localization-service",
            "Localization Service",
            "Localization Management Services",
            "Localization Service",
            "Applicable to localized management service Swagger authorization",
            "Applicable to localized management service Swagger authorization",
            "Applicable to the Localization service Swagger authorization",
            ["localization-service"]);
        // Message Service
        await CreateServiceScopeAsync(
            "message-service",
            "Message Service",
            "Message Management Service",
            "Message Service",
            "Applicable to message management service Swagger authorization",
            "Applicable to message management service Swagger authorization",
            "Applicable to the Message service Swagger authorization",
            ["message-service"]);
        // Platform Service
        await CreateServiceScopeAsync(
            "platform-service",
            "Platform Service",
            "Platform Management Services",
            "Platform Service",
            "Applicable to platform management service Swagger authorization",
            "Applicable to platform management service Swagger authorization",
            "Applicable to the Platform service Swagger authorization",
            ["platform-service"]);
        // Task Service
        await CreateServiceScopeAsync(
            "task-service",
            "Task Service",
            "Task Management Service",
            "Task Service",
            "Applicable to task management service Swagger authorization",
            "Applicable to task management service Swagger authorization",
            "Applicable to the Task service Swagger authorization",
            ["task-service"]);
        // Webhook Service
        await CreateServiceScopeAsync(
            "webhook-service",
            "Webhook Service",
            "Webhook Management Service",
            "Webhook Service",
            "Applicable to Webhook management service Swagger authorization",
            "Applicable to Webhook management service Swagger authorization",
            "Applicable to the Webhook service Swagger authorization",
            ["webhook-service"]);
        // Wechat Service
        await CreateServiceScopeAsync(
            "wechat-service",
            "Wechat Service",
            "WeChat Management Service",
            "Wechat Service",
            "Applicable to WeChat management service Swagger authorization",
            "Applicable to WeChat management service Swagger authorization",
            "Applicable to the Wechat service Swagger authorization",
            ["wechat-service"]);
        // Workflow Service
        await CreateServiceScopeAsync(
            "workflow-service",
            "Workflow Service",
            "Workflow Management Services",
            "Workflow Service",
            "Applicable to workflow management service Swagger authorization",
            "Applicable to workflow management service Swagger authorization",
            "Applicable to the Workflow service Swagger authorization",
            ["workflow-service"]);
        // AI Service
        await CreateServiceScopeAsync(
            "ai-service",
            "Artificial Intelligence Service",
            "AI Management Services",
            "Artificial Intelligence Service",
            "Applicable to AI management service Swagger authorization",
            "Applicable to AI management service Swagger authorization",
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
