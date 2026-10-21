using System;

namespace LINGYUN.Abp.AuditLogging.OpenObserve;

/// <summary>
/// OpenObserve data stream name normalizer
/// </summary>
/// <remarks>
/// Refer to the <c>IIndexNameNormalizer</c> implemented by Elasticsearch: different tenants use different data streams,
/// Avoid mixing multi-tenant data
/// </remarks>
public interface IOpenObserveStreamNameNormalizer
{
    /// <summary>
    /// Normalized data flow name (append tenant suffix)
    /// </summary>
    /// <param name="stream">Basic data stream name</param>
    /// <param name="tenantId">Tenant ID, if empty, use the current tenant</param>
    string NormalizeStream(string stream, Guid? tenantId = null);
}
