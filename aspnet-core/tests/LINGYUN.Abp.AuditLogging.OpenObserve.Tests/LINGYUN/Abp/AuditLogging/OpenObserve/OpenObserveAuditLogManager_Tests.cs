using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;

namespace LINGYUN.Abp.AuditLogging.OpenObserve;

public class OpenObserveAuditLogManager_Tests : AuditLogManager_Tests<AbpAuditLoggingOpenObserveTestModule>
{
    private readonly IAuditLogManager _manager;

    public OpenObserveAuditLogManager_Tests()
    {
        _manager = GetRequiredService<IAuditLogManager>();
    }

    /// <summary>
    /// OpenObserve does not support deleting records
    /// </summary>
    /// <remarks>
    /// The data will be cleaned at the end of the shared use case, and the thrown business exception and error code will be overwritten and asserted here.
    /// </remarks>
    protected override async Task DeleteAsync(Guid id)
    {
        var exception = await Should.ThrowAsync<BusinessException>(() => _manager.DeleteAsync(id));

        exception.Code.ShouldBe(AuditLoggingOpenObserveErrorCodes.DeleteNotSupported);
    }

    protected override async Task DeleteManyAsync(List<Guid> ids)
    {
        var exception = await Should.ThrowAsync<BusinessException>(() => _manager.DeleteManyAsync(ids));

        exception.Code.ShouldBe(AuditLoggingOpenObserveErrorCodes.DeleteNotSupported);
    }
}
