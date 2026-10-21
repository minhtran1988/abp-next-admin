using Shouldly;
using System;
using System.Threading.Tasks;
using Volo.Abp.Auditing;
using Volo.Abp.MultiTenancy;
using Xunit;

namespace LINGYUN.Abp.AuditLogging.OpenObserve;

/// <summary>
/// Multi-tenant isolation test
/// </summary>
/// <remarks>
/// Refer to Elasticsearch implementation: different tenants use different data streams, writing is based on the log of the own tenant, and querying is based on the current tenant.
/// </remarks>
public class OpenObserveMultiTenancy_Tests : AbpAuditLoggingOpenObserveTestBase
{
    private readonly IAuditLogWriter _writer;
    private readonly IAuditLogManager _manager;
    private readonly ICurrentTenant _currentTenant;

    public OpenObserveMultiTenancy_Tests()
    {
        _writer = GetRequiredService<IAuditLogWriter>();
        _manager = GetRequiredService<IAuditLogManager>();
        _currentTenant = GetRequiredService<ICurrentTenant>();
    }

    [Fact]
    public async Task Should_Isolate_Audit_Logs_By_Tenant()
    {
        var tenantId = Guid.NewGuid();

        var auditLogInfo = new AuditLogInfo
        {
            TenantId = tenantId,
            ExecutionTime = DateTime.Now,
            UserName = "_tenant_user",
        };

        var id = Guid.Parse(await _writer.WriteAsync(auditLogInfo));

        await Task.Delay(10000);

        // The host (no tenant specified) cannot see the tenant's data flow
        (await _manager.GetAsync(id)).ShouldBeNull();
        (await _manager.GetCountAsync()).ShouldBe(0);

        // Visible after switching to tenant
        using (_currentTenant.Change(tenantId))
        {
            (await _manager.GetAsync(id)).ShouldNotBeNull();
            (await _manager.GetCountAsync()).ShouldBe(1);
        }
    }
}
