using LINGYUN.Abp.OpenObserve;
using LINGYUN.Abp.OpenObserve.Models.Stream;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Modularity;
using Volo.Abp.Threading;

namespace LINGYUN.Abp.AuditLogging.OpenObserve;

[DependsOn(
    typeof(AbpAuditLoggingTestModule),
    typeof(AbpAuditLoggingOpenObserveModule))]
public class AbpAuditLoggingOpenObserveTestModule : AbpModule
{
    private const string UserSecretsId = "376A428E-4806-44BA-BF39-FA6F0E085982";

    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.ReplaceConfiguration(ConfigurationHelper.BuildConfiguration(builderAction: builder =>
        {
            builder.AddUserSecrets(UserSecretsId);
        }));
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        // Integration tests use independent data streams per host instance to avoid historical data affecting count assertions
        // (AbpIntegratedTest Each test method will create a new host instance, and only the timestamp will share the data stream due to collisions with the same second)
        var suffix = $"{DateTime.Now:yyyyMMddHHmmss}{Guid.NewGuid():N}"[..22];

        Configure<AbpAuditLoggingOpenObserveOptions>(options =>
        {
            options.AuditLogStream = $"audit_log_test_{suffix}";
            options.SecurityLogStream = $"security_log_test_{suffix}";
            // Entity change/action data stream calculated by AuditLogStream: {AuditLogStream}_entity_changes / _actions
        });

        // Collect logs for assertions (such as "data flow does not exist" warning)
        context.Services.AddLogging(builder => builder.AddProvider(new TestLoggerProvider()));
    }

    public override void OnApplicationShutdown(ApplicationShutdownContext context)
    {
        AsyncHelper.RunSync(async () => await OnApplicationShutdownAsync(context));
    }

    public async override Task OnApplicationShutdownAsync(ApplicationShutdownContext context)
    {
        await RemoveTestStreamsAsync(context.ServiceProvider);
    }

    private async static Task RemoveTestStreamsAsync(IServiceProvider serviceProvider)
    {
        var options = serviceProvider.GetRequiredService<IOptions<AbpAuditLoggingOpenObserveOptions>>().Value;
        var client = serviceProvider.GetRequiredService<OpenObserveClient>();

        var response = await client.GetStreamsAsync(
            options.Organization,
            fetchSchema: false,
            type: StreamType.Logs);

        // Data flows are isolated by tenant. Tenant data flows are prefixed with the base name (such as audit_log_test_xxx_<tenantId>), so they are cleaned based on prefix matching.
        var prefixes = new List<string>
        {
            options.AuditLogStream,
            options.SecurityLogStream,
        };
        if (options.WriteActions)
        {
            prefixes.Add(options.GetActionStream());
        }
        if (options.WriteEntityChanges)
        {
            prefixes.Add(options.GetEntityChangeStream());
        }

        var targets = response.List
            .Select(stream => stream.Name)
            .Where(name => prefixes.Any(prefix => name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)))
            .ToList();

        foreach (var target in targets)
        {
            await client.DeleteStreamAsync(
                target,
                StreamType.Logs,
                true,
                options.Organization);
        }
    }
}
