using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp.IO;
using Volo.Abp.Modularity.PlugIns;

namespace LINGYUN.Abp.MicroService.IdentityService;

public class Program
{
    public async static Task<int> Main(string[] args)
    {
        try
        {
            Console.Title = "IdentityService";
            Log.Information("Starting IdentityService.");

            var builder = WebApplication.CreateBuilder(args);
            builder.Host.AddAppSettingsSecretsJson()
                .UseAutofac()
                .ConfigureAppConfiguration((context, config) =>
                {
                    if (context.Configuration.GetValue("AgileConfig:IsEnabled", false))
                    {
                        config.AddAgileConfig(new AgileConfig.Client.ConfigClient(context.Configuration));
                    }
                })
                .UseSerilog((context, provider, config) =>
                {
                    config.ReadFrom.Configuration(context.Configuration);
                });
            await builder.AddApplicationAsync<IdentityServiceModule>(options =>
            {
                IdentityServiceModule.ApplicationName = Environment.GetEnvironmentVariable("APPLICATION_NAME")
                    ?? IdentityServiceModule.ApplicationName;
                options.ApplicationName = IdentityServiceModule.ApplicationName;
                // Get user secret configuration from environment variables, suitable for container testing
                options.Configuration.UserSecretsId = Environment.GetEnvironmentVariable("APPLICATION_USER_SECRETS_ID");
                // If the container does not specify a user secret, read from the project
                options.Configuration.UserSecretsAssembly = typeof(IdentityServiceModule).Assembly;
                // Search all files in the Modules directory as plug-ins
                // Cancel the display of modules that reference all other projects and instead reference them through plug-ins.
                var pluginFolder = Path.Combine(
                        Directory.GetCurrentDirectory(), "Modules");
                DirectoryHelper.CreateIfNotExists(pluginFolder);
                options.PlugInSources.AddFolder(
                    pluginFolder,
                    SearchOption.AllDirectories);
            });
            var app = builder.Build();
            await app.InitializeApplicationAsync();
            await app.RunAsync();
            return 0;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly!");
            Console.WriteLine("Host terminated unexpectedly!");
            Console.WriteLine(ex.ToString());
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}