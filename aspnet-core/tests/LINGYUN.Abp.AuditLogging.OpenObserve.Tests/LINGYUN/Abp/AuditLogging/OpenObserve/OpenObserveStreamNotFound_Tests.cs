using Microsoft.Extensions.Logging;
using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.MultiTenancy;
using Xunit;

namespace LINGYUN.Abp.AuditLogging.OpenObserve;

/// <summary>
/// Processing when the data stream does not exist
/// </summary>
/// <remarks>
/// When OpenObserve has not written data, the data stream has not yet been created. The query will be processed as an empty result and a warning log will be recorded.
/// </remarks>
public class OpenObserveStreamNotFound_Tests : AbpAuditLoggingOpenObserveTestBase
{
    private readonly IAuditLogManager _manager;
    private readonly ICurrentTenant _currentTenant;

    public OpenObserveStreamNotFound_Tests()
    {
        _manager = GetRequiredService<IAuditLogManager>();
        _currentTenant = GetRequiredService<ICurrentTenant>();
    }

    [Fact]
    public async Task Should_Log_Warning_When_Stream_Not_Found()
    {
        TestLoggerProvider.Clear();

        var tenantId = Guid.NewGuid();

        // This tenant has never written data, and its data stream has not been created yet
        using (_currentTenant.Change(tenantId))
        {
            (await _manager.GetCountAsync()).ShouldBe(0);
        }

        var warning = TestLoggerProvider.Entries
            .FirstOrDefault(entry => entry.Level == LogLevel.Warning &&
                                     entry.Message.Contains("does not exist yet", StringComparison.OrdinalIgnoreCase));

        warning.ShouldNotBeNull();
        // The log category is the manager type (determined by the ILogger<T> injected by the subclass)
        warning!.Category.ShouldContain("OpenObserveAuditLogManager");
    }
}
