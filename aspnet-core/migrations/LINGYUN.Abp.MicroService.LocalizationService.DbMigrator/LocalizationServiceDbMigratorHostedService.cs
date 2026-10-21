using LINGYUN.Abp.MicroService.LocalizationService.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Volo.Abp;
using Volo.Abp.Data;

namespace LINGYUN.Abp.MicroService.LocalizationService.DbMigrator;

public class LocalizationServiceDbMigratorHostedService : IHostedService
{
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly IConfiguration _configuration;

    public LocalizationServiceDbMigratorHostedService(
        IHostApplicationLifetime hostApplicationLifetime, 
        IConfiguration configuration)
    {
        _hostApplicationLifetime = hostApplicationLifetime;
        _configuration = configuration;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var application = await AbpApplicationFactory
            .CreateAsync<LocalizationServiceDbMigratorModule>(options =>
        {
            // Get user secret configuration from environment variables, suitable for container testing
            options.Configuration.UserSecretsId = Environment.GetEnvironmentVariable("APPLICATION_USER_SECRETS_ID");
            // If the container does not specify a user secret, read from the project
            options.Configuration.UserSecretsAssembly = typeof(LocalizationServiceDbMigratorHostedService).Assembly;
            options.Services.ReplaceConfiguration(_configuration);
            options.UseAutofac();
            options.Services.AddLogging(c => c.AddSerilog());
            options.AddDataMigrationEnvironment();
        });
        await application.InitializeAsync();

        await application
            .ServiceProvider
            .GetRequiredService<LocalizationServiceDbMigrationService>()
            .CheckAndApplyDatabaseMigrationsAsync();

        await application.ShutdownAsync();

        _hostApplicationLifetime.StopApplication();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

