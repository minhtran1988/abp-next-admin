using Hangfire;
using Hangfire.MemoryStorage;
using LINGYUN.Abp.Identity;
using LINGYUN.Abp.Identity.Session;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using PackageName.CompanyName.ProjectName.AIO.EntityFrameworkCore.DataSeeder;
using PackageName.CompanyName.ProjectName.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.Security.Claims;

namespace PackageName.CompanyName.ProjectName;

[DependsOn(
    typeof(ProjectNameDomainTestModule),
    typeof(ProjectNameApplicationModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementDomainIdentityModule),
    typeof(ProjectNameEntityFrameworkCoreTestModule)
    )]
public class ProjectNameApplicationTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        // //Set ILogger to NullLogger
        context.Services.AddLogging(builder => builder.AddProvider(NullLoggerProvider.Instance));
        context.Services.AddTransient<IProjectNameDataSeeder, ProjectNameDataSeeder>();
        context.Services.AddLogging(builder => builder.AddProvider(NullLoggerProvider.Instance));
        context.Services.AddTransient<IHostEnvironment, TestHostEnvironment>();
        context.Services.AddTransient<IFileProvider, TestFileProvider>();
        
        // Add configuration file definition, required when creating a new tenant
        Configure<IdentityOptions>(options =>
        {
            // Allow Chinese usernames
            options.User.AllowedUserNameCharacters = null;
            // Support weak passwords
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 1;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            // Add default two-factor token provider configuration
            options.Tokens.ProviderMap.Add("Default", new TokenProviderDescriptor(typeof(EmailTokenProvider<IdentityUser>)));
        });
        Configure<AbpClaimsPrincipalFactoryOptions>(options =>
        {
            options.IsDynamicClaimsEnabled = true;
        });
        Configure<IdentitySessionCleanupOptions>(options =>
        {
            options.IsCleanupEnabled = true;
        });
        // Configure Hangfire
        context.Services.AddHangfire(config =>
        {
            config.UseMemoryStorage();
        });
    }
}
