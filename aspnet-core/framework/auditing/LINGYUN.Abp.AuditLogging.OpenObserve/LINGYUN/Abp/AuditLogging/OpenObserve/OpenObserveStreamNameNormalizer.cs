using System;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace LINGYUN.Abp.AuditLogging.OpenObserve;

/// <summary>
/// OpenObserve data stream name normalizer
/// </summary>
/// <remarks>
/// The tenant data manifold is like <c>audit_log_{tenantId:N}</c>, and the host (no tenant specified) uses the base name <c>audit_log</c>.
/// Explicitly pass in the tenant of the log itself when writing (audit log writing occurs in the background queue, the current tenant may not be the tenant that generates the log),
/// Use the current tenant when querying
/// </remarks>
public class OpenObserveStreamNameNormalizer : IOpenObserveStreamNameNormalizer, ISingletonDependency
{
    /// <summary>
    /// Tenant separator
    /// </summary>
    /// <remarks>
    /// OpenObserve will normalize <c>-</c> in the data stream name to <c>_</c> (actual measurement),
    /// Therefore, <c>_</c> is used directly here to avoid writing inconsistent names with queries.
    /// </remarks>
    public const char TenantSeparator = '_';

    protected ICurrentTenant CurrentTenant { get; }

    public OpenObserveStreamNameNormalizer(ICurrentTenant currentTenant)
    {
        CurrentTenant = currentTenant;
    }

    public virtual string NormalizeStream(string stream, Guid? tenantId = null)
    {
        if (stream.IsNullOrWhiteSpace())
        {
            return stream;
        }

        var tenant = tenantId ?? CurrentTenant.Id;

        return tenant.HasValue
            ? $"{stream}{TenantSeparator}{tenant.Value:N}"
            : stream;
    }
}
