using LINGYUN.Abp.MicroService.AIService;
using LINGYUN.Abp.Serilog.Enrichers.Application;
using Serilog;
using Volo.Abp.IO;
using Volo.Abp.Modularity.PlugIns;

Log.Information("Starting AIService Host...");

try
{
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

    builder.AddServiceDefaults();

    await builder.AddApplicationAsync<AIServiceModule>(options =>
    {
        var applicationName = Environment.GetEnvironmentVariable("APPLICATION_NAME") ?? "AIService";
        options.ApplicationName = applicationName;
        AbpSerilogEnrichersConsts.ApplicationName = applicationName;

        var pluginFolder = Path.Combine(Directory.GetCurrentDirectory(), "Modules");
        DirectoryHelper.CreateIfNotExists(pluginFolder);
        options.PlugInSources.AddFolder(pluginFolder, SearchOption.AllDirectories);
    });

    var app = builder.Build();

    await app.InitializeApplicationAsync();

    app.MapDefaultEndpoints();

    app.UseForwardedHeaders();
    // localization
    app.UseMapRequestLocalization();
    // http call chain
    app.UseCorrelationId();
    // file system
    app.MapAbpStaticAssets();
    // routing
    app.UseRouting();
    // cross domain
    app.UseCors();
    // Authentication
    app.UseAuthentication();
    app.UseJwtTokenMiddleware();
    // Multi-tenant
    app.UseMultiTenancy();
    // jwt
    app.UseDynamicClaims();
    // Authorize
    app.UseAuthorization();
    // Swagger
    app.UseSwagger();
    // Swagger visual interface
    app.UseAbpSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Support AI Service API");

        var configuration = app.Configuration;
        options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
        options.OAuthScopes(configuration["AuthServer:Audience"]);
    });
    // Audit log
    app.UseAuditing();
    app.UseAbpSerilogEnrichers();
    // routing
    app.UseConfiguredEndpoints();

    await app.RunAsync();
}
catch (Exception ex)
{
    if (ex is HostAbortedException)
    {
        throw;
    }

    Log.Fatal(ex, "Host terminated unexpectedly!");
}
finally
{
    await Log.CloseAndFlushAsync();
}
